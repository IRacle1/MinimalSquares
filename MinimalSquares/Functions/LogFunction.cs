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
    // y = a * ln x + b
    public class LogFunction : PolynomialFunction
    {
        public override string Name { get; } = "Логарифмическая функция";

        public LogFunction() : 
            base(2)
        {
        }

        public override bool IsAcceptablePoint(double x, double y)
        {
            return x > 0f;
        }

        public override double GetMonomialValue(int monomialIndex, double x) =>
            monomialIndex switch
            {
                0 => Math.Log(x),
                1 => 1,
                _ => double.NaN,
            };
    }
}
