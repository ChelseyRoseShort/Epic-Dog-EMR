using System;

namespace EpicPetEMR.Shared.Models
{
    public sealed class VetTripDto
    {
        public int Id { get; set; }

        public int PetId { get; set; }

        public string Hospital { get; set; } = string.Empty;

        public string Vet { get; set; } = string.Empty;

      
        public DateTime VisitDateTime { get; set; }


        public VetTripReason Reason { get; set; }

        public List<PetDocumentDto> Documents { get; set; } = [];
    }
}
