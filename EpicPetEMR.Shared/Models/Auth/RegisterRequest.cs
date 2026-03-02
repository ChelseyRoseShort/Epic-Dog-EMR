namespace EpicPetEMR.Shared.Models.Auth;

public sealed class RegisterRequest
{
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public string FamilyName { get; set; } = "";
}
