using FluentValidation;
using LeitnerSystem.Application.Dto;

namespace LeitnerSystem.Application.Validator;

public class CreateCardDtoValidator : AbstractValidator<CreateCardDto>
{
    public CreateCardDtoValidator()
    {
        RuleFor(x => x.Question).NotEmpty().WithMessage("A question is required to create a card.");
        RuleFor(x => x.Answer).NotEmpty().WithMessage("An answer is required to create a card.");
        RuleFor(x => x.Tag).NotEmpty().WithMessage("A tag for the question is required to create a card.");
    }
}