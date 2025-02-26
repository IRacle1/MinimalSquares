using Microsoft.Xna.Framework.Input;
using System;

namespace MinimalSquares.Input.Keyboard.KeyEvents
{
    public class TwoStatesKeyEvent : IKeyEvent
    {
        public TwoStatesKeyEvent(Action<Keys> keyDown, Action<Keys> keyUp, Keys key)
        {
            KeyDownHandle = keyDown;
            KeyUpHandle = keyUp;

            TargetKeys = new Keys[1] { key };
        }

        public TwoStatesKeyEvent(Action<Keys> keyDown, Action<Keys> keyUp, params Keys[] keys)
        {
            KeyDownHandle = keyDown;
            KeyUpHandle = keyUp;

            TargetKeys = keys;
        }

        public Keys[] TargetKeys { get; } = null!;
        public InputType InputType { get; } = InputType.All;

        public Action<Keys> KeyDownHandle { get; } = null!;
        public Action<Keys> KeyUpHandle { get; } = null!;

        public void Invoke(InputType inputType, Keys keys)
        {
            switch (inputType)
            {
                case InputType.OnKeyDown:
                    KeyDownHandle.Invoke(keys);
                    break;
                case InputType.OnKeyUp:
                    KeyUpHandle.Invoke(keys);
                    break;
            }
        }
    }
}
