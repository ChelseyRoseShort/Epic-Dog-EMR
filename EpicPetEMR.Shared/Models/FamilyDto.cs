namespace EpicPetEMR.Shared.Models;

public sealed class FamilyDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string Role { get; set; } = "";
    public DateTime CreatedUtc { get; set; }
    public List<FamilyMemberDto> Members { get; set; } = new();
}

public sealed class FamilyMemberDto
{
    public int UserId { get; set; }
    public string? DisplayName { get; set; }
    public string? Email { get; set; }
    public string Role { get; set; } = "";
}

public sealed class UserLookupDto
{
    public int Id { get; set; }
    public string? DisplayName { get; set; }
    public string? Email { get; set; }
}