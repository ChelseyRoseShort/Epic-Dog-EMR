using EpicPetEMR.Shared.Models;
namespace EpicPetEMR.Api.Models;

public class User
{
    public int Id { get; set; }
    public string DisplayName { get; set; } = "";
    public string Email { get; set; } = "";

    public int FamilyId { get; set; }
    public Family Family { get; set; } = default!;
}
