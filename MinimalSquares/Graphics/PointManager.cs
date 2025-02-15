using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MinimalSquares;
using MinimalSquares.Components;
using MinimalSquares.Components.Abstractions;
using MinimalSquares.Functions;
using MinimalSquares.Input.Keyboard;
using MinimalSquares.Input.Keyboard.KeyEvents;
using MinimalSquares.Input.MouseInput;

namespace MinimalSquares.Graphics
{
    public class PointManager : BaseComponent, IDrawableComponent
    {
        private KeyboardManager keyboard = null!;

        private VertexPositionColor[] pointLines = null!;

        public List<Vector2> Points { get; } = new List<Vector2>();
        public event Action? OnPointsUpdate;

        public int Order { get; } = 1;

        public Color PointColor { get; set; } = Color.Crimson;

        public bool CanAddPoint { get; set; } = false;

        public override void Start(MainGame game)
        {
            keyboard = ComponentManager.Get<KeyboardManager>()!;

            keyboard.Register(new BasicKeyEvent(HandleRemovePoint, InputType.OnKeyDown, Keys.Z));

            base.Start(game);
        }

        public void Draw()
        {
            if (Points.Count > 0)
                targetGame.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, pointLines, 0, Points.Count * 2);
        }

        public void SetNewPoint(Vector3 point)
        {            
            Points.Add(new Vector2(point.X, point.Y));

            TriggerUpdate();
        }

        public void TriggerUpdate()
        {
            UpdateVertex();
            OnPointsUpdate?.Invoke();
        }

        public void UpdateVertex()
        {
            List<VertexPositionColor> vertexes = new(Points.Count * 6);

            float step = MainView.Step * 100;

            for (int i = 0; i < Points.Count; i++)
            {
                Vector3 worldVec = new(Points[i], 0.0f);
                vertexes.Add(new VertexPositionColor(worldVec + new Vector3(step, step, 0), PointColor));
                vertexes.Add(new VertexPositionColor(worldVec + new Vector3(-step, step, 0), PointColor));
                vertexes.Add(new VertexPositionColor(worldVec + new Vector3(step, -step, 0), PointColor));
                vertexes.Add(new VertexPositionColor(worldVec + new Vector3(-step, step, 0), PointColor));
                vertexes.Add(new VertexPositionColor(worldVec + new Vector3(step, -step, 0), PointColor));
                vertexes.Add(new VertexPositionColor(worldVec + new Vector3(-step, -step, 0), PointColor));
            }

            pointLines = vertexes.ToArray();
        }

        private void HandleRemovePoint(InputType type, Keys keys)
        {
            if (Points.Count > 0)
            {
                if (keyboard.PressedKeys.Contains(Keys.LeftShift))
                {
                    Points.Clear();
                }
                else
                {
                    Points.RemoveAt(Points.Count - 1);
                }
                OnPointsUpdate?.Invoke();
            }
        }
    }
}
