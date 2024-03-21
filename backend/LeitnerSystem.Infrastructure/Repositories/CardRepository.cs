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
        var documents = await _cardsCollection.Find(new BsonDocument()).ToListAsync();
        var collection = _cardsCollection.AsQueryable();
        var cards = await _cardsCollection.Find(Builders<Card>.Filter.Empty).ToListAsync();
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

    public async Task<IEnumerable<Card>> GetCardsForReviewAsync(DateOnly reviewDate)
    {
        DateTime startOfReviewDate = reviewDate.ToDateTime(new TimeOnly(0, 0), DateTimeKind.Utc); 

        DateTime endOfReviewDate = startOfReviewDate.AddDays(1); 

        var cardsForReview = await _cardsCollection.Find(card =>
            card.Metadata.NextDateQuestion >= startOfReviewDate &&
            card.Metadata.NextDateQuestion < endOfReviewDate &&
            !card.Metadata.IsCompleted).ToListAsync();

        return cardsForReview;
    }

    public async Task SetCardAnswer(Guid id, bool isValid)
    { 
        var card = await GetByIdAsync(id);
        if (!isValid)
        {
            card.Reset();
        }
        card.Promote();
        await _cardsCollection.ReplaceOneAsync(c => c.Id == card.Id, card);
    }

    public Task<IEnumerable<Card>> GetCardsForReviewAsync(DateTime reviewDate)
    {
        throw new NotImplementedException();
    }
}