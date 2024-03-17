namespace LeitnerSystem.Infrastructure.MongoDatabase;

public sealed class MongoDbSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string CardsCollectionName { get; set; } = null!;
}
