using EpicPetEMR.Api.Data;
using EpicPetEMR.Api.Mappers;
using EpicPetEMR.Api.Models;
using EpicPetEMR.Mappers;
using EpicPetEMR.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpicPetEMR.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetController : BaseController
{
    private readonly AppDbContext _db;
    private readonly IWebHostEnvironment _env;

    public PetController(AppDbContext db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
    }

    // GET /api/pet/demo?includeMeds=false
    [HttpGet("demo")]
    public async Task<IActionResult> GetDemoPets([FromQuery] bool includeMeds = false)
    {
        var pets = await GetUserPets(_db).Include(p => p.Family).ToListAsync();
        if (!includeMeds)
            return Ok(pets.Select(p => p.ToDto()));

        var petIds = pets.Select(p => p.Id).ToList();
        var meds = await _db.Medications.Where(m => petIds.Contains(m.PetId)).ToListAsync();

        var result = pets.Select(p => new
        {
            Pet = p.ToDto(),
            Medications = meds.Where(m => m.PetId == p.Id).Select(m => m.ToDto()).ToList()
        });

        return Ok(result);
    }

    // GET /api/pet/{id}
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPet(int id)
    {
        var pet = await GetUserPets(_db).FirstOrDefaultAsync(p => p.Id == id);
        if (pet == null)
            return NotFound("Pet not found");

        return Ok(pet.ToDto());
    }

    // POST /api/pet
    [HttpPost]
    public async Task<IActionResult> AddPet([FromBody] PetDto petDto)
    {
        var pet = petDto.ToEntity();

        if (string.IsNullOrWhiteSpace(pet.ProfilePic))
        {
            pet.ProfilePic = pet.Species switch
            {
                Species.Cat => "/a-sleek-aesthetic-line-art-of-a-cat-in-a-side-profile-the-cat-has-sharp-geometric-angles-for-the-ears-and-soft-curves-for-the-body-vector.jpg",
                Species.Dog => "/360_F_1381163227_EfXTrDaEGIrk4izbfwh5zvP2yO8ABMyP.jpg",
                _ => ""
            };
        }

        _db.Pets.Add(pet);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPet), new { id = pet.Id }, pet.ToDto());
    }

    // PUT /api/pet
    [HttpPut]
    public async Task<IActionResult> UpdatePet([FromBody] PetDto petDto)
    {
        var pet = petDto.ToEntity();
        _db.Pets.Update(pet);
        await _db.SaveChangesAsync();

        return Ok(pet.ToDto());
    }

    // DELETE /api/pet
    [HttpDelete]
    public async Task<IActionResult> DeletePet([FromBody] PetDto petDto)
    {
        var pet = petDto.ToEntity();
        _db.Pets.Attach(pet);
        _db.Pets.Remove(pet);
        await _db.SaveChangesAsync();

        return NoContent();
    }

    // POST /api/pet/{id}/profilepic
    [HttpPost("{id:int}/profilepic")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadProfilePic(int id, IFormFile file)
    {
        var pet = await _db.Pets.FindAsync(id);
        if (pet == null)
            return NotFound("Pet not found");

        var uploadsFolder = Path.Combine(_env.ContentRootPath, "wwwroot", "petphotos");
        Directory.CreateDirectory(uploadsFolder);

        var safeFilename = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var fullPath = Path.Combine(uploadsFolder, safeFilename);

        using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);

        pet.ProfilePic = $"/petphotos/{safeFilename}";
        await _db.SaveChangesAsync();

        return Ok(pet.ToDto());
    }
}