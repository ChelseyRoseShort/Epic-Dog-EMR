using System.ComponentModel.DataAnnotations;
using EpicPetEMR.Shared.Models;
namespace EpicPetEMR.Api.Models;

public class FamilyInvite
{
    public int Id { get; set; }

    public int FamilyId { get; set; }
    public Family Family { get; set; } = default!;

    [Required, MaxLength(64)]
    public string Token { get; set; } = default!; 

    public DateTimeOffset ExpiresAt { get; set; }

    [MaxLength(256)]
    public string? Email { get; set; }            

    public int? AcceptedUserId { get; set; }      
}
