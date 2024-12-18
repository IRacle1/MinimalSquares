using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalSquares.Components.Abstractions
{
    public interface IDrawableComponent : IComponent
    {
        public void Draw();
    }
}
