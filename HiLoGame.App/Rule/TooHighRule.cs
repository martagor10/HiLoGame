using HiLoGame.App.Interface;
using HiLoGame.App.Model;

namespace HiLoGame.App.Rule;

internal class TooHighRule : IGameRule
{
    public GuessResult Result => GuessResult.TooHigh;

    public bool FulfillsRule(int userInput, int numberToGuess)
    {
        return userInput > numberToGuess;
    }
}