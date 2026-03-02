using Microsoft.AspNetCore.Identity;

namespace EpicPetEMR.Api.Models;

public class User : IdentityUser<int>
{
    public string DisplayName { get; set; } = "";
    public List<FamilyMembership> FamilyMemberships { get; set; } = new();
}
