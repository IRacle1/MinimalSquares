using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace MinimalSquares.ConsoleCommands
{
    public class HelpCommand : BaseCommand
    {
        public HelpCommand() : base("помощь", new string[] { "help", }, "Помощь по всем командам")
        {
            
        }

        public override void Handle()
        {
            foreach (var item in CommandManager.Commands.OrderBy(t => t.Name))
            {
                CommandManager.WriteLineText($"{item.Name} : {item.Description}");
            }
        }
    }
}
