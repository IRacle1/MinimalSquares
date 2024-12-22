using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;

namespace MinimalSquares.Functions
{
    // y = a * sin(kx + b) + c
    // k - константа

    // y = a * cos(b) * sin(kx) + a * sin(b) * cos(kx) + c
    // a1 = a * cos(b)
    // a2 = a * sin(b)
    // a^2 = a1^2 + a2^2
    // b = arctg(a2/a1)
    public class SinFunction : PolynomialFunction
    {
        public SinFunction(float k) : 
            base(new Func<float, float>[]
            {
                (x) => MathF.Sin(k * x),
                (x) => MathF.Cos(k * x),
                (x) => 1,
            })
        {
            K = k;
        }

        public float K { get; }

        public override float GetValue(float x)
        {
            return Parameters[0] * MathF.Sin(x * K + Parameters[1]) + Parameters[2];
        }

        public override float[] InitParameters()
        {
            return new float[3];
        }

        public override void SetParameters(Vector<float> ansv)
        {
            (float a1, float a2) = (ansv[0], ansv[1]);
            Parameters[2] = ansv[2];
            Parameters[0] = MathF.Sqrt(a1 * a1 + a2 * a2);
            Parameters[1] = MathF.Atan2(a2, a1);
        }
    }
}
