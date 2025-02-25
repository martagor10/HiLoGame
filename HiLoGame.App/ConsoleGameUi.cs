using HiLoGame.App.Interface;
using HiLoGame.App.Model;
using Spectre.Console;

namespace HiLoGame.App;

internal class ConsoleGameUi(IAnsiConsole console, IGameEngine game) : IGameUi
{
    private CancellationToken _cancellationToken;

    public async Task BeginGame(CancellationToken cancellationToken = default)
    {
        _cancellationToken = cancellationToken;

        var prompt = new SelectionPrompt<string>()
            .Title("Hello Player, what do you want to do?")
            .AddChoices(MenuOption.StartNew, MenuOption.DisplayScore, MenuOption.Exit);

        string selection;

        do
        {
            Console.Clear();

            selection = await prompt.ShowAsync(console, cancellationToken);

            var action = selection switch
            {
                MenuOption.StartNew => HandleStartNew(),
                MenuOption.DisplayScore => HandleDisplayScore(),
                MenuOption.Exit => Task.CompletedTask,
                _ => throw new ArgumentOutOfRangeException(nameof(selection), selection, null)
            };

            await action;
        } while (selection != MenuOption.Exit);
    }

    private async Task HandleStartNew()
    {
        WriteHeader("New game");

        Console.WriteLine(
            "Your goal is to guess the secret number between 1 and 100 in fewest possible attempts. Good luck!");
        Console.WriteLine();

        game.StartNew();

        bool gameFinished;

        do
        {
            var guess = await GetUserGuess();

            var result = game.GuessNumber(guess);

            gameFinished = HandleGuessResult(result);
        } while (!gameFinished);

        await WaitForReturnToMenu();
    }

    private bool HandleGuessResult(GuessResult result)
    {
        switch (result)
        {
            case GuessResult.Correct:
                console.MarkupLine(
                    $"Your guess is [green]correct[/]! You guessed the number in [green]{game.GetLastScore()}[/] attempt(s)!");
                break;
            case GuessResult.TooHigh:
                console.MarkupLine("Your guess is [red]too high[/]! Try again!");
                break;
            case GuessResult.TooLow:
                console.MarkupLine("Your guess is [aqua]too low[/]! Try again!");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(result), result, null);
        }

        console.WriteLine();

        return result == GuessResult.Correct;
    }

    private async Task<int> GetUserGuess()
    {
        var prompt = new TextPrompt<int>("What's your guess?: ");

        return await prompt.ShowAsync(console, _cancellationToken);
    }

    private async Task HandleDisplayScore()
    {
        WriteHeader("Last score");

        try
        {
            var score = game.GetLastScore();

            console.MarkupLine($"Your last score is [green]{score}[/]");
        }
        catch (InvalidOperationException ex)
        {
            console.MarkupLine($"[red]{ex.Message}[/]");
        }

        await WaitForReturnToMenu();
    }

    private void WriteHeader(string text)
    {
        var rule = new Spectre.Console.Rule($"[yellow]{text}[/]").LeftJustified();

        console.Write(rule);
        console.WriteLine();
    }

    private async Task WaitForReturnToMenu()
    {
        console.WriteLine("Press any key to return to the menu...");

        await console.Input.ReadKeyAsync(true, _cancellationToken);
    }
}