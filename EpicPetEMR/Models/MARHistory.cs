using EpicPetEMR.Shared.Models;

namespace EpicPetEMR.Api.Models;

    public sealed class MARHistory
    {
  
            public int Id { get; set; }
            public int PetId { get; set; }
            public int MedId { get; set; }
            public int Hour { get; set; }

            public MedicationAction Action { get; set; }   // NEW ✔ "Given" or "Held"
            public string? Reason { get; set; }            // optional "Held reason"

            public DateTime TimeRecorded { get; set; }
        }

    

