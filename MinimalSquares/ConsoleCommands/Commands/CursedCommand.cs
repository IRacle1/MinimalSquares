using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MinimalSquares.Components;
using MinimalSquares.Functions;
using MinimalSquares.Functions.Special;
using MinimalSquares.Graphics;

using Sharprompt;

namespace MinimalSquares.ConsoleCommands.Commands
{
    [ConsoleCommand]
    class CursedCommand : BaseCommand
    {
        private FunctionManager functionManager = null!;

        public CursedCommand() : base("Эксперемент", new string[] { "Cursed" }, "Взаимодействие со странными функциями, созданными по приколу")
        {
            functionManager = ComponentManager.Get<FunctionManager>()!;
        }

        public List<AbstractFunction> CursedFunctions = new()
        {
            new ExponentPolynomial(new PolynomialFunction(3)),
            new LinearSin(),
            new SquareRoot(),
        };

        public override void Handle()
        {
            AbstractFunction function = Prompt.Select(BuildMessage("Выберете функцию из списка"), CursedFunctions, 5);
            functionManager.CurrentFunctions.Clear();
            functionManager.CurrentFunctions.Add(function);
            functionManager.UpdateParameters();

            ComponentManager.MainView.RenderRequest(RenderRequestType.Function);

            Success();
        }
    }
}
