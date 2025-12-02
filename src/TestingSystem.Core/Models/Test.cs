namespace TestingSystem.Core.Models;

public class Test
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int AuthorId { get; set; }
    public User? Author { get; set; } 
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public TimeSpan? TimeLimit { get; set; }
    public bool IsActive { get; set; } = false;
    public bool QuestionsOrderRandom { get; set; } = false;
    public bool AnswerOptionsRandom { get; set; } = false;
    public List<Question> Questions { get; set; } = new();
}