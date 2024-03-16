using FluentValidation;
using LeitnerSystem.Application.Dto;

namespace LeitnerSystem.Application.Validator;

public class UpdateCardDtoValidator : AbstractValidator<UpdateCardDto>
{
    public UpdateCardDtoValidator()
    {
        RuleFor(x => x.Question).NotEmpty().WithMessage("A question is required to update the card.");
        RuleFor(x => x.Answer).NotEmpty().WithMessage("An answer is required to update the card.");
        RuleFor(x => x.Tag).NotEmpty().WithMessage("A tag is required to update the card.");
    }
}