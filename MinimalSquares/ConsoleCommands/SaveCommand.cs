using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;

using MinimalSquares.Components;
using MinimalSquares.Functions;
using MinimalSquares.Generic;
using MinimalSquares.Graphics;

namespace MinimalSquares.ConsoleCommands
{
    public class SaveCommand : BaseCommand
    {
        private PointManager pointManager = null!;

        public SaveCommand() : base("сохранить", new string[] { "save" }, "")
        {  
            pointManager = ComponentManager.Get<PointManager>()!;
        }

        public override void Handle()
        {
            if (!CommandManager.TryReadString("Введите название файла (пустой - стандартное название): ", out string? path))
            {
                Abort();
                return;
            }

            if (!Directory.Exists("Points"))
            {
                Directory.CreateDirectory("Points");
            }

            if(string.IsNullOrWhiteSpace(path))
                path = $"points_{DateTime.Now:HH-mm-ss}.txt";

            string fullPath = Path.Combine("Points", path);

            string targetText = string.Join(Environment.NewLine, pointManager.Points.Select(p => $"{p.X} {p.Y}"));

            //можно и лучше
            if (File.Exists(path))
            {
                if (!CommandManager.TryReadBool("Файл с таким названием уже существует, хотите перезаписать его?\n[Y - да/любое другое - нет]", out bool? flag))
                {
                    Abort();
                    return;
                }
            }

            File.WriteAllText(fullPath, targetText);

            Success();
        }
    }
}
