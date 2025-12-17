using EpicPetEMR.Api.Data;
using EpicPetEMR.Api.Mappers;
using EpicPetEMR.Api.Models;
using EpicPetEMR.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EpicPetEMR.Api.Controllers
{
    [ApiController]
    [Route("api/vettrips")]
    public class VetTripsApiController : ControllerBase
    {
        private readonly AppDbContext _db;

        public VetTripsApiController(AppDbContext db)
        {
            _db = db;
        }

        // GET: api/vettrips/by-pet/1
        [HttpGet("by-pet/{petId:int}")]
        public async Task<ActionResult<List<VetTripDto>>> GetByPet(int petId)
        {
            var trips = await _db.VetTrips
                .Where(v => v.PetId == petId)
                .OrderByDescending(v => v.VisitDateTime)
                .Select(v => v.ToDto())
                .ToListAsync();

            return Ok(trips);
        }

        [HttpGet("today")]
        public async Task<ActionResult<List<VetTripDto>>> GetTodayTrips()
        {
            var today = DateTime.Now.Date;
            var tomorrow = today.AddDays(1);

            var trips = await _db.VetTrips
                .Where(v => v.VisitDateTime >= today && v.VisitDateTime < tomorrow)
                .OrderByDescending(v => v.VisitDateTime)
                .Select(v => v.ToDto())
                .ToListAsync();

            return Ok(trips);
        }

        // GET: api/vettrips/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<VetTripDto>> Get(int id)
        {
            var entity = await _db.VetTrips.FindAsync(id);
            if (entity == null)
                return NotFound();

            return Ok(entity.ToDto());
        }

        // POST: api/vettrips
        [HttpPost]
        public async Task<ActionResult<VetTripDto>> Create([FromBody] VetTripDto dto)
        {
            var entity = dto.ToEntity();

            _db.VetTrips.Add(entity);
            await _db.SaveChangesAsync();

            var resultDto = entity.ToDto();

            // So PetApi.AddVetTripAsync can read the created DTO
            return CreatedAtAction(nameof(Get), new { id = resultDto.Id }, resultDto);
        }

        // PUT: api/vettrips/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] VetTripDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var entity = await _db.VetTrips.FindAsync(id);
            if (entity == null)
                return NotFound();

            entity.PetId = dto.PetId;
            entity.Hospital = dto.Hospital;
            entity.Vet = dto.Vet;
            entity.VisitDateTime = dto.VisitDateTime;
            entity.Reason = dto.Reason;

            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
