using EpicPetEMR_Ui.ViewModels;
using EpicPetEMR.Shared.Extensions;
using EpicPetEMR.Shared.Models;

namespace EpicPetEMR_Ui.Services;

public sealed class MedicationScheduleService
{
    public record MedScheduleItem(int MedId, string Name)
    {
        public bool IsCompleted { get; init; }
    }

    public Dictionary<int, Dictionary<int, List<MedScheduleItem>>> BuildBrainSchedule(
        List<PetWithMedsVm> pets,
        int startHour,
        int endHour,
        List<MARHistoryDto> marHistory)
    {
        // (PetId, MedId, Hour) → completed
        var completed = new HashSet<(int PetId, int MedId, int Hour)>(
            marHistory
                // decide which actions count as “completed”
                .Where(h => h.Action == MedicationAction.Given
                         || h.Action == MedicationAction.Held)
                .Select(h => (h.PetId, h.MedId, h.Hour))
        );

        var tasks = new Dictionary<int, Dictionary<int, List<MedScheduleItem>>>();

        foreach (var p in pets)
        {
            var petId = p.Pet.Id;

            if (p.Medications.Count == 0)
            {
                continue;
            }

            foreach (var med in p.Medications)
            {
                if (!med.IsActive)
                {
                    continue;
                }

                if (med.Frequency.IsPrn())
                {
                    continue;
                }

                if (med.StartDate is null || med.StartTime is null)
                {
                    continue;
                }

                int interval = med.Frequency.GetIntervalHours();

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
                    {
                        continue;
                    }

                    if (!tasks.TryGetValue(petId, out var perHour))
                    {
                        perHour = new Dictionary<int, List<MedScheduleItem>>();
                        tasks[petId] = perHour;
                    }

                    if (!perHour.TryGetValue(hour, out var list))
                    {
                        list = new List<MedScheduleItem>();
                        perHour[hour] = list;
                    }

                    bool isCompleted = completed.Contains((petId, med.Id, hour));

                    list.Add(new MedScheduleItem(med.Id, med.Name)
                    {
                        IsCompleted = isCompleted
                    });
                }
            }
        }

        return tasks;
    }

    public List<DateTime> GenerateDoseTimes(
     DateOnly startDate,
     TimeOnly startTime,
     int intervalHours,
     int startHour,
     int endHour)
    {
        var results = new List<DateTime>();

        var firstDose = startDate.ToDateTime(startTime);
        var today = DateTime.Today;

        var windowStart = today.AddHours(startHour);
        var windowEnd = startHour <= endHour
            ? today.AddHours(endHour)
            : today.AddDays(1).AddHours(endHour);

        var current = firstDose;
        while (current < windowStart.AddHours(-intervalHours))
        {
            current = current.AddHours(intervalHours);
        }

        var lastPossible = windowEnd.AddHours(intervalHours);

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


    private bool IsHourInRange(int hour, int start, int end)
    {
        return start <= end
            ? hour >= start && hour <= end
            : hour >= start || hour <= end;
    }

    public bool IsPrnDoseTooEarly(DateTime lastGiven, Frequency freq)
    {
        var nextAllowed = lastGiven.AddHours(freq.GetIntervalHours());
        return DateTime.Now < nextAllowed;
    }
}
