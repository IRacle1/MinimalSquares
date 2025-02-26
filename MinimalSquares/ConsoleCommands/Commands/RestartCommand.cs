using Microsoft.Xna.Framework;
using MinimalSquares.Components;
using MinimalSquares.Functions;
using MinimalSquares.Generic;
using MinimalSquares.Graphics;

namespace MinimalSquares.ConsoleCommands.Commands
{
    [ConsoleCommand]
    public class RestartCommand : BaseCommand
    {
        private InternalManager internalManager;

        public RestartCommand() : base("Рестарт", new string[] { "Restart" }, "Откатывает программу на начальное состояние")
        {
            internalManager = ComponentManager.Get<InternalManager>()!;
        }

        public override void Handle()
        {
            internalManager.Reset();

            Success();
        }
    }
}
