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
    public class GamesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            return await _context.Games
                .Include(g => g.Developer)
                .Include(g => g.Publisher)
                .Include(g => g.GameReviews)
                .ThenInclude(gr => gr.Reviewer)
                .ToListAsync();
        }

        // POST: api/Games
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame", new { id = game.GameID }, game);
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = await _context.Games
                .Include(g => g.Developer)
                .Include(g => g.Publisher)
                .Include(g => g.GameReviews)
                .ThenInclude(gr => gr.Reviewer)
                .FirstOrDefaultAsync(g => g.GameID == id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        // PUT: api/Games/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, Game gameUpdateRequest)
        {
            if (id != gameUpdateRequest.GameID)
            {
                return BadRequest("Game ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var game = await _context.Games
                                     .Include(g => g.Developer)
                                     .Include(g => g.Publisher)
                                     .FirstOrDefaultAsync(g => g.GameID == id);

            if (game == null)
            {
                return NotFound();
            }

            _context.Entry(game).CurrentValues.SetValues(gameUpdateRequest);

            if (gameUpdateRequest.Developer != null)
            {
                if (gameUpdateRequest.Developer.DeveloperID != 0)
                {
                    var developer = await _context.Developers.FirstOrDefaultAsync(d => d.DeveloperID == gameUpdateRequest.Developer.DeveloperID);
                    if (developer != null)
                    {
                        _context.Entry(developer).CurrentValues.SetValues(gameUpdateRequest.Developer);
                    }
                }
                else
                {
                    _context.Developers.Add(gameUpdateRequest.Developer);
                    game.Developer = gameUpdateRequest.Developer;
                }
            }

            if (gameUpdateRequest.Publisher != null)
            {
                if (gameUpdateRequest.Publisher.PublisherID != 0)
                {
                    var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.PublisherID == gameUpdateRequest.Publisher.PublisherID);
                    if (publisher != null)
                    {
                        _context.Entry(publisher).CurrentValues.SetValues(gameUpdateRequest.Publisher);
                    }
                }
                else
                {
                    _context.Publishers.Add(gameUpdateRequest.Publisher);
                    game.Publisher = gameUpdateRequest.Publisher;
                }
            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok(game);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _context.Games
                                     .Include(g => g.GameReviews)
                                     .ThenInclude(gr => gr.Reviewer)
                                     .SingleOrDefaultAsync(g => g.GameID == id);
            
            if (game == null)
            {
                return NotFound();
            }

            foreach (var review in game.GameReviews.ToList())
            {
                _context.GameReviews.Remove(review);
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.GameID == id);
        }
    }
}
