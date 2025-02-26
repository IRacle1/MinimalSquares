using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathNet.Numerics.Distributions;

using Microsoft.Xna.Framework;

using MinimalSquares.Components;
using MinimalSquares.Functions;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MinimalSquares.Generic
{
    public class ReportManager : BaseComponent
    {
        public const int CharOffset = 'a';

        public const string DirectoryPath = "Reports";
        public const string FunctionTemplate = "functionTemplate.md";
        public const string ReportTemplate = "reportTemplate.md";

        public string FunctionFormat { get; private set; } = null!;
        public string GeneralFormat { get; private set; } = null!;

        public override void Start(MainGame game)
        {
            base.Start(game);

            if (Directory.Exists(DirectoryPath))
                Directory.CreateDirectory(DirectoryPath);

            if (!File.Exists(FunctionTemplate) || !File.Exists(ReportTemplate))
            {
                Console.WriteLine("Файлы с шблоном репортов не найдены, функции связанные с ними не будут работать!");
                return;
            }

            FunctionFormat = File.ReadAllText(FunctionTemplate);
            GeneralFormat = File.ReadAllText(ReportTemplate);
        }

        public char GetVariableName(int index)
        {
            return (char)(CharOffset + index);
        }

        public string GetParametersTable(BaseFunction function)
        {
            StringBuilder parameters = new("|");
            for (int i = 0; i < function.MonomialCount; i++)
            {
                parameters.AppendFormat("{0}|", ComponentManager.ReportManager.GetVariableName(i));
            }

            parameters.AppendLine()
                .Append('|');

            for (int i = 0; i < function.MonomialCount; i++)
            {
                parameters.Append("-|");
            }

            parameters.AppendLine()
                .Append('|');

            for (int i = 0; i < function.MonomialCount; i++)
            {
                parameters.AppendFormat("{0}|", function.GetFormattedParameter(i));
            }

            return parameters.ToString();
        }

        public string GetPointsTable(IEnumerable<Vector2> points)
        {
            StringBuilder pointsStringBuilder = new();

            pointsStringBuilder.AppendLine("|x|y|")
                .AppendLine("|-|-|");

            foreach (Vector2 point in points)
            {
                pointsStringBuilder.AppendFormat("|{0}|{1}|", MathF.Round(point.X, 2), MathF.Round(point.Y, 2))
                    .AppendLine();
            }


            return pointsStringBuilder.ToString();
        }

        public string GetFunctionReport(BaseFunction function)
        {
            return string.Format(FunctionFormat, function.Name, function.GetGeneralNotation(), function.GetFunctionNotation(), GetParametersTable(function));
        }

        public string GetGeneralReport(IEnumerable<BaseFunction> functions, IEnumerable<Vector2> points)
        {
            StringBuilder functionsString = new();

            string pointsString = GetPointsTable(points);

            foreach (BaseFunction function in functions)
            {
                functionsString.AppendLine(GetFunctionReport(function));
            }

            return string.Format(GeneralFormat, pointsString, functionsString.ToString());

        }
    }
}
