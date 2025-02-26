using MathNet.Numerics.LinearAlgebra;
using System;

namespace MinimalSquares.Functions
{
    // y = a * sin(kx + b) + c
    // k - константа

    // y = a * cos(b) * sin(kx) + a * sin(b) * cos(kx) + c
    // a1 = a * cos(b)
    // a2 = a * sin(b)
    // a^2 = a1^2 + a2^2
    // b = arctg(a2/a1)
    public class SinFunction : BaseFunction
    {
        public override string Name { get; } = "Синусоида";

        public SinFunction(double k) :
            base(3)
        {
            K = k;
        }

        public double K { get; }

        public override double GetValue(double x)
        {
            return Parameters[0] * Math.Sin(x * K + Parameters[1]) + Parameters[2];
        }

        public override double GetMonomialValue(int monomialIndex, double x) => monomialIndex switch
        {
            0 => Math.Sin(K * x),
            1 => Math.Cos(K * x),
            2 => 1,
            _ => double.NaN,
        };

        public override void SetParameters(Vector<double> ansv)
        {
            (double a1, double a2) = (ansv[0], ansv[1]);
            Parameters[2] = ansv[2];
            Parameters[0] = Math.Sqrt(a1 * a1 + a2 * a2);
            Parameters[1] = Math.Atan2(a2, a1);
        }
    }
}
