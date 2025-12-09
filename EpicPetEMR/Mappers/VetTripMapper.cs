using EpicPetEMR.Api.Models;
using EpicPetEMR.Shared.Models;

namespace EpicPetEMR.Api.Mappers
{
    public static class VetTripMapper
    {
        public static VetTripDto ToDto(this VetTrip entity)
        {
            if (entity == null) return null!;

            return new VetTripDto
            {
                Id = entity.Id,
                PetId = entity.PetId,
                Hospital = entity.Hospital,
                Vet = entity.Vet,
                VisitDateTime = entity.VisitDateTime,
                Reason = entity.Reason
            };
        }

        public static VetTrip ToEntity(this VetTripDto dto)
        {
            if (dto == null) return null!;

            return new VetTrip
            {
                Id = dto.Id,
                PetId = dto.PetId,
                Hospital = dto.Hospital,
                Vet = dto.Vet,
                VisitDateTime = dto.VisitDateTime,
                Reason = dto.Reason
            };
        }
    }
}
