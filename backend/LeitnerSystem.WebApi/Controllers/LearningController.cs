using LeitnerSystem.Application.Dto;
using LeitnerSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> GetCardsForTodayReview()
    {
        var cards = await _cardsService.GetCardsForTodayReviewAsync();
        return Ok(cards);
    }

    [HttpPatch]
    [Route("{cardId}/answer")]
    public async Task SetCardAnswer(Guid cardId, [FromBody] AnswerUpdateRequestDto request)
    {
        await _cardsService.SetCardAnswerTrueOrFalse(cardId, request.IsValid);
    }
}