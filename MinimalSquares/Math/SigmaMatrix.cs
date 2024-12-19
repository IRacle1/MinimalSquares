using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace MinimalSquares.Math
{
    public readonly struct SigmaMatrix
    {
        private readonly float[,] values;

        public int Size { get; }

        public static SigmaMatrix GetE(int n)
        {
            float[,] matrix = new float[n,n];
            for (int i = 0; i < n; i++)
            {
                matrix[i, i] = 1f;
            }

            return new SigmaMatrix(matrix);
        }

        public SigmaMatrix(int n)
        {
            Size = n;
            values = new float[Size, Size];
        }

        public SigmaMatrix(float[,] variables)
        {
            if (variables.GetLength(0) != variables.GetLength(1))
                throw new ArgumentException(nameof(variables));

            Size = variables.GetLength(0);
            values = variables;
        }

        public readonly float this[int row, int column]
        {
            get
            {
                return values[row, column];
            }
        }

        public SigmaMatrix GetMinor(int row, int column)
        {
            float[,] newMatrix = new float[Size - 1, Size - 1];
            int i1 = 0;
            for (int i = 0; i < Size; i++)
            {
                if (row == i)
                    continue;

                int j1 = 0;
                for (int j = 0; j < Size; j++)
                {
                    if (column == j)
                        continue;
                    newMatrix[i1, j1] = values[i, j];
                    j1++;
                }
                i1++;
            }

            return new SigmaMatrix(newMatrix);
        }

        public float GetDeterminant()
        {
            if (Size == 1)
            {
                return values[0, 0];
            }
            
            if (Size == 2)
            {
                return values[0, 0] * values[1, 1] - values[1, 0] * values[0, 1];
            }

            if (Size == 3)
            {
                return
                    values[0, 0] * values[1, 1] * values[2, 2] +
                    values[0, 2] * values[1, 0] * values[2, 1] +
                    values[0, 1] * values[1, 2] * values[2, 0] -
                    values[0, 2] * values[1, 1] * values[2, 0] -
                    values[0, 0] * values[1, 2] * values[2, 1] -
                    values[0, 1] * values[1, 0] * values[2, 2];
            }

            float det = 0.0f;

            if (Size > 2)
            {
                for (int j = 0; j < Size; j++)
                {
                    float val = values[0, j] * GetMinor(0, j).GetDeterminant();
                    det += j % 2 == 0 ? val : -val;
                }
            }

            return det;
        }

        public SigmaMatrix ReplaceColumn(float[] values, int j)
        {
            if (Size != values.Length)
                throw new ArgumentOutOfRangeException(nameof(values));

            SigmaMatrix newMatrix = Clone();
            for (int i = 0; i < values.Length; i++)
            {
                newMatrix.values[i, j] = values[i];
            }

            return newMatrix;
        }

        public SigmaMatrix Clone()
        {
            return new SigmaMatrix(CopyTwoDimArray(values));
        }

        private static float[,] CopyTwoDimArray(float[,] original)
        {
            (int x, int y) = (original.GetLength(0), original.GetLength(1));

            float[,] newArray = new float[x, y];

            Array.Copy(original, newArray, x * y);

            return newArray;
        }
    }
}
