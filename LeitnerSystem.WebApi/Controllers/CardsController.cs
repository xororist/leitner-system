using LeitnerSystem.Application.Dto;
using Microsoft.AspNetCore.Mvc;
using LeitnerSystem.Application.Interfaces;

namespace LeitnerSystem.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CardsController : ControllerBase
{
    private readonly ICardsApplicationService _cardsService;

    public CardsController(ICardsApplicationService cardsService)
    {
        _cardsService = cardsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCardsForTodayReview()
    {
        var cards = await _cardsService.GetCardsForTodayReviewAsync();
        return Ok(cards);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCard(CreateCardDto createCardDto)
    {
        var cardId = await _cardsService.CreateCardAsync(createCardDto);
        return Ok(cardId);
    }
}