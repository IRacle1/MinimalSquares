using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Double.Solvers;

using Microsoft.Xna.Framework;

namespace MinimalSquares.Functions
{
    public abstract class BaseFunction
    {
        public BaseFunction(int monomialsCount)
        {
            MonomialCount = monomialsCount;
            Parameters = InitParameters();
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
            double[][] xSums = new double[MonomialCount][];

            for (int j = 0; j < MonomialCount; j++)
            {
                xSums[j] = new double[MonomialCount];
            }

            double[] yxSums = new double[MonomialCount];

            int acceptableCount = 0;

            for (int i = 0; i < x.Length; i++)
            {
                if (!IsAcceptablePoint(x[i], y[i]))
                {
                    continue;
                }
                acceptableCount++;

                for (int j = 0; j < MonomialCount; j++)
                {
                    yxSums[j] += GetYValue(y[i]) * GetMonomialValue(j, x[i]);

                    for (int k = 0; k < MonomialCount; k++)
                    {
                        xSums[j][k] += GetMonomialValue(j, x[i]) * GetMonomialValue(k, x[i]);
                    }
                }
            }

            Matrix<double> mainMatrix = MathNet.Numerics.LinearAlgebra.Double.Matrix.Build.DenseOfRowArrays(xSums);

            Vector<double> vector = Vector.Build.DenseOfArray(yxSums);

            Vector<double> ansv;

            if (acceptableCount >= MonomialCount)
                ansv = mainMatrix.Solve(vector);
            else
                ansv = mainMatrix.SolveIterative(vector, new TFQMR());

            SetParameters(ansv);
        }

        public virtual double[] InitParameters()
        {
            return new double[MonomialCount];
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
