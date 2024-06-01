/* 
Name: Sabrina Quadir 
Description: 
The Developers.cs file is a C# class definition that is part of an 
Entity Framework Core data model in a .NET application. 
This file represents a video game developer in the application's database.

DeveloperID: A unique identifier for the developer.
Name: The name of the developer.
Location: The location or headquarters of the developer.
FoundingDate: The date when the developer was founded.
These properties are mapped to columns in a database table, allowing the application to perform CRUD

The Developers.cs file facilitates the application to interact with the database and manage developer-related information efficiently.

 */


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGameDatabase.Data;
using VideoGameDatabase.Models;

namespace VideoGameDatabase.Controllers
{
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

        // POST: api/Developers
        [HttpPost]
        public async Task<ActionResult<Developer>> PostDeveloper(Developer developer)
        {
            _context.Developers.Add(developer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeveloper", new { id = developer.DeveloperID }, developer);
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

        // PUT: api/Developers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeveloper(int id, Developer developerUpdateRequest)
        {
            if (id != developerUpdateRequest.DeveloperID)
            {
                return BadRequest("Developer ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var developer = await _context.Developers.FirstOrDefaultAsync(d => d.DeveloperID == id);

            if (developer == null)
            {
                return NotFound();
            }

            _context.Entry(developer).CurrentValues.SetValues(developerUpdateRequest);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(developer);
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
    }
}
