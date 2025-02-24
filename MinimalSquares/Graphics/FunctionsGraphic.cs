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
        private MainView view = null!;

        private List<VertexPositionColor> vertexCache = new(100);
        private VertexPositionColor[] functionsVertex = null!;

        public int Order { get; } = 3;

        public override void Start(MainGame game)
        {
            base.Start(game);
            functionManager = ComponentManager.Get<FunctionManager>()!;
            view = ComponentManager.Get<MainView>()!;

            functionManager.OnFunctionUpdate += UpdateVertex;
        }

        public void UpdateVertex()
        {
            vertexCache.Clear();

            for (int i = 0; i < functionManager.CurrentFunctions.Count; i++)
            {
                BaseFunction function = functionManager.CurrentFunctions[i];

                float left = view.RenderLeftUpBorder.X;
                float right = view.RenderRightDownBorder.X;

                for (float x = left; x < right; x += MainView.Step)
                {
                    float y = (float)function.GetValue(x);

                    if (!float.IsNormal(y))
                        continue;

                    vertexCache.Add(new VertexPositionColor(new Vector3(x, y, 0f), function.Color));

                    vertexCache.Add(new VertexPositionColor(new Vector3(x, y + MainView.GrafhicStep, 0f), function.Color));
                    vertexCache.Add(new VertexPositionColor(new Vector3(x, y - MainView.GrafhicStep, 0f), function.Color));
                    vertexCache.Add(new VertexPositionColor(new Vector3(x + MainView.GrafhicStep, y, 0f), function.Color));
                    vertexCache.Add(new VertexPositionColor(new Vector3(x - MainView.GrafhicStep, y, 0f), function.Color));

                    vertexCache.Add(new VertexPositionColor(new Vector3(x, y + 2 * MainView.GrafhicStep, 0f), function.Color));
                    vertexCache.Add(new VertexPositionColor(new Vector3(x, y - 2 * MainView.GrafhicStep, 0f), function.Color));
                    vertexCache.Add(new VertexPositionColor(new Vector3(x + 2 * MainView.GrafhicStep, y, 0f), function.Color));
                    vertexCache.Add(new VertexPositionColor(new Vector3(x - 2 * MainView.GrafhicStep, y, 0f), function.Color));
                }
            }

            functionsVertex = vertexCache.ToArray();
        }

        public void Draw()
        {
            if (functionsVertex != null && functionsVertex.Length > 0)
                targetGame.GraphicsDevice.DrawUserPrimitives(PrimitiveType.PointList, functionsVertex, 0, functionsVertex.Length);
        }
    }
}
