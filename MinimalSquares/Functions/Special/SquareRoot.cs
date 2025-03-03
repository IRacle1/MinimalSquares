﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalSquares.Functions.Special
{
    // y = sqrt(ax + b)
    // y^2 = ax + b
    public class SquareRoot : BaseFunction
    {
        public SquareRoot() : base(2)
        {
        }

        public override string Name { get; } = "Квадратный корень";

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

        public override double GetYValue(double y)
        {
            return y * y;
        }

        public override double GetMonomialValue(int monomialIndex, double x) =>
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
    }
}
