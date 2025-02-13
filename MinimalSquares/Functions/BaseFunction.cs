using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace MinimalSquares.Functions
{
    public abstract class BaseFunction
    {
        public virtual Color Color { get; set; } = Color.MediumSlateBlue;

        public virtual float Step { get; set; } = Program.Step;

        public virtual bool IsAcceptablePoint(double x, double y) => true;

        public abstract void UpdateParameters(double[] x, double[] y);

        public abstract double GetValue(double x);
    }
}
