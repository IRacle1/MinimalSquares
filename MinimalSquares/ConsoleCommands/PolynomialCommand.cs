using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MinimalSquares.Components;
using MinimalSquares.Functions;

namespace MinimalSquares.ConsoleCommands
{
    public class PolynomialCommand : BaseCommand
    {
        public PolynomialCommand()
            : base("полином", "")
        {
                
        }

        public override void Handle()
        {
            while (true)
            {
                CommandManager.WriteText("Введите максимальную степень полинома: ");

                int result = CommandManager.IntReadLine();

                ComponentManager.Get<FunctionManager>()!.CurrentFunctions.Add(new PolynomialFunction(result));
                CommandManager.WriteLineText("Успешно!", CommandStatus.Success);

                break;
            }
        }
    }
}
