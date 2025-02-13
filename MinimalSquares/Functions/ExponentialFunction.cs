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
    // y = b * e^ax
    // ln y = ax + ln b
    public class ExponentialFunction : PolynomialFunction
    {
        public ExponentialFunction() : 
            base(new Func<double, double>[]
            {
                (x) => x,
                (x) => 1,
            }, Math.Log)
        {
        }

        public override bool IsAcceptablePoint(double x, double y)
        {
            return y > 0f;
        }

        public override double GetValue(double x)
        {
            return Parameters[1] * Math.Exp(Parameters[0] * x);
        }

        public override void SetParameters(Vector<double> ansv)
        {
            Parameters[0] = ansv[0];
            Parameters[1] = Math.Exp(ansv[1]);
        }
    }
}
