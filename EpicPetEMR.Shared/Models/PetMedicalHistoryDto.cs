namespace EpicPetEMR.Shared.Models
{
    public class PetMedicalHistoryDto
    {
        public int Id { get; set; }

        public int PetId { get; set; }

        public string Diagnosis { get; set; } = "";

        public DateTime? DiagnosedOn { get; set; }
        public DateTime? ResolvedOn { get; set; }

        public bool IsActive { get; set; } = true;

        public string? Notes { get; set; }
    }
}
