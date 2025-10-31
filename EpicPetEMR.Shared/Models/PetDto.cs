namespace EpicPetEMR.Shared.Models;

public sealed class PetDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Breed { get; set; }
    public string? Species { get; set; }
    public string? Sex { get; set; }
    public decimal? Weight { get; set; }
    public string? WeightUnit { get; set; }

    // Use DateOnly, consistent with your entity
    public DateOnly? DateOfBirth { get; set; }

    public FamilyDto? Family { get; set; }
}
