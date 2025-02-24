using Microsoft.Xna.Framework;
using MinimalSquares.Components;
using MinimalSquares.Functions;
using MinimalSquares.Generic;
using MinimalSquares.Graphics;

namespace MinimalSquares.ConsoleCommands.Commands
{
    [ConsoleCommand]
    public class RestartCommand : BaseCommand
    {
        private MainView mainView;
        private PointManager pointManager;
        private FunctionManager functionManager;

        public RestartCommand() : base("Рестарт", new string[] { "Restart" }, "откатывает программу")
        {
            mainView = ComponentManager.Get<MainView>()!;
            pointManager = ComponentManager.Get<PointManager>()!;
            functionManager = ComponentManager.Get<FunctionManager>()!;
        }

        public override void Handle()
        {
            mainView.SetCamera(new Vector3(0, 0, 6), Vector3.Zero);
            pointManager.Points.Clear();
            functionManager.CurrentFunctions.Clear();

            pointManager.TriggerUpdate();

            ComponentManager.UpdateVertexes();

            Success();
        }
    }
}
