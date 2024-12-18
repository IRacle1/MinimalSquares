using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace MinimalSquares.Functions
{
    public class LinearFunction : IFunction
    {
        public float A { get; set; }
        public float B { get; set; }

        public Color Color { get; } = Color.Blue;

        public float GetValue(float x)
        {
            return A * x + B;
        }

        public void UpdateParameters(float[] x, float[] y)
        {
            float xSqrSum = 0;
            float xSum = 0;
            float xySum = 0;
            float ySum = 0;

            for (int i = 0; i < x.Length; i++)
            {
                xSqrSum += x[i] * x[i];
                xSum += x[i];
                xySum += y[i] * x[i];
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
