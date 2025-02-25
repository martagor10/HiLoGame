using HiLoGame.App.Interface;
using HiLoGame.App.Model;

namespace HiLoGame.App.Rule;

internal class TooLowRule : IGameRule
{
    public GuessResult Result => GuessResult.TooLow;

    public bool FulfillsRule(int userInput, int numberToGuess)
    {
        return userInput < numberToGuess;
    }
}