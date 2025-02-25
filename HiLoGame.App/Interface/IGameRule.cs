using HiLoGame.App.Model;

namespace HiLoGame.App.Interface;

public interface IGameRule
{
    GuessResult Result { get; }
    bool FulfillsRule(int userInput, int numberToGuess);
}