using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

using MinimalSquares.Components;
using MinimalSquares.Functions;
using MinimalSquares.Generic;

using Sharprompt;

namespace MinimalSquares.ConsoleCommands.Commands
{
    [ConsoleCommand]
    public class ReportCommand : BaseCommand
    {
        private FunctionManager functionManager = null!;
        private PointManager pointsManager = null!;

        private List<Func<object?, ValidationResult?>> validators = new(1);

        public ReportCommand() : base("Репорт", new string[] { "Report", }, "Формирует информацию по функциям в файл")
        {
            functionManager = ComponentManager.Get<FunctionManager>()!;
            pointsManager = ComponentManager.Get<PointManager>()!;
            validators.Add(Extensions.ValidateFileName("Это название не может быть именем файла"));
        }

        public override void Handle()
        {
            if (!Directory.Exists("Reports"))
            {
                Directory.CreateDirectory("Reports");
            }

            string file = Prompt.Input<string>(BuildMessage("Введите название файла для сохранения"), $"report_{DateTime.Now:HH-mm-ss}", validators: validators) + ".md";

            string fullPath = Path.Combine("Reports", file);

            if (File.Exists(fullPath))
            {
                if (!Prompt.Confirm(BuildMessage("Файл с таким названием уже существует, хотите перезаписать его?")))
                {
                    return;
                }
            }

            string result = ComponentManager.ReportManager.GetGeneralReport(functionManager.CurrentFunctions, pointsManager.Points);

            File.WriteAllText(fullPath, result);

            Success();
        }
    }
}
