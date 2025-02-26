using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Double.Solvers;
using MathNet.Numerics.LinearAlgebra.Solvers;

using Microsoft.Xna.Framework;

namespace MinimalSquares.Functions
{
    public abstract class BaseFunction
    {
        private static IIterativeSolver<double> cachedSolver = new TFQMR();

        public BaseFunction(int monomialsCount)
        {
            MonomialCount = monomialsCount;
            Parameters = new double[MonomialCount];
        }

        public abstract string Name { get; }
        public virtual Color Color { get; set; } = Color.MediumSlateBlue;

        public double[] Parameters { get; }
        public int MonomialCount { get; }

        public virtual bool IsAcceptablePoint(double v1, double v2) => true;

        public abstract double GetMonomialValue(int monomialIndex, double x);

        public virtual double GetYValue(double y) => y;

        public abstract double GetValue(double x);

        public void UpdateParameters(double[] x, double[] y)
        {
            Matrix<double> xSums = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.Dense(MonomialCount, MonomialCount);

            Vector<double> yxSums = Vector.Build.Dense(MonomialCount);

            int acceptableCount = 0;

            double[] monomialsCache = new double[MonomialCount];

            for (int i = 0; i < x.Length; i++)
            {
                if (!IsAcceptablePoint(x[i], y[i]))
                    continue;

                int lastSetCache = -1;

                acceptableCount++;

                double yValue = GetYValue(y[i]);

                for (int j = 0; j < MonomialCount; j++)
                {
                    double xj = GetMonomialsByCache(ref monomialsCache, j, ref lastSetCache, x[i]);

                    yxSums[j] += yValue * xj;

                    for (int k = 0; k < MonomialCount; k++)
                    {
                        double xk = GetMonomialsByCache(ref monomialsCache, k, ref lastSetCache, x[i]);

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

        private double GetMonomialsByCache(ref double[] cache, int index, ref int cacheIndex, double x)
        {
            if (cacheIndex < index)
            {
                cacheIndex = index;
                cache[index] = GetMonomialValue(index, x);
            }

            return cache[index];
        }

        public virtual void SetParameters(Vector<double> ansv)
        {
            for (int i = 0; i < Parameters.Length; i++)
            {
                Parameters[i] = ansv[i];
            }
        }
    }
}
