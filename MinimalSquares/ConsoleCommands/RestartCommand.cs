using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using MinimalSquares.Graphics;
using MinimalSquares.Functions;
using MinimalSquares.Components;

namespace MinimalSquares.ConsoleCommands
{
    public class RestartCommand : BaseCommand
    {
        MainView mainView;
        PointManager pointManager;
        FunctionManager functionsManager;

        public RestartCommand() : base("рестарт", Array.Empty<string>(), "откатывает программу") 
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
