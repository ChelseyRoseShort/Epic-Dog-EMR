using EpicPetEMR_Ui.ViewModels;
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
        Console.WriteLine("=== BUILDING BRAIN SCHEDULE ===");
        Console.WriteLine($"Shift Window: {startHour}:00 → {endHour}:00");
        Console.WriteLine($"Pets Count: {pets.Count}");
        Console.WriteLine("--------------------------------");

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
            Console.WriteLine($"\n--- PET {petId}: {p.Pet.Name} ---");

            if (p.Medications.Count == 0)
            {
                Console.WriteLine("No medications for this pet.");
                continue;
            }

            foreach (var med in p.Medications)
            {
                Console.WriteLine($"\nChecking Medication: {med.Name} (ID {med.Id})");

                if (!med.IsActive)
                {
                   
                    continue;
                }

                if (IsPrn(med.Frequency))
                {
             
                    continue;
                }

                if (med.StartDate is null || med.StartTime is null)
                {
              
                    continue;
                }

                Console.WriteLine($"Start Date: {med.StartDate}, Start Time: {med.StartTime}");

                int interval = GetIntervalHours(med.Frequency);
                Console.WriteLine($"Interval Hours: {interval}");

                var doses = GenerateDoseTimes(
                    med.StartDate.Value,
                    med.StartTime.Value,
                    interval,
                    startHour,
                    endHour
                );

                Console.WriteLine($"Generated {doses.Count} dose times:");
                foreach (var d in doses)
                    Console.WriteLine($"  → {d} (hour {d.Hour})");

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

                    Console.WriteLine(
                        $"Added PET {petId}, HOUR {hour}, MED {med.Name}, IsCompleted={isCompleted}"
                    );
                }
            }
        }


        foreach (var petEntry in tasks)
        {
            Console.WriteLine($"Pet {petEntry.Key}:");

            foreach (var hourEntry in petEntry.Value)
            {
                Console.WriteLine($"  Hour {hourEntry.Key}:");

                foreach (var item in hourEntry.Value)
                {
                    Console.WriteLine($"    - {item.Name} (ID {item.MedId})");
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
        Console.WriteLine($"Generating dose times...");
        Console.WriteLine($"Start: {startDate} {startTime}, Interval: {intervalHours} hours");

        var results = new List<DateTime>();

        var firstDose = startDate.ToDateTime(startTime);
        var today = DateTime.Today;

        var windowStart = today.AddHours(startHour);
        var windowEnd = startHour <= endHour
            ? today.AddHours(endHour)
            : today.AddDays(1).AddHours(endHour);

        Console.WriteLine($"Window: {windowStart} → {windowEnd}");

       
        var current = firstDose;
        while (current < windowStart.AddHours(-intervalHours))
        {
            current = current.AddHours(intervalHours);
        }

        Console.WriteLine($"Aligned Current Dose Start: {current}");

        var lastPossible = windowEnd.AddHours(intervalHours);

        while (current <= lastPossible)
        {
            Console.WriteLine($"  Checking {current}");

            if (current >= windowStart && current <= windowEnd)
            {
            
                results.Add(current);
            }
            else
            {
                Console.WriteLine($" Outside window");
            }

            current = current.AddHours(intervalHours);
        }

        return results;
    }


    private bool IsHourInRange(int hour, int start, int end)
    {
        bool result = start <= end
            ? hour >= start && hour <= end
            : hour >= start || hour <= end;

        Console.WriteLine($"IsHourInRange({hour}, {start}, {end}) => {result}");
        return result;
    }

    public int GetIntervalHours(Frequency freq)
    {
        Console.WriteLine($"GetIntervalHours({freq})");

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
        bool result = freq.ToString().StartsWith("AsNeeded");
        Console.WriteLine($"IsPrn({freq}) => {result}");
        return result;
    }

    public bool IsPrnDoseTooEarly(DateTime lastGiven, Frequency freq)
    {
        int interval = GetIntervalHours(freq);
        var nextAllowed = lastGiven.AddHours(interval);
        bool tooEarly = DateTime.Now < nextAllowed;

        Console.WriteLine($"IsPrnDoseTooEarly: last={lastGiven}, nextAllowed={nextAllowed}, tooEarly={tooEarly}");

        return tooEarly;
    }
}
