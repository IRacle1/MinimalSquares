using System;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Double.Solvers;
using MathNet.Numerics.LinearAlgebra.Solvers;

namespace MinimalSquares.Functions
{
    public abstract class BaseFunction
    {
        private static readonly IIterativeSolver<double> cachedSolver = new TFQMR();

        public BaseFunction(int monomialsCount)
        {
            MonomialCount = monomialsCount;
            Parameters = new double[MonomialCount];
        }

        public abstract string Name { get; }
        public virtual Microsoft.Xna.Framework.Color Color { get; set; } = Microsoft.Xna.Framework.Color.MediumSlateBlue;

        public double[] Parameters { get; }
        public int MonomialCount { get; }

        public virtual bool IsAcceptablePoint(double x, double y) => true;

        public abstract double GetMonomialValue(int monomialIndex, double x);

        public virtual double GetYValue(double y) => y;

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

        protected double GetMonomialsByCache(ref double[] cache, int index, ref int cacheIndex, double x)
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

        public abstract string GetGeneralNotation();

        public abstract string GetFunctionNotation();

        public virtual double GetFormattedParameter(int order)
        {
            return Math.Round(Parameters[order], 4);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
