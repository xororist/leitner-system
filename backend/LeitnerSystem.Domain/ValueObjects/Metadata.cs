using LeitnerSystem.Domain.Enums;

namespace LeitnerSystem.Domain.ValueObjects;

public sealed class Metadata
{
    public DateTime CreationDate { get; set; }
    public DateTime NextDateQuestion { get; set; }
    public bool IsCompleted { get; set; }

    public Metadata()
    {
        CreationDate = DateTime.Now.Date;
        NextDateQuestion = DateTime.Now.Date.AddDays(1);
        IsCompleted = false;
    }

    public void NextDateQuestionIsAsked(Category category)
    {
        int delay = 1;
        for (int i = 0; i < (int)category; i++)
        {
            delay *= 2; 
        }
        NextDateQuestion = DateTime.Now.Date.AddDays(delay);
    }

    public void SetAsCompleted()
    {
        IsCompleted = true;
    }
}