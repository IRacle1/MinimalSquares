using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;

using MinimalSquares.Components;
using MinimalSquares.Graphics;
using MinimalSquares.Input.Keyboard;
using MinimalSquares.Input.Keyboard.KeyEvents;

namespace MinimalSquares.Functions
{
    public class FunctionManager : BaseComponent
    {
        private PointManager pointManager = null!;
        private FunctionsGraphic functionsGraphic = null!;
        private KeyboardManager keyboardManager = null!;

        public List<BaseFunction> CurrentFunctions { get; } = new();

        public List<BaseFunction> AvaibleFunctions { get; } = new()
        {
            new PolynomialFunction(0),
            new PolynomialFunction(1),
            new PolynomialFunction(2),
            new PolynomialFunction(3),
            new PolynomialFunction(4),
            new PolynomialFunction(5),
            new PolynomialFunction(new Func<double, double>[] {
                Math.Log,
                x => 1
            }, acceptablePoint: (x, y) => x > 0),
            new ExponentialFunction(),
            new PowerFunction(),
            new SinFunction(1f),
        };

        public override void Start(MainGame game)
        {
            pointManager = ComponentManager.Get<PointManager>()!;
            keyboardManager = ComponentManager.Get<KeyboardManager>()!;
            keyboardManager.Register(new BasicKeyEvent(
                (_, key) =>
                {
                    int num = (int)key - 48;
                    CurrentFunctions.Clear();
                    CurrentFunctions.Add(AvaibleFunctions[num]);

                    pointManager.TriggerUpdate();
                }, InputType.OnKeyDown, Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9));

            functionsGraphic = ComponentManager.Get<FunctionsGraphic>()!;

            pointManager.OnPointsUpdate += OnPointsUpdate; 

            base.Start(game);
        }

        internal void OnPointsUpdate()
        {
            double[] arrX = pointManager.Points.Select(i => (double)i.X).ToArray();
            double[] arrY = pointManager.Points.Select(i => (double)i.Y).ToArray();

            foreach (var item in CurrentFunctions)
            {
                item.UpdateParameters(arrX, arrY);
            }

            functionsGraphic.UpdateVertex();
        }
    }
}
