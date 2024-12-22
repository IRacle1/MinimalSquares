﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MinimalSquares.Components;

namespace MinimalSquares.Graphics
{
    public class MainView : BaseComponent
    {
        private Matrix projectionMatrix;
        private Matrix viewMatrix;
        private Matrix worldMatrix;

        private float AspectRatio;

        public Vector3 LeftDownBorder { get; private set; }
        public Vector3 RightDownBorder { get; private set; }

        public Color BackgroundColor { get; set; } = Color.White;
        public Color MainColor { get; set; } = Color.DarkGray;

        public override void Start(MainGame game)
        {
            base.Start(game);

            viewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, 6), Vector3.Zero, Vector3.Up);

            AspectRatio = (float)game.Window.ClientBounds.Width /
                (float)game.Window.ClientBounds.Height;

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                AspectRatio,
                1, 10);

            worldMatrix = Matrix.CreateWorld(Vector3.Zero, Vector3.Forward, Vector3.Up);

            //game.Window.AllowUserResizing = true;
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
                View = viewMatrix,
                Projection = projectionMatrix
            };

            game.IsFixedTimeStep = true;
            game.TargetElapsedTime = TimeSpan.FromSeconds(1d / 60);

            //game.Window.ClientSizeChanged += ClientSizeChanged;

            LeftDownBorder = GetMouseWorldPosition(new Vector2(0, 0));
            RightDownBorder = GetMouseWorldPosition(new Vector2(targetGame.Window.ClientBounds.Width, targetGame.Window.ClientBounds.Height));
        }

        private void ClientSizeChanged(object? sender, EventArgs e)
        {
            targetGame.GraphicsManager.PreferredBackBufferWidth = targetGame.Window.ClientBounds.Width;
            targetGame.GraphicsManager.PreferredBackBufferHeight = targetGame.Window.ClientBounds.Height;
            targetGame.GraphicsManager.ApplyChanges();

            LeftDownBorder = GetMouseWorldPosition(new Vector2(0, 0));
            RightDownBorder = GetMouseWorldPosition(new Vector2(targetGame.Window.ClientBounds.Width, targetGame.Window.ClientBounds.Height));
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
    }
}
