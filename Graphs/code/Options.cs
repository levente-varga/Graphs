using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public static class Options
    {
        public static bool stretchChart = false;
        public static bool gradient = false;
        public static bool showDegree = false;
        public static bool sortChart = false;
        public static bool showNodes = true;
        public static bool showChartValues = true;
        public static bool forceDirectedArrangement = false;
        public static bool pauseForceDirectedArrangement = false;
        public static bool showChart = true;
        public static bool showGraph = true;
        public static bool generateSamples = false;
        public static bool autoGenerateOnChange = true;
        public static GraphDisplayMode graphDisplayMode = GraphDisplayMode.Graph; 

        public enum GraphDisplayMode
        {
            Graph,
            Matrix,
        }
    }
}
