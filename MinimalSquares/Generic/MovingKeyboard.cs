using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using MinimalSquares.Components;
using MinimalSquares.Components.Abstractions;
using MinimalSquares.Functions;
using MinimalSquares.Graphics;
using MinimalSquares.Input.Keyboard;

namespace MinimalSquares.Generic
{
    public class MovingKeyboard : BaseComponent, IUpdatedComponent
    {
        private Dictionary<Keys, Vector3> KeysToVectors = new()
        {
            { Keys.W, Vector3.UnitY },
            { Keys.S, -Vector3.UnitY },
            { Keys.D, Vector3.UnitX },
            { Keys.A, -Vector3.UnitX },
        };
            

        public override void Start(MainGame game)
        {
            base.Start(game);
        }

        public void Update(GameTime gameTime)
        {
            Vector3 moveVector = Vector3.Zero;

            foreach (var item in KeysToVectors)
            {
                if (ComponentManager.KeyboardManager.PressedKeys.Contains(item.Key))
                {
                    moveVector += item.Value;
                }
            }

            if (moveVector == Vector3.Zero)
                return;

            moveVector.Normalize();

            Vector3 newCameraPosition = ComponentManager.MainView.CameraPosition + (moveVector * MainView.GrafhicStep * 10f);
            if (!ComponentManager.MainView.SetCamera(newCameraPosition))
                return;

            if (!ComponentManager.MainView.IsOnScreen(ComponentManager.MainView.CameraPosition.GetXY()))
                ComponentManager.MainView.RenderRequest(RenderRequestType.All);
        }
    }
}
