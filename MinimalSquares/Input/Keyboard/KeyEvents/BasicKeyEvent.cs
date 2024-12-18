using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;
using MinimalSquares.Input.Keyboard;

namespace MinimalSquares.Input.Keyboard.KeyEvents
{
    public class BasicKeyEvent : IKeyEvent
    {
        public BasicKeyEvent(Action<InputType, Keys> handle, InputType inputType, Keys key)
        {
            Handle = handle;
            InputType = inputType;
            TargetKeys = new Keys[1] { key };
        }

        public BasicKeyEvent(Action<InputType, Keys> handle, InputType inputType, params Keys[] keys)
        {
            Handle = handle;
            InputType = inputType;
            TargetKeys = keys;
        }

        public Keys[] TargetKeys { get; } = null!;
        public InputType InputType { get; }
        public Action<InputType, Keys> Handle { get; } = null!;

        public void Invoke(InputType inputType, Keys keys)
        {
            Handle.Invoke(inputType, keys);
        }
    }
}
