namespace LeitnerSystem.Domain.ValueObjects;

public class Question
{
    public string Text { get; private set; }

    public Question(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) throw new ArgumentException("Question cannot be empty", nameof(text));
        Text = text;
    }
}
