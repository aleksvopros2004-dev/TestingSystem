using System.ComponentModel.DataAnnotations.Schema;

namespace TestingSystem.Core.Models;

public class Question
{
    public int Id { get; set; }

    [Column("test_id")]
    public int TestId { get; set; }

    [Column("question_text")]
    public string QuestionText { get; set; } = string.Empty;

    [Column("question_type")]
    public string QuestionType { get; set; } = string.Empty;

    [Column("order_index")]
    public int OrderIndex { get; set; }

    [Column("image_data")]
    public byte[]? ImageData { get; set; }

    [Column("image_content_type")]
    public string? ImageContentType { get; set; }

    public List<AnswerOption> AnswerOptions { get; set; } = new();
}