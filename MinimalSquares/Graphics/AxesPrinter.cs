using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MinimalSquares.Components;
using MinimalSquares.Components.Abstractions;
using System;
using System.Collections.Generic;

namespace MinimalSquares.Graphics
{
    public class AxesPrinter : BaseComponent, IDrawableComponent
    {
        Dictionary<float, float> StepToGridElem { get; } = new()
        {
            { 0.05f, 10f },
            { 0.005f, 1f },
            { 0.0025f, 0.5f },
            { 0.0005f, 0.25f },
        };

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

            float gridValue = 0.001f;

            foreach (var item in StepToGridElem)
            {
                if (MainView.GrafhicStep >= item.Key)
                {
                    gridValue = item.Value;
                    break;
                }
            }
            Vector3 renderLeftUp = view.RenderLeftUpBorder;
            Vector3 renderRightDown = view.RenderRightDownBorder;

            if (renderLeftUp.X <= 0f && renderRightDown.X >= 0f)
            {
                for (int i = 0; i < 5; i++)
                {
                    linesCache.Add(new() { Color = view.MainColor, Position = new Vector3(i * MainView.GrafhicStep, renderRightDown.Y, 0f) });
                    linesCache.Add(new() { Color = view.MainColor, Position = new Vector3(i * MainView.GrafhicStep, renderLeftUp.Y, 0f) });
                    linesCache.Add(new() { Color = view.MainColor, Position = new Vector3(-i * MainView.GrafhicStep, renderRightDown.Y, 0f) });
                    linesCache.Add(new() { Color = view.MainColor, Position = new Vector3(-i * MainView.GrafhicStep, renderLeftUp.Y, 0f) });
                }
            }
            if (renderLeftUp.Y >= 0f && renderRightDown.Y <= 0f)
            {
                for (int i = 0; i < 5; i++)
                {
                    linesCache.Add(new() { Color = view.MainColor, Position = new Vector3(renderLeftUp.X, i * MainView.GrafhicStep, 0f) });
                    linesCache.Add(new() { Color = view.MainColor, Position = new Vector3(renderRightDown.X, i * MainView.GrafhicStep, 0f) });
                    linesCache.Add(new() { Color = view.MainColor, Position = new Vector3(renderLeftUp.X, -i * MainView.GrafhicStep, 0f) });
                    linesCache.Add(new() { Color = view.MainColor, Position = new Vector3(renderRightDown.X, -i * MainView.GrafhicStep, 0f) });
                }
            }

            for (float x = FloorBy(renderLeftUp.X, gridValue); x < CeilingBy(renderRightDown.X, gridValue); x += gridValue)
            {
                linesCache.Add(new() { Color = view.MainColor, Position = new Vector3(x, renderRightDown.Y, 0f) });
                linesCache.Add(new() { Color = view.MainColor, Position = new Vector3(x, renderLeftUp.Y, 0f) });
            }

            for (float y = FloorBy(renderRightDown.Y, gridValue); y < CeilingBy(renderLeftUp.Y, gridValue); y += gridValue)
            {
                linesCache.Add(new() { Color = view.MainColor, Position = new Vector3(renderLeftUp.X, y, 0f) });
                linesCache.Add(new() { Color = view.MainColor, Position = new Vector3(renderRightDown.X, y, 0f) });
            }

            allLines = linesCache.ToArray();
        }

        public float FloorBy(float value, float by)
        {
            return (MathF.Floor(value / by) - 1) * by;
        }

        public float CeilingBy(float value, float by)
        {
            return (MathF.Ceiling(value / by) + 1) * by;
        }
    }
}
