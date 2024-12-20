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
        private ViewInitializing view = null!;

        private List<VertexPositionColor[]> functionsVertex = new(10);

        public override void Start(MainGame game)
        {
            base.Start(game);
            functionManager = ComponentManager.Get<FunctionManager>()!;
            pointManager = ComponentManager.Get<PointManager>()!;
            view = ComponentManager.Get<ViewInitializing>()!;
        }

        public void UpdateVertex()
        {
            functionsVertex.Clear();
            for (int i = 0; i < functionManager.Functions.Count; i++)
            {
                BaseFunction function = functionManager.Functions[i];
                if (!functionManager.IsValidFunction(function))
                    continue;
                List<VertexPositionColor> color = new(1000);
               float left = view.LeftDownBorder.X;
               float right = view.RightDownBorder.X;

                for (float x = left; x < right; x += function.Step)
                {
                    float y = function.GetValue(x);
                    if (float.IsNormal(y))
                    {
                        color.Add(new VertexPositionColor(new Vector3(x, y, 0f), function.Color));
                        color.Add(new VertexPositionColor(new Vector3(x, y + Program.Step, 0f), function.Color));
                        color.Add(new VertexPositionColor(new Vector3(x, y - Program.Step, 0f), function.Color));
                        color.Add(new VertexPositionColor(new Vector3(x + Program.Step, y, 0f), function.Color));
                        color.Add(new VertexPositionColor(new Vector3(x - Program.Step, y, 0f), function.Color));
                    }
                }
                functionsVertex.Add(color.ToArray());
            }
        }

        public void Draw()
        {
            foreach (var item in functionsVertex)
            {
                if (item.Length > 0)
                    targetGame.GraphicsDevice.DrawUserPrimitives(PrimitiveType.PointList, item, 0, item.Length);
            }
        }
    }
}
