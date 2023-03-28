
using FC.Data;
using FC.Logic;
using Microsoft.AspNetCore.Mvc;

namespace FC.API.Controlllers;

[ApiController]
[Route("Flashcards")]
public class FlashcardController : ControllerBase
{
    private readonly IFlashcardRepo _repo;

    public FlashcardController(FlashcardRepo repo){
        this._repo = repo;
    }

    //Read
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Flashcard>>> Get(){
        return Ok(await _repo.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Flashcard>> Get(Guid id){
        var getfc = await _repo.Get(id);
        return (getfc is null ? NotFound() : Ok(getfc));
    }

    //Create
    [HttpPost]
    public async Task<ActionResult> CreateFlashcard(Flashcard fc){
        /* fc.FlashcardID = null;
        await _context.Flashcards.AddAsync(fc); */
        try {
            /* await _context.SaveChangesAsync(); */
            await _repo.Add(fc);
        }
        catch(Exception e){
            return Conflict(e);
        }

        return CreatedAtAction("Get", new {FlashcardID = fc.FlashcardID}, fc);
    }

    //Update
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateFlashcard(Flashcard fc){
        /* //the Flashcard was not found
        if (!_context.Flashcards.Any(e => e.FlashcardID==fc.FlashcardID)) return NotFound();


        //
        _context.Entry(fc).State = EntityState.Modified; */
        try {
            /* await _context.SaveChangesAsync(); */
            await _repo.Update(fc);
        }
        catch (Exception e) {
            if (e.Message=="Not Found") return NotFound();
            return Conflict(e);
        }

        return NoContent();
    }

    //Delete
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteFlashcard(Guid id){
        /* //Attempt to find the flashcard, return 404 if not found
        var fc = await _context.Flashcards.FindAsync(id);
        if (fc is null) return NotFound();

        //Attempt to remove the flashcard and save changes
        _context.Flashcards.Remove(fc); */
        try {
            await _repo.Remove(id);
        }
        catch(Exception e){
            if (e.Message=="Not Found") return NotFound();
            return Conflict(e);
        }

        //Removed, return a 204
        return NoContent();
    }
}