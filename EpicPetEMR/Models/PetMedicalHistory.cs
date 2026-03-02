using EpicPetEMR.Api.Models;

namespace EpicPetEMR.Models
{
    public class PetMedicalHistory
    {
        public int Id { get; set; }

        public int PetId { get; set; }
        public Pet Pet { get; set; } = null!;

        public string Diagnosis { get; set; } = "";   

        public DateTime? DiagnosedOn { get; set; }
        public DateTime? ResolvedOn { get; set; }

        public bool IsActive { get; set; } = true;

        public string? Notes { get; set; }
    }

}
