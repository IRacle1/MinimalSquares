using MinimalSquares.Components;
using MinimalSquares.Generic;
using Sharprompt;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace MinimalSquares.ConsoleCommands.Commands
{
    [ConsoleCommand]
    public class SaveCommand : BaseCommand
    {
        private PointManager pointManager = null!;
        private List<Func<object?, ValidationResult?>> validators = new(1);

        public SaveCommand() : base("Сохранить", new string[] { "Save" }, "")
        {
            pointManager = ComponentManager.Get<PointManager>()!;
            validators.Add(Extensions.ValidateFileName("Это название не может быть именем файла"));
        }

        public override void Handle()
        {
            if (!Directory.Exists("Points"))
            {
                Directory.CreateDirectory("Points");
            }

            string file = Prompt.Input<string>(BuildMessage("Введите название файла"), $"points_{DateTime.Now:HH-mm-ss}", validators: validators) + ".txt";

            string fullPath = Path.Combine("Points", file);

            if (File.Exists(fullPath))
            {
                if (!Prompt.Confirm(BuildMessage("Файл с таким названием уже существует, хотите перезаписать его?")))
                {
                    return;
                }
            }

            string targetText = string.Join(Environment.NewLine, pointManager.Points.Select(p => $"{p.X} {p.Y}"));

            File.WriteAllText(fullPath, targetText);

            Success();
        }
    }
}
