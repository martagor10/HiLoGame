using HiLoGame.App.Model;

namespace HiLoGame.App.Interface;

internal interface IGameEngine
{
    void StartNew();
    GuessResult GuessNumber(int number);
    int GetLastScore();
}