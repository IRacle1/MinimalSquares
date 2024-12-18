using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace MinimalSquares.Functions
{
    public class HyberbolicFunction : BaseFunction
    {
        public float A { get; set; }
        public float B { get; set; }

        public override Color Color { get; set; } = Color.Blue;

        public override float Step { get; set; } = 0.001f;

        public override float GetValue(float x)
        {
            if (x == 0)
            {
                return float.NaN;
            }
            return A / x + B;
        }

        public override bool IsAcceptable(float x)
        {
            return x != 0f;
        }

        public override void UpdateParameters(float[] x, float[] y)
        {
            float xSqrSum = 0;
            float xSum = 0;
            float xySum = 0;
            float ySum = 0;

            for (int i = 0; i < x.Length; i++)
            {
                if (!IsAcceptable(x[i]))
                    continue;
                xSqrSum += 1 / (x[i] * x[i]);
                xSum += 1 / x[i];
                xySum += y[i] / x[i];
                ySum += y[i];
            }

            Matrix4x4 main = new Matrix4x4(xSqrSum, xSum, 0f, 0f, xSum, x.Length, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);
            Matrix4x4 getA = new Matrix4x4(xySum, xSum, 0f, 0f, ySum, x.Length, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);
            Matrix4x4 getB = new Matrix4x4(xSqrSum, xySum, 0f, 0f, xSum, ySum, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);

            float mainD = main.GetDeterminant();
            float a = getA.GetDeterminant();
            float b = getB.GetDeterminant();

            A = a / mainD;
            B = b / mainD;
        }
    }
}
