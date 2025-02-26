using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MinimalSquares.ConsoleCommands
{
    public static class CommandManager
    {
        private static List<BaseCommand> allCommands { get; } = new();

        public static IReadOnlyList<BaseCommand> Commands => allCommands;

        public static Dictionary<string, BaseCommand> AliasesToCommands { get; } = new(10);

        static CommandManager()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => type.GetCustomAttribute<ConsoleCommandAttribute>() is not null);

            var objects = types.Select(Activator.CreateInstance);

            allCommands.AddRange(objects.OfType<BaseCommand>());

            foreach (BaseCommand command in Commands)
            {
                AliasesToCommands.Add(command.Name.ToLowerInvariant(), command);
                foreach (string alias in command.Aliases)
                {
                    AliasesToCommands.Add(alias.ToLowerInvariant(), command);
                }
            }
        }

        public static void Handle(string command)
        {
            if (AliasesToCommands.TryGetValue(command.ToLowerInvariant(), out BaseCommand? targetCommand))
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
