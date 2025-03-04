using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathNet.Numerics.LinearAlgebra.Solvers;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double.Solvers;

namespace MinimalSquares.Functions
{
    public abstract class AbstractFunction
    {
        private static readonly IIterativeSolver<double> cachedSolver = new TFQMR();

        public abstract string Name { get; }
        public virtual Microsoft.Xna.Framework.Color Color { get; set; } = Microsoft.Xna.Framework.Color.MediumSlateBlue;

        public abstract int MonomialCount { get; }

        public virtual bool IsAcceptablePoint(double x, double y) => true;

        public abstract double GetMonomialValue(int monomialIndex, double x, double y);

        public virtual double GetYValue(double x, double y) => y;

        public abstract double GetValue(double x);

        public virtual void UpdateParameters(double[] x, double[] y)
        {
            Matrix<double> xSums = Matrix.Build.Dense(MonomialCount, MonomialCount);

            Vector<double> yxSums = Vector.Build.Dense(MonomialCount);

            int acceptableCount = 0;

            double[] monomialsCache = new double[MonomialCount];

            for (int i = 0; i < x.Length; i++)
            {
                if (!IsAcceptablePoint(x[i], y[i]))
                    continue;

                int lastSetCache = -1;

                acceptableCount++;

                double yValue = GetYValue(x[i], y[i]);

                for (int j = 0; j < MonomialCount; j++)
                {
                    double xj = GetMonomialsByCache(ref monomialsCache, j, ref lastSetCache, x[i], y[i]);

                    yxSums[j] += yValue * xj;

                    for (int k = 0; k < MonomialCount; k++)
                    {
                        double xk = GetMonomialsByCache(ref monomialsCache, k, ref lastSetCache, x[i], y[i]);

                        xSums[j, k] += xj * xk;
                    }
                }
            }

            Vector<double> ansv;

            if (acceptableCount >= MonomialCount)
                ansv = xSums.Solve(yxSums);
            else
                ansv = xSums.SolveIterative(yxSums, cachedSolver);

            SetParameters(ansv);
        }

        protected double GetMonomialsByCache(ref double[] cache, int index, ref int cacheIndex, double x, double y)
        {
            if (cacheIndex < index)
            {
                cacheIndex = index;
                cache[index] = GetMonomialValue(index, x, y);
            }

            return cache[index];
        }

        public abstract void SetParameters(Vector<double> ansv);

        public abstract string GetGeneralNotation();

        public abstract string GetFunctionNotation();

        public abstract string GetFormattedParameter(int order, bool sign);

        public override string ToString() => Name;
    }
}
