

namespace EpicPetEMR.Shared.Models;

    public sealed class MARHistoryDto
    {
       
            public int Id { get; set; }
            public int PetId { get; set; }
            public int MedId { get; set; }
            public int Hour { get; set; }

            public MedicationAction Action { get; set; }   
            public string? Reason { get; set; }            
            public DateTime TimeRecorded { get; set; }
        }

    

