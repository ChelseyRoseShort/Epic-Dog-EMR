using EpicPetEMR.Api.Models;
using EpicPetEMR.Shared.Models;

namespace EpicPetEMR.Api.Mapping;

public static class AvatarMapping
{
    public static AvatarDto ToDto(this Avatar e) => new()
    {
        Id = e.Id,
        PetId = e.PetId,

        Type = e.Type,
        IsActive = e.IsActive,

        CreatedAtUtc = e.CreatedAtUtc,
        UpdatedAtUtc = e.UpdatedAtUtc,
        PlacedAtUtc = e.PlacedAtUtc,
        RemovedAtUtc = e.RemovedAtUtc,

        Note = e.Note,
        Assessment = e.Assessment,

        PhotoDocumentId = e.PhotoDocumentId,

        MapKey = e.MapKey,
        CellIndex = e.CellIndex,
        GridRows = e.GridRows,
        GridCols = e.GridCols
    };

 
    public static Avatar FromCreate(this CreateAvatarRequest r, int petIdFromRoute)
    {
        return new Avatar(petIdFromRoute)
        {
            Type = r.Type,
            IsActive = r.IsActive,

     
            PlacedAtUtc = r.PlacedAtUtc,
            RemovedAtUtc = null,

            Note = r.Note,
            Assessment = r.Assessment,

            PhotoDocumentId = r.PhotoDocumentId,

            MapKey = r.MapKey,
            CellIndex = r.CellIndex,
            GridRows = r.GridRows,
            GridCols = r.GridCols
        };
    }

    public static void ApplyUpdate(this Avatar entity, UpdateAvatarRequest req)
    {
        entity.UpdatedAtUtc = DateTime.UtcNow;
        entity.IsActive = true;

        var addition = req.Assessment?.Trim();
        if (!string.IsNullOrWhiteSpace(addition))
        {
            var existing = entity.Assessment ?? string.Empty;

            // Prevent duplicates if you want
            if (!existing.Contains(addition, StringComparison.OrdinalIgnoreCase))
            {
                entity.Assessment = string.IsNullOrWhiteSpace(existing)
                    ? addition
                    : $"{existing}\n{addition}";
            }
        }
    }

}
