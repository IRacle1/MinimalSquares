using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalSquares.ConsoleCommands
{
    public abstract class BaseCommand
    {
        protected BaseCommand(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; }
        public string Description { get; }

        public abstract void Handle();

        protected abstract string GetResponse(CommandStatus status);
    }

    public enum CommandStatus
    {
        None,
        Invalid,
        Exit,
        Success
    }
}
