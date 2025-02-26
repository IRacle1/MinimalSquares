using MinimalSquares.ConsoleCommands;
using Sharprompt;
using System.Threading.Tasks;

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
        Prompt.ThrowExceptionOnCancel = true;
        do
        {
            try
            {
                string command = Prompt.Input<string>("Название команды");

                if (string.IsNullOrWhiteSpace(command))
                    continue;

                CommandManager.Handle(command);
            }
            catch (PromptCanceledException)
            {
                CommandManager.WriteLineText("[Прервано]", CommandStatus.Exit);
            }
        }
        while (true);
    }
}