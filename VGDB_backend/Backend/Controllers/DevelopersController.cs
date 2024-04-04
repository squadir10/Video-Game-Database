using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Add this line
using System.Threading.Tasks;
using VideoGameDatabase.Data; // Make sure this matches your ApplicationDbContext namespace
using System.Linq; // Add this line if it's not already there


[ApiController]
[Route("api/[controller]")]
public class DevelopersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DevelopersController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Developers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Developer>>> GetDevelopers()
    {
        return await _context.Developers.ToListAsync();
    }

    // PUT: api/Developers/5
// To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
[HttpPut("{id}")]
public async Task<IActionResult> PutDeveloper(int id, Developer developer)
{
    if (id != developer.DeveloperID)
    {
        return BadRequest();
    }

    _context.Entry(developer).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!DeveloperExists(id))
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

// DELETE: api/Developers/5
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteDeveloper(int id)
{
    var developer = await _context.Developers.FindAsync(id);
    if (developer == null)
    {
        return NotFound();
    }

    _context.Developers.Remove(developer);
    await _context.SaveChangesAsync();

    return NoContent();
}

private bool DeveloperExists(int id)
{
    return _context.Developers.Any(e => e.DeveloperID == id);
}

// POST: api/Developers
[HttpPost]
public async Task<ActionResult<Developer>> PostDeveloper(Developer developer)
{
    _context.Developers.Add(developer);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetDeveloper), new { id = developer.DeveloperID }, developer);
}

// GET: api/Developers/5
[HttpGet("{id}")]
public async Task<ActionResult<Developer>> GetDeveloper(int id)
{
    var developer = await _context.Developers.FindAsync(id);
    if (developer == null)
    {
        return NotFound();
    }
    return developer;
}


}



