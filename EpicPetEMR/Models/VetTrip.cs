using EpicPetEMR.Shared.Models;
using Microsoft.CodeAnalysis;

namespace EpicPetEMR.Api.Models;

public class VetTrip
{
    public int Id { get; set; }

  
    public int PetId { get; set; }
    public Pet Pet { get; set; } = null!;

    public string Hospital { get; set; } = string.Empty;
    public string Vet { get; set; } = string.Empty;

   
    public DateTime VisitDateTime { get; set; }

    public VetTripReason Reason { get; set; }


    public ICollection<PetDocument> Documents { get; set; } = new List<PetDocument>();
}
