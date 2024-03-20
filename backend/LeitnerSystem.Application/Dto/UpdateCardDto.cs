namespace LeitnerSystem.Application.Dto;

public class UpdateCardDto
{
    public Guid CardId { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
    public string Tag { get; set; }
}
