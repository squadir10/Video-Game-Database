/* 
Name: Sabrina Quadir 
Description: 
The GameReviewsController.cs file is a controller in an ASP.NET Core web application 
that handles HTTP requests related to game reviews. It defines endpoints for performing 
CRUD operations on game reviews. 

The controller interacts with the GameReview entity in the database, enabling 
users to manage reviews associated with different games. 


 */



using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGameDatabase.Data;
using VideoGameDatabase.Models;

namespace VideoGameDatabase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GameReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/GameReviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameReview>>> GetGameReviews()
        {
            return await _context.GameReviews
                .Include(r => r.Reviewer)
                .ToListAsync();
        }

        // POST: api/GameReviews
        [HttpPost]
        public async Task<ActionResult<GameReview>> PostGameReview(GameReview gameReview)
        {
            _context.GameReviews.Add(gameReview);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGameReview", new { id = gameReview.GameReviewID }, gameReview);
        }

        // GET: api/GameReviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameReview>> GetGameReview(int id)
        {
            var gameReview = await _context.GameReviews
                .Include(r => r.Reviewer)
                .FirstOrDefaultAsync(r => r.GameReviewID == id);

            if (gameReview == null)
            {
                return NotFound();
            }

            return gameReview;
        }

        // PUT: api/GameReviews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGameReview(int id, GameReview gameReviewUpdateRequest)
        {
            if (id != gameReviewUpdateRequest.GameReviewID)
            {
                return BadRequest("GameReview ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gameReview = await _context.GameReviews
                                           .Include(r => r.Reviewer)
                                           .FirstOrDefaultAsync(r => r.GameReviewID == id);

            if (gameReview == null)
            {
                return NotFound();
            }

            _context.Entry(gameReview).CurrentValues.SetValues(gameReviewUpdateRequest);

            if (gameReviewUpdateRequest.Reviewer != null)
            {
                if (gameReviewUpdateRequest.Reviewer.ReviewerID != 0)
                {
                    var reviewer = await _context.Reviewers.FirstOrDefaultAsync(r => r.ReviewerID == gameReviewUpdateRequest.Reviewer.ReviewerID);
                    if (reviewer != null)
                    {
                        _context.Entry(reviewer).CurrentValues.SetValues(gameReviewUpdateRequest.Reviewer);
                    }
                }
                else
                {
                    _context.Reviewers.Add(gameReviewUpdateRequest.Reviewer);
                    gameReview.Reviewer = gameReviewUpdateRequest.Reviewer;
                }
            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok(gameReview);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameReviewExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpGet("Scores")]
        public IActionResult GetScores()
        {
            var scores = _context.GameReviews
                .GroupBy(gr => gr.GameID)
                .Select(g => new
                {
                    GameID = g.Key,
                    Score = g.Average(gr => gr.Score) // or any other logic to calculate the score
                })
                .ToList();

            return Ok(scores);
        }


        // DELETE: api/GameReviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameReview(int id)
        {
            var gameReview = await _context.GameReviews
                                           .Include(r => r.Reviewer)
                                           .SingleOrDefaultAsync(r => r.GameReviewID == id);

            if (gameReview == null)
            {
                return NotFound();
            }

            _context.GameReviews.Remove(gameReview);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameReviewExists(int id)
        {
            return _context.GameReviews.Any(e => e.GameReviewID == id);
        }
    }
}
