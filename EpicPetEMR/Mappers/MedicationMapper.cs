using EpicPetEMR.Api.Models;
using EpicPetEMR.Shared.Models;

namespace EpicPetEMR.Api.Mappers;

public static class MedicationMapper
{
    public static MedicationDto ToDto(this Medication med)
    {
        return new MedicationDto
        {
            Id = med.Id,
            PetId = med.PetId,
            Name = med.Name,
            DoseValue = med.DoseValue,
            DoseType = med.DoseType,
            Route = med.Route,
            Frequency = med.Frequency,
            Instructions = med.Instructions,
            StartDate = med.StartDate,
            StartTime = med.StartTime,      // ← FIXED
            EndDate = med.EndDate,
            IsActive = med.IsActive
        };
    }

    public static Medication ToEntity(this MedicationDto dto)
    {
        return new Medication
        {
            Id = dto.Id,
            PetId = dto.PetId,
            Name = dto.Name,
            DoseValue = dto.DoseValue,
            DoseType = dto.DoseType,
            Route = dto.Route,
            Frequency = dto.Frequency,
            Instructions = dto.Instructions,
            StartDate = dto.StartDate,
            StartTime = dto.StartTime,      // ← FIXED
            EndDate = dto.EndDate,
            IsActive = dto.IsActive
        };
    }

    public static void UpdateEntity(this Medication entity, MedicationDto dto)
    {
        entity.Name = dto.Name;
        entity.DoseValue = dto.DoseValue;
        entity.DoseType = dto.DoseType;
        entity.Route = dto.Route;
        entity.Frequency = dto.Frequency;
        entity.Instructions = dto.Instructions;
        entity.StartDate = dto.StartDate;
        entity.StartTime = dto.StartTime;  // ← FIXED
        entity.EndDate = dto.EndDate;
        entity.IsActive = dto.IsActive;
    }
}
