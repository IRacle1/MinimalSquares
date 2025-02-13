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
            base(new Func<double, double>[]
            {
                Math.Log,
                (x) => 1,
            }, Math.Log)
        {
        }

        public override bool IsAcceptablePoint(double x, double y)
        {
            return y > 0f && x > 0f;
        }

        public override double GetValue(double x)
        {
            return Parameters[1] * Math.Pow(x, Parameters[0]);
        }

        public override void SetParameters(Vector<double> ansv)
        {
            Parameters[0] = ansv[0];
            Parameters[1] = Math.Exp(ansv[1]);
        }
    }
}
