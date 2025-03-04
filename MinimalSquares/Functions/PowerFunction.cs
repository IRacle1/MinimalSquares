using MathNet.Numerics.LinearAlgebra;
using System;

namespace MinimalSquares.Functions
{
    // y = b * x^a
    // ln y = a * ln x + ln b
    public class PowerFunction : BaseFunction
    {
        public override string Name { get; } = "Степенная";

        public PowerFunction() :
            base(2)
        {
        }

        public override bool IsAcceptablePoint(double x, double y)
        {
            return y > 0f && x > 0f;
        }

        public override double GetMonomialValue(int monomialIndex, double x, double y) =>
            monomialIndex switch
            {
                0 => Math.Log(x),
                1 => 1,
                _ => double.NaN,
            };

        public override double GetFreeValue(double x, double y)
        {
            return Math.Log(y);
        }

        public override double GetValue(double x)
        {
            return Parameters[1] * Math.Pow(x, Parameters[0]);
        }

        public override void SetParameters(Vector<double> ansv)
        {
            Parameters[0] = ansv[0];
            Parameters[1] = Math.Exp(ansv[1]);
        }

        public override string GetGeneralNotation()
        {
            return @"b * x^a";
        }

        public override string GetFunctionNotation()
        {
            return $"{GetFormattedParameter(1, false)} * x^{{{GetFormattedParameter(0, false)}}}";
        }
    }
}
