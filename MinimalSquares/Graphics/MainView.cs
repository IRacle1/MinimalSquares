using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MinimalSquares.Components;
using MinimalSquares.Functions;
using MinimalSquares.Generic;

namespace MinimalSquares.Graphics
{
    public class MainView : BaseComponent
    {
        public event Action<RenderRequestType>? OnViewUpdate;

        public static float Step { get; private set; } = 0.0005f;
        public static float GrafhicStep { get; private set; } = 0.005f;

        private Matrix projectionMatrix;
        private Matrix viewMatrix;
        private Matrix worldMatrix;

        private float AspectRatio;

        public Vector3 LeftUpBorder { get; private set; }
        public Vector3 RightDownBorder { get; private set; }
        public Vector3 RenderLeftUpBorder { get; private set; }
        public Vector3 RenderRightDownBorder { get; private set; }
        public Vector3 CameraPosition { get; private set; }

        public Color BackgroundColor { get; set; } = Color.White;
        public Color MainColor { get; set; } = Color.DarkGray;

        public Vector2 CenterScreen => targetGame.Window.ClientBounds.Size.ToVector2() / 2f;
        public Vector3 CenterWorld => GetMouseWorldPosition(CenterScreen);

        public override void Start(MainGame game)
        {
            base.Start(game);

            CameraPosition = new Vector3(0, 0, 6);

            AspectRatio = (float)game.Window.ClientBounds.Width /
                (float)game.Window.ClientBounds.Height;

            worldMatrix = Matrix.CreateWorld(Vector3.Zero, Vector3.Forward, Vector3.Up);

            game.Window.AllowUserResizing = true;
            game.IsMouseVisible = true;
            game.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            game.GraphicsManager.IsFullScreen = false;
            game.GraphicsManager.PreferredBackBufferWidth = 1080;
            game.GraphicsManager.PreferredBackBufferHeight = 720;
            game.GraphicsManager.ApplyChanges();

            game.Effect = new(game.GraphicsDevice)
            {
                VertexColorEnabled = true,

                World = worldMatrix,
            };

            SetCamera(CameraPosition);

            game.IsFixedTimeStep = true;
            game.TargetElapsedTime = TimeSpan.FromSeconds(1d / 60);

            UpdateBorders();
        }

        public void RenderRequest(RenderRequestType render)
        {
            OnViewUpdate?.Invoke(render);
        }

        private void ClientSizeChanged(object? sender, EventArgs e)
        {
            targetGame.GraphicsManager.PreferredBackBufferWidth = targetGame.Window.ClientBounds.Width;
            targetGame.GraphicsManager.PreferredBackBufferHeight = targetGame.Window.ClientBounds.Height;
            targetGame.GraphicsManager.ApplyChanges();

            AspectRatio = (float)targetGame.Window.ClientBounds.Width /
                (float)targetGame.Window.ClientBounds.Height;

            UpdateBorders();
            UpdateSteps();

            //RenderRequest(RenderRequestType.All);
        }

        private void UpdateBorders()
        {
            LeftUpBorder = GetMouseWorldPosition(Vector2.Zero);
            RightDownBorder = GetMouseWorldPosition(new Vector2(targetGame.Window.ClientBounds.Width, targetGame.Window.ClientBounds.Height));
            RenderLeftUpBorder = new Vector3(2f * LeftUpBorder.X - RightDownBorder.X, 2f * LeftUpBorder.Y - RightDownBorder.Y, 0);
            RenderRightDownBorder = new Vector3(2f * RightDownBorder.X - LeftUpBorder.X, 2f * RightDownBorder.Y - LeftUpBorder.Y, 0);
        }

        private void UpdateSteps()
        {
            Vector3 relativeVector = LeftUpBorder;
            Vector3 deltaYVector = GetMouseWorldPosition(Vector2.UnitY);

            float deltaY = relativeVector.Y - deltaYVector.Y;

            GrafhicStep = deltaY;
            Step = GrafhicStep / 10f;
            targetGame.Window.Title = GrafhicStep.ToString();
        }

        public bool IsOnScreen(Vector2 point)
        {
            return RenderLeftUpBorder.X < point.X &&
                RenderRightDownBorder.X > point.X &&
                RenderLeftUpBorder.Y > point.Y &&
                RenderRightDownBorder.Y < point.Y;
        }

        public Vector3 GetMouseWorldPosition(Vector2 screenPosition)
        {
            Vector3 nearWorldPoint = targetGame.GraphicsDevice.Viewport.Unproject(new Vector3(screenPosition.X, screenPosition.Y, 0), projectionMatrix, viewMatrix, worldMatrix);
            Vector3 farWorldPoint = targetGame.GraphicsDevice.Viewport.Unproject(new Vector3(screenPosition.X, screenPosition.Y, 1), projectionMatrix, viewMatrix, worldMatrix);
            Ray ray = new(nearWorldPoint, farWorldPoint - nearWorldPoint);

            Plane groundPlane = new(Vector3.Zero, Vector3.UnitZ);
            if (ray.Intersects(groundPlane) is float distance)
            {
                Vector3 intersection = ray.Position + distance * ray.Direction;
                intersection.Z = 0f;
                return intersection;
            }
            else
                return Vector3.Zero;
        }

        public void SetCamera(Vector3 cameraPosition) 
        {
            CameraPosition = cameraPosition;

            Vector3 cameraTarget = cameraPosition;
            cameraTarget.Z = 0;

            viewMatrix = Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.Up);

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                AspectRatio,
                MathF.Max(CameraPosition.Z - 0.2f, 0.00001f), CameraPosition.Z + 0.2f);

            targetGame.Effect.View = viewMatrix;
            targetGame.Effect.Projection = projectionMatrix;

            UpdateBorders();
            UpdateSteps();
        }
    }
}
