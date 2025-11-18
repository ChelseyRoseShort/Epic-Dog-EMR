namespace EpicPetEMR.Shared.Models;

public sealed class MedicationDto
{
    public int Id { get; set; }
    public int PetId { get; set; }

    public string Name { get; set; } = default!;

    public decimal? DoseValue { get; set; }
    public Dosages DoseType { get; set; }      

    public string Route { get; set; } = "Unknown";
    public string Frequency { get; set; } = "Unknown";

    public string? Instructions { get; set; }

    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }

    public bool IsActive { get; set; }
}
