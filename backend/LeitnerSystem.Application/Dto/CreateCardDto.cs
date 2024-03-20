using System.ComponentModel.DataAnnotations;

namespace LeitnerSystem.Application.Dto;

public class CreateCardDto
{
    [Required]
    public string Question { get; set; }
    [Required]
    public string Answer { get; set; }
    public string Tag { get; set; }
}