using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MinimalSquares.Components;
using MinimalSquares.Components.Abstractions;

namespace MinimalSquares.Input.MouseInput
{
    public class MouseController : BaseComponent, IUpdatedComponent
    {
        public Vector2 CursorPosition { get; private set; } = Vector2.Zero;

        public bool IsLeftButtonPressed { get; private set; } = false;
        public bool IsRightButtonPressed { get; private set; } = false;

        public event Action<Vector2, Vector2>? OnCursorMoving;

        public event Action? OnLeftButtonPressed;
        public event Action? OnLeftButtonReleased;
        public event Action? OnRightButtonPressed;
        public event Action? OnRightButtonReleased;

        public override void Start(MainGame game)
        {
            base.Start(game);
        }

        public void Update() 
        {
            if (!targetGame.IsActive)
                return;

            MouseState state = Mouse.GetState();

            if (state.X != CursorPosition.X ||
                state.Y != CursorPosition.Y)
            {
                Vector2 newPosition = new Vector2(state.X, state.Y);
                OnCursorMoving?.Invoke(CursorPosition, newPosition);
                CursorPosition = newPosition;
            }

            if (state.LeftButton == ButtonState.Pressed != IsLeftButtonPressed)
            {
                if (state.LeftButton == ButtonState.Pressed)
                {
                    OnLeftButtonPressed?.Invoke();
                }
                else
                {
                    OnLeftButtonReleased?.Invoke();
                }

                IsLeftButtonPressed = !IsLeftButtonPressed;
            }

            if (state.RightButton == ButtonState.Pressed != IsRightButtonPressed)
            {
                if (state.RightButton == ButtonState.Pressed)
                {
                    OnRightButtonPressed?.Invoke();
                }
                else
                {
                    OnRightButtonReleased?.Invoke();
                }

                IsRightButtonPressed = !IsRightButtonPressed;
            }
        }
    }
}
