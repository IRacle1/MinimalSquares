﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using MinimalSquares.Components;
using MinimalSquares.Functions;
using MinimalSquares.Graphics;

namespace MinimalSquares.Generic
{
    public class InternalManager : BaseComponent
    {
        private FunctionManager functionManager = null!;
        private PointManager pointManager = null!;

        public override void Start(MainGame game)
        {
            functionManager = ComponentManager.Get<FunctionManager>()!;
            pointManager = ComponentManager.Get<PointManager>()!;

            base.Start(game);
        }

        public void Reset()
        {
            functionManager.CurrentFunctions.Clear();
            pointManager.Clear();

            ComponentManager.MainView.SetCamera(new Vector3(0, 0, 6));
            ComponentManager.MainView.RenderRequest(RenderRequestType.All);
        }
    }
}
