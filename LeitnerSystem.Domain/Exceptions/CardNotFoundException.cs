namespace LeitnerSystem.Domain.Exceptions;

public class CardNotFoundException : Exception
{
    public CardNotFoundException(Guid cardId)
        : base($"Card {cardId} not found.")
    {
    }
}