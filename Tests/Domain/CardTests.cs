using FluentAssertions;
using LeitnerSystem.Domain.Entities;
using LeitnerSystem.Domain.Enums;
using LeitnerSystem.Domain.ValueObjects;

namespace Tests.Domain;

public class CardTests
{
    [Fact]
    public void Card_Should_Be_Promoted_To_Next_Category()
    {
        // Arrange
        var card = new Card(new Question("What is TDD?"), new Answer("Test Driven Development"), "Development");

        // Act
        card.Promote();

        // Assert
        card.Category.Should().Be(Category.SECOND);
    }
    
    [Fact]
    public void Card_Should_Be_Reset_To_First_Category()
    {
        // Arrange
        var card = new Card(new Question("What is DDD?"), new Answer("Domain Driven Design"), "Architecture");
        card.Promote();
        card.Promote();

        // Act
        card.Reset();

        // Assert
        card.Category.Should().Be(Category.FIRST);
    }
    
    [Fact]
    public void Card_Next_Review_Date_Should_Be_Correct()
    {
        // Arrange
        var card = new Card(new Question("Is the Earth flat?"), new Answer("Yes"), "Trolling");
        var initialReviewDate = card.Metadata.NextDateQuestion;

        // Act
        card.Promote();

        // Assert
        card.Metadata.NextDateQuestion.Should().BeAfter(initialReviewDate);
    }
    
    [Fact]
    public void Card_Should_Be_Reset_To_First_Category_If_Incorrect_Answer()
    {
        // Arrange
        var card = new Card(new Question("42"), new Answer("Yes"), "Life");

        // Act
        card.CheckAnswer("Incorrect answer");

        // Assert
        card.Category.Should().Be(Category.FIRST);
    }
}