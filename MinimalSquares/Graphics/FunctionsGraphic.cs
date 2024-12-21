using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MinimalSquares.Components;
using MinimalSquares.Components.Abstractions;
using MinimalSquares.Functions;

namespace MinimalSquares.Graphics
{
    public class FunctionsGraphic : BaseComponent, IDrawableComponent
    {
        private FunctionManager functionManager = null!;
        private PointManager pointManager = null!;
        private MainView view = null!;

        private VertexPositionColor[] functionsVertex = null!;

        public int Order { get; } = 3;

        public override void Start(MainGame game)
        {
            base.Start(game);
            functionManager = ComponentManager.Get<FunctionManager>()!;
            pointManager = ComponentManager.Get<PointManager>()!;
            view = ComponentManager.Get<MainView>()!;
        }

        public void UpdateVertex()
        {
            List<VertexPositionColor> list = new(functionManager.Functions.Count * 1000);

            for (int i = 0; i < functionManager.Functions.Count; i++)
            {
                BaseFunction function = functionManager.Functions[i];
                if (!functionManager.IsValidFunction(function))
                    continue;
                float left = view.LeftDownBorder.X;
                float right = view.RightDownBorder.X;

                for (float x = left; x < right; x += function.Step)
                {
                    float y = function.GetValue(x);
                    if (float.IsNormal(y))
                    {
                        list.Add(new VertexPositionColor(new Vector3(x, y, 0f), function.Color));

                        list.Add(new VertexPositionColor(new Vector3(x, y + Program.GrafhicStep, 0f), function.Color));
                        list.Add(new VertexPositionColor(new Vector3(x, y - Program.GrafhicStep, 0f), function.Color));
                        list.Add(new VertexPositionColor(new Vector3(x + Program.GrafhicStep, y, 0f), function.Color));
                        list.Add(new VertexPositionColor(new Vector3(x - Program.GrafhicStep, y, 0f), function.Color));

                        list.Add(new VertexPositionColor(new Vector3(x, y + 2 * Program.GrafhicStep, 0f), function.Color));
                        list.Add(new VertexPositionColor(new Vector3(x, y - 2 * Program.GrafhicStep, 0f), function.Color));
                        list.Add(new VertexPositionColor(new Vector3(x + 2 * Program.GrafhicStep, y, 0f), function.Color));
                        list.Add(new VertexPositionColor(new Vector3(x - 2 * Program.GrafhicStep, y, 0f), function.Color));
                    }
                }
            }

            functionsVertex = list.ToArray();
        }

        public void Draw()
        {
            if (functionsVertex != null && functionsVertex.Length > 0)
                targetGame.GraphicsDevice.DrawUserPrimitives(PrimitiveType.PointList, functionsVertex, 0, functionsVertex.Length);
        }
    }
}
