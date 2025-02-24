using Microsoft.Xna.Framework.Input;
using System;

namespace MinimalSquares.Input.Keyboard.KeyEvents
{
    public class PressedSingleKeyEvent : IDynamicKeyEvent
    {
        public PressedSingleKeyEvent(Action keyStartPressed, Action keyPressed, Action keyEndPressed, Keys key)
        {
            StartPressed = keyStartPressed;
            Pressing = keyPressed;
            EndPressed = keyEndPressed;

            TargetKeys = new Keys[1] { key };
        }

        public Keys[] TargetKeys { get; } = null!;
        public InputType InputType { get; } = InputType.All;

        public Action StartPressed { get; } = null!;
        public Action Pressing { get; } = null!;
        public Action EndPressed { get; } = null!;

        public bool IsPressed { get; private set; } = false;

        public void Invoke(InputType inputType, Keys keys)
        {
            switch (inputType)
            {
                case InputType.OnKeyDown:
                    StartPressed.Invoke();
                    IsPressed = true;
                    break;
                case InputType.OnKeyUp:
                    EndPressed.Invoke();
                    IsPressed = false;
                    break;
            }
        }

        public void Update(Keys key)
        {
            if (IsPressed)
                Pressing.Invoke();
        }
    }
}
