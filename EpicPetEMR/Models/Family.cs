namespace EpicPetEMR.Api.Models;

public class Family
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public List<FamilyMembership> Memberships { get; set; } = new();
    public List<Pet> Pets { get; set; } = new();
}
