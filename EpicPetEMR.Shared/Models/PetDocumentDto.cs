using System;

namespace EpicPetEMR.Shared.Models
{
    public sealed class PetDocumentDto
    {
        public int Id { get; set; }

        public int PetId { get; set; }

        public DocumentType Type { get; set; }

        public int? VetTripId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string FileName { get; set; } = string.Empty;

        public string ContentType { get; set; } = string.Empty;

        public byte[] Data { get; set; } = Array.Empty<byte>();

        public DateTime UploadedAt { get; set; }
    }
}
