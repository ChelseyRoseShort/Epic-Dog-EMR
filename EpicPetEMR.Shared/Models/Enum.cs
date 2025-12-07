namespace EpicPetEMR.Shared.Models;

public enum Species { Dog = 0, Cat = 1, Other = 2,
    Unknown = 3
}
public enum Sex { Unknown = 0, Male = 1, Female = 2, NeuteredMale = 3, SpayedFemale = 4 }
public enum Unit { Kg = 0, Lb = 1 }
public enum AppointmentStatus { Scheduled = 0, Completed = 1, Canceled = 2, NoShow = 3 }
public enum Dosages { Mg = 0, Ml = 1, Gm = 2, Mcg = 3, Units = 4, Puffs = 5, Drops = 6, Tablets = 7, Capsules = 8 }

public enum Frequency { OnceAMonth, OnceInMorning, OnceInEvening, TwiceADay, ThreeTimesADay, FourTimesADay, AsNeededEveryTwoHours, AsNeededEveryFourHours, AsNeededEverySixHours,AsNeededEveryTwelveHours }

public enum Routes { ByMouth, IV, EyeDrops, EarDrops, Puffs, IntramuscularShot, SubcutaneousShot, Topical,  }

public enum MedicationAction { Given = 0, Held = 1 }
