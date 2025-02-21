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
            : base("полином", Array.Empty<string>(), "")
        {
            functionManager = ComponentManager.Get<FunctionManager>()!;
        }

        public override void Handle()
        {
            if (!CommandManager.TryReadScalarLine("Введите максимальную степень полинома: ", out int pow))
            {
                Abort();
                return;
            }

            functionManager.CurrentFunctions.Add(new PolynomialFunction(pow));
            Success();
        }
    }
}
