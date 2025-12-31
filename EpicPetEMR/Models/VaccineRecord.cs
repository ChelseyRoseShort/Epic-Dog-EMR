using System.ComponentModel.DataAnnotations;
using EpicPetEMR.Shared.Models;
namespace EpicPetEMR.Api.Models;

public class VaccineRecord
{
    public int Id { get; set; }

    public int PetId { get; set; }
    public Pet Pet { get; set; } = default!;

    [Required, MaxLength(160)]
    public string Vaccine { get; set; } = default!;  

    public DateOnly DateAdministered { get; set; }
    public DateOnly? ExpiresOn { get; set; }
}
