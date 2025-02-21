using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MinimalSquares.Components;
using MinimalSquares.Components.Abstractions;

namespace MinimalSquares.Graphics
{
    public class AxesPrinter : BaseComponent, IDrawableComponent
    {
        private List<VertexPositionColor> linesCache = new(100);

        private MainView view = null!;
        private VertexPositionColor[] allLines = null!;

        public int Order { get; } = 0;

        public override void Start(MainGame game)
        {
            base.Start(game);
            view = ComponentManager.Get<MainView>()!;

            UpdateVertex();
        }

        public void Draw()
        {
            if (allLines.Length > 0)
                targetGame.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, allLines, 0, allLines.Length / 2);
        }

        public void UpdateVertex()
        {
            linesCache.Clear();
            Vector3 leftUp = view.LeftUpBorder;
            Vector3 rightDown = view.RightDownBorder;
            Vector3 renderLeftUp = view.RenderLeftUpBorder;
            Vector3 renderRightDown = view.RenderRightDownBorder;
            for (int i = 0; i < 5; i++)
            {
                if (renderLeftUp.X <= 0f && renderRightDown.X >= 0f)
                {
                    linesCache.Add(new() { Color = view.MainColor, Position = new Vector3(i * MainView.GrafhicStep, renderRightDown.Y, 0f) });
                    linesCache.Add(new() { Color = view.MainColor, Position = new Vector3(i * MainView.GrafhicStep, renderLeftUp.Y, 0f) });
                    linesCache.Add(new() { Color = view.MainColor, Position = new Vector3(-i * MainView.GrafhicStep, renderRightDown.Y, 0f) });
                    linesCache.Add(new() { Color = view.MainColor, Position = new Vector3(-i * MainView.GrafhicStep, renderLeftUp.Y, 0f) });   
                }
                if (renderLeftUp.Y >= 0f && renderRightDown.Y <= 0f)
                {
                    linesCache.Add(new() { Color = view.MainColor, Position = new Vector3(renderLeftUp.X, i * MainView.GrafhicStep, 0f) });
                    linesCache.Add(new() { Color = view.MainColor, Position = new Vector3(renderRightDown.X, i * MainView.GrafhicStep, 0f) });
                    linesCache.Add(new() { Color = view.MainColor, Position = new Vector3(renderLeftUp.X, -i * MainView.GrafhicStep, 0f) });
                    linesCache.Add(new() { Color = view.MainColor, Position = new Vector3(renderRightDown.X, -i * MainView.GrafhicStep, 0f) });
                }
            }

            for (float x = MathF.Floor(renderLeftUp.X); x < MathF.Ceiling(renderRightDown.X); x++)
            {
                linesCache.Add(new() { Color = view.MainColor, Position = new Vector3(x, renderRightDown.Y, 0f) });
                linesCache.Add(new() { Color = view.MainColor, Position = new Vector3(x, renderLeftUp.Y, 0f) });
            }

            for (float y = MathF.Floor(renderRightDown.Y); y < MathF.Ceiling(renderLeftUp.Y); y++)
            {
                linesCache.Add(new() { Color = view.MainColor, Position = new Vector3(renderLeftUp.X, y, 0f) });
                linesCache.Add(new() { Color = view.MainColor, Position = new Vector3(renderRightDown.X, y, 0f) });
            }

            allLines = linesCache.ToArray();
        }
    }
}
