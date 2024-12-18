using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MinimalSquares.Components;
using MinimalSquares.Components.Abstractions;

namespace MinimalSquares.Graphics
{
    public class AxesPrinter : BaseComponent, IDrawableComponent
    {
        private VertexPositionColor[] mainAxes = new VertexPositionColor[]
        {
            new() { Color = Color.White, Position = new Vector3(-10f, 0f, 0f) },
            new() { Color = Color.White, Position = new Vector3(10f, 0f, 0f) },
            new() { Color = Color.White, Position = new Vector3(0f, -10f, 0f) },
            new() { Color = Color.White, Position = new Vector3(0f, 10f, 0f) },

            new() { Color = Color.White, Position = new Vector3(-10f, 0.005f, 0f) },
            new() { Color = Color.White, Position = new Vector3(10f, 0.005f, 0f) },
            new() { Color = Color.White, Position = new Vector3(0.005f, -10f, 0f) },
            new() { Color = Color.White, Position = new Vector3(0.005f, 10f, 0f) },

            new() { Color = Color.White, Position = new Vector3(-10f, -0.005f, 0f) },
            new() { Color = Color.White, Position = new Vector3(10f, -0.005f, 0f) },
            new() { Color = Color.White, Position = new Vector3(-0.005f, -10f, 0f) },
            new() { Color = Color.White, Position = new Vector3(-0.005f, 10f, 0f) },

            new() { Color = Color.White, Position = new Vector3(-10f, 0.01f, 0f) },
            new() { Color = Color.White, Position = new Vector3(10f, 0.01f, 0f) },
            new() { Color = Color.White, Position = new Vector3(0.01f, -10f, 0f) },
            new() { Color = Color.White, Position = new Vector3(0.01f, 10f, 0f) },

            new() { Color = Color.White, Position = new Vector3(-10f, -0.01f, 0f) },
            new() { Color = Color.White, Position = new Vector3(10f, -0.01f, 0f) },
            new() { Color = Color.White, Position = new Vector3(-0.01f, -10f, 0f) },
            new() { Color = Color.White, Position = new Vector3(-0.01f, 10f, 0f) },
        };

        public void Draw()
        {
            targetGame.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, mainAxes, 0, mainAxes.Length / 2);
        }
    }
}
