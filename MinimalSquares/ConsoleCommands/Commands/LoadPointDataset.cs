using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using MinimalSquares.Components;
using MinimalSquares.Functions;
using MinimalSquares.Generic;
using MinimalSquares.Graphics;

using Sharprompt;

namespace MinimalSquares.ConsoleCommands.Commands
{
    [ConsoleCommand]
    public class LoadPointDataset : BaseCommand
    {
        private PointManager pointManager = null!;

        public LoadPointDataset() : base("Загрузить", new string[] { "Load" }, "")
        {
            pointManager = ComponentManager.Get<PointManager>()!;
        }

        public override void Handle()
        {
            if (!Directory.Exists("Points"))
                Directory.CreateDirectory("Points");

            string[] avaibleFiles = Directory.GetFiles("Points", "*.txt").Select(file => Path.GetFileName(file)!).ToArray();
            
            string file = Prompt.Select(BuildMessage("Выберите файл точек из списка"), avaibleFiles, 5);
            string fullPath = Path.Combine("Points", file);

            string[] fileContent = File.ReadAllLines(fullPath);

            pointManager.Points.Clear();

            foreach (string Line in fileContent) 
            {
                float[] a = Line.Split(' ').Select(float.Parse).ToArray();
                Vector2 vector = new(a[0], a[1]);
                pointManager.Points.Add(vector);
            }

            pointManager.TriggerUpdate();

            Success();
        }
    }
}
