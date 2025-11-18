using System.ComponentModel.DataAnnotations;
using EpicPetEMR.Shared.Models;
namespace EpicPetEMR.Api.Models;

public class Medication
{
    public int Id { get; set; }

    public int PetId { get; set; }
    public Pet Pet { get; set; } = default!;

    [Required, MaxLength(160)]
    public string Name { get; set; } = default!;     // e.g., Vetmedin

    [MaxLength(160)]
    public string? Dosage { get; set; }              // e.g., "5 mg PO BID"

    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }           // null if ongoing

    public bool IsActive => !EndDate.HasValue || EndDate >= DateOnly.FromDateTime(DateTime.UtcNow);
}
