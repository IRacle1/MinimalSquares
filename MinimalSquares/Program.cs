using System;
using System.Threading.Tasks;

using MinimalSquares.ConsoleCommands;

namespace MinimalSquares;

internal class Program
{
    public static MainGame MainGame = null!;

    private static void Main(string[] args)
    {
        MainGame = new MainGame();
        Task.Run(ConsoleHandle);
        MainGame.Run();
    }

    private static void ConsoleHandle()
    {
        CommandManager.Init();

        do
        {
            CommandManager.WriteText(": ", space: false);
            string? str = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(str))
                continue;

            CommandManager.Handle(str);
        }
        while (true);
    }
}