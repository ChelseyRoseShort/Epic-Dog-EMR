using System.ComponentModel.DataAnnotations;

namespace EpicPetEMR.Api.Models;

public class VaccineRecord
{
    public int Id { get; set; }

    public int PetId { get; set; }
    public Pet Pet { get; set; } = default!;

    [Required, MaxLength(160)]
    public string Vaccine { get; set; } = default!;  // Rabies, DHPP, etc.

    public DateOnly DateAdministered { get; set; }
    public DateOnly? ExpiresOn { get; set; }
}
