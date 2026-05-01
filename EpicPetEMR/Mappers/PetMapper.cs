using EpicPetEMR.Api.Models;
using EpicPetEMR.Shared.Models;

namespace EpicPetEMR.Mappers;

public static class PetMapper
{
    public static PetDto ToDto(this Pet p) => new()
    {
        Id = p.Id,
        Name = p.Name,
        Breed = p.Breed,
        Species = p.Species.ToString(),
        Sex = Enum.TryParse<Sex>(p.Sex.ToString(), true, out var sex) ? sex : Sex.Unknown,
        Weight = p.Weight,
        ProfilePic = p.ProfilePic,
        WeightUnit = p.WeightUnit?.ToString(),
        DateOfBirth = p.DateOfBirth,
        Family = p.Family is null ? null : new FamilyDto
        {
            Id = p.Family.Id,
            Name = p.Family.Name
        }
    };

    public static Pet ToEntity(this PetDto dto)
    {
        Enum.TryParse<Species>(dto.Species, true, out var species);
        var sex = dto.Sex;
        Enum.TryParse<WeightUnit>(dto.WeightUnit, true, out var weightUnit);

        return new Pet
        {
            Id = dto.Id,
            Name = dto.Name ?? "",
            Breed = dto.Breed,
            Species = species,
            Sex = sex,
            Weight = dto.Weight,
            WeightUnit = weightUnit,
            DateOfBirth = dto.DateOfBirth,
            FamilyId = dto.FamilyId ?? dto.Family?.Id,
            ProfilePic = dto.ProfilePic ?? ""
        };
    }
}
