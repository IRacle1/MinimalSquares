using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MinimalSquares.Components;
using MinimalSquares.Functions;
using MinimalSquares.Graphics;

namespace MinimalSquares.ConsoleCommands
{
    public class LoadPointDataset : BaseCommand
    {
        PointManager pointManager = null!;

        public LoadPointDataset() : base("загрузить", "")
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
            Vector3 vector;

            foreach (var Line in SaveFile) 
            {
                var a = Line.Split(' ').Select(float.Parse).ToArray();
                vector = new Vector3(a[0], a[1], 0f);
                pointManager.SetNewPoint(vector);
            }

            CommandManager.WriteLineText(";=;", CommandStatus.Success);
        }
    }
}
