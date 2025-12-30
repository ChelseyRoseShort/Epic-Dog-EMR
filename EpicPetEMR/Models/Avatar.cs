using EpicPetEMR.Shared.Models;

namespace EpicPetEMR.Api.Models;

public class Avatar
{

    private Avatar() { }


    public Avatar(int petId)
    {
        PetId = petId;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public int Id { get; set; }

    public int PetId { get; private set; }
    public Pet Pet { get; set; } = null!;

    public AvatarFinding Type { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? UpdatedAtUtc { get; set; }

    public DateTime? PlacedAtUtc { get; set; }
    public DateTime? RemovedAtUtc { get; set; }

    public string? Note { get; set; }
    public string? Assessment { get; set; }


    public int? PhotoDocumentId { get; set; }
    public PetDocument? PhotoDocument { get; set; }


    public BodyMapKey? MapKey { get; set; }
    public int? CellIndex { get; set; }
    public int? GridRows { get; set; }
    public int? GridCols { get; set; }
}
