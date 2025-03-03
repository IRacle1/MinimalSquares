using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathNet.Numerics.LinearAlgebra;

namespace MinimalSquares.Functions
{
    // y = a/(x+b)
    // xy + by = a
    // xy = - by + a
    public class HyperbolaFunciton : BaseFunction
    {
        public HyperbolaFunciton() : base(2)
        {
        }

        public override string Name { get; } = "Гипербола";

        public override bool IsAcceptablePoint(double x, double y)
        {
            return y > 0.0;
        }

        public override string GetFunctionNotation()
        {
            return $"\\sqrt{{{GetFormattedParameter(0, false)} x {GetFormattedParameter(1, true)}}}";
        }

        public override string GetGeneralNotation()
        {
            return @"\sqrt{ax + b}";
        }

        public override double GetYValue(double y, double x)
        {
            return x * y;
        }

        public override double GetMonomialValue(int monomialIndex, double x, double y) =>
            monomialIndex switch
            {
                0 => y,
                1 => 1,
                _ => double.NaN,
            };

        public override void SetParameters(Vector<double> ansv)
        {
            Parameters[1] = -ansv[0];
            Parameters[0] = ansv[1];
        }

        public override double GetValue(double x)
        {
            return Parameters[0] / (x + Parameters[1]);
        }
    }
}
