using HiLoGame.App;
using HiLoGame.App.Interface;
using HiLoGame.App.Model;
using HiLoGame.App.Rule;
using NSubstitute;
using Xunit;

namespace HiLoGame.Tests;

public class GameEngineTests
{
    private const int MinRangeValue = 1;
    private const int MaxRangeValue = 101;

    private readonly IEnumerable<IGameRule> _gameRules = new List<IGameRule>
    {
        new CorrectRule(),
        new TooHighRule(),
        new TooLowRule()
    };

    private readonly IRandomNumberGenerator _randomNumberGenerator = Substitute.For<IRandomNumberGenerator>();

    [Theory]
    [InlineData(10, 10, GuessResult.TooHigh)]
    [InlineData(10, 0, GuessResult.Correct)]
    [InlineData(10, -6, GuessResult.TooLow)]
    public void GuessNumberTest(int numberToGuess, int numberToAdd, GuessResult expectedResult)
    {
        // arrange
        _randomNumberGenerator.Next(MinRangeValue, MaxRangeValue).Returns(numberToGuess);
        var userInput = _randomNumberGenerator.Next(MinRangeValue, MaxRangeValue) + numberToAdd;
        var gameEngine = new GameEngine(_gameRules, _randomNumberGenerator);
        gameEngine.StartNew();

        // act
        var result = gameEngine.GuessNumber(userInput);

        // assert
        Assert.Equal(result, expectedResult);
    }

    [Fact]
    public void GuessNumberThrowsExceptionWhenGameIsNotStarted()
    {
        // arrange
        const int userInput = 10;
        var gameEngine = new GameEngine(_gameRules, _randomNumberGenerator);

        // act & assert
        Assert.Throws<InvalidOperationException>(() => gameEngine.GuessNumber(userInput));
    }

    [Theory]
    [InlineData(10, 10, 11)]
    [InlineData(10, -6, 7)]
    public void GetLastScoreTest(int numberToGuess, int numberToAdd, int expectedResult)
    {
        // arrange
        _randomNumberGenerator.Next(MinRangeValue, MaxRangeValue).Returns(numberToGuess);
        var userInput = _randomNumberGenerator.Next(MinRangeValue, MaxRangeValue) + numberToAdd;
        var numberOfIterations = numberToGuess > userInput ? numberToGuess - userInput : userInput - numberToGuess;
        var gameEngine = new GameEngine(_gameRules, _randomNumberGenerator);
        gameEngine.StartNew();

        for (var i = 0; i <= numberOfIterations; i++)
        {
            gameEngine.GuessNumber(userInput);
            if (numberToGuess > userInput)
                userInput++;
            else
                userInput--;
        }

        // act
        var result = gameEngine.GetLastScore();

        // assert
        Assert.Equal(result, expectedResult);
    }

    [Fact]
    public void GetLastScoreThrowsExceptionWhenGameHasNoLastScore()
    {
        // arrange
        var gameEngine = new GameEngine(_gameRules, _randomNumberGenerator);

        // act & assert
        Assert.Throws<InvalidOperationException>(() => gameEngine.GetLastScore());
    }
}