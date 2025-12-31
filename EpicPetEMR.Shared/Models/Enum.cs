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

public enum VetTripReason { Wellness = 0, Sick = 1, Surgery = 2}

public enum DocumentType { VetTrip = 0, Assessment = 1}

public enum OhNoEventType
{
    None = 0,
    Seizure = 1,
    Bleeding = 2,
    GI = 3,
    Cardiac = 4,
    Respiratory = 5,
    Trauma = 6,
    AccidentalIngestion = 7,
    Urinary = 8,
    PainMobility = 9,
    Behavior = 10,
    Medication = 11,
    Other = 12
}

public enum BodyMapKey
{
    Right = 0,
    Left = 1,
    TopView = 2,
    BellyView = 3,
    Head = 4
}

public enum AvatarFinding
{
  
    SurgicalIncision = 0,
    OpenWound = 1,
    Lump = 2,
    Rash = 3,
    Bruise = 4,
    Abrasion = 5,
    Ulcer = 6,
    HotSpot = 7,
    TickBite = 8,
    Abscess = 9,
    Mass = 10,
    OtherSkinIssue = 11,

    
    Dexcom = 100,
    IV = 101,
    Drain = 102,
    AirwayAccess = 103,  
    ChestTube = 104,
    NGT = 105,
    Foley = 106,

    OtherDeviceOrTube = 199
}
