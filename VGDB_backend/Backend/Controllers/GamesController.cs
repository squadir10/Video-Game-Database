using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Add this line
using System.Threading.Tasks;
using VideoGameDatabase.Data; // Make sure this matches your ApplicationDbContext namespace
using System.Linq; // Add this line if it's not already there

// ... rest of your controller code ...


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

    // Update the properties of game
    _context.Entry(game).CurrentValues.SetValues(gameUpdateRequest);

    // Handle Developer update or addition
    if (gameUpdateRequest.Developer != null)
    {
        if (gameUpdateRequest.Developer.DeveloperID.HasValue)
        {
            // Update existing Developer
            var developer = await _context.Developers.FirstOrDefaultAsync(d => d.DeveloperID == gameUpdateRequest.Developer.DeveloperID.Value);
            if (developer != null)
            {
                _context.Entry(developer).CurrentValues.SetValues(gameUpdateRequest.Developer);
            }
        }
        else
        {
            // Add new Developer
            _context.Developers.Add(gameUpdateRequest.Developer);
            // Assign the new Developer to the game
            game.Developer = gameUpdateRequest.Developer;
        }
    }

    // Handle Publisher update or addition
    if (gameUpdateRequest.Publisher != null)
    {
        if (gameUpdateRequest.Publisher.PublisherID.HasValue)
        {
            // Update existing Publisher
            var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.PublisherID == gameUpdateRequest.Publisher.PublisherID.Value);
            if (publisher != null)
            {
                _context.Entry(publisher).CurrentValues.SetValues(gameUpdateRequest.Publisher);
            }
        }
        else
        {
            // Add new Publisher
            _context.Publishers.Add(gameUpdateRequest.Publisher);
            // Assign the new Publisher to the game
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




[HttpDelete("{id}")]
public async Task<IActionResult> DeleteGame(int id)
{
    var game = await _context.Games
                             .Include(g => g.GameReviews) // Include the related reviews
                             .SingleOrDefaultAsync(g => g.GameID == id);
    
    if (game == null)
    {
        return NotFound();
    }

    // Remove the related reviews
    foreach (var review in game.GameReviews.ToList())
    {
        _context.GameReviews.Remove(review);
    }

    // Now it's safe to remove the game
    _context.Games.Remove(game);
    await _context.SaveChangesAsync();

    return NoContent();
}


private bool GameExists(int id)
{
    return _context.Games.Any(e => e.GameID == id);
}

}
