using LeitnerSystem.Application.Dto;

namespace LeitnerSystem.Application.Interfaces;

public interface ICardsApplicationService
{
    Task<Guid> CreateCardAsync(CreateCardDto createCardDto);
    Task<IEnumerable<CardDto>> GetCardsForTodayReviewAsync();
    Task PromoteCardAsync(Guid cardId, string userAnswer);
    Task ResetCardAsync(Guid cardId);
    Task UpdateCardAsync(UpdateCardDto updateCardDto);
    Task DeleteCardAsync(Guid cardId);
    Task<IEnumerable<CardDto>> GetAllCardsAsync();
    Task SetCardAnswerTrueOrFalse(Guid cardId, bool isValid);
    Task<IEnumerable<CardDto>> FindCardsByTagAsync(string[] tags); 
}