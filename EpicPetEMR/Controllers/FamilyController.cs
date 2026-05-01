using EpicPetEMR.Api.Data;
using EpicPetEMR.Api.Models;
using EpicPetEMR.Mappers;
using EpicPetEMR.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace EpicPetEMR.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FamiliesController : ControllerBase
{
    private readonly AppDbContext _db;

    public FamiliesController(AppDbContext db)
    {
        _db = db;
    }

    private int GetUserId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet("with-pets")]
    public async Task<IActionResult> GetMyFamiliesWithPets()
    {
        var userId = GetUserId();
        var memberships = await _db.FamilyMemberships
            .Where(m => m.UserId == userId)
            .Include(m => m.Family)
                .ThenInclude(f => f.Memberships)
                    .ThenInclude(m => m.User)
            .Include(m => m.Family)
                .ThenInclude(f => f.Pets)
            .ToListAsync(); // ← pull from DB first

        var result = memberships.Select(m => new
        {
            m.Family.Id,
            m.Family.Name,
            m.Role,
            m.CreatedUtc,
            Members = m.Family.Memberships.Select(fm => new
            {
                fm.UserId,
                fm.User.DisplayName,
                fm.User.Email,
                fm.Role
            }),
            Pets = m.Family.Pets.Select(p => p.ToDto()) // ← now safe to call in memory
        });

        return Ok(result);
    }

    // ─── GET api/families ────────────────────────────────────────────────────
    /// <summary>Returns all families the current user is a member of.</summary>
    [HttpGet]
    public async Task<IActionResult> GetMyFamilies()
    {
        var userId = GetUserId();

        

        var families = await _db.FamilyMemberships
            .Where(m => m.UserId == userId)
            .Include(m => m.Family)
            .ThenInclude(f => f.Memberships)
            .ThenInclude(m => m.User)
            .Include(m => m.Family)
            .ThenInclude(f => f.Pets)
                    
                       
            .Select(m => new
            {
                m.Family.Id,
                m.Family.Name,
                m.Role,
                m.CreatedUtc,
                Members = m.Family.Memberships.Select(fm => new
                {
                    fm.UserId,
                    fm.User.DisplayName,
                    fm.User.Email,
                    fm.Role
                }),
                Pets = m.Family.Pets.Select(p => new
                {
                    p.Id,
                    p.FamilyId,
                    p.Name,
                    p.ProfilePic,
                    p.Species,
                    p.Sex,
                    p.Breed,
                    p.DateOfBirth,
                    p.Weight,
                    p.WeightUnit,
                    p.Appointments,
                    p.Attachments,
                    p.MedicalHistory
                })
            })
            .ToListAsync();

        return Ok(families);
    }

    // ─── GET api/families/{id} ───────────────────────────────────────────────
    /// <summary>Returns a single family. User must be a member.</summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetFamily(int id)
    {
        var userId = GetUserId();

        var membership = await _db.FamilyMemberships
            .Where(m => m.FamilyId == id && m.UserId == userId)
            .Include(m => m.Family)
                .ThenInclude(f => f.Memberships)
                    .ThenInclude(m => m.User)
            .Include(m => m.Family)
                .ThenInclude(f => f.Pets)
            .FirstOrDefaultAsync();

        if (membership is null)
            return NotFound();

        var f = membership.Family;
        return Ok(new
        {
            f.Id,
            f.Name,
            Members = f.Memberships.Select(m => new
            {
                m.UserId,
                m.User.DisplayName,
                m.User.Email,
                m.Role,
                m.CreatedUtc
            }),
            Pets = f.Pets.Select(p => new { p.Id, p.Name }) // expand as needed
        });
    }

    // GET api/families/{id}/pets
    [HttpGet("{id:int}/pets")]
    public async Task<IActionResult> GetFamilyPets(int id)
    {
        var userId = GetUserId();

        // Make sure the user is actually a member of this family
        var membership = await _db.FamilyMemberships
            .FirstOrDefaultAsync(m => m.FamilyId == id && m.UserId == userId);

        if (membership is null)
            return NotFound();

        var pets = await _db.Pets
            .Where(p => p.FamilyId == id)
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Species,
                p.Breed,
                p.ProfilePic
            })
            .ToListAsync();

        return Ok(pets);
    }

    // ─── POST api/families ───────────────────────────────────────────────────
    /// <summary>Creates a new family. Creator becomes Owner.</summary>
    [HttpPost]
    public async Task<IActionResult> CreateFamily([FromBody] CreateFamilyRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Family name is required.");

        var userId = GetUserId();

        var family = new Family { Name = request.Name };
        _db.Families.Add(family);
        await _db.SaveChangesAsync(); // get the generated Id

        var membership = new FamilyMembership
        {
            FamilyId = family.Id,
            UserId = userId,
            Role = FamilyRole.Owner
        };
        _db.FamilyMemberships.Add(membership);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetFamily), new { id = family.Id }, new
        {
            family.Id,
            family.Name,
            Role = FamilyRole.Owner
        });
    }

    // ─── PUT api/families/{id} ───────────────────────────────────────────────
    /// <summary>Updates family name. Only Owner or Admin can do this.</summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateFamily(int id, [FromBody] UpdateFamilyRequest request)
    {
        var userId = GetUserId();

        var membership = await _db.FamilyMemberships
            .Include(m => m.Family)
            .FirstOrDefaultAsync(m => m.FamilyId == id && m.UserId == userId);

        if (membership is null)
            return NotFound();

        if (membership.Role > FamilyRole.Admin) // Member or ReadOnly
            return Forbid();

        if (!string.IsNullOrWhiteSpace(request.Name))
            membership.Family.Name = request.Name;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    // ─── DELETE api/families/{id} ────────────────────────────────────────────
    /// <summary>Deletes a family. Only Owner can do this.</summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteFamily(int id)
    {
        var userId = GetUserId();

        var membership = await _db.FamilyMemberships
            .Include(m => m.Family)
            .FirstOrDefaultAsync(m => m.FamilyId == id && m.UserId == userId);

        if (membership is null)
            return NotFound();

        if (membership.Role != FamilyRole.Owner)
            return Forbid();

        _db.Families.Remove(membership.Family);
        await _db.SaveChangesAsync();
        return NoContent();
    }

    // ─── POST api/families/{id}/members ─────────────────────────────────────
    /// <summary>Adds a user to a family. Only Owner or Admin can invite.</summary>
    [HttpPost("{id:int}/members")]
    public async Task<IActionResult> AddMember(int id, [FromBody] AddMemberRequest request)
    {
        var userId = GetUserId();

        var callerMembership = await _db.FamilyMemberships
            .FirstOrDefaultAsync(m => m.FamilyId == id && m.UserId == userId);

        if (callerMembership is null)
            return NotFound();

        if (callerMembership.Role > FamilyRole.Admin)
            return Forbid();

        // Prevent escalation — no one can assign a role higher than their own
        if (request.Role < callerMembership.Role)
            return BadRequest("Cannot assign a role higher than your own.");

        var targetUser = await _db.Users.FindAsync(request.UserId);
        if (targetUser is null)
            return BadRequest("User not found.");

        var alreadyMember = await _db.FamilyMemberships
            .AnyAsync(m => m.FamilyId == id && m.UserId == request.UserId);

        if (alreadyMember)
            return Conflict("User is already a member of this family.");

        _db.FamilyMemberships.Add(new FamilyMembership
        {
            FamilyId = id,
            UserId = request.UserId,
            Role = request.Role
        });

        await _db.SaveChangesAsync();
        return Ok();
    }

    // ─── DELETE api/families/{id}/members/{memberId} ─────────────────────────
    /// <summary>Removes a member. Owner/Admin can remove others; anyone can remove themselves.</summary>
    [HttpDelete("{id:int}/members/{memberId:int}")]
    public async Task<IActionResult> RemoveMember(int id, int memberId)
    {
        var userId = GetUserId();

        var callerMembership = await _db.FamilyMemberships
            .FirstOrDefaultAsync(m => m.FamilyId == id && m.UserId == userId);

        if (callerMembership is null)
            return NotFound();

        // Allow self-removal
        if (memberId != userId && callerMembership.Role > FamilyRole.Admin)
            return Forbid();

        // Owner cannot be removed (must delete family instead)
        if (memberId == userId && callerMembership.Role == FamilyRole.Owner)
            return BadRequest("Owner cannot leave the family. Transfer ownership or delete the family.");

        var targetMembership = await _db.FamilyMemberships
            .FirstOrDefaultAsync(m => m.FamilyId == id && m.UserId == memberId);

        if (targetMembership is null)
            return NotFound();

        _db.FamilyMemberships.Remove(targetMembership);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}

// ─── Request DTOs ────────────────────────────────────────────────────────────

public record CreateFamilyRequest(string Name);
public record UpdateFamilyRequest(string? Name);
public record AddMemberRequest(int UserId, FamilyRole Role);