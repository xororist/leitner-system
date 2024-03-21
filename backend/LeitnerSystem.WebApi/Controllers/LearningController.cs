using LeitnerSystem.Application.Dto;
using LeitnerSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace LeitnerSystem.WebApi.Controllers;

[ApiController]
[Route("cards")]
public class LearningController : ControllerBase
{
    private readonly ICardsApplicationService _cardsService;

    public LearningController(ICardsApplicationService cardsApplicationService)
    {
        _cardsService = cardsApplicationService;
    }

    [HttpGet]
    [Route("quizz")]
    public async Task<IActionResult> GetCardsForTodayReview([FromQuery] string? date = null)
    {
        if (!string.IsNullOrEmpty(date))
        {
            if (DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime tempDate))
            {
                var cards = await _cardsService.GetAllCardsAsync();
                var filteredCards = cards.Where(
                    d => DateOnly.FromDateTime(DateTime.Parse(d.NextReviewDate)) == DateOnly.FromDateTime(tempDate.Date));
                return Ok(filteredCards);
            }
            else
            {
                return BadRequest("Provide a date in format: yyyy-MM-dd.");
            }
        }

        var cardsForReviewToday = await _cardsService.GetCardsForTodayReviewAsync();
        return Ok(cardsForReviewToday);
    }

    [HttpPatch]
    [Route("{cardId}/answer")]
    public async Task SetCardAnswer(Guid cardId, [FromBody] AnswerUpdateRequestDto request)
    {
        await _cardsService.SetCardAnswerTrueOrFalse(cardId, request.IsValid);
    }
}