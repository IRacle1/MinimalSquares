﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;

using MinimalSquares.Components;
using MinimalSquares.Generic;
using MinimalSquares.Graphics;
using MinimalSquares.Input.Keyboard;
using MinimalSquares.Input.Keyboard.KeyEvents;

namespace MinimalSquares.Functions
{
    public class FunctionManager : BaseComponent
    {
        private PointManager pointManager = null!;
        private FunctionsGraphic functionsGraphic = null!;

        public List<BaseFunction> CurrentFunctions { get; } = new();

        public List<BaseFunction> AvaibleFunctions { get; } = new()
        {
            new PolynomialFunction(1),
            new PolynomialFunction(2),
            new PolynomialFunction(3),
            new PolynomialFunction(4),
            new PolynomialFunction(5),
            new PolynomialFunction(6),
            new LogFunction(),
            new ExponentialFunction(),
            new PowerFunction(),
            new SinFunction(1.0),
        };

        public event Action? OnFunctionUpdate;

        public override void Start(MainGame game)
        {
            pointManager = ComponentManager.Get<PointManager>()!;
            functionsGraphic = ComponentManager.Get<FunctionsGraphic>()!;

            pointManager.OnPointsUpdate += UpdateParameters;

            base.Start(game);
        }

        public void UpdateParameters()
        {
            double[] arrX = pointManager.Points.Select(i => (double)i.X).ToArray();
            double[] arrY = pointManager.Points.Select(i => (double)i.Y).ToArray();

            foreach (var item in CurrentFunctions)
            {
                item.UpdateParameters(arrX, arrY);
            }

            TriggerUpdate();
        }

        public void TriggerUpdate()
        {
            OnFunctionUpdate?.Invoke();
        }
    }
}
