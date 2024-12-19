using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MinimalSquares.Components;
using MinimalSquares.Components.Abstractions;
using MinimalSquares.Functions;

namespace MinimalSquares.Graphics
{
    public class DeltaPrinter : BaseComponent, IDrawableComponent
    {
        private PointManager pointManager = null!;
        private FunctionManager functionManager = null!;

        private VertexPositionColor[] LinesCoords = null!;

        public override void Start(MainGame game)
        {
            pointManager = ComponentManager.Get<PointManager>()!;
            functionManager = ComponentManager.Get<FunctionManager>()!;

            pointManager.OnPointsUpdate += OnPointsUpdate;
            base.Start(game);
        }

        private void OnPointsUpdate()
        {
            List<VertexPositionColor> lines = new();
            foreach (Vector2 point in pointManager.Points)
            {
                foreach (BaseFunction function in functionManager.Functions)
                {
                    if (!functionManager.IsValidFunction(function))
                        continue;
                    float y = function.GetValue(point.X);
                    if (!float.IsNormal(y))
                        continue;

                    lines.Add(new VertexPositionColor(new Vector3(point.X, point.Y, 0f), function.Color));
                    lines.Add(new VertexPositionColor(new Vector3(point.X, y, 0f), function.Color));
                }
            }

            LinesCoords = lines.ToArray();
        }

        public void Draw()
        {
            if (LinesCoords != null && LinesCoords.Length > 0)
                targetGame.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, LinesCoords, 0, LinesCoords.Length / 2);
        }
    }
}
