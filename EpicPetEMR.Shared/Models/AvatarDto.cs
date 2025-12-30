namespace EpicPetEMR.Shared.Models;

public sealed class AvatarDto
{
    public int Id { get; set; }
    public int PetId { get; set; }

    public AvatarFinding Type { get; set; }
    public bool IsActive { get; set; }

    public DateTime CreatedAtUtc { get; set; }
    public DateTime? UpdatedAtUtc { get; set; }
    public DateTime? PlacedAtUtc { get; set; }
    public DateTime? RemovedAtUtc { get; set; }

    public string? Note { get; set; }
    public string? Assessment { get; set; } //later add time and put on brain

    public int? PhotoDocumentId { get; set; }

    public BodyMapKey? MapKey { get; set; }
    public int? CellIndex { get; set; }
    public int? GridRows { get; set; }
    public int? GridCols { get; set; }
}

public sealed class CreateAvatarRequest
{
    // Recommended: do NOT send PetId in body; use route petId.
    // If you keep it, ignore/overwrite it server-side.
    // public int PetId { get; set; }

    public AvatarFinding Type { get; set; }
    public bool IsActive { get; set; } = true;

    public DateTime? PlacedAtUtc { get; set; }

    public string? Note { get; set; }
    public string? Assessment { get; set; }

    public int? PhotoDocumentId { get; set; }

    public BodyMapKey? MapKey { get; set; }
    public int? CellIndex { get; set; }
    public int? GridRows { get; set; }
    public int? GridCols { get; set; }
}

public sealed class UpdateAvatarRequest
{
    public AvatarFinding Type { get; set; }
    public bool IsActive { get; set; }

    public DateTime? PlacedAtUtc { get; set; }


    public int Id { get; set; }
    public string? Note { get; set; }
    public string? Assessment { get; set; }

    public int? PhotoDocumentId { get; set; }

    public BodyMapKey? MapKey { get; set; }
    public int? CellIndex { get; set; }
    public int? GridRows { get; set; }
    public int? GridCols { get; set; }
}
