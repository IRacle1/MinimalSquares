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
using MinimalSquares.Graphics;

namespace MinimalSquares.ConsoleCommands
{
    public class SaveCommand : BaseCommand
    {
        PointManager pointManager = null!;
        FileStream SaveFile = null!;

        public SaveCommand() : base("сохранить", "")
        {  
            pointManager = ComponentManager.Get<PointManager>()!;
        }

        public override async void Handle()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var point in pointManager.Points)
            {
                stringBuilder.AppendLine(point.X + " " + point.Y);
            }
            
            byte[] savebuffer = Encoding.Default.GetBytes(stringBuilder.ToString());

            //можно и лучше
            if (!File.Exists("СписокТочек.pt"))
            {
                SaveFile = File.OpenWrite("СписокТочек.pt");
                SaveFile.Write(savebuffer, 0, savebuffer.Length);
            }
            else 
            {
                for (int i = 0; ; i++)
                {
                    if (!File.Exists($"СписокТочек{i}.pt"))
                    {
                        SaveFile = File.OpenWrite($"СписокТочек{i}.pt");
                        await SaveFile.WriteAsync(savebuffer, 0, savebuffer.Length);
                        CommandManager.WriteLineText($"Сохранено в СписокТочек{i}.pt", CommandStatus.Success);
                        break;
                    }
                }
            }
            SaveFile.Close();
        }
    }
}
