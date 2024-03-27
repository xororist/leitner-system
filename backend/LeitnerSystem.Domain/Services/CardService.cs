using LeitnerSystem.Domain.Entities;
using LeitnerSystem.Domain.Exceptions;
using LeitnerSystem.Domain.Factories;
using LeitnerSystem.Domain.Interfaces;
using LeitnerSystem.Domain.ValueObjects;

namespace LeitnerSystem.Domain.Services;

public class CardService : ICardService
{
    private readonly ICardRepository _cardRepository;

    public CardService(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
    }
    
    public async Task<IEnumerable<Card>> GetCardsForTodayReviewAsync()
    {
        var today = DateTime.UtcNow.Date;
        var cardsForReview = await _cardRepository.GetCardsNeedingReviewTodayAsync();
        return cardsForReview.Where(card => card.Metadata.NextDateQuestion.Date <= today);
    }
    
    public async Task<IEnumerable<Card>> GetAllCardsAsync()
    {
        var cards = await _cardRepository.GetAllCardsAsync();
        return cards;
    }
    
    public async Task ProcessUserAnswerAsync(Guid cardId, string userAnswer)
    {
        var card = await _cardRepository.GetByIdAsync(cardId);
        if (card == null) throw new CardNotFoundException(cardId);

        card.CheckAnswer(userAnswer);
        await _cardRepository.UpdateAsync(card);
    }

    public async Task<Card> AddCardAsync(string question, string answer, string tag)
    {
        var card = CardFactory.CreateCard(question, answer, tag);
        await _cardRepository.AddAsync(card);
        return card;
    }

    public async Task UpdateCardAsync(Guid cardId, string question, string answer, string tag)
    {
        var card = await _cardRepository.GetByIdAsync(cardId);
        if (card == null) throw new CardNotFoundException(cardId);

        card.Update(new Question(question), new Answer(answer), tag);
        await _cardRepository.UpdateAsync(card);
    }
    
    public async Task ResetCardAsync(Guid cardId)
    {
        var card = await _cardRepository.GetByIdAsync(cardId);
        if (card == null) throw new CardNotFoundException(cardId);

        card.Reset();
        await _cardRepository.UpdateAsync(card);
    }
    
    public async Task<IEnumerable<Card>> GetCardsForReviewAsync()
    {
        return await _cardRepository.GetCardsNeedingReviewTodayAsync();
    }
    
    public async Task<IEnumerable<Card>> FindCardsByTagAsync(string[] tags)
    {
        var cards = await _cardRepository.FindCardsByTagsAsync(tags);
        return cards;
    }
    
    public async Task DeleteCardAsync(Guid cardId)
    { 
        await _cardRepository.DeleteAsync(cardId);
    }
    
    public async Task SetCardAnswer(Guid cardId, bool isValid)
    { 
        await _cardRepository.SetCardAnswer(cardId, isValid);
    }
}

