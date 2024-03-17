using LeitnerSystem.Domain.Entities;
using LeitnerSystem.Domain.Interfaces;
using MongoDB.Driver;
using LeitnerSystem.Infrastructure.MongoDatabase;

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
        return await _cardsCollection.Find(card => card.Id == id).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Card card)
    {
        await _cardsCollection.ReplaceOneAsync(c => c.Id == card.Id, card);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _cardsCollection.DeleteOneAsync(card => card.Id == id);
    }

    public async Task<IEnumerable<Card>> FindCardsByTagAsync(string tag)
    {
        return await _cardsCollection.Find(card => card.Tag == tag).ToListAsync();
    }

    public async Task<IEnumerable<Card>> GetCardsNeedingReviewTodayAsync()
    {
        var today = DateTime.UtcNow.Date;
        return await _cardsCollection.Find(card => card.Metadata.NextDateQuestion <= today && !card.Metadata.IsCompleted).ToListAsync();
    }
}