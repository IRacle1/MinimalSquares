using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;

using MinimalSquares.Components;
using MinimalSquares.Components.Abstractions;

namespace MinimalSquares.Graphics
{
    internal class BackgroundComponent : BaseComponent, IDrawableComponent
    {
        public int Order { get; } = int.MinValue;

        public override void Start(MainGame game)
        {
            base.Start(game);
        }

        public void Draw()
        {
            targetGame.GraphicsDevice.Clear(ComponentManager.MainView.BackgroundColor);
        }

        public void UpdateVertex() { }
    }
}
