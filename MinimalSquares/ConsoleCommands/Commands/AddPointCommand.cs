using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MinimalSquares.Components;
using MinimalSquares.Graphics;
using MinimalSquares.Generic;
using Sharprompt;

namespace MinimalSquares.ConsoleCommands.Commands
{
    [ConsoleCommand]
    public class AddPoint : BaseCommand
    {
        private PointManager pointManager = null!;

        public AddPoint() : base("Точка", new string[] { "Point", "Pt" }, "добавляет точку по координатам x, y") 
        {
            pointManager = ComponentManager.Get<PointManager>()!;
        }
        
        public override void Handle()
        {
            float x = Prompt.Input<float>(BuildMessage("Введите координату X"));
            float y = Prompt.Input<float>(BuildMessage("Введите координату X"));

            pointManager.Add(new(x, y));

            Success();
        }
    }
}
