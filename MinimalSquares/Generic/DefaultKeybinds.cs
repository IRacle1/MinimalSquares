using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MinimalSquares.Components;
using MinimalSquares.Input.Keyboard;
using MinimalSquares.Input.Keyboard.KeyEvents;

namespace MinimalSquares.Generic
{
    public class DefaultKeybinds : BaseComponent
    {
        private KeyboardManager keyboardManager = null!;
        public override void Start(MainGame game)
        {
            keyboardManager = ComponentManager.Get<KeyboardManager>()!;

            keyboardManager.Register(new BasicKeyEvent(
                (_, _) => targetGame.Exit(), 
                InputType.OnKeyDown, 
                Microsoft.Xna.Framework.Input.Keys.Escape));
            base.Start(game);
        }
    }
}
