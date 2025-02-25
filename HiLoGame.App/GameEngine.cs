using HiLoGame.App.Interface;
using HiLoGame.App.Model;

namespace HiLoGame.App;

internal class GameEngine(IEnumerable<IGameRule> rules, IRandomNumberGenerator numberGenerator) : IGameEngine
{
    private int _currentAttempts;
    private int? _lastScore;
    private int? _numberToGuess;

    public void StartNew()
    {
        _numberToGuess = numberGenerator.Next(1, 101);
    }

    public GuessResult GuessNumber(int number)
    {
        if (!_numberToGuess.HasValue)
            throw new InvalidOperationException("Cannot guess a number because game is not started");

        _currentAttempts++;

        var result = rules.First(x => x.FulfillsRule(number, _numberToGuess.Value)).Result;

        if (result == GuessResult.Correct) FinishGame();

        return result;
    }

    public int GetLastScore()
    {
        if (!_lastScore.HasValue)
            throw new InvalidOperationException("No previous score was found");

        return _lastScore.Value;
    }

    private void FinishGame()
    {
        _numberToGuess = null;
        _lastScore = _currentAttempts;
        _currentAttempts = 0;
    }
}