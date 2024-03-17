using LeitnerSystem.Domain.Interfaces;
using LeitnerSystem.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using LeitnerSystem.Domain.Entities;
using LeitnerSystem.Domain.ValueObjects;
using LeitnerSystem.Infrastructure.MongoDatabase;
using LeitnerSystem.Infrastructure.Utils;
using MongoDB.Bson.Serialization.IdGenerators;

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
            cm.MapIdMember(c => c.Id).SetIdGenerator(GuidGenerator.Instance);
            cm.MapMember(c => c.Question).SetSerializer(new ValueObjectSerializer<Question>());
            cm.MapMember(c => c.Answer).SetSerializer(new ValueObjectSerializer<Answer>());
            cm.MapMember(c => c.Metadata).SetSerializer(new ValueObjectSerializer<Metadata>());
        });

        BsonClassMap.RegisterClassMap<Question>(cm =>
        {
            cm.AutoMap();
            cm.MapMember(q => q.Text).SetElementName("question");
        });

        BsonClassMap.RegisterClassMap<Answer>(cm =>
        {
            cm.AutoMap();
            cm.MapMember(a => a.Text).SetElementName("answer");
        });

        BsonClassMap.RegisterClassMap<Metadata>(cm =>
        {
            cm.AutoMap();
            cm.MapMember(m => m.CreationDate).SetElementName("creationDate");
            cm.MapMember(m => m.NextDateQuestion).SetElementName("nextDateQuestion");
            cm.MapMember(m => m.IsCompleted).SetElementName("isCompleted");
        });
    }
}