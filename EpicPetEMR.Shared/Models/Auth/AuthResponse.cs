namespace EpicPetEMR.Shared.Models.Auth;

public sealed class AuthResponse
{
    public string Token { get; set; } = "";
    public DateTime ExpiresUtc { get; set; }
}
