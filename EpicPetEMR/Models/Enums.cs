namespace EpicPetEMR.Api.Models;

public enum Species { Dog = 0, Cat = 1, Other = 2,
    Unknown = 3
}
public enum Sex { Unknown = 0, Male = 1, Female = 2, NeuteredMale = 3, SpayedFemale = 4 }
public enum Unit { Kg = 0, Lb = 1 }
public enum AppointmentStatus { Scheduled = 0, Completed = 1, Canceled = 2, NoShow = 3 }
