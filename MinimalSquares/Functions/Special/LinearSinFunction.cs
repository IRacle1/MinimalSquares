using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MinimalSquares.Components;

namespace MinimalSquares.Functions.Special
{
    public class LinearSinFunction : BaseFunction
    {
        public LinearSinFunction() : base(3)
        {
        }

        public override string Name { get; } = "Линейный синус";

        public override double GetMonomialValue(int monomialIndex, double x, double y) =>
            monomialIndex switch
            {
                0 => Math.Sin(x),
                1 => x,
                2 => 1,
                _ => double.NaN,
            };

        public override double GetValue(double x)
        {
            return Parameters[0] * Math.Sin(x) + Parameters[1] * x + Parameters[2];
        }

        public override string GetGeneralNotation()
        {
            return @"a \sin(x) + bx + c";
        }

        public override string GetFunctionNotation()
        {
            string a = GetFormattedParameter(0, false);
            string b = GetFormattedParameter(1, true);
            string c = GetFormattedParameter(2, true);

            return $"{a} \\sin(x) {b} x {c} ";
        }
    }
}
