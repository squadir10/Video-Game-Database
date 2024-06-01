/* 
Name: Sabrina Quadir 
Description: 
The GamesController.cs file is a controller in an ASP.NET Core web application 
that handles HTTP requests related to video games. A lot of the heavy lifting is done here.

This controller defines endpoints for performing CRUD operations on the Game entity. 
This controller allows users to manage game-related data such as

-titles
-release dates
-genres 
-platforms
-relationships with developers, publishers, and reviews. 

Typical actions include:
-viewing game details
-adding new games
-editing existing game entries
-deleting games from the database.
 */

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            if (game.Developer.DeveloperID == 0)
            {
                _context.Developers.Add(game.Developer);
            }
            else
            {
                _context.Entry(game.Developer).State = EntityState.Unchanged;
            }

            if (game.Publisher.PublisherID == 0)
            {
                _context.Publishers.Add(game.Publisher);
            }
            else
            {
                _context.Entry(game.Publisher).State = EntityState.Unchanged;
            }

            foreach (var review in game.GameReviews)
            {
                if (review.Reviewer.ReviewerID == 0)
                {
                    _context.Reviewers.Add(review.Reviewer);
                }
                else
                {
                    _context.Entry(review.Reviewer).State = EntityState.Unchanged;
                }
            }

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
                                     .Include(g => g.GameReviews)
                                     .ThenInclude(gr => gr.Reviewer)
                                     .FirstOrDefaultAsync(g => g.GameID == id);

            if (game == null)
            {
                return NotFound();
            }

            // Update game details
            _context.Entry(game).CurrentValues.SetValues(gameUpdateRequest);

            // Update developer
            if (gameUpdateRequest.Developer != null)
            {
                if (gameUpdateRequest.Developer.DeveloperID != 0)
                {
                    _context.Entry(game.Developer).CurrentValues.SetValues(gameUpdateRequest.Developer);
                }
                else
                {
                    game.Developer = gameUpdateRequest.Developer;
                    _context.Developers.Add(gameUpdateRequest.Developer);
                }
            }

            // Update publisher
            if (gameUpdateRequest.Publisher != null)
            {
                if (gameUpdateRequest.Publisher.PublisherID != 0)
                {
                    _context.Entry(game.Publisher).CurrentValues.SetValues(gameUpdateRequest.Publisher);
                }
                else
                {
                    game.Publisher = gameUpdateRequest.Publisher;
                    _context.Publishers.Add(gameUpdateRequest.Publisher);
                }
            }

            // Update reviews
            foreach (var review in game.GameReviews.ToList())
            {
                _context.GameReviews.Remove(review);
            }
            game.GameReviews.Clear();

            foreach (var reviewUpdate in gameUpdateRequest.GameReviews)
            {
                var reviewer = await _context.Reviewers.FindAsync(reviewUpdate.Reviewer.ReviewerID);
                if (reviewer == null)
                {
                    reviewer = new Reviewer
                    {
                        Name = reviewUpdate.Reviewer.Name,
                        Affiliation = reviewUpdate.Reviewer.Affiliation,
                        ExperienceYears = reviewUpdate.Reviewer.ExperienceYears
                    };
                    _context.Reviewers.Add(reviewer);
                }
                else
                {
                    reviewer.Name = reviewUpdate.Reviewer.Name;
                    reviewer.Affiliation = reviewUpdate.Reviewer.Affiliation;
                    reviewer.ExperienceYears = reviewUpdate.Reviewer.ExperienceYears;
                    _context.Entry(reviewer).State = EntityState.Modified;
                }

                var newReview = new GameReview
                {
                    GameID = game.GameID,
                    ReviewerID = reviewer.ReviewerID,
                    Score = reviewUpdate.Score,
                    ReviewText = reviewUpdate.ReviewText,
                    ReviewDate = reviewUpdate.ReviewDate,
                    Reviewer = reviewer
                };
                game.GameReviews.Add(newReview);
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
