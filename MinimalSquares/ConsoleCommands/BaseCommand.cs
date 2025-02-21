using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalSquares.ConsoleCommands
{
    public abstract class BaseCommand
    {
        protected BaseCommand(string name, string[] aliases, string description)
        {
            Name = name;
            Aliases = aliases;
            Description = description;
        }

        public string Name { get; }
        public string[] Aliases { get; }
        public string Description { get; }

        public abstract void Handle();

        protected void Success()
        {
            CommandManager.WriteLineText("Успешно!", CommandStatus.Success);
        }

        protected void Abort()
        {
            CommandManager.WriteLineText("Прервано", CommandStatus.Exit);
        }
    }

    public enum CommandStatus
    {
        None,
        Invalid,
        Exit,
        Success
    }
}
