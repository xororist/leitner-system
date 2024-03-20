using LeitnerSystem.Domain.Entities;
using LeitnerSystem.Domain.Interfaces;
using MongoDB.Driver;
using LeitnerSystem.Infrastructure.MongoDatabase;
using MongoDB.Bson;

namespace LeitnerSystem.Infrastructure.Repositories;

public class CardRepository : ICardRepository
{
    private readonly IMongoCollection<Card> _cardsCollection;

    public CardRepository(MongoDbSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _cardsCollection = database.GetCollection<Card>(settings.CardsCollectionName);
    }

    public async Task AddAsync(Card card)
    {
        await _cardsCollection.InsertOneAsync(card);
    }

    public async Task<IEnumerable<Card>> GetAllCardsAsync()
    {
        return await _cardsCollection.Find(_ => true).ToListAsync();
    }

    public async Task<Card> GetByIdAsync(Guid id)
    {
        return await _cardsCollection.Find<Card>(card => card.Id == id).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Card card)
    {
        await _cardsCollection.ReplaceOneAsync(c => c.Id == card.Id, card);
    }

    public async Task DeleteAsync(Guid id)
    {
        var filter = Builders<Card>.Filter.Eq(c => c.Id, id);
        await _cardsCollection.DeleteOneAsync(filter);
    }

    public async Task<IEnumerable<Card>> FindCardsByTagsAsync(string[] tags)
    {
        var filter = Builders<Card>.Filter.In(card => card.Tag, tags);
        return await _cardsCollection.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<Card>> GetCardsNeedingReviewTodayAsync()
    {
        var today = DateTime.UtcNow.Date;
        return await _cardsCollection.Find(card => card.Metadata.NextDateQuestion <= today && !card.Metadata.IsCompleted).ToListAsync();
    }
}