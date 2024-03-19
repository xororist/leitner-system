using LeitnerSystem.Domain.Interfaces;
using LeitnerSystem.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LeitnerSystem.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<ICardService, CardService>();

        return services;
    }
}