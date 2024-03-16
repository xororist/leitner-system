using FluentValidation;
using LeitnerSystem.Application.Dto;
using LeitnerSystem.Application.Interfaces;
using LeitnerSystem.Domain.Interfaces;

namespace LeitnerSystem.Application.Services;

public class CardsApplicationService : ICardsApplicationService
{
    private readonly ICardService _cardService;
    private readonly IValidator<CreateCardDto> _createCardValidator;
    private readonly IValidator<UpdateCardDto> _updateCardValidator;

    public CardsApplicationService(
        ICardService cardService, 
        IValidator<CreateCardDto> createCardValidator,
        IValidator<UpdateCardDto> updateCardValidator)
    {
        _cardService = cardService;
        _createCardValidator = createCardValidator;
        _updateCardValidator = updateCardValidator;
    }

    public async Task<Guid> CreateCardAsync(CreateCardDto createCardDto)
    {
        var validationResult = await _createCardValidator.ValidateAsync(createCardDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var card = await _cardService.AddCardAsync(createCardDto.Question, createCardDto.Answer, createCardDto.Tag);
        return card.Id;
    }

    public async Task<IEnumerable<CardDto>> GetCardsForTodayReviewAsync()
    {
        var cards = await _cardService.GetCardsForTodayReviewAsync();
        return cards.Select(card => new CardDto
        {
            Id = card.Id,
            Question = card.Question.Text,
            Answer = card.Answer.Text,
            Tag = card.Tag,
            Category = card.Category.ToString(),
            IsCompleted = card.Metadata.IsCompleted
        });
    }

    public async Task PromoteCardAsync(Guid cardId, string userAnswer)
    {
        await _cardService.ProcessUserAnswerAsync(cardId, userAnswer);
    }

    public async Task ResetCardAsync(Guid cardId)
    {
        await _cardService.ResetCardAsync(cardId);
    }

    public async Task UpdateCardAsync(UpdateCardDto updateCardDto)
    {
        var validationResult = await _updateCardValidator.ValidateAsync(updateCardDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        await _cardService.UpdateCardAsync(updateCardDto.CardId, updateCardDto.Question, updateCardDto.Answer, updateCardDto.Tag);
    }
    
    public async Task DeleteCardAsync(Guid cardId)
    {
        await _cardService.DeleteCardAsync(cardId);
    }

    public async Task<IEnumerable<CardDto>> GetAllCardsAsync()
    {
        var cards = await _cardService.GetAllCardsAsync();
        return cards.Select(card => new CardDto
        {
            Id = card.Id,
            Question = card.Question.Text,
            Answer = card.Answer.Text,
            Tag = card.Tag,
            Category = card.Category.ToString(),
            IsCompleted = card.Metadata.IsCompleted
        });
    }

    public async Task<IEnumerable<CardDto>> FindCardsByTagAsync(string tag)
    {
        var cards = await _cardService.FindCardsByTagAsync(tag);
        return cards.Select(card => new CardDto
        {
            Id = card.Id,
            Question = card.Question.Text,
            Answer = card.Answer.Text,
            Tag = card.Tag,
            Category = card.Category.ToString(),
            IsCompleted = card.Metadata.IsCompleted
        });
    }
}
