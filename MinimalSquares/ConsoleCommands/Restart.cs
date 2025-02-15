using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalSquares.ConsoleCommands
{
    internal class Restart : BaseCommand
    {
        public Restart() : base("рестарт", "resetting all") 
        {
        
        }
        public override void Handle()
        {
            CommandManager.WriteText("Test", CommandStatus.Success);
        }
    }
}
