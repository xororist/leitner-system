using LeitnerSystem.Domain.Enums;

namespace LeitnerSystem.Domain.ValueObjects;

public sealed class Metadata
{
    public DateTime CreationDate { get; init; }
    public DateTime NextDateQuestion { get; private set; }

    public Metadata()
    {
        CreationDate = DateTime.Now;
        NextDateQuestion = DateTime.Now;
    }

    public void NextDateQuestionIsAsked(Category category)
    {
        switch (category)
        {
            case Category.FIRST:
                NextDateQuestion = DateTime.Now.AddDays(1);
                break;
            case Category.SECOND:
                NextDateQuestion = DateTime.Now.AddDays(2);
                break;
            case Category.THIRD:
                NextDateQuestion = DateTime.Now.AddDays(4);
                break;
            case Category.FOURTH:
                NextDateQuestion = DateTime.Now.AddDays(8);
                break;
            case Category.FIFTH:
                NextDateQuestion = DateTime.Now.AddDays(16);
                break;
            case Category.SIXTH:
                NextDateQuestion = DateTime.Now.AddDays(32);
                break;
            case Category.SEVENTH:
                NextDateQuestion = DateTime.Now.AddDays(64);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(category));
        }
    }
}