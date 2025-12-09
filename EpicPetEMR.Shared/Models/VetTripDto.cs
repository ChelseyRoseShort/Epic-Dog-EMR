using System;

namespace EpicPetEMR.Shared.Models
{
    public sealed class VetTripDto
    {
        public int Id { get; set; }

        public int PetId { get; set; }

        public string Hospital { get; set; } = string.Empty;

        public string Vet { get; set; } = string.Empty;

        // When the visit occurred
        public DateTime VisitDateTime { get; set; }

        // This references the shared enum you already created
        public VetTripReason Reason { get; set; }
    }
}
