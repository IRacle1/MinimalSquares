using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MinimalSquares.Components;
using MinimalSquares.Components.Abstractions;
using MinimalSquares.Functions;
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

            float step = MainView.GrafhicStep * 2;

            for (int i = 0; i < functionManager.CurrentFunctions.Count; i++)
            {
                BaseFunction function = functionManager.CurrentFunctions[i];

                float left = ComponentManager.MainView.RenderLeftUpBorder.X;
                float right = ComponentManager.MainView.RenderRightDownBorder.X;

                for (float x = left; x < right; x += MainView.Step)
                {
                    float y = (float)function.GetValue(x);

                    if (!float.IsNormal(y))
                        continue;

                    Vector3 center = new Vector3(x, y, 0);

                    if (!ComponentManager.MainView.IsOnRenderScreen(center.GetXY()))
                        continue;

                    vertexCache.Add(new VertexPositionColor(center + new Vector3(step, step, 0), function.Color));
                    vertexCache.Add(new VertexPositionColor(center + new Vector3(-step, step, 0), function.Color));
                    vertexCache.Add(new VertexPositionColor(center + new Vector3(step, -step, 0), function.Color));
                    vertexCache.Add(new VertexPositionColor(center + new Vector3(-step, step, 0), function.Color));
                    vertexCache.Add(new VertexPositionColor(center + new Vector3(step, -step, 0), function.Color));
                    vertexCache.Add(new VertexPositionColor(center + new Vector3(-step, -step, 0), function.Color));
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
