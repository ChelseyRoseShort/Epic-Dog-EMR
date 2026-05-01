using System.ComponentModel.DataAnnotations;

namespace EpicPetEMR.Shared.Models;

public sealed class PetDto
{
    public int Id { get; set; }

    public string? ProfilePic { get; set; }
    public string? Name { get; set; }
    public string? Breed { get; set; }
    public string? Species { get; set; }
    public Sex Sex { get; set; }
    public decimal? Weight { get; set; }
    public string? WeightUnit { get; set; } 

    public DateOnly? DateOfBirth { get; set; }
    [Required]
    public int? FamilyId { get; set; }


    public FamilyDto? Family { get; set; }
}

public enum WeightUnit
{
    Unknown = 0,
    lbs,
    kg
}