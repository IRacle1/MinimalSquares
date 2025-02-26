using MinimalSquares.Components;
using MinimalSquares.Functions;
using MinimalSquares.Graphics;

using Sharprompt;

namespace MinimalSquares.ConsoleCommands.Commands
{
    [ConsoleCommand]
    public class PolynomialCommand : BaseCommand
    {
        private FunctionManager functionManager;

        public PolynomialCommand()
            : base("Полином", new string[] { "Polymonial" }, "")
        {
            functionManager = ComponentManager.Get<FunctionManager>()!;
        }

        public override void Handle()
        {
            int pow = Prompt.Input<int>(BuildMessage("Введите максимальную степень полинома"));

            functionManager.CurrentFunctions.Clear();
            functionManager.CurrentFunctions.Add(new PolynomialFunction(pow + 1));

            functionManager.UpdateParameters();
            ComponentManager.MainView.RenderRequest(RenderRequestType.Function);
            Success();
        }
    }
}
