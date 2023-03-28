using FC.Logic;

namespace FC.Data;

public interface IFlashcardRepo/*  : IDisposable */
{
    Task<IEnumerable<Flashcard>> GetAll();
    Task<Flashcard?> Get(Guid id);

    Task Add(Flashcard entity);

    Task Update(Flashcard entity);

    Task Remove(Guid id);
    
    Task Save();
}