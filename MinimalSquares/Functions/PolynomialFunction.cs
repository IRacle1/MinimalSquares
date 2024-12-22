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
            MonomialCount = maxPow + 1;

            Parameters = InitParameters();

            YFunction = (y) => y;
            MonomialFunctions = new Func<float, float>[MonomialCount];
            for (int i = 0; i < MonomialCount; i++)
            {
                int k = i;
                MonomialFunctions[i] = (x) => MathF.Pow(x, k);
            }

            RequiredPoints = MonomialCount;
        }

        public PolynomialFunction(Func<float, float>[] monomials, Func<float, float>? yFunction = null, Func<float, float, bool>? AcceptablePoint = null)
        {
            MonomialCount = monomials.Length;

            Parameters = InitParameters();
            AcceptablePointDelegate = AcceptablePoint;

            YFunction = yFunction ?? ((y) => y);
            MonomialFunctions = monomials;

            RequiredPoints = MonomialCount;
        }

        public Func<float, float>[] MonomialFunctions { get; }
        public Func<float, float> YFunction { get; }

        public Func<float, float, bool>? AcceptablePointDelegate { get; }

        public float[] Parameters { get; }
        public int MonomialCount { get; }

        public override bool IsAcceptablePoint(float x, float y)
        {
            return AcceptablePointDelegate == null ? base.IsAcceptablePoint(x, y) : AcceptablePointDelegate(x, y);
        }

        public override float GetValue(float x)
        {
            float y = 0.0f;

            for (int i = 0; i < Parameters.Length; i++)
            {
                y += Parameters[i] * MonomialFunctions[i](x);
            }

            return y;
        }

        public override void UpdateParameters(float[] x, float[] y)
        {
            if (x.Length < RequiredPoints)
                return;

            float[][] xSums = new float[MonomialCount][];

            for (int j = 0; j < MonomialCount; j++)
            {
                xSums[j] = new float[MonomialCount];
            }

            float[] yxSums = new float[MonomialCount];

            for (int i = 0; i < x.Length; i++)
            {
                if (!IsAcceptablePoint(x[i], y[i]))
                {
                    continue;
                }
                
                for (int j = 0; j < MonomialCount; j++)
                {
                    yxSums[j] += YFunction(y[i]) * MonomialFunctions[j](x[i]);

                    for (int k = 0; k < MonomialCount; k++)
                    {
                        xSums[j][k] += MonomialFunctions[j](x[i]) * MonomialFunctions[k](x[i]);
                    }
                }
            }

            Matrix<float> mainMatrix = Matrix.Build.DenseOfRowArrays(xSums);

            Vector<float> vector = Vector.Build.DenseOfArray(yxSums);

            Vector<float> ansv = mainMatrix.Solve(vector);

            SetParameters(ansv);
        }

        public virtual float[] InitParameters()
        {
            return new float[MonomialCount];
        }

        public virtual void SetParameters(Vector<float> ansv)
        {
            for (int i = 0; i < Parameters.Length; i++)
            {
                Parameters[i] = ansv[i];
            }
        }
    }
}
