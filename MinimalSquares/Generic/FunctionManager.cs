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

        public List<BaseFunction> Functions { get; } = new();

        public override void Start(MainGame game)
        {
            pointManager = ComponentManager.Get<PointManager>()!;
            keyboardManager = ComponentManager.Get<KeyboardManager>()!;
            //keyboardManager.Register(new BasicKeyEvent(
            //    (_, key) =>
            //    {
            //        int num = (int)key - 48;
            //        Functions.Clear();
            //        Functions.Add(new PolynomialFunction(num));

            //        pointManager.TriggerUpdate();
            //    }, InputType.OnKeyDown, Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9));

            functionsGraphic = ComponentManager.Get<FunctionsGraphic>()!;

            Functions.Add(new SinFunction(1f));

            pointManager.OnPointsUpdate += OnPointsUpdate; 

            base.Start(game);
        }

        internal void OnPointsUpdate()
        {
            float[] arrX = pointManager.Points.Select(i => i.X).ToArray();
            float[] arrY = pointManager.Points.Select(i => i.Y).ToArray();

            foreach (var item in Functions)
            {
                if (IsValidFunction(item))
                    item.UpdateParameters(arrX, arrY);
            }

            functionsGraphic.UpdateVertex();
        }

        public bool IsValidFunction(BaseFunction function)
        {
            return pointManager.Points.Count >= function.RequiredPoints;
        }
    }
}
