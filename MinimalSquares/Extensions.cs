using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace MinimalSquares
{
    public static class Extensions
    {
        public static Vector2 GetXY(this Vector3 vector3)
        {
            return new Vector2(vector3.X, vector3.Y);
        }
    }
}
