using EpicPetEMR.Api.Models;
using EpicPetEMR.Shared.Models;

namespace EpicPetEMR.Api.Mappers;

public static class MARHistoryMapper
{
    public static MARHistoryDto ToDto(this MARHistory history)
    {
        return new MARHistoryDto
        {
            Id = history.Id,
            PetId = history.PetId,
            MedId = history.MedId,
            Hour = history.Hour,
            Action = history.Action,
            Reason = history.Reason,
            TimeRecorded = history.TimeRecorded
        };
    }

    public static MARHistory ToEntity(this MARHistoryDto dto)
    {
        return new MARHistory
        {
            Id = dto.Id,
            PetId = dto.PetId,
            MedId = dto.MedId,
            Hour = dto.Hour,
            Action = dto.Action,
            Reason = dto.Reason,
            TimeRecorded = dto.TimeRecorded
        };
    }

    public static void UpdateEntity(this MARHistory entity, MARHistoryDto dto)
    {
        entity.PetId = dto.PetId;
        entity.MedId = dto.MedId;
        entity.Hour = dto.Hour;
        entity.Action = dto.Action;
        entity.Reason = dto.Reason;
        entity.TimeRecorded = dto.TimeRecorded;
    }
}
