using HiLoGame.App.Interface;
using HiLoGame.App.Model;

namespace HiLoGame.App.Rule;

internal class CorrectRule : IGameRule
{
    public GuessResult Result => GuessResult.Correct;

    public bool FulfillsRule(int userInput, int numberToGuess)
    {
        return userInput == numberToGuess;
    }
}