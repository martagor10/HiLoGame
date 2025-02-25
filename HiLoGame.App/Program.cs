using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Parsing;
using HiLoGame.App;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Spectre.Console;

var parser = new CommandLineBuilder(new PlayHiLoGameCommand())
    .UseHost(
        _ => Host.CreateDefaultBuilder(),
        builder => builder.ConfigureServices((_, services) =>
            {
                services
                    .AddSingleton(AnsiConsole.Console)
                    .AddHiLoGame()
                    .AddLogging(c =>
                        c.ClearProviders());
            })
            .UseCommandHandler<PlayHiLoGameCommand, PlayHiLoGameCommandHandler>()
    )
    .UseDefaults()
    .Build();

await parser.InvokeAsync(args);