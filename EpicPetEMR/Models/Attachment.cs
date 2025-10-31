namespace EpicPetEMR.Api.Models;

public class Attachment
{
    public int Id { get; set; }

    public int PetId { get; set; }
    public Pet Pet { get; set; } = default!;

    public string FileName { get; set; } = "";
    public string? ContentType { get; set; }
    public string StoragePath { get; set; } = "";
    public DateTimeOffset UploadedAt { get; set; } = DateTimeOffset.UtcNow;
}
