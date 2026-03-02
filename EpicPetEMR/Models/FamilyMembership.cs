namespace EpicPetEMR.Api.Models;

public enum FamilyRole
{
    Owner = 0,
    Admin = 1,
    Member = 2,
    ReadOnly = 3
}

public class FamilyMembership
{
    public int FamilyId { get; set; }
    public Family Family { get; set; } = default!;

    public int UserId { get; set; }
    public User User { get; set; } = default!;

    public FamilyRole Role { get; set; } = FamilyRole.Member;

    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
}
