using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalSquares.ConsoleCommands
{
    public class CommandManager
    {
        public static List<BaseCommand> Commands { get; } = new(10)
        {
            new PolynomialCommand(),
            new Restart(),
        };

        public static void Handle(string command)
        {
            if (Commands.Find(c => c.Name == command) is BaseCommand targetCommand)
            {
                targetCommand.Handle();
            }
            else
            {
                WriteLineText($"Команда '{command}'не найдена!", CommandStatus.Invalid);
            }
        }

        public static void WriteLineText(string text, CommandStatus status = CommandStatus.None)
        {
            Console.Write(" - ");

            ConsoleColor consoleColor = Console.ForegroundColor;

            switch (status)
            {
                case CommandStatus.Invalid:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case CommandStatus.Exit:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case CommandStatus.Success:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
            }

            Console.WriteLine(text);

            Console.ForegroundColor = consoleColor;
        }

        public static void WriteText(string text, CommandStatus status = CommandStatus.None)
        {
            Console.Write(" - ");

            ConsoleColor consoleColor = Console.ForegroundColor;

            switch (status)
            {
                case CommandStatus.Invalid:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case CommandStatus.Exit:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case CommandStatus.Success:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
            }

            Console.Write(text);

            Console.ForegroundColor = consoleColor;
        }
    }
}
