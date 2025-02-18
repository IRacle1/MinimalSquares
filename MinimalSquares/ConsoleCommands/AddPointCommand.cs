using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MinimalSquares.Components;
using MinimalSquares.Graphics;

namespace MinimalSquares.ConsoleCommands
{
    public class AddPoint : BaseCommand
    {
        PointManager pointManager;

        public AddPoint() : base("точка", "добавляет точку по координатам x, y") 
        {
            pointManager = ComponentManager.Get<PointManager>()!;
        }
        
        public override void Handle()
        {
            Vector3 point = Vector3.Zero;
            CommandManager.WriteText("Введите координату X: ");
            point.X = CommandManager.IntReadLine();
            CommandManager.WriteText("Введите координату Y: ");
            point.Y = CommandManager.IntReadLine();
            pointManager.SetNewPoint(point);
        }
    }
}
