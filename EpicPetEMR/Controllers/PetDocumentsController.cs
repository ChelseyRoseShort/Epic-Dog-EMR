using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using EpicPetEMR.Api.Data;
using EpicPetEMR.Api.Models;
using EpicPetEMR.Shared.Models;

namespace EpicPetEMR.Api.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class PetsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public PetsController(AppDbContext db)
        {
            _db = db;
        }

       
        public sealed class UploadPetDocumentRequest
        {
            public IFormFile File { get; set; } = null!;

            public string Name { get; set; } = string.Empty;

            public DocumentType Type { get; set; }

            public int? VetTripId { get; set; }
        }

        [HttpPost("{petId:int}/documents")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<PetDocumentDto>> UploadDocument(
            int petId,
            [FromForm] UploadPetDocumentRequest request)
        {
            // Basic guard checks
            if (request.File == null || request.File.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var pet = await _db.Pets.FirstOrDefaultAsync(p => p.Id == petId);
            if (pet == null)
            {
                return NotFound($"Pet {petId} not found.");
            }

          
            byte[] bytes;
            await using (var ms = new MemoryStream())
            {
                await request.File.CopyToAsync(ms);
                bytes = ms.ToArray();
            }

            var doc = new PetDocument
            {
                PetId       = petId,
                VetTripId   = request.VetTripId,
                Type        = request.Type,                         // enum: 0 = VetTrip, 1 = Assessment
                Name        = request.Name,
                FileName    = request.File.FileName,
                ContentType = request.File.ContentType ?? "application/octet-stream",
                Data        = bytes,
                UploadedAt  = DateTime.UtcNow
            };

            _db.PetDocuments.Add(doc);
            await _db.SaveChangesAsync();

            // Map to shared DTO
            var dto = new PetDocumentDto
            {
                Id          = doc.Id,
                PetId       = doc.PetId,
                VetTripId   = doc.VetTripId,
                Type        = doc.Type,
                Name        = doc.Name,
                FileName    = doc.FileName,
                ContentType = doc.ContentType,
               
                Data        = Array.Empty<byte>(),
                UploadedAt  = doc.UploadedAt
            };

            return Ok(dto);
        }

[HttpGet("{id}/preview")]
public async Task<IActionResult> Preview(int id)
{
    var doc = await _db.PetDocuments.FindAsync(id);
    if (doc == null)
        return NotFound();

  
    return File(doc.Data, doc.ContentType ?? "application/octet-stream");
}
        [HttpGet("{Id}/download")]
        public async Task<ActionResult> DownloadPetDocument(int Id)
        {
            var doc = await _db.PetDocuments.FindAsync(Id);
            if (doc == null)
                return NotFound();

            return File(doc.Data, doc.ContentType, doc.FileName);
        }

        [HttpGet("{petId:int}")]
        public async Task<ActionResult<List<PetDocumentDto>>> GetDocumentsForPet(int petId)
        {
            var petExists = await _db.Pets.AnyAsync(p => p.Id == petId);
            if (!petExists)
                return NotFound($"Pet {petId} not found.");

            var docs = await _db.PetDocuments
                .Where(d => d.PetId == petId)
                .OrderByDescending(d => d.UploadedAt)
                .ToListAsync();

            var dtos = docs.Select(d => new PetDocumentDto
            {
                Id          = d.Id,
                PetId       = d.PetId,
                VetTripId   = d.VetTripId,
                Type        = d.Type,
                Name        = d.Name,
                FileName    = d.FileName,
                ContentType = d.ContentType,
                Data        = Array.Empty<byte>(),  
                UploadedAt  = d.UploadedAt
            }).OrderBy(d => d.UploadedAt).ToList();

            return Ok(dtos);
        }
    }
}
