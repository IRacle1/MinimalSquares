using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace MinimalSquares.Functions
{
    public class LinearFunction : LinearAbstractFunction
    {
        public override Color Color { get; set; } = Color.Blue;

        public override float GetValue(float x)
        {
            return A * x + B;
        }
    }
}
