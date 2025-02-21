using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MinimalSquares.Components;
using MinimalSquares.Graphics;
using MinimalSquares.Generic;

namespace MinimalSquares.ConsoleCommands
{
    public class AddPoint : BaseCommand
    {
        private PointManager pointManager = null!;

        public AddPoint() : base("точка", new string[] { "point", "pt" }, "добавляет точку по координатам x, y") 
        {
            pointManager = ComponentManager.Get<PointManager>()!;
        }
        
        public override void Handle()
        {
            if (!CommandManager.TryReadScalarLine("Введите координату X: ", out float x))
            {
                Abort();
                return;
            }
            if (!CommandManager.TryReadScalarLine("Введите координату Y: ", out float y))
            {
                Abort();
                return;
            }

            Vector2 point = new(x, y);
            pointManager.SetNewPoint(point, true);

            Success();
        }
    }
}
