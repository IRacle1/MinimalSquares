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
        private FunctionManager functionManager = null!;

        private ViewInitializing view = null!;

        public Stack<Vector2> Points = new Stack<Vector2>();
        private VertexPositionColor[] pointLines = new VertexPositionColor[1000];

        public override void Start(MainGame game)
        {
            mouseController = ComponentManager.Get<MouseController>()!;
            keyboard = ComponentManager.Get<KeyboardManager>()!;
            functionManager = ComponentManager.Get<FunctionManager>()!;

            view = ComponentManager.Get<ViewInitializing>()!;

            mouseController.OnLeftButtonPressed += MouseController_OnLeftButtonPressed;
            keyboard.Register(new BasicKeyEvent(HandleRemovePoint, InputType.OnKeyDown, Keys.Back));

            base.Start(game);
        }

        public void Draw()
        {
            if (Points.Count > 0)
                targetGame.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, pointLines, 0, Points.Count * 2);
        }

        private void HandleRemovePoint(InputType type, Keys keys)
        {
            if (Points.Count > 0)
            {
                Points.Pop();
                functionManager.OnPointUpdate();
            }
        }

        private void SetNewPoint(Vector3 point)
        {
            int offset = Points.Count * 6;

            float step = 0.05f;

            pointLines[offset + 0] = new VertexPositionColor(point + new Vector3(step, step, 0), Color.Red);
            pointLines[offset + 1] = new VertexPositionColor(point + new Vector3(-step, step, 0), Color.Red);
            pointLines[offset + 2] = new VertexPositionColor(point + new Vector3(step, -step, 0), Color.Red);
            pointLines[offset + 3] = new VertexPositionColor(point + new Vector3(-step, step, 0), Color.Red);
            pointLines[offset + 4] = new VertexPositionColor(point + new Vector3(step, -step, 0), Color.Red);
            pointLines[offset + 5] = new VertexPositionColor(point + new Vector3(-step, -step, 0), Color.Red);
        }

        private void MouseController_OnLeftButtonPressed()
        {
            Vector3 vector = view.GetMouseWorldPosition(mouseController.CursorPosition);

            SetNewPoint(vector);

            Points.Push(new Vector2(vector.X, vector.Y));

            functionManager.OnPointUpdate();
        }
    }
}
