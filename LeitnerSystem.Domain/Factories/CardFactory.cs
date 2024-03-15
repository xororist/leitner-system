using LeitnerSystem.Domain.Entities;
using LeitnerSystem.Domain.ValueObjects;

namespace LeitnerSystem.Domain.Factories;

public static class CardFactory
{
    public static Card CreateCard(string questionText, string answerText, string tag)
    {
        var question = new Question(questionText);
        var answer = new Answer(answerText);
        return new Card(question, answer, tag);
    }
}