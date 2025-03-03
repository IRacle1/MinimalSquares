using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MinimalSquares.Components;
using MinimalSquares.Components.Abstractions;
using MinimalSquares.Functions;

using System;
using System.Collections.Generic;

namespace MinimalSquares.Graphics
{
    public class FunctionsGraphic : BaseComponent, IDrawableComponent
    {
        private FunctionManager functionManager = null!;

        private List<VertexPositionColor> vertexCache = new(100);
        private VertexPositionColor[] functionsVertex = null!;

        public int Order { get; } = 3;

        public override void Start(MainGame game)
        {
            base.Start(game);
            functionManager = ComponentManager.Get<FunctionManager>()!;

            ComponentManager.MainView.OnViewUpdate += OnViewUpdate;
        }

        private void OnViewUpdate(RenderRequestType renderRequestType)
        {
            if (!renderRequestType.HasFlag(RenderRequestType.Function))
                return;

            UpdateVertex();
        }

        public void UpdateVertex()
        {
            vertexCache.Clear();

            float step = MainView.GrafhicStep * 2.0f;

            for (int i = 0; i < functionManager.CurrentFunctions.Count; i++)
            {
                AbstractFunction function = functionManager.CurrentFunctions[i];

                float left = ComponentManager.MainView.RenderLeftUpBorder.X;
                float right = ComponentManager.MainView.RenderRightDownBorder.X;

                float lastX = left;
                float lastY = (float)function.GetValue(lastX);

                for (float x = left + MainView.Step; x < right; x += MainView.Step)
                {
                    float y = (float)function.GetValue(x);

                    if (!float.IsNormal(y))
                    {
                        lastX = float.NaN;
                        continue;
                    }

                    Vector2 center = new Vector2(x, y);

                    if (!ComponentManager.MainView.IsOnRenderScreen(center))
                    {
                        lastX = x;
                        lastY = y;
                        continue;
                    }

                    if (float.IsNormal(lastX))
                    {
                        vertexCache.AddRange(ComponentManager.MainView.DrawRectangle(new Vector2(x + step, MathF.Min(y, lastY) - step), new Vector2(lastX - step, MathF.Max(y, lastY) + step), function.Color));
                    }

                    lastX = x;
                    lastY = y;
                }
            }

            functionsVertex = vertexCache.ToArray();
        }

        public void Draw()
        {
            if (functionsVertex != null && functionsVertex.Length > 0)
                targetGame.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, functionsVertex, 0, functionsVertex.Length / 3);
        }
    }
}
