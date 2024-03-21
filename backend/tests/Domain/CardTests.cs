using FluentAssertions;
using LeitnerSystem.Domain.Entities;
using LeitnerSystem.Domain.Enums;
using LeitnerSystem.Domain.ValueObjects;

namespace Tests.Domain;

public class CardTests
{
    [Fact]
    public void Constructor_InitializesWithExpectedValues()
    {
        var metadata = new Metadata();

        Assert.Equal(DateTime.Now.Date, metadata.CreationDate);
        Assert.Equal(DateTime.Now.Date.AddDays(1), metadata.NextDateQuestion);
        Assert.False(metadata.IsCompleted);
    }

    [Theory]
    [InlineData(Category.FIRST, 1)] 
    [InlineData(Category.SECOND, 2)] 
    [InlineData(Category.THIRD, 4)] 
    [InlineData(Category.FOURTH, 8)] 
    [InlineData(Category.FIFTH, 16)]
    [InlineData(Category.SIXTH, 32)]
    [InlineData(Category.SEVENTH, 64)]
    public void Card_Next_Review_Date_Should_Be_Set_Correctly_After_Promotion(Category nextCategory, int delay)
    {
        // Arrange
        var card = new Card(new Question("Simple Question"), new Answer("Simple Answer"), "Simple Tag");
        var today = DateTime.Now.Date;

        // Act
        while (card.Category < nextCategory)
        {
            card.Promote();
        }

        // Assert
        var expectedReviewDate = today.AddDays(delay);
        card.Metadata.NextDateQuestion.Date.Should().Be(expectedReviewDate);
    }
    
    [Fact]
    public void Card_Next_Review_Date_Should_Be_Set_To_64_And_Done_Category()
    {
        // Arrange
        var card = new Card(new Question("What is the meaning of life?"), new Answer("42"), "Philosophy");

        // Act
        while (card.Category < Category.DONE)
        {
            card.Promote();
        }
        card.Promote();
        var expectedReviewDate = DateTime.Now.Date.AddDays(128);

        // Assert
        card.Metadata.NextDateQuestion.Should().Be(expectedReviewDate);
    }

    [Fact]
    public void Card_Should_Be_Considered_Done_After_Final_Promotion()
    {
        // Arrange
        var card = new Card(new Question("Final Question"), new Answer("Final Answer"), "Final Tag");

        // Act
        while (card.Category < Category.DONE)
        {
            card.Promote();
        }

        // Assert
        card.Category.Should().Be(Category.DONE);
    }
    
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

    [Fact]
    public void Set_As_Completed_Sets_Is_Completed_To_True()
    {
        var metadata = new Metadata();
        metadata.SetAsCompleted();

        Assert.True(metadata.IsCompleted);
    }
}