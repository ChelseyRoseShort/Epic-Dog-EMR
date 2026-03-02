using EpicPetEMR.Api.Models;
using EpicPetEMR.Models;
using EpicPetEMR.Shared.Models;

namespace EpicPetEMR.Mappers
{
    public static class PetMedicalHistoryMapper
    {
        public static PetMedicalHistoryDto ToDto(this PetMedicalHistory entity)
        {
            return new PetMedicalHistoryDto
            {
                Id = entity.Id,
                PetId = entity.PetId,
                Diagnosis = entity.Diagnosis ?? string.Empty,
                DiagnosedOn = entity.DiagnosedOn,
                ResolvedOn = entity.ResolvedOn,
                IsActive = entity.IsActive,
                Notes = entity.Notes
            };
        }

        public static PetMedicalHistory ToEntity(this PetMedicalHistoryDto dto)
        {
            return new PetMedicalHistory
            {
                Id = dto.Id,
                PetId = dto.PetId,
                Diagnosis = dto.Diagnosis ?? string.Empty,
                DiagnosedOn = dto.DiagnosedOn,
                ResolvedOn = dto.ResolvedOn,
                IsActive = dto.IsActive,
                Notes = dto.Notes
            };
        }

        
        public static void ApplyToEntity(this PetMedicalHistoryDto dto, PetMedicalHistory entity)
        {
            entity.Diagnosis = dto.Diagnosis ?? string.Empty;
            entity.DiagnosedOn = dto.DiagnosedOn;
            entity.ResolvedOn = dto.ResolvedOn;
            entity.IsActive = dto.IsActive;
            entity.Notes = dto.Notes;
        }
    }
}
