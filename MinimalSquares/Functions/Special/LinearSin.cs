using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MinimalSquares.Components;

namespace MinimalSquares.Functions.Special
{
    public class LinearSin : PolynomialFunction
    {
        public LinearSin() : base(3)
        {
        }

        public override string Name { get; } = "Линейный синус";

        public override double GetMonomialValue(int monomialIndex, double x) =>
            monomialIndex switch
            {
                0 => Math.Sin(x),
                1 => x,
                2 => 1,
                _ => double.NaN,
            };

        public override string GetGeneralNotation()
        {
            return @"y \sim a \sin(x) + bx + c";
        }

        public override string GetFunctionNotation()
        {
            double a = GetFormattedParameter(0);
            double b = GetFormattedParameter(1);
            double c = GetFormattedParameter(2);

            return $"y ={(a < 0 ? a.ToString() : $"+ {a}")} \\sin(x) {(b < 0 ? b.ToString() : $"+ {b}")} x {(c < 0 ? c.ToString() : $"+ {c}")} ";
        }
    }
}
