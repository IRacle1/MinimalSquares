using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
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
        private FunctionManager functionManager = null!;
        private InternalManager internalManager = null!;
        private PointManager pointManager = null!;

        public override void Start(MainGame game)
        {
            base.Start(game);

            functionManager = ComponentManager.Get<FunctionManager>()!;
            internalManager = ComponentManager.Get<InternalManager>()!;
            pointManager = ComponentManager.Get<PointManager>()!;

            ComponentManager.KeyboardManager.Register(new BasicKeyEvent(
                (_, _) => targetGame.Exit(), 
                InputType.OnKeyDown, 
                Keys.Escape));

            ComponentManager.KeyboardManager.Register(new BasicKeyEvent(
                (_, _) =>
                {
                    internalManager.ResetAll();
                },
                InputType.OnKeyDown,
                Keys.R));

            ComponentManager.KeyboardManager.Register(new BasicKeyEvent(
                (_, _) =>
                {
                    pointManager.Add(ComponentManager.MainView.CenterWorld.GetXY());
                    ComponentManager.MainView.RenderRequest(RenderRequestType.Static);
                },
                InputType.OnKeyDown,
                Keys.Space));

            ComponentManager.KeyboardManager.Register(new BasicKeyEvent(
                (_, key) =>
                {
                    int num = (int)key - 48;
                    functionManager.CurrentFunctions.Clear();
                    functionManager.CurrentFunctions.Add(functionManager.AvaibleFunctions[num]);

                    functionManager.UpdateParameters();

                    ComponentManager.MainView.RenderRequest(RenderRequestType.Function);
                }, InputType.OnKeyDown, Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9));
        }
    }
}
