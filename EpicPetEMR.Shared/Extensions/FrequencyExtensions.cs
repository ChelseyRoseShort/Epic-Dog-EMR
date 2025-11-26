using EpicPetEMR.Shared.Models;

namespace EpicPetEMR.Shared.Extensions;

public static class FrequencyExtensions
{
    public static int GetIntervalHours(this Frequency freq)
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

            Frequency.OnceAMonth => 24 * 30,

            _ => 24,
        };
    }

    public static bool IsPrn(this Frequency freq)
    {
        return freq.ToString().StartsWith("AsNeeded");
    }
}
