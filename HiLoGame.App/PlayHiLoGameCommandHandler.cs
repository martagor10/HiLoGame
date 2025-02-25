using System.CommandLine.Invocation;
using HiLoGame.App.Interface;
using JetBrains.Annotations;

namespace HiLoGame.App;

[UsedImplicitly]
public class PlayHiLoGameCommandHandler(IGameUi gameUi) : ICommandHandler
{
    public int Invoke(InvocationContext context)
    {
        throw new NotImplementedException();
    }

    public async Task<int> InvokeAsync(InvocationContext context)
    {
        await gameUi.BeginGame(context.GetCancellationToken());

        return 0;
    }
}