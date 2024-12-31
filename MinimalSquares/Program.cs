using System;
using System.Threading.Tasks;

using MinimalSquares.ConsoleCommands;

namespace MinimalSquares;

internal class Program
{
    public static MainGame MainGame = null!;

    public const float Step = 0.0005f;
    public const float GrafhicStep = 0.005f;

    private static void Main(string[] args)
    {
        MainGame = new MainGame();
        Task.Run(ConsoleHandle);
        MainGame.Run();
    }

    private static void ConsoleHandle()
    {
        CommandManager.WriteText("Введите команду: ");

        do
        {
            string? str = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(str))
                continue;

            CommandManager.Handle(str.ToLowerInvariant());
        }
        while (true);
    }
}