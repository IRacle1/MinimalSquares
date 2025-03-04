using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathNet.Numerics.LinearAlgebra;

namespace MinimalSquares.Functions.Special
{
    // y = a/(x+b)
    // xy + by = a
    // xy = a - by
    // x = a/y - b
    public class HyperbolaFunciton : BaseFunction
    {
        public HyperbolaFunciton() : base(2)
        {
        }

        public override string Name { get; } = "Гипербола";

        public override string GetFunctionNotation()
        {
            string a = GetFormattedParameter(0, false);
            string b = GetFormattedParameter(1, true);

            return $"\\frac{{{a}}}{{x {b}}}";
        }

        public override string GetGeneralNotation()
        {
            return @"\frac{a}{x+b}";
        }

        public override double GetFreeValue(double x, double y)
        {
            return x;
        }

        public override double GetMonomialValue(int monomialIndex, double x, double y) =>
            monomialIndex switch
            {
                0 => 1 / y,
                1 => 1,
                _ => double.NaN,
            };

        public override void SetParameters(Vector<double> ansv)
        {
            Parameters[0] = ansv[0];
            Parameters[1] = -ansv[1];
        }

        public override double GetValue(double x)
        {
            return Parameters[0] / (x + Parameters[1]);
        }
    }
}
