using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;

using MinimalSquares.Components;
using MinimalSquares.Functions;
using MinimalSquares.Graphics;
using MinimalSquares.Input.Keyboard;
using MinimalSquares.Input.Keyboard.KeyEvents;

namespace MinimalSquares.Generic
{
    public class DefaultKeybinds : BaseComponent
    {
        private KeyboardManager keyboardManager = null!;
        private FunctionManager functionManager = null!;

        public override void Start(MainGame game)
        {
            keyboardManager = ComponentManager.Get<KeyboardManager>()!;
            functionManager = ComponentManager.Get<FunctionManager>()!;

            keyboardManager.Register(new BasicKeyEvent(
                (_, _) => targetGame.Exit(), 
                InputType.OnKeyDown, 
                Keys.Escape));
            base.Start(game);

            keyboardManager.Register(new BasicKeyEvent(
                (_, key) =>
                {
                    int num = (int)key - 48;
                    functionManager.CurrentFunctions.Clear();
                    functionManager.CurrentFunctions.Add(functionManager.AvaibleFunctions[num]);

                    functionManager.UpdateParameters();
                }, InputType.OnKeyDown, Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9));
        }
    }
}
