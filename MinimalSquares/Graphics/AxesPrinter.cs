using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MinimalSquares.Components;
using MinimalSquares.Components.Abstractions;

namespace MinimalSquares.Graphics
{
    public class AxesPrinter : BaseComponent, IDrawableComponent
    {
        private List<VertexPositionColor> axes = new(100);
        private List<VertexPositionColor> grid = new(100);

        private MainView view = null!;
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
            if (mainAxes.Length > 0)
                targetGame.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, mainAxes, 0, mainAxes.Length / 2);
            if (NonPrimaryGrid.Length > 0)
                targetGame.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, NonPrimaryGrid, 0, NonPrimaryGrid.Length / 2);
        }

        public void UpdateVertex()
        {
            axes.Clear();
            Vector3 leftUp = view.LeftUpBorder;
            Vector3 rightDown = view.RightDownBorder;

            for (int i = 0; i < 5; i++)
            {
                if (leftUp.X < 0f && rightDown.X > 0f)
                {
                    axes.Add(new() { Color = view.MainColor, Position = new Vector3(leftUp.X, i * Program.GrafhicStep, 0f) });
                    axes.Add(new() { Color = view.MainColor, Position = new Vector3(rightDown.X, i * Program.GrafhicStep, 0f) });
                    axes.Add(new() { Color = view.MainColor, Position = new Vector3(leftUp.X, -i * Program.GrafhicStep, 0f) });
                    axes.Add(new() { Color = view.MainColor, Position = new Vector3(rightDown.X, -i * Program.GrafhicStep, 0f) });
                }
                if (leftUp.Y > 0f && rightDown.Y < 0f)
                {
                    axes.Add(new() { Color = view.MainColor, Position = new Vector3(i * Program.GrafhicStep, rightDown.Y, 0f) });
                    axes.Add(new() { Color = view.MainColor, Position = new Vector3(i * Program.GrafhicStep, leftUp.Y, 0f) });
                    axes.Add(new() { Color = view.MainColor, Position = new Vector3(-i * Program.GrafhicStep, rightDown.Y, 0f) });
                    axes.Add(new() { Color = view.MainColor, Position = new Vector3(-i * Program.GrafhicStep, leftUp.Y, 0f) });
                }
            }

            mainAxes = axes.ToArray();

            grid.Clear();
            for (float x = MathF.Floor(leftUp.X); x < MathF.Ceiling(rightDown.X); x++)
            {
                grid.Add(new() { Color = view.MainColor, Position = new Vector3(x, rightDown.Y, 0f) });
                grid.Add(new() { Color = view.MainColor, Position = new Vector3(x, leftUp.Y, 0f) });
            }

            for (float y = MathF.Floor(rightDown.Y); y < MathF.Ceiling(leftUp.Y); y++)
            {
                grid.Add(new() { Color = view.MainColor, Position = new Vector3(leftUp.X, y, 0f) });
                grid.Add(new() { Color = view.MainColor, Position = new Vector3(rightDown.X, y, 0f) });
            }

            NonPrimaryGrid = grid.ToArray();
        }
    }
}
