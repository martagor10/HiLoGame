using HiLoGame.App.Rule;
using Xunit;

namespace HiLoGame.Tests;

public class RuleTests
{
    [Theory]
    [InlineData(1, 1, true)]
    [InlineData(1, 2, false)]
    public void CorrectRuleTest(int userInput, int numberToGuess, bool expectedResult)
    {
        // arrange
        var correctRule = new CorrectRule();

        // act
        var result = correctRule.FulfillsRule(userInput, numberToGuess);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(1, 1, false)]
    [InlineData(1, 2, false)]
    [InlineData(2, 1, true)]
    public void TooHighRuleTest(int userInput, int numberToGuess, bool expectedResult)
    {
        // arrange
        var tooHighRule = new TooHighRule();

        // act
        var result = tooHighRule.FulfillsRule(userInput, numberToGuess);

        // assert
        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(1, 1, false)]
    [InlineData(1, 2, true)]
    [InlineData(2, 1, false)]
    public void TooLowRuleTest(int userInput, int numberToGuess, bool expectedResult)
    {
        // arrange
        var tooLowRule = new TooLowRule();

        // act
        var result = tooLowRule.FulfillsRule(userInput, numberToGuess);

        // assert
        Assert.Equal(expectedResult, result);
    }
}