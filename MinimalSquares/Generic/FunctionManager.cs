using MinimalSquares.Components;
using MinimalSquares.Functions.Special;
using MinimalSquares.Generic;
using System;
using System.Collections.Generic;

namespace MinimalSquares.Functions
{
    public class FunctionManager : BaseComponent
    {
        private PointManager pointManager = null!;

        public List<AbstractFunction> CurrentFunctions { get; } = new();

        public List<AbstractFunction> AvaibleFunctions { get; } = new()
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

            pointManager.OnPointsUpdate += UpdateParameters;

            base.Start(game);
        }

        public void UpdateParameters()
        {
            (double[] arrX, double[] arrY) = pointManager.GetAsArrays();

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
