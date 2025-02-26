using System;

namespace MinimalSquares.Functions
{
    // y = a * ln x + b
    public class LogFunction : PolynomialFunction
    {
        public override string Name { get; } = "Логарифм";

        public LogFunction() :
            base(2)
        {
        }

        public override bool IsAcceptablePoint(double x, double y)
        {
            return x > 0f;
        }

        public override double GetMonomialValue(int monomialIndex, double x) =>
            monomialIndex switch
            {
                0 => Math.Log(x),
                1 => 1,
                _ => double.NaN,
            };

        public override string GetGeneralNotation()
        {
            return @"y \sim a * \ln(x) + b";
        }

        public override string GetFunctionNotation()
        {
            double a = GetFormattedParameter(0);
            double b = GetFormattedParameter(1);

            return $"y = {a} * \\ln(x) + {(b < 0 ? b.ToString() : $"+ {b}")}";
        }
    }
}
