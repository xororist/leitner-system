namespace LeitnerSystem.Domain.ValueObjects;

public sealed class Answer
{
    public string Text { get; private set; }

    public Answer(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) throw new ArgumentException("Answer cannot be empty", nameof(text));
        Text = text;
    }
}
