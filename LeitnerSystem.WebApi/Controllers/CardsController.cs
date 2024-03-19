using LeitnerSystem.Application.Dto;
using Microsoft.AspNetCore.Mvc;
using LeitnerSystem.Application.Interfaces;

namespace LeitnerSystem.WebApi.Controllers;

[ApiController]
[Route("cards")]
public class CardsController : ControllerBase
{
    private readonly ICardsApplicationService _cardsService;

    public CardsController(ICardsApplicationService cardsService)
    {
        _cardsService = cardsService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCards()
    {
        var cards = await _cardsService.GetAllCardsAsync();
        return Ok(cards);
    }
    
    [HttpGet]
    [Route("tag")]
    public async Task<IActionResult> GetCardsByTag(string tag)
    {
        var cards = await _cardsService.FindCardsByTagAsync(tag);
        return Ok(cards);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCard(CreateCardDto createCardDto)
    {
        var cardId = await _cardsService.CreateCardAsync(createCardDto);
        return Ok(cardId);
    }
    
    [HttpPost]
    [Route("answer/{cardId}")]
    public async Task<IActionResult> PromoteCard(Guid cardId, string answer)
    {
         await _cardsService.PromoteCardAsync(cardId, answer);
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateCard(UpdateCardDto updateCardDto)
    {
        await _cardsService.UpdateCardAsync(updateCardDto);
        return Ok();
    }
    
    [HttpDelete]
    [Route("delete/{cardId}")]
    public async Task<IActionResult> DeleteCard(Guid cardId)
    {
        await _cardsService.DeleteCardAsync(cardId);
        return Ok();
    }
}