using System.ComponentModel.DataAnnotations;

namespace EpicPetEMR.Api.Models;

public class FamilyInvite
{
    public int Id { get; set; }

    public int FamilyId { get; set; }
    public Family Family { get; set; } = default!;

    [Required, MaxLength(64)]
    public string Token { get; set; } = default!; // random url-safe string

    public DateTimeOffset ExpiresAt { get; set; }

    [MaxLength(256)]
    public string? Email { get; set; }            // optional intended recipient

    public int? AcceptedUserId { get; set; }      // filled once used
}
