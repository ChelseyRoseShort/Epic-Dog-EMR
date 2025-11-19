using System.ComponentModel.DataAnnotations;
using EpicPetEMR.Shared.Models;

namespace EpicPetEMR.Api.Models;

public class Medication
{
    public int Id { get; set; }

    public int PetId { get; set; }
    public Pet Pet { get; set; } = default!;

    [Required, MaxLength(160)]
    public string Name { get; set; } = default!;

    public decimal? DoseValue { get; set; }

    [MaxLength(160)]
    public Dosages DoseType { get; set; }

    public string Route { get; set; } = "Unknown";
    public string Frequency { get; set; } = "Unknown";

    public string? Instructions { get; set; }

    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }

    // UI IsActive can be overridden or you can compute it here
    public bool IsActive { get; set; }
}
