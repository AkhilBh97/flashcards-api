
using FC.Logic;
using Microsoft.EntityFrameworkCore;


namespace FC.Data;

public class FlashcardContext : DbContext
{
    public FlashcardContext(DbContextOptions<FlashcardContext> options) 
        : base(options)
    {}
    public DbSet<Flashcard> Flashcards { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flashcard>(entity => {
            entity.ToTable("Flashcard", "FC");
        });
    }
}