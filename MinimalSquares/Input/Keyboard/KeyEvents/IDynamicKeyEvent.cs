using Microsoft.Xna.Framework.Input;

namespace MinimalSquares.Input.Keyboard.KeyEvents
{
    public interface IDynamicKeyEvent : IKeyEvent
    {
        public void Update(Keys key);
    }
}
