using System;

using MinimalSquares.Components;
using MinimalSquares.Graphics;
using MinimalSquares.Input.MouseInput;

using Microsoft.Xna.Framework;

namespace MinimalSquares.Input
{
    class MainMouseHandler : BaseComponent
    {
        private MouseController mouseController = null!;
        private MainView view = null!;
        private PointManager pointManager = null!;

        private bool shouldUpdateVertex = false;

        private Vector2 leftPressed;

        public override void Start(MainGame game)
        {
            mouseController = ComponentManager.Get<MouseController>()!;

            view = ComponentManager.Get<MainView>()!;
            pointManager = ComponentManager.Get<PointManager>()!;

            //mouseController.OnWheelScrolled += OnWheelScrolled;
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
                Vector3 vector = view.GetMouseWorldPosition(mouseController.CursorPosition);

                pointManager.Points.Add(vector.GetXY());

                pointManager.TriggerUpdate();
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
            float z = view.CameraPosition.Z;
            z += z / 100;

            Vector3 newCameraPosition = new Vector3(view.CameraPosition.X, view.CameraPosition.Y, z);
            view.SetCamera(newCameraPosition, new Vector3(newCameraPosition.GetXY(), 0));
        }
    }
}
