using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MinimalSquares.Components;
using MinimalSquares.Components.Abstractions;
using MinimalSquares.Generic;
using System.Collections.Generic;

namespace MinimalSquares.Graphics
{
    public class PointPrinter : BaseComponent, IDrawableComponent
    {
        private PointManager pointManager = null!;

        private List<VertexPositionColor> cachePointLines = new();
        private VertexPositionColor[] pointLines = null!;

        public int Order { get; } = 1;

        public Color PointColor { get; set; } = Color.Crimson;

        public override void Start(MainGame game)
        {
            pointManager = ComponentManager.Get<PointManager>()!;

            ComponentManager.MainView.OnViewUpdate += OnViewUpdate;
            base.Start(game);
        }

        public void Draw()
        {
            if (pointLines != null && pointLines.Length > 0)
                targetGame.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, pointLines, 0, pointLines.Length / 3);
        }

        private void OnViewUpdate(RenderRequestType renderRequestType)
        {
            if (!renderRequestType.HasFlag(RenderRequestType.Points))
                return;

            UpdateVertex();
        }

        public void UpdateVertex()
        {
            cachePointLines.Clear();
            cachePointLines.EnsureCapacity(pointManager.Points.Count * 6);

            float step = MainView.GrafhicStep * 10;

            for (int i = 0; i < pointManager.Points.Count; i++)
            {
                Vector2 point = pointManager.Points[i];

                if (!ComponentManager.MainView.IsOnRenderScreen(point))
                    continue;

                cachePointLines.AddRange(ComponentManager.MainView.DrawRectangle(point + new Vector2(step, -step), point + new Vector2(-step, step), PointColor));
            }

            pointLines = cachePointLines.ToArray();
        }
    }
}
