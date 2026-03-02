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
 
    var avatar = await _db.PetFindings
        .FirstOrDefaultAsync(a => a.PetId == petId && a.Id == req.Id);


    if (avatar is null)
        return NotFound();

   
    var newText = req.Assessment?.Trim();


    if (!string.IsNullOrWhiteSpace(newText))
    {
     
        if (string.IsNullOrWhiteSpace(avatar.Assessment))
        {
            avatar.Assessment = newText;
        }
        else
        {
 
            if (!avatar.Assessment.Contains(newText))
                avatar.Assessment = $"{avatar.Assessment.TrimEnd()}\n{newText}";
        }
    }
      
            avatar.IsActive = true;
        
     
        avatar.ApplyUpdate(req);


    await _db.SaveChangesAsync();


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
