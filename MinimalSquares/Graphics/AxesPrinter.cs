using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MinimalSquares.Components;
using MinimalSquares.Components.Abstractions;

namespace MinimalSquares.Graphics
{
    public class AxesPrinter : BaseComponent, IDrawableComponent
    {
        private MainView view;
        
        private VertexPositionColor[] mainAxes = null!;
        private VertexPositionColor[] NonPrimaryGrid = null!;

        public int Order { get; } = 0;

        public override void Start(MainGame game)
        {
            base.Start(game);
            view = ComponentManager.Get<MainView>()!;

            UpdateVertex();
        }

        public void Draw()
        {
            targetGame.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, mainAxes, 0, mainAxes.Length / 2);
            targetGame.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, NonPrimaryGrid, 0, NonPrimaryGrid.Length / 2);
        }

        public void UpdateVertex()
        {
            List<VertexPositionColor> axes = new(100) {
                new() { Color = view.MainColor, Position = new Vector3(-10f, 0f, 0f) },
                new() { Color = view.MainColor, Position = new Vector3(10f, 0f, 0f) },
                new() { Color = view.MainColor, Position = new Vector3(0f, -10f, 0f) },
                new() { Color = view.MainColor, Position = new Vector3(0f, 10f, 0f) },
            };

            for (int i = 1; i < 5; i++)
            {
                axes.Add(new() { Color = view.MainColor, Position = new Vector3(-10f, i * Program.GrafhicStep, 0f) });
                axes.Add(new() { Color = view.MainColor, Position = new Vector3(10f, i * Program.GrafhicStep, 0f) });
                axes.Add(new() { Color = view.MainColor, Position = new Vector3(-10f, -i * Program.GrafhicStep, 0f) });
                axes.Add(new() { Color = view.MainColor, Position = new Vector3(10f, -i * Program.GrafhicStep, 0f) });

                axes.Add(new() { Color = view.MainColor, Position = new Vector3(i * Program.GrafhicStep, -10f, 0f) });
                axes.Add(new() { Color = view.MainColor, Position = new Vector3(i * Program.GrafhicStep, 10f, 0f) });
                axes.Add(new() { Color = view.MainColor, Position = new Vector3(-i * Program.GrafhicStep, -10f, 0f) });
                axes.Add(new() { Color = view.MainColor, Position = new Vector3(-i * Program.GrafhicStep, 10f, 0f) });
            }

            mainAxes = axes.ToArray();

            List<VertexPositionColor> grid = new(100);
            for (float x = -5f; x < 5f; x++)
            {
                grid.Add(new() { Color = view.MainColor, Position = new Vector3(x, -10f, 0f) });
                grid.Add(new() { Color = view.MainColor, Position = new Vector3(x, 10f, 0f) });
            }

            for (float y = -5f; y < 5f; y++)
            {
                grid.Add(new() { Color = view.MainColor, Position = new Vector3(-10f, y, 0f) });
                grid.Add(new() { Color = view.MainColor, Position = new Vector3(10f, y, 0f) });
            }

            NonPrimaryGrid = grid.ToArray();
        }
    }
}
