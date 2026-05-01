using EpicPetEMR.Api.Data;
using EpicPetEMR.Api.Mappers;
using EpicPetEMR.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpicPetEMR.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicationController : BaseController
{
    private readonly AppDbContext _db;

    public MedicationController(AppDbContext db)
    {
        _db = db;
    }

    // GET /api/medication/{id}
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMedication(int id)
    {
        var med = await _db.Medications.FirstOrDefaultAsync(m => m.Id == id);
        if (med == null)
            return NotFound("Medication not found");

        return Ok(med.ToDto());
    }

    // GET /api/medication/bypet/{petId}
    [HttpGet("bypet/{petId:int}")]
    public async Task<IActionResult> GetMedicationsByPet(int petId)
    {
        var meds = await _db.Medications
            .Where(m => m.PetId == petId)
            .Select(m => m.ToDto())
            .ToListAsync();

        return Ok(meds);
    }

    // POST /api/medication
    [HttpPost]
    public async Task<IActionResult> AddMedication([FromBody] MedicationDto medicationDto)
    {
        var medication = medicationDto.ToEntity();
        _db.Medications.Add(medication);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMedication), new { id = medication.Id }, medication.ToDto());
    }

    // DELETE /api/medication
    [HttpDelete]
    public async Task<IActionResult> DeleteMedication([FromBody] MedicationDto medicationDto)
    {
        var med = medicationDto.ToEntity();
        _db.Medications.Attach(med);
        _db.Medications.Remove(med);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}
