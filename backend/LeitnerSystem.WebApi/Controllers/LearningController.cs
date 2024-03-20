using LeitnerSystem.Application.Interfaces;
using LeitnerSystem.Domain.Interfaces;
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
    
    [HttpGet]
    [Route("{cardId}/answer")]
    public async Task<IActionResult> SetCardAnswerToTrue()
    {
        var cards = await _cardsService.GetCardsForTodayReviewAsync();
        return Ok(cards);
    }
}