using MathNet.Numerics.LinearAlgebra;
using MinimalSquares.Components;

using System;
using System.Text;

namespace MinimalSquares.Functions
{
    // y = b * e^ax
    // ln y = ax + ln b
    public class ExponentialFunction : BaseFunction
    {
        public override string Name { get; } = "Экспонента";

        public ExponentialFunction() :
            base(2)
        {
        }

        public override bool IsAcceptablePoint(double x, double y)
        {
            return y > 0f;
        }

        public override double GetMonomialValue(int monomialIndex, double x, double y) =>
            monomialIndex switch
            {
                0 => x,
                1 => 1,
                _ => double.NaN,
            };

        public override double GetYValue(double y, double x)
        {
            return Math.Log(y);
        }

        public override double GetValue(double x)
        {
            return Parameters[1] * Math.Exp(Parameters[0] * x);
        }

        public override void SetParameters(Vector<double> ansv)
        {
            Parameters[0] = ansv[0];
            Parameters[1] = Math.Exp(ansv[1]);
        }

        public override string GetGeneralNotation()
        {
            return @"b * e^{ax}";
        }

        public override string GetFunctionNotation()
        {
            return $"{GetFormattedParameter(1, false)} * e^{{{GetFormattedParameter(0, false)}x}}";
        }
    }
}
