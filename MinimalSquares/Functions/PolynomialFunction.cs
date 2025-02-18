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

        public PolynomialFunction(int maxPow)
        {
            MonomialCount = maxPow + 1;

            Parameters = InitParameters();

            YFunction = (y) => y;
            MonomialFunctions = new Func<double, double>[MonomialCount];
            for (int i = 0; i < MonomialCount; i++)
            {
                int k = i;
                MonomialFunctions[i] = (x) => Math.Pow(x, k);
            }
        }

        public PolynomialFunction(Func<double, double>[] monomials, Func<double, double>? yFunction = null, Func<double, double, bool>? acceptablePoint = null)
        {
            MonomialCount = monomials.Length;

            Parameters = InitParameters();
            AcceptablePointDelegate = acceptablePoint;

            YFunction = yFunction ?? ((y) => y);
            MonomialFunctions = monomials;
        }

        public Func<double, double>[] MonomialFunctions { get; }
        public Func<double, double> YFunction { get; }

        public Func<double, double, bool>? AcceptablePointDelegate { get; }

        public double[] Parameters { get; }
        public int MonomialCount { get; }

        public override bool IsAcceptablePoint(double x, double y)
        {
            return AcceptablePointDelegate == null ? base.IsAcceptablePoint(x, y) : AcceptablePointDelegate(x, y);
        }

        public override double GetValue(double x)
        {
            double y = 0.0f;

            for (int i = 0; i < Parameters.Length; i++)
            {
                y += Parameters[i] * MonomialFunctions[i](x);
            }

            return y;
        }

        public override void UpdateParameters(double[] x, double[] y)
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
                acceptableCount++;
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

            Matrix<double> mainMatrix = Matrix.Build.DenseOfRowArrays(xSums);

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
        public override double[] GetParameters() 
        {
            return Parameters;
        }
    }
}
