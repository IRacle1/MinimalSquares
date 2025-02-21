using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
            new SaveCommand(),
            new LoadPointDataset(),
            new HelpCommand(),
        };

        public static void Handle(string command)
        {
            if (Commands.Find(c => c.Name.Equals(command, StringComparison.InvariantCultureIgnoreCase) || c.Aliases.Any(al => al.Equals(command, StringComparison.InvariantCultureIgnoreCase))) is BaseCommand targetCommand)
            {
                targetCommand.Handle();
            }
            else
            {
                WriteLineText($"Команда '{command}' не найдена!", CommandStatus.Invalid);
            }
        }

        public static void WriteLineText(string text, CommandStatus status = CommandStatus.None, bool space = true)
        {
            if (space)
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

        public static void WriteText(string text, CommandStatus status = CommandStatus.None, bool space = true)
        {
            if (space)
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

        public static bool TryReadBool(string message, [NotNullWhen(true)] out bool? toReturn)
        {
            return TryReadAbstact(message, out toReturn, str => (string.Equals(str, "Y", StringComparison.InvariantCultureIgnoreCase), true));
        }

        public static bool TryReadString(string message, out string? toReturn)
        {
            return TryReadAbstact(message, out toReturn, str => (str, true));
        }

        public static bool TryReadScalarLine<T>(string message, [NotNullWhen(true)] out T? toReturn)
        {
            return TryReadAbstact(message, out toReturn, GetScalar<T>);
        }

        private static (T?, bool) GetScalar<T>(string? str)
        {
            MethodInfo method = typeof(T).GetMethod("TryParse", BindingFlags.Static | BindingFlags.Public, new Type[] { typeof(string), typeof(T).MakeByRefType() })!;

            object[] invokeParams = new object[] { str!, default(T)! };
            if (string.IsNullOrWhiteSpace(str) || !(bool)method.Invoke(null, invokeParams)!)
            {
                return (default, false);
            }

            return ((T)invokeParams[1], true);
        }

        public static bool TryReadAbstact<T>(string message, out T? toReturn, Func<string?, (T?, bool)> objectFunction)
        {
            while (true)
            {
                WriteText(message);
                string str = Console.ReadLine()!;

                if (string.Equals(str, "выход", StringComparison.InvariantCultureIgnoreCase))
                {
                    toReturn = default;
                    return false;
                }

                var result = objectFunction.Invoke(str);
                if (!result.Item2)
                {
                    WriteLineText("Значение введено неверно!", CommandStatus.Invalid);
                    continue;
                }

                toReturn = result.Item1;

                return true;
            }
        }
    }
}
