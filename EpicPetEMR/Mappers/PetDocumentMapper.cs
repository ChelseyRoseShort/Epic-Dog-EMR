using EpicPetEMR.Api.Models;
using EpicPetEMR.Shared.Models;

namespace EpicPetEMR.Api.Mappers
{
    public static class PetDocumentMapper
    {
        public static PetDocumentDto ToDto(this PetDocument entity)
        {
            if (entity == null) return null!;

            return new PetDocumentDto
            {
                Id = entity.Id,
                PetId = entity.PetId,
                Type = entity.Type,
                VetTripId = entity.VetTripId,
                Name = entity.Name,
                FileName = entity.FileName,
                ContentType = entity.ContentType,
                Data = entity.Data,
                UploadedAt = entity.UploadedAt
            };
        }
    }
}
