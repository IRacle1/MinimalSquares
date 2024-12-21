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

        public virtual int RequiredPoints { get; set; } = 1;

        public virtual bool IsAcceptablePoint(float x, float y) => true;

        public abstract void UpdateParameters(float[] x, float[] y);

        public abstract float GetValue(float x);
    }
}
