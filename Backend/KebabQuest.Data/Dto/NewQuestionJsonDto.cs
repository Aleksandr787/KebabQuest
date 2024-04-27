using KebabQuest.Data.Models;

namespace KebabQuest.Data.Dto;

public class NewQuestionJsonDto
{
    public string? Question { get; set; }
    public Options? Options { get; set; }
}