using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Graphs_Framework
{
    public partial class Form1 : Form
    {
        public static string VERSION = "1.1.0";

        Chart selectedChartType = 0;
        enum Chart
        {
            Node,
            Degree,
            AverageDegree,
        }

        private Generator generator;
        Graphics graphPanelGraphics;
        Graphics chartPanelGraphics;
        Graphics graphDrawerGraphics;
        Graphics chartDrawerGraphics;
        Graphics graphPanelDrawerGraphics;
        List<Double2> points;

        bool stretchChart = false;
        bool gradientGraph = false;
        bool showDegree = false;
        bool sortChart = false;
        bool showNodes = true;
        bool showChartValues = false;
        bool forceDirectedArrangement = false;
        bool showChart = true;

        Color yellow = Color.FromArgb(255, 189, 0);
        Color blue = Color.FromArgb(0, 120, 215);
        Color background = Color.FromArgb(50, 50, 50);
        Color foreground = Color.FromArgb(80, 80, 80);
        Color green = Color.FromArgb(8, 229, 129);
        Color orange = Color.FromArgb(255, 123, 13);
        Color darkGrey = Color.FromArgb(30, 30, 30);
        Color mainColor;

        Bitmap bmGraph;
        Bitmap bmChart;
        Bitmap bmGraphPanel;

        FormWindowState lastWindowState;

        private static double CHART_COLUMN_WIDTH_RATIO = 0.6;
        private static int CHART_VERTICAL_VALUES_WIDTH = 20;
        private static int CHART_HORIZONTAL_VALUES_HEIGHT = 17;
        private static int NODE_SIZE = 8;

        public Form1()
        {
            InitializeComponent();
            SetupGraphics();
            lVersion.Text = VERSION;

            generator = new Generator();
            mainColor = yellow;

            lastWindowState = WindowState;

            

            Double2 p1 = new Double2(5, 4);
            Double2 p2 = new Double2(0, 0);
            lMaxF.Text = p1.Normalize().DistanceFrom(new Double2(0, 0)).ToString();
        }

        private void SetupGraphics()
        {
            int graphSize = Math.Min(panelGraph.Width, panelGraph.Height);

            if (graphPanelGraphics != null) graphPanelGraphics.Dispose();
            if (chartPanelGraphics != null) chartPanelGraphics.Dispose();
            if (graphDrawerGraphics != null) graphDrawerGraphics.Dispose();
            if (chartDrawerGraphics != null) chartDrawerGraphics.Dispose();
            if (graphPanelDrawerGraphics != null) graphPanelDrawerGraphics.Dispose();
            if (bmChart != null) bmChart.Dispose();

            if (panelGraph.Width > 0 && panelGraph.Height > 0)
            {
                if (bmGraphPanel != null) bmGraphPanel.Dispose();
                if (bmGraph != null) bmGraph.Dispose();
                bmGraphPanel = new Bitmap(panelGraph.Width, panelGraph.Height);
                bmGraph = new Bitmap(graphSize, graphSize);
            }

            bmChart = new Bitmap(panelChart.Width, panelChart.Height);
            graphPanelGraphics = panelGraph.CreateGraphics();
            chartPanelGraphics = panelChart.CreateGraphics();
            graphDrawerGraphics = Graphics.FromImage(bmGraph);
            chartDrawerGraphics = Graphics.FromImage(bmChart);
            graphPanelDrawerGraphics = Graphics.FromImage(bmGraphPanel);
            graphDrawerGraphics.SmoothingMode = SmoothingMode.AntiAlias;
        }

        private void trackBarN_Scroll(object sender, EventArgs e)
        {
            lValueOfN.Text = trackBarN.Value.ToString();
            generator.NodesToGenerate = trackBarN.Value;
        }

        private void trackBarS_Scroll(object sender, EventArgs e)
        {
            double power = trackBarS.Value * 0.1;
            lValueOfS.Text = power.ToString("0.0");
            generator.Power = power;
        }

        private void trackBarP_Scroll(object sender, EventArgs e)
        {
            double probability = trackBarP.Value * 0.02;
            lValueOfP.Text = probability.ToString("0.00");
            generator.Probability = probability;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            generator.GenerateRandomGraph();
            mainColor = yellow;
            GeneratePoints();
            DrawUI();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            generator.GeneratePopularityGraph();
            mainColor = green;
            GeneratePoints();
            DrawUI();
        }

        private void DrawUI()
        {
            SetupGraphics();
            UpdateChartButtons();
            FillStatistics();
            DrawChart();
            DrawGraph();
        }

        private void UpdateUI()
        {
            UpdateGraph();
            UpdateChart();
        }

        private void UpdateGraph()
        {
            graphPanelDrawerGraphics.Clear(background);
            graphPanelDrawerGraphics.DrawImage(bmGraph, bmGraphPanel.Width / 2 - bmGraph.Width / 2, bmGraphPanel.Height / 2 - bmGraph.Height / 2);
            graphPanelGraphics.DrawImage(bmGraphPanel, 0, 0);
        }

        private void UpdateChart()
        {
            chartPanelGraphics.DrawImage(bmChart, 0, 0);
        }

        private void FillStatistics()
        {
            int maxEdgeCount = generator.Nodes * (generator.Nodes - 1) / 2;
            lAverageDegreeValue.Text = generator.CalculateAverageDegree().ToString("0.00");
            lAverageDegreeSamples.Text = generator.AverageSamples.ToString();
            lEdgeCount.Text = generator.CountEdges().ToString() + " / " + maxEdgeCount + "   [" + ((double)generator.CountEdges() / maxEdgeCount * 100).ToString("0.0") + "%]";

            lAverageDegreeSamples.Refresh();
            lAverageDegreeValue.Refresh();
            lEdgeCount.Refresh();
        }

        private void GeneratePoints()
        {
            if (forceDirectedArrangement)
            {
                points = GraphArranger.GetForceDirectedArrangement(generator.NeighbourMatrix, this);
            }
            else
            {
                int radius = Math.Min(bmGraph.Width, bmGraph.Height) / 2 - 22;
                points = GraphArranger.GetCircularArrangement(generator.Nodes, radius, new Double2(bmGraph.Width / 2, bmGraph.Height / 2));
            }

            FitToCanvas(points, GetGraphPadding());
        }

        private int GetGraphPadding() => forceDirectedArrangement ? NODE_SIZE / 2 : 22;

        private void FitToCanvas(List<Double2> ps, int padding = 0)
        {
            if (ps == null) return;

            int skip = 0;

            if (forceDirectedArrangement)
            {
                while (skip < ps.Count && generator.CalculateDegree(skip) == 0)
                {
                    skip++;
                }
                if (skip == ps.Count) return;
            }

            Double2 min = new Double2(ps[skip]);
            Double2 max = new Double2(ps[skip]);

            for (int i = skip; i < ps.Count; i++)
            {
                if (forceDirectedArrangement && generator.CalculateDegree(i) == 0) continue;
                if (ps[i].X < min.X) min.X = ps[i].X;
                if (ps[i].Y < min.Y) min.Y = ps[i].Y;
                if (ps[i].X > max.X) max.X = ps[i].X;
                if (ps[i].Y > max.Y) max.Y = ps[i].Y;
            }

            double scale = Math.Max((max.X - min.X) / (bmGraph.Width - padding * 2), (max.Y - min.Y) / (bmGraph.Height - padding * 2));

            for (int i = 0; i < ps.Count; i++)
            {
                ps[i] -= (max + min) / 2;
                ps[i] = ps[i] / scale;
                ps[i] += new Double2(bmGraph.Width / 2, bmGraph.Height / 2);
            }
        }

        public void SetPoints(List<Double2> newPoints, double maxF, int t)
        {
            points = new List<Double2>(newPoints);
            lMaxF.Text = maxF.ToString("0.0000") + " [" + t + "]";
            lMaxF.Refresh();

            FitToCanvas(points, GetGraphPadding());
        }

        public void DrawGraph()
        {
            if (generator.NeighbourMatrix == null) return;

            Font font = new Font("Segoe UI", 10);
            SolidBrush brush = new SolidBrush(mainColor);
            Pen pen = new Pen(mainColor);

            graphDrawerGraphics.Clear(background);

            int radius = Math.Min(bmGraph.Width, bmGraph.Height) / 2 - 22;

            if (showNodes)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    if (forceDirectedArrangement && generator.CalculateDegree(i) == 0) continue;

                    int posX = (int)points[i].X - NODE_SIZE / 2;
                    int posY = (int)points[i].Y - NODE_SIZE / 2;

                    graphDrawerGraphics.FillEllipse(brush, new Rectangle(posX, posY, NODE_SIZE, NODE_SIZE));
                }
            }

            if (showDegree && !forceDirectedArrangement)
            {
                List<Double2> textPositions = GraphArranger.GetCircularArrangement(generator.Nodes, radius + 15, new Double2(bmGraph.Width / 2, bmGraph.Height / 2));
                FitToCanvas(textPositions, 7);
                for (int i = 0; i < generator.Nodes; i++)
                {
                    graphDrawerGraphics.DrawString(generator.CalculateDegree(i).ToString(), font, brush, (int)textPositions[i].X - (generator.CalculateDegree(i) < 10 ? 5 : 9), (int)textPositions[i].Y - 8);
                }
            }

            for (int i = 0; i < points.Count - 1; i++)
            {
                for (int j = i + 1; j < points.Count; j++)
                {
                    if (generator.NeighbourMatrix[i][j])
                    {
                        int posX1 = (int)points[i].X;
                        int posY1 = (int)points[i].Y;
                        int posX2 = (int)points[j].X;
                        int posY2 = (int)points[j].Y;

                        if (gradientGraph)
                        {
                            LinearGradientBrush gradBrush = new LinearGradientBrush(
                                new Point(posX1 + (posX2 > posX1 ? -1 : 1), posY1 + (posY2 > posY1 ? -1 : 1)),
                                new Point(posX2 + (posX2 > posX1 ? 1 : -1), posY2 + (posY2 > posY1 ? 1 : -1)),
                                Color.FromArgb((int)(255.0 * Math.Pow((double)generator.CalculateDegree(i) / generator.MaxDegree, 2.5)), pen.Color.R, pen.Color.G, pen.Color.B),
                                Color.FromArgb((int)(255.0 * Math.Pow((double)generator.CalculateDegree(j) / generator.MaxDegree, 2.5)), pen.Color.R, pen.Color.G, pen.Color.B)
                                );
                            Pen gradPen = new Pen(gradBrush);

                            graphDrawerGraphics.DrawLine(gradPen, posX1, posY1, posX2, posY2);

                            gradPen.Dispose();
                            gradBrush.Dispose();
                        }
                        else
                        {
                            graphDrawerGraphics.DrawLine(pen, posX1, posY1, posX2, posY2);
                        }
                    }
                }
            }

            UpdateGraph();

            font.Dispose();
            pen.Dispose();
            brush.Dispose();
        }

        private void DrawChart()
        {
            if (generator.NeighbourMatrix == null) return;

            chartDrawerGraphics.Clear(background);

            if (!showChart)
            {
                UpdateChart();
                return;
            }

            List<double> values = null;
            int maxValue = 0;

            switch (selectedChartType)
            {
                case Chart.Node: values = GetNodeList(); maxValue = generator.Nodes - 1; break;
                case Chart.Degree: values = GetDegreeList(); maxValue = generator.Nodes; break;
                case Chart.AverageDegree: values = GetAverageDegreeList(); maxValue = generator.Nodes; break;
            }

            if (values == null || values.Count == 0)
            {
                UpdateChart();
                return;
            }

            SolidBrush brush = null;
            switch (selectedChartType)
            {
                case Chart.Node: brush = new SolidBrush(mainColor); break;
                case Chart.Degree: brush = new SolidBrush(blue); break;
                case Chart.AverageDegree: brush = new SolidBrush(orange); break;
            }

            if (sortChart) values.Sort(delegate (double a, double b) { return a > b ? -1 : 1; });

            int horizontalOffset = selectedChartType == Chart.AverageDegree ? values.Max() >= 10.0 ? 10 : 3 : 0;
            int startX = showChartValues ? CHART_VERTICAL_VALUES_WIDTH + horizontalOffset : 0;
            int chartWidth = showChartValues ? panelChart.Width - (CHART_VERTICAL_VALUES_WIDTH + horizontalOffset) : panelChart.Width;
            int chartHeight = (showChartValues && !sortChart && (selectedChartType == Chart.Degree || selectedChartType == Chart.AverageDegree)) ? panelChart.Height - CHART_HORIZONTAL_VALUES_HEIGHT : panelChart.Height;

            double x = startX;
            double columnFactor = values.Count * CHART_COLUMN_WIDTH_RATIO;
            double gapFactor = (values.Count - 1) * (1 - CHART_COLUMN_WIDTH_RATIO);
            double totalWidth = columnFactor + gapFactor;
            double columnWidth = (chartWidth * (columnFactor / totalWidth)) / values.Count;
            double gapWidth = (chartWidth * (gapFactor / totalWidth)) / (values.Count - 1);

            if (showChartValues)
            {
                Pen pen = new Pen(foreground);
                Font font = new Font("Segoe UI", 10);
                float[] dashValues = { 4, 4 };
                pen.DashPattern = dashValues;

                int linePosY;
                if (stretchChart)
                    linePosY = 0;
                else
                    linePosY = (int)((1 - (double)values.Max() / maxValue) * chartHeight);

                chartDrawerGraphics.DrawLine(pen, 0, linePosY, panelChart.Width, linePosY);

                string tMaxValue;
                if (values.Max() != (double)(int)values.Max())
                    tMaxValue = values.Max().ToString("0.0");
                else
                    tMaxValue = ((int)values.Max()).ToString();
                chartDrawerGraphics.DrawString(tMaxValue, font, brush, 0, chartHeight - linePosY > 17 ? linePosY : linePosY - 17);

                if (!sortChart && (selectedChartType == Chart.Degree || selectedChartType == Chart.AverageDegree))
                {
                    double textX = startX;
                    for (int i = 0; i < values.Count * 2 - 1; i++)
                    {
                        double w = i % 2 == 0 ? columnWidth : gapWidth;

                        if (i % 2 == 0)
                        {
                            int column = i / 2;
                            if (column % (int)Math.Sqrt(values.Count) == 0)
                            {
                                chartDrawerGraphics.DrawString(
                                    column.ToString(), font, brush,
                                    (int)(textX + columnWidth / 2 - (column < 10 ? 5 : 9.5)), chartHeight + 1);
                            }
                        }

                        textX += w;
                    }
                }

                font.Dispose();
                pen.Dispose();
            }

            for (int i = 0; i < values.Count * 2 - 1; i++)
            {
                double w = i % 2 == 0 ? columnWidth : gapWidth;

                if (i % 2 == 0)
                {
                    int h = (int)((double)values[i / 2] / (stretchChart ? values.Max() : maxValue) * chartHeight);
                    int y = chartHeight - h;

                    chartDrawerGraphics.FillRectangle(brush, (int)x, y, (int)w, h);
                }

                x += w;
            }

            brush.Dispose();

            UpdateChart();
        }

        private List<double> GetNodeList()
        {
            List<double> degrees = new List<double>();
            for (int i = 0; i < generator.Nodes; i++)
                degrees.Add(generator.CalculateDegree(i));

            return degrees;
        }

        private List<double> GetDegreeList()
        {
            List<double> counts = new List<double>(new double[generator.Nodes]);
            for (int i = 0; i < generator.Nodes; i++)
            {
                int degree = generator.CalculateDegree(i);
                counts[degree]++;
            }

            return counts;
        }

        private List<double> GetAverageDegreeList()
        {
            return generator.AverageDegreeDistribution;
        }

        private void lValueOfN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    int result = Int32.Parse(lValueOfN.Text.ToString());
                    generator.NodesToGenerate = result;
                    trackBarN.Value = generator.NodesToGenerate;
                    lValueOfN.Text = generator.NodesToGenerate.ToString();
                    bRandom.Focus();
                }
                catch (FormatException)
                {
                    lValueOfN.Text = trackBarN.Value.ToString();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void lValueOfP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    double result = Double.Parse(lValueOfP.Text.ToString());
                    generator.Probability = result;
                    trackBarP.Value = (int)(generator.Probability * 100);
                    lValueOfP.Text = generator.Probability.ToString("0.00");
                    bRandom.Focus();
                }
                catch (FormatException)
                {
                    lValueOfP.Text = trackBarP.Value.ToString();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void lValueOfS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    double result = Double.Parse(lValueOfS.Text.ToString());
                    generator.Power = result;
                    trackBarS.Value = (int)(generator.Power * 10);
                    lValueOfS.Text = generator.Power.ToString("0.0");
                    bRandom.Focus();
                }
                catch (FormatException)
                {
                    lValueOfS.Text = trackBarS.Value.ToString();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void bStretch_Click(object sender, EventArgs e)
        {
            stretchChart = !stretchChart;
            if (stretchChart)
                bStretch.BackColor = blue;
            else
                bStretch.BackColor = background;

            DrawChart();
        }

        private void bGradient_Click(object sender, EventArgs e)
        {
            gradientGraph = !gradientGraph;
            if (gradientGraph)
                bGradient.BackColor = blue;
            else
                bGradient.BackColor = background;

            DrawGraph();
        }

        private void bShowDegree_Click(object sender, EventArgs e)
        {
            showDegree = !showDegree;
            if (showDegree)
                bShowDegree.BackColor = blue;
            else
                bShowDegree.BackColor = background;

            DrawGraph();
        }

        private void bOrder_Click(object sender, EventArgs e)
        {
            sortChart = !sortChart;
            if (sortChart)
                bSort.BackColor = blue;
            else
                bSort.BackColor = background;

            DrawChart();
        }

        private void UpdateChartButtons()
        {
            bChart1.BackColor = background;
            bChart2.BackColor = background;
            bChart3.BackColor = background;

            if (showChart)
            {
                switch (selectedChartType)
                {
                    case Chart.Node: bChart1.BackColor = mainColor; break;
                    case Chart.Degree: bChart2.BackColor = blue; break;
                    case Chart.AverageDegree: bChart3.BackColor = orange; break;
                }
            }

            bChart1.Refresh();
            bChart2.Refresh();
            bChart3.Refresh();
        }

        private void bChart1_Click(object sender, EventArgs e)
        {
            if (selectedChartType == Chart.Node)
            {
                showChart = !showChart;
            }
            else
            {
                showChart = true;
            }
            selectedChartType = Chart.Node;
            UpdateChartButtons();
            DrawChart();
        }

        private void bChart2_Click(object sender, EventArgs e)
        {
            if (selectedChartType == Chart.Degree)
            {
                showChart = !showChart;
            }
            else
            {
                showChart = true;
            }
            selectedChartType = Chart.Degree;
            UpdateChartButtons();
            DrawChart();
        }

        private void bChart3_Click(object sender, EventArgs e)
        {
            if (selectedChartType == Chart.AverageDegree)
            {
                showChart = !showChart;
            }
            else
            {
                showChart = true;
            }
            selectedChartType = Chart.AverageDegree;
            UpdateChartButtons();
            DrawChart();
        }

        private void bShowNodes_Click(object sender, EventArgs e)
        {
            showNodes = !showNodes;
            if (showNodes)
                bShowNodes.BackColor = blue;
            else
                bShowNodes.BackColor = background;

            DrawGraph();
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState != lastWindowState)
            {
                SetupGraphics();
                FitToCanvas(points, GetGraphPadding());
                DrawUI();

                lastWindowState = WindowState;
            }
            else
            {
                if (graphPanelDrawerGraphics != null) graphPanelDrawerGraphics.Dispose();
                if (graphPanelGraphics != null) graphPanelGraphics.Dispose();

                if (panelGraph.Width > 0 && panelGraph.Height > 0)
                {
                    if (bmGraphPanel != null) bmGraphPanel.Dispose();
                    bmGraphPanel = new Bitmap(panelGraph.Width, panelGraph.Height);
                }

                graphPanelGraphics = panelGraph.CreateGraphics();
                graphPanelDrawerGraphics = Graphics.FromImage(bmGraphPanel);

                UpdateUI();
            }
        }

        private void Main_ResizeEnd(object sender, EventArgs e)
        {
            SetupGraphics();
            FitToCanvas(points, GetGraphPadding());
            DrawUI();
        }

        private void bShowValues_Click(object sender, EventArgs e)
        {
            showChartValues = !showChartValues;
            if (showChartValues)
                bShowValues.BackColor = blue;
            else
                bShowValues.BackColor = background;

            DrawChart();
        }

        private void bArrangement_Click(object sender, EventArgs e)
        {
            forceDirectedArrangement = !forceDirectedArrangement;
            if (forceDirectedArrangement)
                bArrangement.BackColor = blue;
            else
                bArrangement.BackColor = background;

            GeneratePoints();
            DrawGraph();
        }
    }
}
