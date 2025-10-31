using EpicPetEMR.Api.Models;         // EF entities (adjust to your actual namespace)
using EpicPetEMR.Shared.Models;  // DTOs from Shared

namespace EpicPetEMR.Mappers;

public static class PetMapper
{
    public static PetDto ToDto(this Pet p) => new()
    {
        Id = p.Id,
        Name = p.Name,
        Breed = p.Breed,
        Species = p.Species.ToString(),
        Sex = p.Sex.ToString(),
        Weight = p.Weight,
        WeightUnit = p.WeightUnit?.ToString(),
        DateOfBirth = p.DateOfBirth,  // direct assignment, no conversion needed
        Family = p.Family is null ? null : new FamilyDto
        {
            Id = p.Family.Id,
            Name = p.Family.Name
        }
    };
}
