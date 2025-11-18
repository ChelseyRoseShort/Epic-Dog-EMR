using EpicPetEMR.Shared.Models;
namespace EpicPetEMR.Api.Models;

public class Family
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public List<User> Users { get; set; } = new();
    public List<Pet> Pets { get; set; } = new();
}
