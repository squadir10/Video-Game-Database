/* 
Name: Sabrina Quadir 
Description: 
The PublishersController.cs file is a controller in an ASP.NET Core web application that handles HTTP requests related to game publishers. 
It defines endpoints for performing CRUD operations on the Publisher entity. 

This controller enables users to view and manage publisher-related data such as:
-names 
-headquarters 
-locations
-founding dates. 

Typical actions include:
-publisher details
-adding new publishers
-updating existing publisher information
-deleting publishers from the database.
 */

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGameDatabase.Data;
using VideoGameDatabase.Models;

namespace VideoGameDatabase.Controllers
{
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
        public async Task<IActionResult> PutPublisher(int id, Publisher publisherUpdateRequest)
        {
            if (id != publisherUpdateRequest.PublisherID)
            {
                return BadRequest("Publisher ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.PublisherID == id);

            if (publisher == null)
            {
                return NotFound();
            }

            _context.Entry(publisher).CurrentValues.SetValues(publisherUpdateRequest);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(publisher);
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
}
