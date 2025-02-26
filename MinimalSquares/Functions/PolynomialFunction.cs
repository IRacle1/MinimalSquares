using System;

namespace MinimalSquares.Functions
{
    public class PolynomialFunction : BaseFunction
    {
        public override string Name { get; }

        public PolynomialFunction(int monomialsCount)
            : base(monomialsCount)
        {
            Name = $"Полином {monomialsCount - 1} степени";
        }

        public override double GetMonomialValue(int monomialIndex, double x) =>
            monomialIndex switch
            {
                0 => 1,
                1 => x,
                _ => Math.Pow(x, monomialIndex),
            };

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
