using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using MinimalSquares.Graphics;
using MinimalSquares.Functions;
using MinimalSquares.Components;
using MinimalSquares.Generic;

namespace MinimalSquares.ConsoleCommands
{
    public class RestartCommand : BaseCommand
    {
        private MainView mainView;
        private PointManager pointManager;
        private FunctionManager functionsManager;

        public RestartCommand() : base("рестарт", new string[] { "restart" }, "откатывает программу") 
        {
            mainView = ComponentManager.Get<MainView>()!;
            pointManager = ComponentManager.Get<PointManager>()!;
            functionsManager = ComponentManager.Get<FunctionManager>()!;
        }

        public override void Handle()
        {
            mainView.SetCamera(new Vector3(0, 0, 6), Vector3.Zero);
            pointManager.Points.Clear();
            functionsManager.CurrentFunctions.Clear();

            ComponentManager.UpdateVertexes();

            Success();
        }
    }
}
