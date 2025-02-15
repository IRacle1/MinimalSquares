using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MinimalSquares.Components;
using MinimalSquares.Components.Abstractions;
using MinimalSquares.Input.MouseInput;

namespace MinimalSquares.Graphics
{
    public class Move : BaseComponent, IUpdatedComponent
    {
        MouseController mouseController = null!;
        MainView view = null!;

        private Vector2 StartPoint { get; set; }
        private Vector2 PreviousPosition { get; set; }
        private Vector2 Delta { get; set; }
        private Vector3 MoveVector { get; set; }
        
        private bool flag { get; set; } = true;

        public override void Start(MainGame game) 
        {
            base.Start(game);
            
            mouseController = ComponentManager.Get<MouseController>()!;
            view = ComponentManager.Get<MainView>()!;
        }

        public void Update()
        {
            if (mouseController.IsRightButtonPressed && flag)
            {
                flag = false;
                
                StartPoint = mouseController.CursorPosition;
                PreviousPosition = StartPoint;
            }
            else if (mouseController.IsRightButtonPressed && !flag)
            {
                Delta = (mouseController.CursorPosition - PreviousPosition) * new Vector2(-1, 1);
                PreviousPosition = mouseController.CursorPosition;

                MoveVector = view.CameraPosition + new Vector3(Delta, 0f) / 130f;
                view.SetCameraPosition(MoveVector, MoveVector);
            }
            else if (!mouseController.IsRightButtonPressed && !flag) 
            {
                flag = true;
                ComponentManager.UpdateVertexes();
            }
        }
    }
}
