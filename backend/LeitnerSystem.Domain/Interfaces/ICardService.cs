using LeitnerSystem.Domain.Entities;

namespace LeitnerSystem.Domain.Interfaces;

public interface ICardService
{
    Task<Card> AddCardAsync(string question, string answer, string tag);
    Task<IEnumerable<Card>> GetCardsForTodayReviewAsync();
    Task ProcessUserAnswerAsync(Guid cardId, string userAnswer);
    Task UpdateCardAsync(Guid cardId, string question, string answer, string tag);
    Task ResetCardAsync(Guid cardId);
    Task SetCardAnswer(Guid cardId, bool isValid);
    Task<IEnumerable<Card>> GetAllCardsAsync();
    Task<IEnumerable<Card>> FindCardsByTagAsync(string[] tags); 
    Task DeleteCardAsync(Guid cardId);
}