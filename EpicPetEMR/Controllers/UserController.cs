using EpicPetEMR.Api.Data;
using EpicPetEMR.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpicPetEMR.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _db;
    public UsersController(AppDbContext db) => _db = db;

    // GET api/users/lookup?email=foo@bar.com
    [HttpGet("lookup")]
    public async Task<IActionResult> LookupByEmail([FromQuery] string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return BadRequest("Email is required.");

        var user = await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email.Trim().ToLower());

        if (user is null)
            return NotFound("No user found with that email.");

        return Ok(new { user.Id, user.DisplayName, user.Email });
    }
}