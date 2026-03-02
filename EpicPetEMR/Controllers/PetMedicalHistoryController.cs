using EpicPetEMR.Api.Data;
using EpicPetEMR.Api.Mappings;
using EpicPetEMR.Api.Models;
using EpicPetEMR.Mappers;
using EpicPetEMR.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpicPetEMR.Api.Controllers;

[ApiController]
[Route("api/pets/{petId:int}/medical-history")]
public class PetMedicalHistoryController : ControllerBase
{
    private readonly AppDbContext _db;

    public PetMedicalHistoryController(AppDbContext db)
    {
        _db = db;
    }

    // GET: api/pets/{petId}/medical-history
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PetMedicalHistoryDto>>> GetAll(int petId)
    {
        var items = await _db.PetMedicalHistories
            .AsNoTracking()
            .Where(x => x.PetId == petId)
            .OrderByDescending(x => x.IsActive)
            .ThenByDescending(x => x.DiagnosedOn)
            .ThenByDescending(x => x.Id)
            .Select(x => x.ToDto())
            .ToListAsync();

        return Ok(items);
    }

    // GET: api/pets/{petId}/medical-history/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PetMedicalHistoryDto>> GetById(int petId, int id)
    {
        var entity = await _db.PetMedicalHistories
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id && x.PetId == petId);

        if (entity is null)
            return NotFound();

        return Ok(entity.ToDto());
    }

    // POST: api/pets/{petId}/medical-history
    [HttpPost]
    public async Task<ActionResult<PetMedicalHistoryDto>> Create(int petId, [FromBody] PetMedicalHistoryDto dto)
    {
        if (dto is null)
            return BadRequest("Request body is required.");

        if (string.IsNullOrWhiteSpace(dto.Diagnosis))
            return BadRequest("Diagnosis is required.");

        // Enforce route petId to prevent mismatches
        dto.PetId = petId;

        var entity = dto.ToEntity();

        _db.PetMedicalHistories.Add(entity);
        await _db.SaveChangesAsync();

        var result = entity.ToDto();

        return CreatedAtAction(
            nameof(GetById),
            new { petId, id = entity.Id },
            result
        );
    }

    // PUT: api/pets/{petId}/medical-history/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult<PetMedicalHistoryDto>> Update(int petId, int id, [FromBody] PetMedicalHistoryDto dto)
    {
        if (dto is null)
            return BadRequest("Request body is required.");

        if (string.IsNullOrWhiteSpace(dto.Diagnosis))
            return BadRequest("Diagnosis is required.");

        var entity = await _db.PetMedicalHistories
            .FirstOrDefaultAsync(x => x.Id == id && x.PetId == petId);

        if (entity is null)
            return NotFound();

        // Ignore incoming Id/PetId; use route identity
        dto.ApplyToEntity(entity);

        await _db.SaveChangesAsync();

        return Ok(entity.ToDto());
    }

    // DELETE: api/pets/{petId}/medical-history/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int petId, int id)
    {
        var entity = await _db.PetMedicalHistories
            .FirstOrDefaultAsync(x => x.Id == id && x.PetId == petId);

        if (entity is null)
            return NotFound();

        _db.PetMedicalHistories.Remove(entity);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
