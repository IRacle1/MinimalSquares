using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MinimalSquares.ConsoleCommands
{
    public class CommandManager
    {
        public static List<BaseCommand> Commands { get; } = new(10)
        {
            new PolynomialCommand(),
            new RestartCommand(),
            new AddPoint(),  
        };

        public static void Handle(string command)
        {
            if (Commands.Find(c => c.Name == command.ToLowerInvariant()) is BaseCommand targetCommand)
            {
                targetCommand.Handle();
            }
            else
            {
                WriteLineText($"Команда '{command}' не найдена!", CommandStatus.Invalid);
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

        public static T TryReadObjectLine<T>(string message)
            where T : struct
        {
            MethodInfo method = typeof(T).GetMethod("TryParse", BindingFlags.Static | BindingFlags.Public, new Type[] { typeof(string), typeof(T).MakeByRefType() })!;

            while (true)
            {
                WriteText(message);
                string? str = Console.ReadLine();
                object[] invokeParams = new object[] { str!, default(T) };
                if (string.IsNullOrWhiteSpace(str) || !(bool)method.Invoke(null, invokeParams)!)
                {
                    WriteLineText("Значение введено неверно!", CommandStatus.Invalid);
                    continue;
                }
                return (T)invokeParams[1];
            }
        }
    }
}
