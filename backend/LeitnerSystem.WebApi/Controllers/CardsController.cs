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
    public async Task<IActionResult> GetCards([FromQuery] string[] tags)
    {
        IEnumerable<CardDto> cards;
        if (tags.Length == 0)
        {
            cards = await _cardsService.GetAllCardsAsync();
        }
        else
        {
            cards = await _cardsService.FindCardsByTagAsync(tags);
        }
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
    
    [HttpPatch]
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