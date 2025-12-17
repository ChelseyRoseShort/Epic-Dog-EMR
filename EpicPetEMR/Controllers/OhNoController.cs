using EpicPetEMR.Api.Data;
using EpicPetEMR.Api.Mappings;
using EpicPetEMR.Api.Models;
using EpicPetEMR.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpicPetEMR.Api.Controllers;

[ApiController]
[Route("api/{petId:int}/ohnoevents")]
public class OhNoEventsController : ControllerBase
{
    private readonly AppDbContext _db;

    public OhNoEventsController(AppDbContext db)
    {
        _db = db;
    }

    // GET: api/pets/{petId}/ohnoevents
    [HttpGet]
    public async Task<ActionResult<List<OhNoEventDto>>> GetForPet(int petId)
    {
        // Make sure the pet exists (optional but nice)
        var petExists = await _db.Pets.AnyAsync(p => p.Id == petId);
        if (!petExists)
        {
            return NotFound($"Pet {petId} not found.");
        }

        var events = await _db.OhNoEvents
            .Where(e => e.PetId == petId)
            .OrderByDescending(e => e.OccurredAt)
            .ToListAsync();

        var dtos = events.Select(e => e.ToDto()).ToList();
        return Ok(dtos);
    }

    // GET: api/pets/{petId}/ohnoevents/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<OhNoEventDto>> GetById(int petId, int id)
    {
        var entity = await _db.OhNoEvents
            .FirstOrDefaultAsync(e => e.Id == id && e.PetId == petId);

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(entity.ToDto());
    }

    // POST: api/ohnoevents
    [HttpPost]
    public async Task<ActionResult<OhNoEventDto>> Create(int petId, [FromBody] OhNoEventDto dto)
    {
        // Ensure pet exists
        var petExists = await _db.Pets.AnyAsync(p => p.Id == petId);
        if (!petExists)
        {
            return NotFound($"Pet {petId} not found.");
        }

        // Trust the route as the source of truth for PetId
        dto.PetId = petId;

        var entity = dto.ToEntity();
        entity.Id = 0; // make sure EF treats it as new

        _db.OhNoEvents.Add(entity);
        await _db.SaveChangesAsync();

        var resultDto = entity.ToDto();

        return CreatedAtAction(
            nameof(GetById),
            new { petId, id = entity.Id },
            resultDto);
    }

    // PUT: api/pets/{petId}/ohnoevents/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult<OhNoEventDto>> Update(int petId, int id, [FromBody] OhNoEventDto dto)
    {
        if (id != dto.Id && dto.Id != 0)
        {
            // If dto.Id is set and doesn't match route, reject it
            return BadRequest("ID in route and body do not match.");
        }

        var entity = await _db.OhNoEvents
            .FirstOrDefaultAsync(e => e.Id == id && e.PetId == petId);

        if (entity == null)
        {
            return NotFound();
        }

        // Keep PetId from route as the source of truth
        dto.Id = id;
        dto.PetId = petId;

        entity.UpdateFromDto(dto);

        await _db.SaveChangesAsync();

        return Ok(entity.ToDto());
    }

    // DELETE: api/pets/{petId}/ohnoevents/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int petId, int id)
    {
        var entity = await _db.OhNoEvents
            .FirstOrDefaultAsync(e => e.Id == id && e.PetId == petId);

        if (entity == null)
        {
            return NotFound();
        }

        _db.OhNoEvents.Remove(entity);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
