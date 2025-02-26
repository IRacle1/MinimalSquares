using System.Linq;

namespace MinimalSquares.ConsoleCommands.Commands
{
    [ConsoleCommand]
    public class HelpCommand : BaseCommand
    {
        public HelpCommand() : base("Помощь", new string[] { "Help", }, "Помощь по всем командам")
        {

        }

        public override void Handle()
        {
            foreach (var item in CommandManager.Commands.OrderBy(t => t.Name))
            {
                CommandManager.WriteLineText($"'{item.Name}' : {item.Description}");
            }
        }
    }
}
