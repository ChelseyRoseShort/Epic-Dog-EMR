using EpicPetEMR.Api.Data;
using EpicPetEMR.Api.Mappers;
using EpicPetEMR.Api.Mapping;
using EpicPetEMR.Api.Models;
using EpicPetEMR.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;



namespace EpicPetEMR.Api.Controllers;

[ApiController]
[Route("api/pets/{petId:int}/avatars")]
public class AvatarController : ControllerBase
{
    private readonly AppDbContext _db;
    public AvatarController(AppDbContext context) => _db = context;

    [HttpPost]
    public async Task<ActionResult<AvatarDto>> PostAvatar(int petId, [FromBody] CreateAvatarRequest req)
    {
        var avatar = req.FromCreate(petId);

        _db.PetFindings.Add(avatar);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAvatar), new { petId, id = avatar.Id }, avatar.ToDto());
    }

    // list for pet
    [HttpGet]
    public async Task<ActionResult<List<AvatarDto>>> GetAvatarsForPet(int petId)
    {
        var list = await _db.PetFindings
            .Where(a => a.PetId == petId && a.IsActive == true)
            .Select(a => a.ToDto())
            .ToListAsync();

        return Ok(list);
    }

    // single by id
    [HttpGet("{id:int}")]
    public async Task<ActionResult<AvatarDto>> GetAvatar(int petId, int id)
    {
        var avatar = await _db.PetFindings
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.PetId == petId && a.Id == id);

        if (avatar is null) return NotFound();

        return Ok(avatar.ToDto());
    }
[HttpPut]
public async Task<ActionResult<AvatarDto>> UpdateAvatar(int petId, [FromBody] UpdateAvatarRequest req)
{
    // 1) Look up the existing Avatar row in the database for this pet + this finding Id
    var avatar = await _db.PetFindings
        .FirstOrDefaultAsync(a => a.PetId == petId && a.Id == req.Id);

    // If we didn't find it, return a 404
    if (avatar is null)
        return NotFound();

    // 2) Append the new Assessment text instead of overwriting
    var newText = req.Assessment?.Trim();

    // Only do anything if the user actually typed something
    if (!string.IsNullOrWhiteSpace(newText))
    {
        // If the database had nothing yet, start it with the new text
        if (string.IsNullOrWhiteSpace(avatar.Assessment))
        {
            avatar.Assessment = newText;
        }
        else
        {
            // Otherwise add it on a new line (but avoid duplicates)
            if (!avatar.Assessment.Contains(newText))
                avatar.Assessment = $"{avatar.Assessment.TrimEnd()}\n{newText}";
        }
    }
      
            avatar.IsActive = true;
        
        // 3) Update the rest of the fields (Type, IsActive, MapKey, etc.)
        // IMPORTANT: Your ApplyUpdate method must NOT overwrite Assessment,
        // or it will undo the append logic above.
        avatar.ApplyUpdate(req);

    // 4) Save the changes
    await _db.SaveChangesAsync();

    // 5) Send back the updated record
    return Ok(avatar.ToDto());
}

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeactivateAvatar(int petId, int id)
    {
        var entity = await _db.PetFindings
            .FirstOrDefaultAsync(a => a.PetId == petId && a.Id == id);

        if (entity is null)
            return NotFound();

        entity.IsActive = false;
        entity.RemovedAtUtc ??= DateTime.UtcNow;
        entity.UpdatedAtUtc = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return NoContent();
    }





}
