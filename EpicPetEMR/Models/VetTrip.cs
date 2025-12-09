using EpicPetEMR.Shared.Models;
using Microsoft.CodeAnalysis;

namespace EpicPetEMR.Api.Models;

public class VetTrip
{
    public int Id { get; set; }

    // FK to Pet
    public int PetId { get; set; }
    public Pet Pet { get; set; } = null!;

    public string Hospital { get; set; } = string.Empty;
    public string Vet { get; set; } = string.Empty;

    // When the visit happened
    public DateTime VisitDateTime { get; set; }

    public VetTripReason Reason { get; set; }

    // Optional: any docs tied to this visit
    public ICollection<PetDocument> Documents { get; set; } = new List<PetDocument>();
}
