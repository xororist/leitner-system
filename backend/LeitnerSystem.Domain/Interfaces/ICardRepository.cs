using LeitnerSystem.Domain.Entities;

namespace LeitnerSystem.Domain.Interfaces;

public interface ICardRepository
{
    Task<Card> GetByIdAsync(Guid id);
    Task<IEnumerable<Card>> GetAllCardsAsync();
    Task<IEnumerable<Card>> FindCardsByTagsAsync(string[] tag);
    Task<IEnumerable<Card>> GetCardsNeedingReviewTodayAsync();
    Task AddAsync(Card card);
    Task UpdateAsync(Card card);
    Task DeleteAsync(Guid id);
    Task SetCardAnswer(Guid id, bool isValid);
    
}