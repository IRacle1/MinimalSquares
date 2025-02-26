using Microsoft.Xna.Framework.Input;

namespace MinimalSquares.Input.Keyboard.KeyEvents
{
    public interface IKeyEvent
    {
        public Keys[] TargetKeys { get; }
        public InputType InputType { get; }

        public void Invoke(InputType inputType, Keys keys);
    }
}
