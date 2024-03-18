using LeitnerSystem.Domain.Interfaces;
using LeitnerSystem.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using LeitnerSystem.Domain.Entities;
using LeitnerSystem.Infrastructure.MongoDatabase;


namespace LeitnerSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, MongoDbSettings settings)
    {
        services.AddSingleton(settings);

        RegisterBsonMappings();

        services.AddScoped<ICardRepository, CardRepository>();

        return services;
    }

    private static void RegisterBsonMappings()
    {
        BsonClassMap.RegisterClassMap<Card>(cm =>
        {
            cm.AutoMap(); 
            cm.MapMember(c => c.Question); 
            cm.MapMember(c => c.Answer); 
            cm.MapMember(c => c.Metadata); 
        });
    }

}
