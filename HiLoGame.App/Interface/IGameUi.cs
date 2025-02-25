namespace HiLoGame.App.Interface;

public interface IGameUi
{
    Task BeginGame(CancellationToken cancellationToken = default);
}