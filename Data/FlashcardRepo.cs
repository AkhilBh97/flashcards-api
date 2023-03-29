using FC.Logic;
using Microsoft.EntityFrameworkCore;

namespace FC.Data;

public class FlashcardRepo : IFlashcardRepo/* , IDisposable */
{
    private readonly FlashcardContext _context;

    public FlashcardRepo(FlashcardContext context){
        this._context = context;
    }

    public async Task Add(Flashcard entity)
    {
        entity.FlashcardID = null;
        await _context.Flashcards.AddAsync(entity);
        try {
            await Save();
        }
        catch (Exception e) {
            throw e;
        }
        
    }

    public async Task<Flashcard?> Get(Guid id)
    {
        return await _context.Flashcards.FindAsync(id);
    }

    public async Task<IEnumerable<Flashcard>> GetAll()
    {
        return await _context.Flashcards.ToListAsync();
    }

    public async Task Remove(Guid id)
    {
        //Attempt to find the flashcard, throw an Exception with "Not Found"
        var fc = await _context.Flashcards.FindAsync(id);
        if (fc is null) throw new Exception("Not Found");

        _context.Flashcards.Remove(fc);
        try {
            await Save();
        } 
        catch(Exception e){
            throw e;
        }
    }

    public async Task Update(Flashcard entity)
    {
        if (!_context.Flashcards.Any(e=> e.FlashcardID==entity.FlashcardID)) throw new Exception("Not Found");

        _context.Entry(entity).State = EntityState.Modified;
        try {
            await Save();
        }
        catch (Exception e){
            throw e;
        }
    }

    public async Task Save(){
        await _context.SaveChangesAsync();
    }

    /* public async Task Dispose(){
        await _context.DisposeAsync();
    } */
}