using LeitnerSystem.Domain.Enums;

namespace LeitnerSystem.Domain.ValueObjects;

public sealed class Metadata
{
    public DateTime CreationDate { get; init; }
    public DateTime NextDateQuestion { get; private set; }
    public bool IsCompleted { get; private set; }

    public Metadata()
    {
        CreationDate = DateTime.Now.Date;
        NextDateQuestion = DateTime.Now.Date;
        IsCompleted = false;
    }

    public void NextDateQuestionIsAsked(Category category)
    {
        switch (category)
        {
            case Category.FIRST:
                NextDateQuestion = DateTime.Now.Date;
                break;
            case Category.SECOND:
                NextDateQuestion = DateTime.Now.Date.AddDays(1);
                break;
            case Category.THIRD:
                NextDateQuestion = DateTime.Now.Date.AddDays(2);
                break;
            case Category.FOURTH:
                NextDateQuestion = DateTime.Now.Date.AddDays(4);
                break;
            case Category.FIFTH:
                NextDateQuestion = DateTime.Now.Date.AddDays(8);
                break;
            case Category.SIXTH:
                NextDateQuestion = DateTime.Now.Date.AddDays(16);
                break;
            case Category.SEVENTH:
                NextDateQuestion = DateTime.Now.Date.AddDays(32);
                break;
            case Category.DONE:
                NextDateQuestion = DateTime.Now.Date.AddDays(64);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(category));
        }
    }
    
    public void SetAsCompleted()
    {
        IsCompleted = true;
    }
}