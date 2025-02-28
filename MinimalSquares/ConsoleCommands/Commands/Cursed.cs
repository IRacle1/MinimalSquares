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
    class Cursed : BaseCommand
    {
        private FunctionManager functionManager = null!;

        public Cursed() : base("Эксперемент", new string[] { "Cursed" }, "Взаимодействие со странными функциями, созданными по приколу")
        {
            functionManager = ComponentManager.Get<FunctionManager>()!;
        }


        public List<BaseFunction> CursedFunctions = new()
        {
            new ExponentPolynomial(3),
        };

        public override void Handle()
        {
            BaseFunction function = Prompt.Select(BuildMessage("Выберете функцию из списка"), CursedFunctions, 5);
            functionManager.CurrentFunctions.Clear();
            functionManager.CurrentFunctions.Add(function);
            functionManager.UpdateParameters();

            ComponentManager.MainView.RenderRequest(RenderRequestType.Function);

            Success();
        }
    }
}
