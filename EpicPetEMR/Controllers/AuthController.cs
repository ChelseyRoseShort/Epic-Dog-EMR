using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EpicPetEMR.Api.Data;
using EpicPetEMR.Api.Models;
using EpicPetEMR.Shared.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EpicPetEMR.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly AppDbContext _db;
    private readonly IConfiguration _config;

    public AuthController(UserManager<User> userManager, AppDbContext db, IConfiguration config)
    {
        _userManager = userManager;
        _db = db;
        _config = config;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest req, CancellationToken ct)
    {
        var email = req.Email.Trim().ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(req.Password))
            return BadRequest("Email and password required.");

        var existing = await _userManager.FindByEmailAsync(email);
        if (existing is not null) return Conflict("Email already exists.");

        var user = new User
        {
            UserName = email,
            Email = email,
            DisplayName = email
        };

        var result = await _userManager.CreateAsync(user, req.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);

        // minimal: create a family and add membership for this user as owner
        var family = new Family { Name = string.IsNullOrWhiteSpace(req.FamilyName) ? "My Family" : req.FamilyName.Trim() };
        _db.Families.Add(family);
        await _db.SaveChangesAsync(ct);

        _db.FamilyMemberships.Add(new FamilyMembership
        {
            FamilyId = family.Id,
            UserId = user.Id,
            Role = FamilyRole.Owner
        });
        await _db.SaveChangesAsync(ct);

        return Ok(CreateToken(user));
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest req)
    {
        var email = req.Email.Trim().ToLowerInvariant();
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user is null) return Unauthorized("Invalid credentials.");

        var ok = await _userManager.CheckPasswordAsync(user, req.Password);
        if (!ok) return Unauthorized("Invalid credentials.");

        return Ok(CreateToken(user));
    }

    private AuthResponse CreateToken(User user)
    {
        var key = _config["Jwt:Key"]!;
        var issuer = _config["Jwt:Issuer"]!;
        var audience = _config["Jwt:Audience"]!;
        var expiresMinutes = int.Parse(_config["Jwt:ExpiresMinutes"] ?? "120");

        var now = DateTime.UtcNow;
        var expires = now.AddMinutes(expiresMinutes);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
        };

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            notBefore: now,
            expires: expires,
            signingCredentials: creds
        );

        return new AuthResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwt),
            ExpiresUtc = expires
        };
    }
}
