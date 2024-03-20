namespace LeitnerSystem.Application.Dto;

public class CardDto
{
    public Guid Id { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
    public string Tag { get; set; }
    public string Category { get; set; }
    public bool IsCompleted { get; set; }
}
