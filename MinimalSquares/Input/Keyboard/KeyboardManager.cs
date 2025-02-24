using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MinimalSquares.Components;
using MinimalSquares.Input.Keyboard.KeyEvents;
using System.Collections.Generic;

namespace MinimalSquares.Input.Keyboard
{
    public class KeyboardManager : BaseComponent
    {
        private Dictionary<Keys, List<IKeyEvent>> KeyDownEvents = new(10);
        private Dictionary<Keys, List<IKeyEvent>> KeyUpEvents = new(10);
        private HashSet<IDynamicKeyEvent> dynamicKeyEvents = new(10);

        public HashSet<Keys> PressedKeys = new();

        public override void Start(MainGame game)
        {
            base.Start(game);
            game.Window.KeyDown += OnKeyDown;
            game.Window.KeyUp += OnKeyUp;
        }

        private void OnKeyDown(object? sender, InputKeyEventArgs e)
        {
            if (!KeyDownEvents.TryGetValue(e.Key, out List<IKeyEvent>? events))
            {
                PressedKeys.Add(e.Key);
                return;
            }

            if (PressedKeys.Contains(e.Key))
            {
                foreach (IKeyEvent @event in events)
                {
                    if (@event is IDynamicKeyEvent dynamicKeyEvent)
                        dynamicKeyEvent.Update(e.Key);
                }

                return;
            }

            foreach (IKeyEvent @event in events)
            {
                @event.Invoke(InputType.OnKeyDown, e.Key);
            }

            PressedKeys.Add(e.Key);
        }

        private void OnKeyUp(object? sender, InputKeyEventArgs e)
        {
            PressedKeys.Remove(e.Key);

            if (!KeyUpEvents.TryGetValue(e.Key, out List<IKeyEvent>? events))
                return;

            foreach (IKeyEvent @event in events)
            {
                @event.Invoke(InputType.OnKeyUp, e.Key);
            }

        }

        public void Register(IKeyEvent keyEvent)
        {
            if (keyEvent.InputType.HasFlag(InputType.OnKeyDown))
            {
                AddNewHandle(KeyDownEvents, keyEvent);
            }

            if (keyEvent.InputType.HasFlag(InputType.OnKeyUp))
            {
                AddNewHandle(KeyUpEvents, keyEvent);
            }
        }

        private void AddNewHandle(Dictionary<Keys, List<IKeyEvent>> dictionary, IKeyEvent keyEvent)
        {
            foreach (Keys key in keyEvent.TargetKeys)
            {
                if (!dictionary.TryGetValue(key, out List<IKeyEvent>? events))
                {
                    events = new List<IKeyEvent>(3) { keyEvent };
                    dictionary.Add(key, events);
                }
                else
                {
                    events.Add(keyEvent);
                }
            }
        }
    }
}
