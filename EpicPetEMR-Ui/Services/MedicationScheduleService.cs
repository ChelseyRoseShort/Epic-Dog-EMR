using EpicPetEMR_Ui.ViewModels;
using EpicPetEMR.Shared.Models;

namespace EpicPetEMR_Ui.Services;

public sealed class MedicationScheduleService
{
    // --- BRAIN SCHEDULE BUILDER ---
    public record MedScheduleItem(int MedId, string Name);

    public Dictionary<int, Dictionary<int, List<MedScheduleItem>>> BuildBrainSchedule(
        List<PetWithMedsVm> pets,
        int startHour,
        int endHour)
    {
        var tasks = new Dictionary<int, Dictionary<int, List<MedScheduleItem>>>();

        foreach (var p in pets)
        {
            var petId = p.Pet.Id;

            foreach (var med in p.Medications)
            {
                if (!med.IsActive) continue;
                if (IsPrn(med.Frequency)) continue;
                if (med.StartDate is null || med.StartTime is null) continue;

                int interval = GetIntervalHours(med.Frequency);

                var doses = GenerateDoseTimes(
                    med.StartDate.Value,
                    med.StartTime.Value,
                    interval,
                    startHour,
                    endHour
                );

                foreach (var dose in doses)
                {
                    int hour = dose.Hour;

                    if (!IsHourInRange(hour, startHour, endHour))
                        continue;

                    // get or create per-hour dictionary
                    if (!tasks.TryGetValue(petId, out var perHour))
                    {
                        perHour = new Dictionary<int, List<MedScheduleItem>>();
                        tasks[petId] = perHour;
                    }

                    // get or create list for this hour
                    if (!perHour.TryGetValue(hour, out var list))
                    {
                        list = new List<MedScheduleItem>();
                        perHour[hour] = list;
                    }

                    // Add both ID + Name here
                    list.Add(new MedScheduleItem(med.Id, med.Name));
                }
            }
        }

        return tasks;
    }




    // --- FIXED DOSE GENERATION (handles overnight windows) ---
    public List<DateTime> GenerateDoseTimes(
        DateOnly startDate,
        TimeOnly startTime,
        int intervalHours,
        int startHour,
        int endHour)
    {
        var results = new List<DateTime>();

        // First actual scheduled dose
        var firstDose = startDate.ToDateTime(startTime);

        // Brain window spans potentially two dates:
        // e.g. 20 → 6 crosses midnight
        var today = DateTime.Today;

        var windowStart = today.AddHours(startHour);
        var windowEnd =
            startHour <= endHour
            ? today.AddHours(endHour)                 // normal window: e.g. 7 → 19
            : today.AddDays(1).AddHours(endHour);     // overnight: e.g. 20 → 6 next day


        // Start generating from firstDose forward up to 24–48 hours
        var current = firstDose;
        var lastPossible = firstDose.AddDays(2);  // safety window

        while (current <= lastPossible)
        {
            if (current >= windowStart && current <= windowEnd)
            {
                results.Add(current);
            }

            current = current.AddHours(intervalHours);
        }

        return results;
    }



    // --- Hour wrap-around support ---
    private bool IsHourInRange(int hour, int start, int end)
    {
        if (start <= end)
            return hour >= start && hour <= end;

        // Overnight window (20 → 6)
        return hour >= start || hour <= end;
    }



    public int GetIntervalHours(Frequency freq)
    {
        return freq switch
        {
            Frequency.TwiceADay => 12,
            Frequency.ThreeTimesADay => 8,
            Frequency.FourTimesADay => 6,

            Frequency.OnceInMorning => 24,
            Frequency.OnceInEvening => 24,

            Frequency.AsNeededEveryTwoHours => 2,
            Frequency.AsNeededEveryFourHours => 4,
            Frequency.AsNeededEverySixHours => 6,
            Frequency.AsNeededEveryTwelveHours => 12,

            _ => 24
        };
    }

    public bool IsPrn(Frequency freq)
    {
        return freq.ToString().StartsWith("AsNeeded");
    }

    public bool IsPrnDoseTooEarly(DateTime lastGiven, Frequency freq)
    {
        int interval = GetIntervalHours(freq);
        var nextAllowed = lastGiven.AddHours(interval);

        return DateTime.Now < nextAllowed;
    }
}
