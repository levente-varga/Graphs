using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public static class Colors
    {
        public static Color yellow = Color.FromArgb(255, 189, 0);
        public static Color green = Color.FromArgb(8, 229, 129); 
        public static Color red = Color.FromArgb(255, 85, 85);
        public static Color purple = Color.FromArgb(187, 134, 252);
        public static Color blue = Color.FromArgb(0, 147, 255);
        public static Color orange = Color.FromArgb(255, 123, 13);
        public static Color yellowAccent = Color.FromArgb(255, 204, 85);

        public static Color main = yellow;

        public static Color nothing = Color.FromArgb(0, 0, 0, 0);
        public static Color background = Color.FromArgb(50, 50, 50);
        public static Color backgroundLight = Color.FromArgb(55, 55, 55);
        public static Color foregroundDark = Color.FromArgb(65, 65, 65);   
        public static Color foreground = Color.FromArgb(80, 80, 80);
        public static Color darkGrey = Color.FromArgb(30, 30, 30);
        public static Color highlight = Color.FromArgb(255, 255, 255);

        public static Color fontWhite = Color.FromArgb(255, 255, 255);
        public static Color fontBlack = Color.FromArgb(0, 0, 0);
        public static Color fontLightGrey = Color.FromArgb(100, 100, 100);

        public static void UpdateMainColor(Color color)
        {
            main = color;
        }
    }
}
