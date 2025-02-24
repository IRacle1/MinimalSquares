using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MinimalSquares.Components;
using MinimalSquares.Graphics;
using MinimalSquares.Input.Keyboard;
using MinimalSquares.Input.Keyboard.KeyEvents;
using MinimalSquares.Input.MouseInput;

namespace MinimalSquares.Generic
{
    class MainInputHandler : BaseComponent
    {
        private MouseController mouseController = null!;
        private MainView view = null!;
        private PointManager pointManager = null!;
        private KeyboardManager keyboard = null!;

        private bool shouldUpdateVertex = false;

        private Vector2 leftPressed;

        public override void Start(MainGame game)
        {
            mouseController = ComponentManager.Get<MouseController>()!;
            view = ComponentManager.Get<MainView>()!;
            pointManager = ComponentManager.Get<PointManager>()!;
            keyboard = ComponentManager.Get<KeyboardManager>()!;

            keyboard.Register(new BasicKeyEvent(HandleRemovePoint, InputType.OnKeyDown, Keys.Z));

            mouseController.OnWheelScrolled += OnWheelScrolled;
            mouseController.OnCursorMoving += OnCursorMoving;
            mouseController.OnLeftButtonPressed += OnLeftButtonPressed;
            mouseController.OnLeftButtonReleased += OnLeftButtonReleased;
            mouseController.OnRightButtonPressed += OnRightButtonPressed;

            base.Start(game);
        }

        private void OnRightButtonPressed()
        {
            Vector2 vector = view.GetMouseWorldPosition(mouseController.CursorPosition).GetXY();

            for (int i = 0; i < pointManager.Points.Count; i++)
            {
                Vector2 point = pointManager.Points[i];
                float dist = Vector2.DistanceSquared(point, vector);

                if (dist < 0.1f * 0.1f)
                {
                    pointManager.Points.RemoveAt(i);
                    pointManager.TriggerUpdate();
                    return;
                }
            }
        }

        private void OnLeftButtonPressed()
        {
            leftPressed = mouseController.CursorPosition;
            shouldUpdateVertex = false;
        }

        private void OnLeftButtonReleased()
        {
            if (shouldUpdateVertex)
            {
                ComponentManager.UpdateVertexes();
            }

            if (Vector2.DistanceSquared(mouseController.CursorPosition, leftPressed) <= 10 * 10)
            {
                Vector2 vector = view.GetMouseWorldPosition(mouseController.CursorPosition).GetXY();

                pointManager.Add(vector);
            }
        }

        private void OnCursorMoving(Vector2 oldPosition, Vector2 newPosition)
        {
            if (mouseController.IsLeftButtonPressed)
            {
                Vector3 oldGlobalPos = view.GetMouseWorldPosition(oldPosition);
                Vector3 newGlobalPos = view.GetMouseWorldPosition(newPosition);

                Vector3 moveVector = oldGlobalPos - newGlobalPos;

                Vector3 newCameraPosition = view.CameraPosition + moveVector;
                view.SetCamera(newCameraPosition, new Vector3(newCameraPosition.GetXY(), 0));

                shouldUpdateVertex = true;
            }
        }

        private void OnWheelScrolled(float oldValue, float newValue)
        {
            float delta = newValue - oldValue;

            Vector3 cursorPos = view.GetMouseWorldPosition(mouseController.CursorPosition);

            Vector3 moveVector = view.CameraPosition - cursorPos;

            Vector3 newCameraPosition;

            if (delta < 0f)
                newCameraPosition = Vector3.Lerp(view.CameraPosition, view.CameraPosition + moveVector, 0.1f);
            else
                newCameraPosition = Vector3.Lerp(view.CameraPosition, view.CameraPosition - moveVector, 0.1f);

            view.SetCamera(newCameraPosition, new Vector3(newCameraPosition.GetXY(), 0));
            ComponentManager.UpdateVertexes();
        }

        private void HandleRemovePoint(InputType type, Keys keys)
        {
            if (pointManager.Points.Count == 0)
                return;

            if (keyboard.PressedKeys.Contains(Keys.LeftShift))
            {
                pointManager.Clear();
            }
            else
            {
                pointManager.RemoveLast();
            }
        }
    }
}
