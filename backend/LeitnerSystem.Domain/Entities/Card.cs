using LeitnerSystem.Domain.Enums;
using LeitnerSystem.Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LeitnerSystem.Domain.Entities;

public sealed class Card
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; private set; }
    public Question Question { get; private set; }
    public Answer Answer { get; private set; }
    public string Tag { get; private set; }
    public Category Category { get; private set; }
    public Metadata Metadata { get; }

    public Card(Question question, Answer answer, string tag)
    {
        Id = Guid.NewGuid();
        Question = question ?? throw new ArgumentNullException(nameof(question));
        Answer = answer ?? throw new ArgumentNullException(nameof(answer));
        Tag = tag;
        Category = Category.FIRST;
        Metadata = new Metadata();
    }

    public void CheckAnswer(string userAnswer)
    {
        if (userAnswer.Trim().Equals(Answer.Text.Trim(), StringComparison.OrdinalIgnoreCase))
        {
            if (Category == Category.DONE)
            {
                Metadata.SetAsCompleted();
            }
            else
            {
                Promote();
            }
        }
        else
        {
            Reset();
        }
    }

    public void Update(Question question, Answer answer, string tag)
    {
        Question = question ?? throw new ArgumentNullException(nameof(question));
        Answer = answer ?? throw new ArgumentNullException(nameof(answer));
        Tag = tag;
    }

    public void Promote()
    {
        if (Category < Category.DONE)
        {
            Category++;
            Metadata.NextDateQuestionIsAsked(Category);
        }
    }

    public void Reset()
    {
        Category = Category.FIRST;
        Metadata.NextDateQuestionIsAsked(Category);
    }
}