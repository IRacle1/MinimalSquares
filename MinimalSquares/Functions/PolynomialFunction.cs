using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;

namespace MinimalSquares.Functions
{
    public class PolynomialFunction : BaseFunction
    {
        public PolynomialFunction(int maxPow)
        {
            Parameters = new float[maxPow + 1];
            MaxPow = maxPow;
            RequiredPoints = MaxPow + 1;
        }

        public float[] Parameters { get; }
        public int MaxPow { get; }

        public override int RequiredPoints { get; set; }

        public override float GetValue(float x)
        {
            float y = 0.0f;

            for (int i = 0; i < Parameters.Length; i++)
            {
                y += Parameters[i] * MathF.Pow(x, i);
            }

            return y;
        }

        public override void UpdateParameters(float[] x, float[] y)
        {
            if (x.Length < RequiredPoints)
                return;

            float[] xSums = new float[MaxPow * 2 + 1];
            float[] yxSums = new float[MaxPow + 1];

            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j < xSums.Length; j++)
                {
                    xSums[j] += MathF.Pow(x[i], j);

                    if (j < yxSums.Length)
                        yxSums[yxSums.Length - 1 - j] += y[i] * MathF.Pow(x[i], j);
                }
            }

            float[][] main = new float[MaxPow + 1][];

            for (int i = 0; i < MaxPow + 1; i++)
            {
                main[i] = new float[MaxPow + 1];

                for (int j = 0; j < MaxPow + 1; j++)
                {
                    main[i][j] = xSums[xSums.Length - 1 - (i + j)];
                }
            }

            Matrix<float> mainMatrix = Matrix.Build.DenseOfRowArrays(main);

            Vector<float> vector = Vector.Build.DenseOfArray(yxSums);

            Vector<float> ansv = mainMatrix.Solve(vector);
            for (int i = 0; i < Parameters.Length; i++)
            {
                Parameters[Parameters.Length - 1 - i] = ansv[i];
            }
        }
    }
}
