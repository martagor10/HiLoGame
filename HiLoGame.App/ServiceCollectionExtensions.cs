using HiLoGame.App.Interface;
using HiLoGame.App.Rule;
using Microsoft.Extensions.DependencyInjection;

namespace HiLoGame.App;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHiLoGame(this IServiceCollection services)
    {
        services.AddSingleton<IGameRule, CorrectRule>();
        services.AddSingleton<IGameRule, TooHighRule>();
        services.AddSingleton<IGameRule, TooLowRule>();

        services.AddSingleton<IGameEngine, GameEngine>();
        services.AddSingleton<IGameUi, ConsoleGameUi>();
        services.AddSingleton<IRandomNumberGenerator, RandomNumberGenerator>();

        return services;
    }
}