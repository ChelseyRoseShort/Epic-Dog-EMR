using EpicPetEMR.Shared.Models;

namespace EpicPetEMR.Api.Models;

public class PetDocument
{
    public int Id { get; set; }

    public int PetId { get; set; }
    public Pet Pet { get; set; } = null!;

    public DocumentType Type { get; set; }
    public int? VetTripId { get; set; }
    public VetTrip? VetTrip { get; set; }

    public string Name { get; set; } = string.Empty;       
    public string FileName { get; set; } = string.Empty;  
    public string ContentType { get; set; } = string.Empty; 

    public byte[] Data { get; set; } = Array.Empty<byte>();

    public DateTime UploadedAt { get; set; } = DateTime.Now;
}
