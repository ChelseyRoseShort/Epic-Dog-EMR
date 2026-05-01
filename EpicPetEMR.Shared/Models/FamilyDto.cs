using System.ComponentModel.DataAnnotations;

namespace EpicPetEMR.Shared.Models;

public sealed class FamilyDto
{
    [Required]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string Role { get; set; } = "";
    public DateTime CreatedUtc { get; set; }
    public List<FamilyMemberDto> Members { get; set; } = new();
    public List<PetDto> Pets { get; set; } = new(); 
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

public sealed class FamilyPetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Species { get; set; } = "";
    public string? Breed { get; set; }
    public string? ProfilePic { get; set; }
}