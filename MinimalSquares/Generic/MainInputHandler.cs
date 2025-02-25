using System;

using MinimalSquares.Components;
using MinimalSquares.Graphics;
using MinimalSquares.Input.MouseInput;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MinimalSquares.Input.Keyboard;
using MinimalSquares.Input.Keyboard.KeyEvents;

namespace MinimalSquares.Generic
{
    class MainInputHandler : BaseComponent
    {
        private MouseController mouseController = null!;
        private PointManager pointManager = null!;
        private KeyboardManager keyboard = null!;

        private bool shouldUpdateVertex = false;

        private Vector2 leftPressed;

        public override void Start(MainGame game)
        {
            mouseController = ComponentManager.Get<MouseController>()!;
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
            Vector2 vector = ComponentManager.MainView.GetMouseWorldPosition(mouseController.CursorPosition).GetXY();

            for (int i = 0; i < pointManager.Points.Count; i++)
            {
                Vector2 point = pointManager.Points[i];
                float dist = Vector2.DistanceSquared(point, vector);

                if (dist < 0.1f * 0.1f)
                {
                    pointManager.Points.RemoveAt(i);
                    pointManager.TriggerUpdate();
                    ComponentManager.MainView.RenderRequest(RenderRequestType.Static);
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
                ComponentManager.MainView.RenderRequest(RenderRequestType.All);
            }

            if (Vector2.DistanceSquared(mouseController.CursorPosition, leftPressed) <= 10 * 10)
            {
                Vector2 vector = ComponentManager.MainView.GetMouseWorldPosition(mouseController.CursorPosition).GetXY();

                pointManager.Add(vector);
                ComponentManager.MainView.RenderRequest(RenderRequestType.Static);
            }
        }

        private void OnCursorMoving(Vector2 oldPosition, Vector2 newPosition)
        {
            if (mouseController.IsLeftButtonPressed)
            {
                Vector3 oldGlobalPos = ComponentManager.MainView.GetMouseWorldPosition(oldPosition);
                Vector3 newGlobalPos = ComponentManager.MainView.GetMouseWorldPosition(newPosition);
                
                Vector3 moveVector = oldGlobalPos - newGlobalPos;

                Vector3 newCameraPosition = ComponentManager.MainView.CameraPosition + moveVector;
                ComponentManager.MainView.SetCamera(newCameraPosition);

                shouldUpdateVertex = true;
            }
        }

        private void OnWheelScrolled(float oldValue, float newValue)
        {
            float delta = newValue - oldValue;
            Vector3 cameraPos = ComponentManager.MainView.CameraPosition;

            if (cameraPos.Z <= 0.001f && delta > 0f || cameraPos.Z >= 1000f && delta < 0f)
                return;

            Vector3 cursorPos = ComponentManager.MainView.GetMouseWorldPosition(mouseController.CursorPosition);

            Vector3 moveVector = (cursorPos - cameraPos) * (MathF.Sign(delta));

            Vector3 newCameraPosition = Vector3.Lerp(cameraPos, cameraPos + moveVector, 0.1f);

            ComponentManager.MainView.SetCamera(newCameraPosition);
            ComponentManager.MainView.RenderRequest(RenderRequestType.All);
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

            ComponentManager.MainView.RenderRequest(RenderRequestType.Static);
        }
    }
}
