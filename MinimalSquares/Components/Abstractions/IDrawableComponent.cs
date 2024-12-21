using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MinimalSquares.Graphics;

namespace MinimalSquares.Components.Abstractions
{
    public interface IDrawableComponent : IComponent
    {
        public int Order { get; }
        public void UpdateVertex();
        public void Draw();
    }

    public class DrawableCompare : IComparer<IDrawableComponent>
    {
        public int Compare(IDrawableComponent? x, IDrawableComponent? y)
        {
            if (x == null || y == null)
                return 0;

            return x.Order.CompareTo(y.Order);
        }
    }
}
