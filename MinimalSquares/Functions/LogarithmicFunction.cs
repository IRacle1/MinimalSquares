using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace MinimalSquares.Functions
{
    public class LogarithmicFunction : LinearAbstractFunction
    {
        public override Color Color { get; set; } = Color.Blue;

        public override float GetValue(float x)
        {
            if (!IsAcceptableArgument(x))
                return float.NaN;
            return A * MathF.Log(x) + B;
        }

        public override bool IsAcceptableArgument(float x)
        {
            return x > 0f;
        }

        public override (float, float) GetModifiedXY(float x, float y)
        {
            return (MathF.Log(x), y);
        }
    }
}
