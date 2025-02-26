using System.Collections.Generic;

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
