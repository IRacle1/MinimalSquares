using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MinimalSquares.Components;
using MinimalSquares.Components.Abstractions;
using System;

namespace MinimalSquares.Input.MouseInput
{
    public class MouseController : BaseComponent, IUpdatedComponent
    {
        public Vector2 CursorPosition { get; private set; } = Vector2.Zero;

        public bool IsLeftButtonPressed { get; private set; } = false;
        public bool IsRightButtonPressed { get; private set; } = false;

        public int WheelScrollValue { get; private set; } = 0;

        public event Action<Vector2, Vector2>? OnCursorMoving;
        public event Action<float, float>? OnWheelScrolled;

        public event Action? OnLeftButtonPressed;
        public event Action? OnLeftButtonReleased;
        public event Action? OnRightButtonPressed;
        public event Action? OnRightButtonReleased;

        public override void Start(MainGame game)
        {
            base.Start(game);
        }

        public void Update(GameTime gameTime)
        {
            if (!targetGame.IsActive)
                return;

            MouseState state = Mouse.GetState();

            if (state.X < 0 || state.X > targetGame.Window.ClientBounds.Width ||
                state.Y < 0 || state.Y > targetGame.Window.ClientBounds.Height)
            {
                return;
            }

            if (WheelScrollValue != state.ScrollWheelValue)
            {
                OnWheelScrolled?.Invoke(WheelScrollValue, state.ScrollWheelValue);
                WheelScrollValue = state.ScrollWheelValue;
            }

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
