using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalSquares.Functions
{
    // y = sqrt(ax + b)
    // y^2 = ax + b
    public class SquareRootFunction : BaseFunction
    {
        public SquareRootFunction() : base(2)
        {
        }

        public override string Name { get; } = "Квадратный корень";

        public override bool IsAcceptablePoint(double x, double y)
        {
            return y > 0.0;
        }

        public override double GetYValue(double x, double y)
        {
            return y * y;
        }

        public override double GetMonomialValue(int monomialIndex, double x, double y) =>
            monomialIndex switch
            {
                0 => x,
                1 => 1,
                _ => double.NaN,
            };

        public override double GetValue(double x)
        {
            return Math.Sqrt(Parameters[0] * x + Parameters[1]);
        }

        public override string GetFunctionNotation()
        {
            return $"\\sqrt{{{GetFormattedParameter(0, false)} x {GetFormattedParameter(1, true)}}}";
        }

        public override string GetGeneralNotation()
        {
            return @"\sqrt{ax + b}";
        }
    }
}
