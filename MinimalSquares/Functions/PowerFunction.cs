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
    // y = b * x^a
    // ln y = a * ln x + ln b
    public class PowerFunction : PolynomialFunction
    {
        public PowerFunction() : 
            base(new Func<float, float>[]
            {
                MathF.Log,
                (x) => 1,
            }, MathF.Log)
        {
        }

        public override bool IsAcceptablePoint(float x, float y)
        {
            return y > 0f && x > 0f;
        }

        public override float GetValue(float x)
        {
            return Parameters[1] * MathF.Pow(x, Parameters[0]);
        }

        public override float[] InitParameters()
        {
            return new float[2];
        }

        public override void SetParameters(Vector<float> ansv)
        {
            Parameters[0] = ansv[0];
            Parameters[1] = MathF.Exp(ansv[1]);
        }
    }
}
