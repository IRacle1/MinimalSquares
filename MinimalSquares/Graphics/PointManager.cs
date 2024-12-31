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
        private MouseController mouseController = null!;
        private KeyboardManager keyboard = null!;
        private MainView view = null!;

        private VertexPositionColor[] pointLines = null!;

        public List<Vector2> Points { get; } = new List<Vector2>();
        public event Action? OnPointsUpdate;

        public int Order { get; } = 1;

        public Color PointColor { get; set; } = Color.Crimson;

        public override void Start(MainGame game)
        {
            mouseController = ComponentManager.Get<MouseController>()!;
            keyboard = ComponentManager.Get<KeyboardManager>()!;

            view = ComponentManager.Get<MainView>()!;

            mouseController.OnLeftButtonPressed += OnLeftButtonPressed;
            mouseController.OnRightButtonPressed += OnRightButtonPressed;

            keyboard.Register(new BasicKeyEvent(HandleRemovePoint, InputType.OnKeyDown, Keys.Back));

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

            float step = 0.05f;

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

        private void OnLeftButtonPressed()
        {
            Vector3 vector = view.GetMouseWorldPosition(mouseController.CursorPosition);
            
            Points.Add(new Vector2(vector.X, vector.Y));

            TriggerUpdate();
        }

        private void OnRightButtonPressed()
        {
            Vector3 vector = view.GetMouseWorldPosition(mouseController.CursorPosition);

            for (int i = 0; i < Points.Count; i++)
            {
                Vector3 point = new Vector3(Points[i], 0f);
                float dist = Vector3.DistanceSquared(point, vector);

                if (dist < 0.1f * 0.1f)
                {
                    Points.RemoveAt(i);
                    TriggerUpdate();
                    return;
                }
            }
        }
    }
}
