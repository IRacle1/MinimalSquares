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
        private PointManager pointManager = null!;

        public AddPoint() : base("точка", "добавляет точку по координатам x, y") 
        {
            pointManager = ComponentManager.Get<PointManager>()!;
        }
        
        public override void Handle()
        {
            Vector3 point = Vector3.Zero;
            point.X = CommandManager.TryReadObjectLine<float>("Введите координату X: ");
            point.Y = CommandManager.TryReadObjectLine<float>("Введите координату Y: ");
            pointManager.SetNewPoint(point);

            CommandManager.WriteLineText("Успешно!", CommandStatus.Success);
        }
    }
}
