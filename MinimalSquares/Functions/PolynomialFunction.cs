using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Double.Solvers;

namespace MinimalSquares.Functions
{
    public class PolynomialFunction : BaseFunction
    {
        public override string Name { get; } = "Полином";

        public PolynomialFunction(int monomialsCount)
            : base(monomialsCount)
        {
        }

        public override double GetMonomialValue(int monomialIndex, double x) =>
            Math.Pow(x, monomialIndex);

        public override double GetValue(double x)
        {
            double y = 0.0;

            for (int i = 0; i < Parameters.Length; i++)
            {
                y += Parameters[i] * GetMonomialValue(i, x);
            }

            return y;
        }
    }
}
