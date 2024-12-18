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
    public class MainGraphicController : BaseComponent, IDrawableComponent
    {
        private FunctionManager functionManager = null!;
        private List<VertexPositionColor[]> functionsVertex = new(10);

        public override void Start(MainGame game)
        {
            base.Start(game);
            functionManager = ComponentManager.Get<FunctionManager>()!;
        }

        public void UpdateVertex()
        {
            functionsVertex.Clear();
            for (int i = 0; i < functionManager.Functions.Count; i++)
            {
                IFunction function = functionManager.Functions[i];
                List<VertexPositionColor> color = new(1000);
                for (float x = -5f; x < 5f; x += Program.Step)
                {
                    color.Add(new VertexPositionColor(new Vector3(x, function.GetValue(x), 0f), function.Color));
                }
                functionsVertex.Add(color.ToArray());
            }
        }

        public void Draw()
        {
            foreach (var item in functionsVertex)
            {
                targetGame.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineStrip, item, 0, item.Length - 1);
            }
        }
    }
}
