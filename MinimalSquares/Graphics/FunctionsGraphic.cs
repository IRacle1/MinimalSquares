﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MinimalSquares.Components;
using MinimalSquares.Components.Abstractions;
using MinimalSquares.Functions;

namespace MinimalSquares.Graphics
{
    public class FunctionsGraphic : BaseComponent, IDrawableComponent
    {
        private FunctionManager functionManager = null!;
        private PointManager pointManager = null!;
        private MainView view = null!;

        private VertexPositionColor[] functionsVertex = null!;

        public int Order { get; } = 3;

        public override void Start(MainGame game)
        {
            base.Start(game);
            functionManager = ComponentManager.Get<FunctionManager>()!;
            pointManager = ComponentManager.Get<PointManager>()!;
            view = ComponentManager.Get<MainView>()!;
        }

        public void UpdateVertex()
        {
            List<VertexPositionColor> list = new(functionManager.CurrentFunctions.Count * 100);

            for (int i = 0; i < functionManager.CurrentFunctions.Count; i++)
            {
                BaseFunction function = functionManager.CurrentFunctions[i];

                float left = view.RenderLeftUpBorder.X;
                float right = view.RenderRightDownBorder.X;

                for (float x = left; x < right; x += MainView.Step)
                {
                    float y = (float)function.GetValue(x);
                    if (float.IsNormal(y))
                    {
                        list.Add(new VertexPositionColor(new Vector3(x, y, 0f), function.Color));

                        list.Add(new VertexPositionColor(new Vector3(x, y + MainView.GrafhicStep, 0f), function.Color));
                        list.Add(new VertexPositionColor(new Vector3(x, y - MainView.GrafhicStep, 0f), function.Color));
                        list.Add(new VertexPositionColor(new Vector3(x + MainView.GrafhicStep, y, 0f), function.Color));
                        list.Add(new VertexPositionColor(new Vector3(x - MainView.GrafhicStep, y, 0f), function.Color));

                        list.Add(new VertexPositionColor(new Vector3(x, y + 2 * MainView.GrafhicStep, 0f), function.Color));
                        list.Add(new VertexPositionColor(new Vector3(x, y - 2 * MainView.GrafhicStep, 0f), function.Color));
                        list.Add(new VertexPositionColor(new Vector3(x + 2 * MainView.GrafhicStep, y, 0f), function.Color));
                        list.Add(new VertexPositionColor(new Vector3(x - 2 * MainView.GrafhicStep, y, 0f), function.Color));
                    }
                }
            }

            functionsVertex = list.ToArray();
        }

        public void Draw()
        {
            if (functionsVertex != null && functionsVertex.Length > 0)
                targetGame.GraphicsDevice.DrawUserPrimitives(PrimitiveType.PointList, functionsVertex, 0, functionsVertex.Length);
        }
    }
}
