using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Add this line
using System.Threading.Tasks;
using VideoGameDatabase.Data; // Make sure this matches your ApplicationDbContext namespace
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class PublishersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PublishersController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Publishers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishers()
    {
        return await _context.Publishers.ToListAsync();
    }

    // POST: api/Publishers
    [HttpPost]
    public async Task<ActionResult<Publisher>> PostPublisher(Publisher publisher)
    {
        _context.Publishers.Add(publisher);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPublisher", new { id = publisher.PublisherID }, publisher);
    }
    // GET: api/Publishers/5
[HttpGet("{id}")]
public async Task<ActionResult<Publisher>> GetPublisher(int id)
{
    var publisher = await _context.Publishers.FindAsync(id);
    if (publisher == null)
    {
        return NotFound();
    }
    return publisher;
}

// PUT: api/Publishers/5
[HttpPut("{id}")]
public async Task<IActionResult> PutPublisher(int id, Publisher publisher)
{
    if (id != publisher.PublisherID)
    {
        return BadRequest();
    }

    _context.Entry(publisher).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!PublisherExists(id))
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

// DELETE: api/Publishers/5
[HttpDelete("{id}")]
public async Task<IActionResult> DeletePublisher(int id)
{
    var publisher = await _context.Publishers.FindAsync(id);
    if (publisher == null)
    {
        return NotFound();
    }

    _context.Publishers.Remove(publisher);
    await _context.SaveChangesAsync();

    return NoContent();
}

private bool PublisherExists(int id)
{
    return _context.Publishers.Any(e => e.PublisherID == id);
}

}
