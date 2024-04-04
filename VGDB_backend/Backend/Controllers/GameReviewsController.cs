using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Add this line
using System.Threading.Tasks;
using VideoGameDatabase.Data; // Make sure this matches your ApplicationDbContext namespace
using System.Linq; // Add this line if it's not already there

// ... rest of your controller code ...


[ApiController]
[Route("api/[controller]")]
public class GameReviewsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public GameReviewsController(ApplicationDbContext context)
    {
        _context = context;
    }

   [HttpGet]
public async Task<ActionResult<IEnumerable<Object>>> GetGameReviews()
{
    var gameReviewsList = await _context.GameReviews
        .Include(gr => gr.Game)
            .ThenInclude(game => game.Developer)
        .Include(gr => gr.Game)
            .ThenInclude(game => game.Publisher)
        .Include(gr => gr.Reviewer) // This ensures the Reviewer data is included in the query
        .ToListAsync();

    var gameReviews = gameReviewsList.Select(gr => new
    {
        gr.GameReviewID,
        Game = new
        {
            gr.Game.GameID,
            gr.Game.Title,
            gr.Game.ReleaseDate,
            gr.Game.Genre,
            gr.Game.Platform,
            DeveloperName = gr.Game.Developer?.Name ?? "Unknown Developer",
            PublisherName = gr.Game.Publisher?.Name ?? "Unknown Publisher"
        },
        Reviewer = new // Include this to shape the reviewer data
        {
            gr.Reviewer.ReviewerID,
            gr.Reviewer.Name,
            gr.Reviewer.Affiliation
        },
        gr.Score,
        gr.ReviewText,
        gr.ReviewDate
    }).ToList();

    return gameReviews;
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
public async Task<ActionResult<Object>> GetGameReview(int id)
{
   var gameReviewQuery = _context.GameReviews
        .Where(gr => gr.GameReviewID == id)
        .Include(gr => gr.Game)
            .ThenInclude(game => game.Developer)
        .Include(gr => gr.Game)
            .ThenInclude(game => game.Publisher)
        .Include(gr => gr.Reviewer);

    // Materialize query to handle null-conditional checks in-memory
    var gameReviewList = await gameReviewQuery.ToListAsync();

    var gameReview = gameReviewList.Select(gr => new
    {
        gr.GameReviewID,
        Game = gr.Game == null ? null : new // Null check for the Game property
        {
            GameID = gr.Game.GameID,
            Title = gr.Game.Title,
            ReleaseDate = gr.Game.ReleaseDate,
            Genre = gr.Game.Genre,
            Platform = gr.Game.Platform,
            DeveloperName = gr.Game.Developer != null ? gr.Game.Developer.Name : "Unknown Developer",
            PublisherName = gr.Game.Publisher != null ? gr.Game.Publisher.Name : "Unknown Publisher"
        },
         Reviewer = new // Include this to shape the reviewer data
        {
            gr.Reviewer.ReviewerID,
            gr.Reviewer.Name,
            gr.Reviewer.Affiliation
        },
        gr.Score,
        gr.ReviewText,
        gr.ReviewDate
    }).FirstOrDefault();

    if (gameReview == null)
    {
        return NotFound();
    }

    return Ok(gameReview);
}

// GET: api/GameReviews/Scores
[HttpGet("Scores")]
public async Task<ActionResult<IEnumerable<Object>>> GetGameScores()
{
    var gameScores = await _context.GameReviews
        .Select(gr => new { gr.Game.GameID, gr.Score })
        .ToListAsync();

    return gameScores;
}


[HttpPut("{id}")]
public async Task<IActionResult> PutGameReview(int id, GameReview gameReview)
{
    if (gameReview == null)
    {
        return BadRequest("GameReview object is null.");
    }
    
    if (id != gameReview.GameReviewID) // Assuming GameReviewID is not nullable.
    {
        return BadRequest();
    }

    _context.Entry(gameReview).State = EntityState.Modified;

    try
    {
        await _context.SaveChangesAsync();
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

    return NoContent();
}

    

// DELETE: api/GameReviews/5
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteGameReview(int id)
{
    var gameReview = await _context.GameReviews.FindAsync(id);
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
