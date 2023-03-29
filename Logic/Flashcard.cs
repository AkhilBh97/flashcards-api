using System.ComponentModel.DataAnnotations;

namespace FC.Logic;

public class Flashcard
{
    [Key]
    public Guid? FlashcardID { get; set; }
    public string? Question { get; set; }
    public string? Answer { get; set; }
}