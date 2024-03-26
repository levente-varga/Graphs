using System;
using System.Drawing;

namespace Graphs.code
{
    internal static class Utils
    {
        public static double RoundDouble(double number, int decimals)
        {
            if (decimals < 0) return 0;
            double multiplier = Math.Pow(10, decimals);
            return Convert.ToInt32(number * multiplier) / multiplier;
        }

        public static bool IsInsideRect(Double2 position, Rectangle rectangle)
        {
            return rectangle.X <= position.X && position.X < rectangle.X + rectangle.Width
                && rectangle.Y <= position.Y && position.Y < rectangle.Y + rectangle.Height;
        }
    }
}
