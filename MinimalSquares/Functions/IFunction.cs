using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace MinimalSquares.Functions
{
    public interface IFunction
    {
        public Color Color { get; }

        public void UpdateParameters(float[] x, float[] y);

        public float GetValue(float x);
    }
}
