using System.ComponentModel.DataAnnotations;

namespace EpicPetEMR.Api.Models;

public class Allergy
{
    public int Id { get; set; }

    public int PetId { get; set; }
    public Pet Pet { get; set; } = default!;

    [Required, MaxLength(160)]
    public string Substance { get; set; } = default!; // e.g., Chicken, Bee stings

    [MaxLength(160)]
    public string? Reaction { get; set; }             // e.g., hives, GI upset

    public bool Severe { get; set; } = false;
}
