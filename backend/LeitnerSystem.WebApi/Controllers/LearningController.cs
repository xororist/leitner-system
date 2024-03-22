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


    /// <summary>
    /// Get all cards for today's review. If date is provided, it will get all cards for this date
    /// </summary>
    /// <param name="date">A date to search for cards that will be reviewed on this date.</param>
    /// <returns></returns>
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

    /// <summary>
    /// Force a valid answer to a card or send a false answer.
    /// </summary>
    /// <param name="cardId">Card identified by his id.</param>
    /// <param name="request">Send a boolean to valid or send a false answer to a question.</param>
    /// <returns></returns>
    [HttpPatch]
    [Route("{cardId}/answer")]
    public async Task<IActionResult> SetCardAnswer(Guid cardId, [FromBody] AnswerUpdateRequestDto request)
    {
        try
        {
            await _cardsService.SetCardAnswerTrueOrFalse(cardId, request.IsValid);
            return NoContent(); 
        }
        catch (KeyNotFoundException) 
        {
            return NotFound(); 
        }       
    }
}