using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MinimalSquares.Components;
using MinimalSquares.Components.Abstractions;
using MinimalSquares.Functions;
using MinimalSquares.Generic;
using System.Collections.Generic;

namespace MinimalSquares.Graphics
{
    public class DeltaPrinter : BaseComponent, IDrawableComponent
    {
        private PointManager pointManager = null!;
        private PointPrinter pointPrinter = null!;
        private FunctionManager functionManager = null!;

        private List<VertexPositionColor> linesCache = new(100);
        private VertexPositionColor[] LinesCoords = null!;

        public int Order { get; } = 2;

        public override void Start(MainGame game)
        {
            pointManager = ComponentManager.Get<PointManager>()!;
            pointPrinter = ComponentManager.Get<PointPrinter>()!;
            functionManager = ComponentManager.Get<FunctionManager>()!;

            ComponentManager.MainView.OnViewUpdate += OnViewUpdate;

            base.Start(game);
        }

        public void Draw()
        {
            if (LinesCoords != null && LinesCoords.Length > 0)
                targetGame.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, LinesCoords, 0, LinesCoords.Length / 2);
        }

        private void OnViewUpdate(RenderRequestType renderRequestType)
        {
            if (!renderRequestType.HasFlag(RenderRequestType.Points) && !renderRequestType.HasFlag(RenderRequestType.Function))
                return;

            UpdateVertex();
        }

        public void UpdateVertex()
        {
            linesCache.Clear();
            foreach (Vector2 point in pointManager.Points)
            {
                if (!ComponentManager.MainView.IsOnRenderScreen(point))
                    continue;

                foreach (BaseFunction function in functionManager.CurrentFunctions)
                {
                    if (!function.IsAcceptablePoint(point.X, point.Y))
                        continue;

                    float y = (float)function.GetValue(point.X);

                    if (!float.IsNormal(y))
                        continue;

                    linesCache.Add(new VertexPositionColor(new Vector3(point.X, point.Y, 0f), pointPrinter.PointColor));
                    linesCache.Add(new VertexPositionColor(new Vector3(point.X, y, 0f), function.Color));

                    linesCache.Add(new VertexPositionColor(new Vector3(point.X - MainView.GrafhicStep, point.Y, 0f), pointPrinter.PointColor));
                    linesCache.Add(new VertexPositionColor(new Vector3(point.X - MainView.GrafhicStep, y, 0f), function.Color));
                    linesCache.Add(new VertexPositionColor(new Vector3(point.X + MainView.GrafhicStep, point.Y, 0f), pointPrinter.PointColor));
                    linesCache.Add(new VertexPositionColor(new Vector3(point.X + MainView.GrafhicStep, y, 0f), function.Color));
                }
            }

            LinesCoords = linesCache.ToArray();
        }
    }
}
