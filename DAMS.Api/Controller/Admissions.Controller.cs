using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAMS.Domain.Entities;
using DAMS.Infrastructure.Persistence;
using DAMS.Application;

namespace DAMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdmissionsController : ControllerBase
    {
        private readonly DamsDbContext _context;

        public AdmissionsController(DamsDbContext context)
        {
            _context = context;
        }

        // GET: api/admissions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdmissionDto>>> GetAdmissions()
        {
            var admissions = await _context.Admissions
                .Include(a => a.Documents)
                .ToListAsync();

            return Ok(admissions.Select(a => new AdmissionDto
            {
                Id = a.Id,
                CandidateName = a.CandidateName,
                Status = a.Status.ToString(),
                CreatedAt = a.CreatedAt,
                CreatedByUserId = a.CreatedByUserId,
                DocumentCount = a.Documents.Count
            }));
        }

        // GET: api/admissions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdmissionDto>> GetAdmission(Guid id)
        {
            var admission = await _context.Admissions
                .Include(a => a.Documents)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (admission == null)
            {
                return NotFound();
            }

            return Ok(new AdmissionDto
            {
                Id = admission.Id,
                CandidateName = admission.CandidateName,
                Status = admission.Status.ToString(),
                CreatedAt = admission.CreatedAt,
                CreatedByUserId = admission.CreatedByUserId,
                DocumentCount = admission.Documents.Count
            });
        }
        // POST: api/admissions
        [HttpPost]
        public async Task<ActionResult<AdmissionDto>> PostAdmission(CreateAdmissionDto dto)
        {
            var admission = new Admission(dto.CandidateName, dto.CreatedByUserId);

            _context.Admissions.Add(admission);
            await _context.SaveChangesAsync();

            var result = new AdmissionDto
            {
                Id = admission.Id,
                CandidateName = admission.CandidateName,
                Status = admission.Status.ToString(),
                CreatedAt = admission.CreatedAt,
                CreatedByUserId = admission.CreatedByUserId,
                DocumentCount = 0
            };

            return CreatedAtAction(nameof(GetAdmission), new { id = admission.Id }, result);
        }

        // DELETE: api/admissions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmission(Guid id)
        {
            var admission = await _context.Admissions.FindAsync(id);
            if (admission == null)
            {
                return NotFound();
            }

            _context.Admissions.Remove(admission);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

    // DTOs
    public record CreateAdmissionDto(string CandidateName, Guid CreatedByUserId);

    public class AdmissionDto
    {
        public Guid Id { get; set; }
        public string CandidateName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public Guid CreatedByUserId { get; set; }
        public int DocumentCount { get; set; }
    }
}
