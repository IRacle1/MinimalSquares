using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MinimalSquares.Components;
using MinimalSquares.Functions;
using MinimalSquares.Graphics;
using Sharprompt;

namespace MinimalSquares.ConsoleCommands.Commands
{
    [ConsoleCommand]
    public class FunctionCommand : BaseCommand
    {
        private FunctionManager functionManager = null!;

        public FunctionCommand() : base("Функция", new string[] { "function" }, "Вручную выбирает функцию для апроксимации.")
        {
            functionManager = ComponentManager.Get<FunctionManager>()!;
        }

        public override void Handle()
        {
            AbstractFunction function = Prompt.Select(BuildMessage("Выберете функцию из списка"), functionManager.AvaibleFunctions, 5);
            functionManager.CurrentFunctions.Clear();
            functionManager.CurrentFunctions.Add(function);
            functionManager.UpdateParameters();

            ComponentManager.MainView.RenderRequest(RenderRequestType.Function);

            Success();
        }
    }
}
