using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MinimalSquares
{
    public static class Extensions
    {
        private static readonly HashSet<string> reservedNames = new()
        {
            "CON", "PRN", "AUX", "NUL",
            "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
            "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9"
        };

        public static Vector2 GetXY(this Vector3 vector3)
        {
            return new Vector2(vector3.X, vector3.Y);
        }

        public static Func<object?, ValidationResult?> ValidateFileName(string errorMessage)
        {
            return (obj) =>
            {
                if (obj is not string str || !IsValidFileName(str))
                    return new ValidationResult(errorMessage);
                return ValidationResult.Success;
            };
        }

        public static bool IsValidFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            if (fileName.Contains(Path.DirectorySeparatorChar) || fileName.Contains(Path.AltDirectorySeparatorChar))
                return false;

            HashSet<char> invalidChars = Path.GetInvalidFileNameChars().ToHashSet();
            if (fileName.Any(invalidChars.Contains))
                return false;

            if (reservedNames.Contains(fileName.ToUpperInvariant()))
                return false;

            return true;
        }

        public static string Format(this double value, bool sign = false)
        {
            double newVal = Math.Round(value, 4);
            if (!sign)
                return newVal.ToString(CultureInfo.InvariantCulture);

            return (newVal > 0 ? '+' : ' ') + newVal.ToString(CultureInfo.InvariantCulture);
        }

        public static string Format(this float value, bool sign = false)
        {
            float newVal = MathF.Round(value, 4);
            if (!sign)
                return newVal.ToString(CultureInfo.InvariantCulture);

            return (newVal > 0 ? '+' : ' ') + newVal.ToString(CultureInfo.InvariantCulture);
        }
    }
}
