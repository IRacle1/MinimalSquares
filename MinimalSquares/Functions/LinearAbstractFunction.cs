using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace MinimalSquares.Functions
{
    public abstract class LinearAbstractFunction : BaseFunction
    {
        public float A { get; set; }
        public float B { get; set; }

        public override bool IsAcceptableArgument(float x) => true;
        public override bool IsAcceptableValue(float y) => true;

        public override void UpdateParameters(float[] x, float[] y)
        {
            float xSqrSum = 0;
            float xSum = 0;
            float xySum = 0;
            float ySum = 0;

            int length = 0;

            for (int i = 0; i < x.Length; i++)
            {
                if (!IsAcceptableArgument(x[i]) || !IsAcceptableValue(y[i]))
                    continue;

                (float curX, float curY) = GetModifiedXY(x[i], y[i]);

                xSqrSum += curX * curX;
                xSum += curX;
                xySum += curY * curX;
                ySum += curY;

                length++;
            }

            Matrix4x4 main = new Matrix4x4(xSqrSum, xSum, 0f, 0f, xSum, length, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);
            Matrix4x4 getA = new Matrix4x4(xySum, xSum, 0f, 0f, ySum, length, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);
            Matrix4x4 getB = new Matrix4x4(xSqrSum, xySum, 0f, 0f, xSum, ySum, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);

            float mainD = main.GetDeterminant();
            float a = getA.GetDeterminant();
            float b = getB.GetDeterminant();

            (A, B) = PostSetter(a / mainD, b / mainD);
        }

        public virtual (float, float) GetModifiedXY(float x, float y)
        {
            return (x, y);
        }

        public virtual (float, float) PostSetter(float a, float b)
        {
            return (a, b);
        }

    }
}
