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

        public override double GetMonomialValue(int monomialIndex, double x, double y) =>
            monomialIndex switch
            {
                0 => Math.Log(x),
                1 => 1,
                _ => double.NaN,
            };

        public override string GetGeneralNotation()
        {
            return @"a * \ln(x) + b";
        }

        public override string GetFunctionNotation()
        {
            string a = GetFormattedParameter(0, false);
            string b = GetFormattedParameter(1, true);

            return $"{a} * \\ln(x) {b}";
        }
    }
}
