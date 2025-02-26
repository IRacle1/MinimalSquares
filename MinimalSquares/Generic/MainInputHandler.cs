using System;

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
        private PointManager pointManager = null!;

        private bool shouldUpdateVertex = false;

        private Vector2 leftPressed;

        public override void Start(MainGame game)
        {
            pointManager = ComponentManager.Get<PointManager>()!;

            ComponentManager.KeyboardManager.Register(new BasicKeyEvent(HandleRemovePoint, InputType.OnKeyDown, Keys.Z));

            ComponentManager.MouseController.OnWheelScrolled += OnWheelScrolled;
            ComponentManager.MouseController.OnCursorMoving += OnCursorMoving;
            ComponentManager.MouseController.OnLeftButtonPressed += OnLeftButtonPressed;
            ComponentManager.MouseController.OnLeftButtonReleased += OnLeftButtonReleased;
            ComponentManager.MouseController.OnRightButtonPressed += OnRightButtonPressed;

            base.Start(game);
        }

        private void OnRightButtonPressed()
        {
            Vector2 vector = ComponentManager.MainView.GetMouseWorldPosition(ComponentManager.MouseController.CursorPosition).GetXY();

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
            leftPressed = ComponentManager.MouseController.CursorPosition;
            shouldUpdateVertex = false;
        }

        private void OnLeftButtonReleased()
        {
            if (shouldUpdateVertex)
            {
                ComponentManager.MainView.RenderRequest(RenderRequestType.All);
            }

            if (Vector2.DistanceSquared(ComponentManager.MouseController.CursorPosition, leftPressed) <= 10 * 10)
            {
                Vector2 vector = ComponentManager.MainView.GetMouseWorldPosition(ComponentManager.MouseController.CursorPosition).GetXY();

                pointManager.Add(vector);
                ComponentManager.MainView.RenderRequest(RenderRequestType.Static);
            }
        }

        private void OnCursorMoving(Vector2 oldPosition, Vector2 newPosition)
        {
            if (ComponentManager.MouseController.IsLeftButtonPressed)
            {
                Vector3 oldGlobalPos = ComponentManager.MainView.GetMouseWorldPosition(oldPosition);
                Vector3 newGlobalPos = ComponentManager.MainView.GetMouseWorldPosition(newPosition);
                
                Vector3 moveVector = oldGlobalPos - newGlobalPos;

                Vector3 newCameraPosition = ComponentManager.MainView.CameraPosition + moveVector;
                if (ComponentManager.MainView.SetCamera(newCameraPosition))
                    shouldUpdateVertex = true;
            }
        }

        private void OnWheelScrolled(float oldValue, float newValue)
        {
            float delta = newValue - oldValue;

            Vector3 cameraPos = ComponentManager.MainView.CameraPosition;

            Vector3 cursorPos = ComponentManager.MainView.GetMouseWorldPosition(ComponentManager.MouseController.CursorPosition);

            Vector3 moveVector = (cursorPos - cameraPos) * (MathF.Sign(delta));

            Vector3 newCameraPosition = Vector3.Lerp(cameraPos, cameraPos + moveVector, 0.1f);

            if (newCameraPosition.Z <= 0.1f || newCameraPosition.Z >= 100f)
                return;

            if (ComponentManager.MainView.SetCamera(newCameraPosition))
                ComponentManager.MainView.RenderRequest(RenderRequestType.All);
        }

        private void HandleRemovePoint(InputType type, Keys keys)
        {
            if (pointManager.Points.Count == 0)
                return;

            if (ComponentManager.KeyboardManager.PressedKeys.Contains(Keys.LeftShift))
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
