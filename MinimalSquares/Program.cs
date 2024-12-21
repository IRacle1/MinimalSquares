using System;
using System.Threading.Tasks;

namespace MinimalSquares;

internal class Program
{
    public static MainGame MainGame = null!;

    public const float Step = 0.0005f;
    public const float GrafhicStep = 0.005f;

    private static void Main(string[] args)
    {
        Task.Run(ConsoleHandle);
        MainGame = new MainGame();
        MainGame.Run();
    }

    private static void ConsoleHandle()
    {
    }
}