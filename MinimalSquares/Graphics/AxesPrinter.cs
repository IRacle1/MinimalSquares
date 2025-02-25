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
        private Dictionary<float, float> StepToGridElem { get; } = new()
        {
            //{ 250f, 50000f },
            //{ 100f, 20000f },
            //{ 50f, 10000f },

            //{ 25f, 5000f },
            //{ 10f, 2000f },
            //{ 5f, 1000f },

            //{ 2.5f, 500f },
            //{ 1.0f, 200f },
            //{ 0.5f, 100f },

            //{ 0.25f, 50f },
            //{ 0.10f, 20f },
            //{ 0.05f, 10f },

            { 0.025f, 5f },
            { 0.010f, 2f },
            { 0.005f, 1f },

            { 0.0025f, 0.5f },
            { 0.0010f, 0.2f },
            { 0.0005f, 0.1f },

            { 0.00025f, 0.05f },
            { 0.00010f, 0.02f },
            { 0.00005f, 0.01f },

            { 0.000025f, 0.005f },
            { 0.000010f, 0.002f },
            { 0.000005f, 0.001f },
        };

        private List<VertexPositionColor> linesCache = new(100);

        private VertexPositionColor[] allLines = null!;

        public int Order { get; } = 0;

        public override void Start(MainGame game)
        {
            base.Start(game);

            ComponentManager.MainView.OnViewUpdate += OnViewUpdate;

            UpdateVertex();
        }

        public void Draw()
        {
            if (allLines.Length > 0)
                targetGame.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, allLines, 0, allLines.Length / 2);
        }

        private void OnViewUpdate(RenderRequestType renderRequestType)
        {
            if (!renderRequestType.HasFlag(RenderRequestType.General))
                return;

            UpdateVertex();
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
            Vector3 renderLeftUp = ComponentManager.MainView.RenderLeftUpBorder;
            Vector3 renderRightDown = ComponentManager.MainView.RenderRightDownBorder;
            Color mainColor = ComponentManager.MainView.MainColor;

            if (renderLeftUp.X <= 0f && renderRightDown.X >= 0f)
            {
                for (int i = 0; i < 5; i++)
                {
                    linesCache.Add(new() { Color = mainColor, Position = new Vector3(i * MainView.GrafhicStep, renderRightDown.Y, 0f) });
                    linesCache.Add(new() { Color = mainColor, Position = new Vector3(i * MainView.GrafhicStep, renderLeftUp.Y, 0f) });
                    linesCache.Add(new() { Color = mainColor, Position = new Vector3(-i * MainView.GrafhicStep, renderRightDown.Y, 0f) });
                    linesCache.Add(new() { Color = mainColor, Position = new Vector3(-i * MainView.GrafhicStep, renderLeftUp.Y, 0f) });
                }
            }
            if (renderLeftUp.Y >= 0f && renderRightDown.Y <= 0f)
            {
                for (int i = 0; i < 5; i++)
                {
                    linesCache.Add(new() { Color = mainColor, Position = new Vector3(renderLeftUp.X, i * MainView.GrafhicStep, 0f) });
                    linesCache.Add(new() { Color = mainColor, Position = new Vector3(renderRightDown.X, i * MainView.GrafhicStep, 0f) });
                    linesCache.Add(new() { Color = mainColor, Position = new Vector3(renderLeftUp.X, -i * MainView.GrafhicStep, 0f) });
                    linesCache.Add(new() { Color = mainColor, Position = new Vector3(renderRightDown.X, -i * MainView.GrafhicStep, 0f) });
                }
            }

            for (float x = FloorBy(renderLeftUp.X, gridValue); x < CeilingBy(renderRightDown.X, gridValue); x += gridValue)
            {
                linesCache.Add(new() { Color = mainColor, Position = new Vector3(x, renderRightDown.Y, 0f) });
                linesCache.Add(new() { Color = mainColor, Position = new Vector3(x, renderLeftUp.Y, 0f) });
            }

            for (float y = FloorBy(renderRightDown.Y, gridValue); y < CeilingBy(renderLeftUp.Y, gridValue); y += gridValue)
            {
                linesCache.Add(new() { Color = mainColor, Position = new Vector3(renderLeftUp.X, y, 0f) });
                linesCache.Add(new() { Color = mainColor, Position = new Vector3(renderRightDown.X, y, 0f) });
            }

            allLines = linesCache.ToArray();
        }

        public static float FloorBy(float value, float by)
        {
            return (MathF.Floor(value / by) - 1) * by;
        }

        public static float CeilingBy(float value, float by)
        {
            return (MathF.Ceiling(value / by) + 1) * by;
        }
    }
}
