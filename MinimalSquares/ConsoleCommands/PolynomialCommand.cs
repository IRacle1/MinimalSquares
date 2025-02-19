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
        private FunctionManager functionManager;

        public PolynomialCommand()
            : base("полином", "")
        {
            functionManager = ComponentManager.Get<FunctionManager>()!;
        }

        public override void Handle()
        {
            int result = CommandManager.TryReadObjectLine<int>("Введите максимальную степень полинома: ");

            functionManager.CurrentFunctions.Add(new PolynomialFunction(result));
            CommandManager.WriteLineText("Успешно!", CommandStatus.Success);
        }
    }
}
