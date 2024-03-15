using LeitnerSystem.Domain.Entities;
using LeitnerSystem.Domain.Exceptions;
using LeitnerSystem.Domain.Factories;
using LeitnerSystem.Domain.Interfaces;
using LeitnerSystem.Domain.ValueObjects;

namespace LeitnerSystem.Domain.Services;

public class CardService
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
        return await _cardRepository.GetAllCardsAsync();
    }
    
    public async Task ProcessUserAnswerAsync(Guid cardId, string userAnswer)
    {
        var card = await _cardRepository.GetByIdAsync(cardId);
        if (card == null) throw new CardNotFoundException(cardId);

        card.CheckAnswer(userAnswer);
        await _cardRepository.UpdateAsync(card);
    }

    public async Task<Card> AddCardAsync(string questionText, string answerText, string tag)
    {
        var card = CardFactory.CreateCard(questionText, answerText, tag);
        await _cardRepository.AddAsync(card);
        return card;
    }

    public async Task UpdateCardAsync(Guid cardId, string questionText, string answerText, string tag)
    {
        var card = await _cardRepository.GetByIdAsync(cardId);
        if (card == null) throw new CardNotFoundException(cardId);

        card.Update(new Question(questionText), new Answer(answerText), tag);
        await _cardRepository.UpdateAsync(card);
    }
    
    public async Task PromoteCardAsync(Guid cardId)
    {
        var card = await _cardRepository.GetByIdAsync(cardId);
        if (card == null) throw new CardNotFoundException(cardId);
        
        card.Promote();
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
    
    public async Task<IEnumerable<Card>> FindCardsByTagAsync(string tag)
    {
        return await _cardRepository.FindCardsByTagAsync(tag);
    }
    
    public async Task DeleteCardAsync(Guid cardId)
    { 
        await _cardRepository.DeleteAsync(cardId);
    }
}

