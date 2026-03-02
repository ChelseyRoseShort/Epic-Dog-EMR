using System.IdentityModel.Tokens.Jwt;

public static class JwtHelpers
{
    public static JwtSecurityToken? Read(string token)
    {
        if (string.IsNullOrWhiteSpace(token)) return null;
        return new JwtSecurityTokenHandler().ReadJwtToken(token);
    }
}
