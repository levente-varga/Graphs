using System.Drawing;

namespace Graphs
{
    public static class Colors
    {
        public static readonly Color yellow = Color.FromArgb(255, 189, 0);
        public static readonly Color green = Color.FromArgb(8, 229, 129);
        public static readonly Color red = Color.FromArgb(255, 85, 85);
        public static readonly Color purple = Color.FromArgb(187, 134, 252);
        public static readonly Color blue = Color.FromArgb(0, 147, 255);
        public static readonly Color orange = Color.FromArgb(255, 123, 13);
        public static readonly Color yellowAccent = Color.FromArgb(255, 204, 85);

        public static Color main = yellow;

        public static readonly Color nothing = Color.FromArgb(0, 0, 0, 0);
        public static readonly Color background = Color.FromArgb(50, 50, 50);
        public static readonly Color backgroundLight = Color.FromArgb(55, 55, 55);
        public static readonly Color foregroundDark = Color.FromArgb(65, 65, 65);
        public static readonly Color foreground = Color.FromArgb(80, 80, 80);
        public static readonly Color darkGrey = Color.FromArgb(30, 30, 30);
        public static readonly Color highlight = Color.FromArgb(255, 255, 255);

        public static readonly Color fontWhite = Color.FromArgb(255, 255, 255);
        public static readonly Color fontBlack = Color.FromArgb(0, 0, 0);
        public static readonly Color fontLightGrey = Color.FromArgb(100, 100, 100);

        public static void UpdateMainColor(Color color)
        {
            main = color;
        }
    }
}
