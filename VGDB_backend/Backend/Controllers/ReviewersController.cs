using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VideoGameDatabase.Data; 
using System.Linq;


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

[HttpPut("{id}")]
public async Task<IActionResult> PutReviewer(int id, Reviewer reviewer)
{
    if (id != reviewer.ReviewerID)
    {
        return BadRequest();
    }

    _context.Entry(reviewer).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
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

    return NoContent();
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
