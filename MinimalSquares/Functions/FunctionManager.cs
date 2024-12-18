using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MinimalSquares.Components;
using MinimalSquares.Graphics;

namespace MinimalSquares.Functions
{
    public class FunctionManager : BaseComponent
    {
        private PointManager pointManager = null!;
        private MainGraphicController mainGraphicController = null!;

        public List<IFunction> Functions { get; } = new();

        public override void Start(MainGame game)
        {
            pointManager = ComponentManager.Get<PointManager>()!;
            mainGraphicController = ComponentManager.Get<MainGraphicController>()!;
            Functions.Add(new LinearFunction());
            base.Start(game);
        }

        internal void OnPointUpdate()
        {
            float[] arrX = pointManager.Points.Select(i => i.X).ToArray();
            float[] arrY = pointManager.Points.Select(i => i.Y).ToArray();

            foreach (var item in Functions)
            {
                item.UpdateParameters(arrX, arrY);
            }

            mainGraphicController.UpdateVertex();
        }
    }
}
