using EpicPetEMR.Api.Data;
using EpicPetEMR.Api.Models;
using EpicPetEMR.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EpicPetEMR.Api.Mappers;

namespace EpicPetEMR.Controllers
{
    [Route("api/mar")]

    [ApiController]
    public class MARHistoriesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public MARHistoriesController(AppDbContext context)
        {
            _db = context;
        }

        // GET: api/MARHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MARHistory>>> GetMARHistory()
        {
            return await _db.MARHistory.ToListAsync();
        }

        // GET: api/MARHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MARHistory>> GetMARHistory(int id)
        {
            var mARHistory = await _db.MARHistory.FindAsync(id);

            if (mARHistory == null)
            {
                return NotFound();
            }

            return mARHistory;
        }

        // PUT: api/MARHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMARHistory(int id, MARHistory mARHistory)
        {
            if (id != mARHistory.Id)
            {
                return BadRequest();
            }

            _db.Entry(mARHistory).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MARHistoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MARHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MARHistory>> PostMARHistory(MARHistory mARHistory)
        {
            _db.MARHistory.Add(mARHistory);
            await _db.SaveChangesAsync();

            return CreatedAtAction("GetMARHistory", new { id = mARHistory.Id }, mARHistory);
        }

        // DELETE: api/MARHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMARHistory(int id)
        {
            var mARHistory = await _db.MARHistory.FindAsync(id);
            if (mARHistory == null)
            {
                return NotFound();
            }

            _db.MARHistory.Remove(mARHistory);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        private bool MARHistoryExists(int id)
        {
            return _db.MARHistory.Any(e => e.Id == id);
        }

        [HttpPost("given")]
        public async Task<IActionResult> MarkGiven([FromBody] List<MARHistoryDto> list)
        {
            var now = DateTime.Now;

            foreach (var dto in list)
            {
                var entity = dto.ToEntity();
                entity.TimeRecorded = now;
                _db.MARHistory.Add(entity);
            }

            var givenMedIds = list
                .Where(d => d.Action == MedicationAction.Given)
                .Select(d => d.MedId)
                .ToHashSet();

            if (givenMedIds.Count > 0)
            {
                var meds = await _db.Medications
                    .Where(m => givenMedIds.Contains(m.Id))
                    .ToListAsync();

                foreach (var med in meds)
                    med.LastGiven = now;
            }

            await _db.SaveChangesAsync();
            return Ok();
        }


        // GET api/mar/history
        [HttpGet("history")]
        public async Task<ActionResult<List<MARHistoryDto>>> GetHistory()
        {
            var cutoff = DateTime.UtcNow.AddYears(-1);

            var result = await _db.MARHistory
                .Where(x => x.TimeRecorded >= cutoff)
                .Select(x => x.ToDto())
                .ToListAsync();

            return Ok(result);
        }

        // GET: api/mar/history/bydate?date=2025-12-03[&petId=1]
        [HttpGet("history/bydate")]
        public async Task<ActionResult<List<MARHistoryDto>>> GetHistoryForDate(
            [FromQuery] string date,
            [FromQuery] int? petId)
        {
            if (!DateOnly.TryParse(date, out var day))
            {
                return BadRequest("Invalid date format. Use yyyy-MM-dd.");
            }

         
            var targetDate = day.ToDateTime(TimeOnly.MinValue).Date;

            var query = _db.MARHistory.AsQueryable();

      
            query = query.Where(x => x.TimeRecorded.Date == targetDate);

            if (petId.HasValue)
            {
                query = query.Where(x => x.PetId == petId.Value);
            }

            var result = await query
                .Select(x => x.ToDto())
                .ToListAsync();

            return Ok(result);
        }


    }
}
