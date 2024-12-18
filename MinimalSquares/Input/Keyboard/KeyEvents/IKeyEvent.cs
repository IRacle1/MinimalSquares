using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;
using MinimalSquares.Input.Keyboard;

namespace MinimalSquares.Input.Keyboard.KeyEvents
{
    public interface IKeyEvent
    {
        public Keys[] TargetKeys { get; }
        public InputType InputType { get; }

        public void Invoke(InputType inputType, Keys keys);
    }
}
