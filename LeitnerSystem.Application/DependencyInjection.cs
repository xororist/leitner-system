using FluentValidation;
using LeitnerSystem.Application.Interfaces;
using LeitnerSystem.Application.Services;
using LeitnerSystem.Application.Validator;
using Microsoft.Extensions.DependencyInjection;

namespace LeitnerSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateCardDtoValidator>();
        services.AddScoped<ICardsApplicationService, CardsApplicationService>();

        return services;
    }
}