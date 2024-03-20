using System.ComponentModel.DataAnnotations;

namespace LeitnerSystem.Application.Dto;

public class UpdateCardDto
{
    public Guid CardId { get; set; }
    [Required]
    public string Question { get; set; }
    [Required]
    public string Answer { get; set; }
    public string Tag { get; set; }
}
