using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MinimalSquares.Components;
using MinimalSquares.Functions;
using MinimalSquares.Generic;
using MinimalSquares.Graphics;

namespace MinimalSquares.ConsoleCommands
{
    public class LoadPointDataset : BaseCommand
    {
        private PointManager pointManager = null!;

        public LoadPointDataset() : base("загрузить", new string[] { "load", "ld" }, "")
        {
            pointManager = ComponentManager.Get<PointManager>()!;
        }

        public override void Handle()
        {
            string path;
            do
            {
                CommandManager.WriteText("Путь до файла: ");
                path = Console.ReadLine()!;
                if (File.Exists(path)) break;
                else CommandManager.WriteLineText("Неверный путь", CommandStatus.Invalid);
            } while (true);

            string[] SaveFile = File.ReadAllLines(path);
            Vector2 vector;

            foreach (var Line in SaveFile) 
            {
                float[] a = Line.Split(' ').Select(float.Parse).ToArray();
                vector = new Vector2(a[0], a[1]);
                pointManager.SetNewPoint(vector, false);
            }

            pointManager.TriggerUpdate();

            Success();
        }
    }
}
