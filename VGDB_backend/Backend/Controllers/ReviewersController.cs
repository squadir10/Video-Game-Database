using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGameDatabase.Data;
using VideoGameDatabase.Models;

namespace VideoGameDatabase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Reviewers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reviewer>>> GetReviewers()
        {
            return await _context.Reviewers.ToListAsync();
        }

        // POST: api/Reviewers
        [HttpPost]
        public async Task<ActionResult<Reviewer>> PostReviewer(Reviewer reviewer)
        {
            _context.Reviewers.Add(reviewer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReviewer", new { id = reviewer.ReviewerID }, reviewer);
        }

        // GET: api/Reviewers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reviewer>> GetReviewer(int id)
        {
            var reviewer = await _context.Reviewers.FindAsync(id);

            if (reviewer == null)
            {
                return NotFound();
            }

            return reviewer;
        }

        // PUT: api/Reviewers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReviewer(int id, Reviewer reviewerUpdateRequest)
        {
            if (id != reviewerUpdateRequest.ReviewerID)
            {
                return BadRequest("Reviewer ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reviewer = await _context.Reviewers.FirstOrDefaultAsync(r => r.ReviewerID == id);

            if (reviewer == null)
            {
                return NotFound();
            }

            _context.Entry(reviewer).CurrentValues.SetValues(reviewerUpdateRequest);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(reviewer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE: api/Reviewers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReviewer(int id)
        {
            var reviewer = await _context.Reviewers.FindAsync(id);
            if (reviewer == null)
            {
                return NotFound();
            }

            _context.Reviewers.Remove(reviewer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReviewerExists(int id)
        {
            return _context.Reviewers.Any(e => e.ReviewerID == id);
        }
    }
}
