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

    /// <summary>
    /// Get all cards.
    /// </summary>
    /// <param name="tags">Tags of cards to find. If not present, all cards will be found.</param>
    /// <returns></returns>
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

    /// <summary>
    /// Create a new card.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateCard(CreateCardDto createCardDto)
    {
        var cardId = await _cardsService.CreateCardAsync(createCardDto);
        return CreatedAtAction(nameof(GetCards), new { id = cardId }, cardId);
    }

    /// <summary>
    /// Answer a question for a card.
    /// </summary>
    /// <param name="cardId">Id of the card to answer in guid format.</param>
    /// <param name="answer">The answer for a given card identified by his id.</param>
    /// <returns></returns>
    [HttpPost]
    [Route("answer/{cardId}")]
    public async Task<IActionResult> PromoteCard(Guid cardId, string answer)
    {
        await _cardsService.PromoteCardAsync(cardId, answer);
        return NoContent();
    }

    /// <summary>
    /// Update question, answer or tag for a card.
    /// </summary>
    /// <returns></returns>
    [HttpPatch]
    public async Task<IActionResult> UpdateCard(UpdateCardDto updateCardDto)
    {
        await _cardsService.UpdateCardAsync(updateCardDto);
        return NoContent();
    }

    /// <summary>
    /// Delete a card identified by his id.
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    [Route("delete/{cardId}")]
    public async Task<IActionResult> DeleteCard(Guid cardId)
    {
        await _cardsService.DeleteCardAsync(cardId);
        return NoContent();
    }
}