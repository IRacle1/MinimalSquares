using System;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Double.Solvers;
using MathNet.Numerics.LinearAlgebra.Solvers;

namespace MinimalSquares.Functions
{
    public abstract class BaseFunction : AbstractFunction
    {
        public BaseFunction(int monomialsCount)
        {
            Parameters = new double[monomialsCount];
        }

        public double[] Parameters { get; }
        public override int MonomialCount => Parameters.Length;

        public override void SetParameters(Vector<double> ansv)
        {
            for (int i = 0; i < Parameters.Length; i++)
            {
                Parameters[i] = ansv[i];
            }
        }

        public override string GetFormattedParameter(int order, bool sign)
        {
            return Parameters[order].Format(sign);
        }
    }
}
