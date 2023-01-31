using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs_Framework
{
    public static class Colors
    {
        public static Color yellow = Color.FromArgb(255, 189, 0);
        public static Color blue = Color.FromArgb(0, 120, 215);
        public static Color background = Color.FromArgb(50, 50, 50);
        public static Color foreground = Color.FromArgb(80, 80, 80);
        public static Color green = Color.FromArgb(8, 229, 129);
        public static Color orange = Color.FromArgb(255, 123, 13);
        public static Color darkGrey = Color.FromArgb(30, 30, 30);
        public static Color highlight = Color.FromArgb(255, 255, 255);
        public static Color main = yellow;

        public static void UpdateMainColor(Graph.Types selectedGraphType)
        {
            switch (selectedGraphType)
            {
                case Graph.Types.Random:
                    main = yellow;
                    break;
                case Graph.Types.Popularity:
                    main = green;
                    break;
            }
        }
    }
}
