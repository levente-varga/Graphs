using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Graphs_Framework
{
    public partial class Form1 : Form
    {
        public static string VERSION = "2.0.0";

        Chart.Type selectedChartType = 0;
        Graph.Types selectedGraphType = 0;

        private GraphManager gm;
        Graphics graphPanelGraphics;
        Graphics chartPanelGraphics;
        Graphics graphDrawerGraphics;
        Graphics chartDrawerGraphics;
        Graphics graphPanelDrawerGraphics;

        List<Double2> scaledPoints;
        Double2 mousePos;
        bool mouseDown;
        double scale = 1;

        Stopwatch time = new Stopwatch();
        double lastElapsedTime = 0;

        bool stretchChart = false;
        bool gradient = false;
        bool showDegree = false;
        bool sortChart = false;
        bool showNodes = true;
        bool showChartValues = true;
        bool forceDirectedArrangement = false;
        bool showChart = true;

        bool graphHovered = false;
        int selectedNodeID = -1;
        Double2 selectedNodeOrigin;

        Color yellow = Color.FromArgb(255, 189, 0);
        Color blue = Color.FromArgb(0, 120, 215);
        Color background = Color.FromArgb(50, 50, 50);
        Color foreground = Color.FromArgb(80, 80, 80);
        Color green = Color.FromArgb(8, 229, 129);
        Color orange = Color.FromArgb(255, 123, 13);
        Color darkGrey = Color.FromArgb(30, 30, 30);
        Color selectionColor = Color.FromArgb(255, 255, 255);
        Color mainColor;

        Bitmap bmGraph;
        Bitmap bmChart;
        Bitmap bmGraphPanel;

        FormWindowState lastWindowState;

        private static double CHART_COLUMN_WIDTH_RATIO = 0.6;
        private static int CHART_VERTICAL_VALUES_WIDTH = 20;
        private static int CHART_HORIZONTAL_VALUES_HEIGHT = 17;
        private static int NODE_SIZE = 8;
        private static int FPS = 75;

        private const int MAX_NODES = 100;
        private const int MIN_NODES = 3;
        private const double MAX_POWER = 2.0;
        private const double MIN_POWER = 0.0;
        private const int NODES_INIT = 13;
        private const double PROBABILITY_INIT = 0.5;
        private const double POWER_INIT = 1.0;

        public static String FONT = "Segoe UI";

        public Form1()
        {
            time.Start();

            Console.WriteLine($"Version: {Environment.Version}");

            InitializeComponent();
            SetupGraphics();
            lVersion.Text = VERSION;

            gm = new GraphManager();

            timer.Start();
            timer.Enabled = true;
            timer.Interval = 1;

            mainColor = yellow;

            scaledPoints = new List<Double2>();

            lastWindowState = WindowState;

            Double2 p1 = new Double2(5, 4);
            Double2 p2 = new Double2(0, 0);
            lMaxF.Text = p1.Normalize().DistanceFrom(new Double2(0, 0)).ToString();

            bShowDegree.BackColor = getToggleButtonColor(showDegree);
            bShowNodes.BackColor = getToggleButtonColor(showNodes);
            bGradient.BackColor = getToggleButtonColor(gradient);
            bSort.BackColor = getToggleButtonColor(sortChart);
            bStretch.BackColor = getToggleButtonColor(stretchChart);
            bShowValues.BackColor = getToggleButtonColor(showChartValues);
            bArrangement.BackColor = getToggleButtonColor(forceDirectedArrangement);
        }

        private void SetupGraphics()
        {
            //int graphSize = Math.Min(panelGraph.Width, panelGraph.Height);

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
                bmGraph = new Bitmap(panelGraph.Width, panelGraph.Height);//graphSize, graphSize);
            }

            bmChart = new Bitmap(panelChart.Width, panelChart.Height);
            graphPanelGraphics = panelGraph.CreateGraphics();
            chartPanelGraphics = panelChart.CreateGraphics();
            graphDrawerGraphics = Graphics.FromImage(bmGraph);
            chartDrawerGraphics = Graphics.FromImage(bmChart);
            graphPanelDrawerGraphics = Graphics.FromImage(bmGraphPanel);
            graphDrawerGraphics.SmoothingMode = SmoothingMode.AntiAlias;
        }

        public void SetProgressBarPercent(double p)
        {
            if (p > 1) p = 1;
            if (p < 0) p = 0;

            pProgressBar.Width = (int) (p * panel1.Width);

            pProgressBar.Refresh();
        }

        private int valueOfNodes() => trackBarNodes.Value;
        private void trackBarNodes_Scroll(object sender, EventArgs e)
        {
            int nodes = valueOfNodes();
            lValueOfNodes.Text = nodes.ToString();
            lValueOfNodes.Update();
        }

        private double valueOfPower() => trackBarPower.Value * 0.1;
        private void trackBarPower_Scroll(object sender, EventArgs e)
        {
            double power = valueOfPower();
            lValueOfPower.Text = power.ToString("0.0");
            lValueOfPower.Update();
        }

        private double valueOfProbability() => trackBarProbability.Value * 0.02;
        private void trackBarProbability_Scroll(object sender, EventArgs e)
        {
            double probability = valueOfProbability();
            lValueOfProbability.Text = probability.ToString("0.00");
            lValueOfProbability.Update();
        }

        private void buttonGenRnd_Click(object sender, EventArgs e) => GenerateGraph(Graph.Types.Random);
        private void buttonGenPop_Click(object sender, EventArgs e) => GenerateGraph(Graph.Types.Popularity);
        private void GenerateGraph(Graph.Types graphType) 
        {
            gm.GenerateGraph(graphType, valueOfNodes(), valueOfProbability(), valueOfPower());

            switch (graphType) {
                case Graph.Types.Random:
                    mainColor = yellow;
                    break;
                case Graph.Types.Popularity:
                    mainColor = green;
                    break;
            }

            pProgressBar.BackColor = mainColor;
            ArrangePoints();
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
            //CompositingMode oldCMForGD  = graphDrawerGraphics.CompositingMode;
            //CompositingMode oldCMForGPD = graphPanelDrawerGraphics.CompositingMode;

            graphPanelDrawerGraphics.CompositingMode = CompositingMode.SourceCopy;
            graphPanelDrawerGraphics.Clear(background);
            graphPanelDrawerGraphics.DrawImage(bmGraph, bmGraphPanel.Width / 2 - bmGraph.Width / 2, bmGraphPanel.Height / 2 - bmGraph.Height / 2);
            graphPanelGraphics.DrawImage(bmGraphPanel, 0, 0);

            //graphPanelDrawerGraphics.CompositingMode = oldCMForGPD;
            //graphDrawerGraphics.CompositingMode = oldCMForGD;
        }

        private void UpdateChart()
        {
            chartPanelGraphics.DrawImage(bmChart, 0, 0);
        }

        private void FillStatistics()
        {
            if (gm.Graph == null) return;

            int maxEdgeCount = gm.Graph.NodeCount * (gm.Graph.NodeCount - 1) / 2;
            lAverageDegreeValue.Text = gm.Graph.CalculateAverageDegree().ToString("0.00");
            lAverageDegreeSamples.Text = gm.AverageSamples.ToString();
            lEdgeCount.Text = gm.Graph.EdgeCount.ToString() + " / " + maxEdgeCount + "   [" + ((double)gm.Graph.EdgeCount / maxEdgeCount * 100).ToString("0.0") + "%]";

            lAverageDegreeSamples.Refresh();
            lAverageDegreeValue.Refresh();
            lEdgeCount.Refresh();
        }

        private void DisableUI(bool disable) 
        {
            bArrangement.Enabled = !disable;
            bRandom.Enabled = !disable;
            bPopularity.Enabled = !disable;
            bArrangement.Update();
            bRandom.Update();
            bPopularity.Update();
        }

        /**
         * Arranges the already existing graph nodes inside the list 'points'
         * The method to arrange the points with is chosen based on the 'forceDirectedArrangement' variable
         */
        private void ArrangePoints()
        {
            if (gm.Graph == null) return;
            if (gm.Graph.NeighbourMatrix == null) return;

            int radius = Math.Min(bmGraph.Width, bmGraph.Height) / 2 - 22;
            gm.ArrangeCircle(radius, new Double2(bmGraph.Width / 2, bmGraph.Height / 2));

            if (forceDirectedArrangement)
            {
                gm.ResetForceDirectedArrangement();
                timer.Enabled = true;
                timer.Start();
            }
        }

        private int GetGraphPadding() => forceDirectedArrangement || !showDegree ? NODE_SIZE / 2 : 22;

        private void FitToCanvas(List<Double2> ps, int padding = 0, bool skipLonelyNodes = false)
        {
            if (ps == null) return;

            int skip = 0;

            if (skipLonelyNodes)
            {
                while (skip < ps.Count && gm.Graph.CalculateDegree(skip) == 0)
                {
                    skip++;
                }
                if (skip == ps.Count) return;
            }

            Double2 min = new Double2(ps[skip]);
            Double2 max = new Double2(ps[skip]);

            for (int i = skip; i < ps.Count; i++)
            {
                if (skipLonelyNodes && gm.Graph.CalculateDegree(i) == 0) continue;
                if (ps[i].X < min.X) min.X = ps[i].X;
                if (ps[i].Y < min.Y) min.Y = ps[i].Y;
                if (ps[i].X > max.X) max.X = ps[i].X;
                if (ps[i].Y > max.Y) max.Y = ps[i].Y;
            }

            scale = Math.Max(
                (max.X - min.X) / (bmGraph.Width  - padding * 2 - 1), 
                (max.Y - min.Y) / (bmGraph.Height - padding * 2 - 1));

            for (int i = 0; i < ps.Count; i++)
            {
                ps[i] -= (max + min) / 2;
                ps[i] = ps[i] / scale;
                ps[i] += new Double2(bmGraph.Width / 2, bmGraph.Height / 2);
            }
        }

        public void DrawGraph(bool scale = true)
        {
            if (gm.Graph == null) return;
            if (gm.Graph.NeighbourMatrix == null) return;

            Font font = new Font(FONT, 10);
            SolidBrush brush = new SolidBrush(mainColor);
            SolidBrush selectionBrush = new SolidBrush(selectionColor);
            Pen pen = new Pen(mainColor);
            Pen sPen = new Pen(selectionColor);

            graphDrawerGraphics.Clear(background);

            int radius = Math.Min(bmGraph.Width, bmGraph.Height) / 2 - 22;
            
            if (scale)
            {
                scaledPoints = new List<Double2>(gm.Points);
                FitToCanvas(scaledPoints, GetGraphPadding(), forceDirectedArrangement);
            }
            
            // Text
            if (showDegree && !forceDirectedArrangement)
            {
                List<Double2> textPositions = gm.GetCircularArrangement(gm.Graph.NodeCount, radius + 15, new Double2(bmGraph.Width / 2, bmGraph.Height / 2));
                FitToCanvas(textPositions, 7, forceDirectedArrangement);
                for (int i = 0; i < gm.Graph.NodeCount; i++)
                {
                    graphDrawerGraphics.DrawString(gm.Graph.CalculateDegree(i).ToString(), font, brush, 
                        (int)textPositions[i].X - (gm.Graph.CalculateDegree(i) < 10 ? 5 : 9), 
                        (int)textPositions[i].Y - 8);
                }
            }

            // Edges
            for (int i = 0; i < scaledPoints.Count - 1; i++)
            {
                for (int j = i + 1; j < scaledPoints.Count; j++)
                {
                    try
                    {
                        if (gm.Graph.NeighbourMatrix[i][j])
                        {
                            int posX1 = (int)scaledPoints[i].X;
                            int posY1 = (int)scaledPoints[i].Y;
                            int posX2 = (int)scaledPoints[j].X;
                            int posY2 = (int)scaledPoints[j].Y;

                            LinearGradientBrush gradBrush = new LinearGradientBrush(
                                new Point(posX1 + (posX2 > posX1 ? -1 :  1), posY1 + (posY2 > posY1 ? -1 :  1)),
                                new Point(posX2 + (posX2 > posX1 ?  1 : -1), posY2 + (posY2 > posY1 ?  1 : -1)),
                                Color.FromArgb(
                                    gradient ? (int)(255.0 * Math.Pow((double)gm.Graph.CalculateDegree(i) / gm.Graph.MaxDegree, 2.5)) : 255, 
                                    i == selectedNodeID ? sPen.Color.R : pen.Color.R,
                                    i == selectedNodeID ? sPen.Color.G : pen.Color.G,
                                    i == selectedNodeID ? sPen.Color.B : pen.Color.B),
                                Color.FromArgb(
                                    gradient ? (int)(255.0 * Math.Pow((double)gm.Graph.CalculateDegree(j) / gm.Graph.MaxDegree, 2.5)) : 255, 
                                    j == selectedNodeID ? sPen.Color.R : pen.Color.R, 
                                    j == selectedNodeID ? sPen.Color.G : pen.Color.G, 
                                    j == selectedNodeID ? sPen.Color.B : pen.Color.B)
                                );
                            Pen gradPen = new Pen(gradBrush);

                            graphDrawerGraphics.DrawLine(gradPen, posX1, posY1, posX2, posY2);

                            gradPen.Dispose();
                            gradBrush.Dispose();
                        }
                    }
                    catch
                    {
                        if (gm.Graph.NeighbourMatrix.Count == 0) Debug.WriteLine("Neighbour Matrix is empty");
                        Debug.WriteLine($"i: {i}, j: {j}, size: {gm.Graph.NeighbourMatrix.Count}x{gm.Graph.NeighbourMatrix[0].Count}");
                    }
                }
            }

            // Nodes
            if (showNodes)
            {
                for (int i = 0; i < scaledPoints.Count; i++)
                {
                    if (forceDirectedArrangement && gm.Graph.CalculateDegree(i) == 0) continue;

                    int posX = (int)scaledPoints[i].X - NODE_SIZE / 2;
                    int posY = (int)scaledPoints[i].Y - NODE_SIZE / 2;

                    graphDrawerGraphics.FillEllipse(selectedNodeID == i ? selectionBrush : brush, new Rectangle(posX, posY, NODE_SIZE, NODE_SIZE));
                }
            }

            UpdateGraph();

            font.Dispose();
            pen.Dispose();
            brush.Dispose();
            selectionBrush.Dispose();
        }

        private void DrawChart()
        {
            if (gm.Graph == null) return;
            if (gm.Graph.NeighbourMatrix == null) return;

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
                case Chart.Type.Node: values = GetNodeList(); maxValue = gm.Graph.NodeCount - 1; break;
                case Chart.Type.Degree: values = GetDegreeList(); maxValue = gm.Graph.NodeCount; break;
                case Chart.Type.AverageDegree: values = GetAverageDegreeList(); maxValue = gm.Graph.NodeCount; break;
            }

            if (values == null || values.Count == 0)
            {
                UpdateChart();
                return;
            }

            SolidBrush brush = null;
            switch (selectedChartType)
            {
                case Chart.Type.Node: brush = new SolidBrush(mainColor); break;
                case Chart.Type.Degree: brush = new SolidBrush(blue); break;
                case Chart.Type.AverageDegree: brush = new SolidBrush(orange); break;
            }

            if (sortChart) values.Sort(delegate (double a, double b) { return a > b ? -1 : 1; });

            int horizontalOffset = selectedChartType == Chart.Type.AverageDegree ? values.Max() >= 10.0 ? 10 : 3 : 0;
            int startX = showChartValues ? CHART_VERTICAL_VALUES_WIDTH + horizontalOffset : 0;
            int chartWidth = showChartValues ? panelChart.Width - (CHART_VERTICAL_VALUES_WIDTH + horizontalOffset) : panelChart.Width;
            int chartHeight = (showChartValues && !sortChart && (selectedChartType == Chart.Type.Degree || selectedChartType == Chart.Type.AverageDegree)) ? panelChart.Height - CHART_HORIZONTAL_VALUES_HEIGHT : panelChart.Height;

            double x = startX;
            double columnFactor = values.Count * CHART_COLUMN_WIDTH_RATIO;
            double gapFactor = (values.Count - 1) * (1 - CHART_COLUMN_WIDTH_RATIO);
            double totalWidth = columnFactor + gapFactor;
            double columnWidth = (chartWidth * (columnFactor / totalWidth)) / values.Count;
            double gapWidth = (chartWidth * (gapFactor / totalWidth)) / (values.Count - 1);

            if (showChartValues)
            {
                Pen pen = new Pen(foreground);
                Font font = new Font(FONT, 10);
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

                if (!sortChart && (selectedChartType == Chart.Type.Degree || selectedChartType == Chart.Type.AverageDegree))
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
            for (int i = 0; i < gm.Graph.NodeCount; i++)
                degrees.Add(gm.Graph.CalculateDegree(i));

            return degrees;
        }

        private List<double> GetDegreeList()
        {
            List<double> counts = new List<double>(new double[gm.Graph.NodeCount]);
            for (int i = 0; i < gm.Graph.NodeCount; i++)
            {
                int degree = gm.Graph.CalculateDegree(i);
                counts[degree]++;
            }

            return counts;
        }

        private List<double> GetAverageDegreeList()
        {
            return gm.AverageDegreeDistribution;
        }

        private void lValueOfNodes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    int result = Int32.Parse(lValueOfNodes.Text.ToString());
                    trackBarNodes.Value = result;
                    lValueOfNodes.Text = result.ToString();
                    bRandom.Focus();
                }
                catch (FormatException)
                {
                    lValueOfNodes.Text = trackBarNodes.Value.ToString();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void lValueOfProbability_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    double result = Double.Parse(lValueOfProbability.Text.ToString());
                    trackBarProbability.Value = (int)(result * 100);
                    lValueOfProbability.Text = result.ToString("0.00");
                    bRandom.Focus();
                }
                catch (FormatException)
                {
                    lValueOfProbability.Text = trackBarProbability.Value.ToString();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void lValueOfPower_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    double result = Double.Parse(lValueOfPower.Text.ToString());
                    trackBarPower.Value = (int)(result * 10);
                    lValueOfPower.Text = result.ToString("0.0");
                    bRandom.Focus();
                }
                catch (FormatException)
                {
                    lValueOfPower.Text = trackBarPower.Value.ToString();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void bStretch_Click(object sender, EventArgs e)
        {
            stretchChart = !stretchChart;
            bStretch.BackColor = getToggleButtonColor(stretchChart);
            DrawChart();
        }

        private void bGradient_Click(object sender, EventArgs e)
        {
            gradient = !gradient;
            bGradient.BackColor = getToggleButtonColor(gradient);
            DrawGraph();
        }

        private void bShowDegree_Click(object sender, EventArgs e)
        {
            showDegree = !showDegree;
            bShowDegree.BackColor = getToggleButtonColor(showDegree);
            DrawGraph();
        }

        private void bSort_Click(object sender, EventArgs e)
        {
            sortChart = !sortChart;
            bSort.BackColor = getToggleButtonColor(sortChart);
            DrawChart();
        }

        private void bShowNodes_Click(object sender, EventArgs e)
        {
            showNodes = !showNodes;
            bShowNodes.BackColor = getToggleButtonColor(showNodes);
            DrawGraph();
        }

        private void bShowValues_Click(object sender, EventArgs e)
        {
            showChartValues = !showChartValues;
            bShowValues.BackColor = getToggleButtonColor(showChartValues);
            DrawChart();
        }

        private void bArrangement_Click(object sender, EventArgs e)
        {
            forceDirectedArrangement = !forceDirectedArrangement;
            bArrangement.BackColor = getToggleButtonColor(forceDirectedArrangement);
            timer.Enabled = forceDirectedArrangement;
            ArrangePoints();
            DrawGraph();
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
                    case Chart.Type.Node: bChart1.BackColor = mainColor; break;
                    case Chart.Type.Degree: bChart2.BackColor = blue; break;
                    case Chart.Type.AverageDegree: bChart3.BackColor = orange; break;
                }
            }

            bChart1.Refresh();
            bChart2.Refresh();
            bChart3.Refresh();
        }

        private void bChart1_Click(object sender, EventArgs e)
        {
            if (selectedChartType == Chart.Type.Node)
            {
                showChart = !showChart;
            }
            else
            {
                showChart = true;
            }
            selectedChartType = Chart.Type.Node;
            UpdateChartButtons();
            DrawChart();
        }

        private void bChart2_Click(object sender, EventArgs e)
        {
            if (selectedChartType == Chart.Type.Degree)
            {
                showChart = !showChart;
            }
            else
            {
                showChart = true;
            }
            selectedChartType = Chart.Type.Degree;
            UpdateChartButtons();
            DrawChart();
        }

        private void bChart3_Click(object sender, EventArgs e)
        {
            if (selectedChartType == Chart.Type.AverageDegree)
            {
                showChart = !showChart;
            }
            else
            {
                showChart = true;
            }
            selectedChartType = Chart.Type.AverageDegree;
            UpdateChartButtons();
            DrawChart();
        }

        Color getToggleButtonColor(bool value)
        {
            return value ? blue : background;
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState != lastWindowState)
            {
                SetupGraphics();
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
            DrawUI();
        }

        private void panelGraph_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer_Tick(object sender, EventArgs e) {
            if (!forceDirectedArrangement) return;
            if (gm.Graph == null) return;
            if (mouseDown && selectedNodeID >= 0) return;
                
            gm.AdvanceForceDirectedArrangement();

            if (graphHovered)
            {
                selectedNodeID = GetSelectedNodeID(mousePos);
            }

            DrawGraph();
        }

        private int GetSelectedNodeID(Double2 mouse)
        {
            if (scaledPoints == null || scaledPoints.Count == 0) return -1;
            if (mouseDown) return selectedNodeID;

            double minDistance = scaledPoints[0].DistanceFrom(mouse);
            int id = 0;

            for (int i = 1; i < scaledPoints.Count; i++)
            {
                double distance = scaledPoints[i].DistanceFrom(mouse);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    id = i;
                }
            }   

            if (minDistance > NODE_SIZE * 1.5) id = -1;
            
            return id;
        }

        private void panelGraph_MouseEnter(object sender, EventArgs e)
        {
            graphHovered = true;
        }

        private void panelGraph_MouseLeave(object sender, EventArgs e)
        {
            graphHovered = false;
            selectedNodeID = -1;
        }

        private void panelGraph_MouseMove(object sender, MouseEventArgs e)
        {
            graphHovered = true;
            mousePos = e.Location;
            selectedNodeID = GetSelectedNodeID(e.Location);

            if (mouseDown && selectedNodeID >= 0)
            {
                scaledPoints[selectedNodeID] = mousePos;
            }

            if (!forceDirectedArrangement || mouseDown && selectedNodeID >= 0)
            {
                DrawGraph(false);
            }
        }

        private void panelGraph_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    mouseDown = true;
                    if (selectedNodeID >= 0)
                    {
                        selectedNodeOrigin = scaledPoints[selectedNodeID];
                    }
                    break;
                case MouseButtons.Right:
                    if (selectedNodeID >= 0)
                    {
                        scaledPoints.RemoveAt(selectedNodeID);
                        gm.DeleteNode(selectedNodeID);
                        FillStatistics();
                    }
                    break;
            }   
        }

        private void panelGraph_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    mouseDown = false;
                    if (selectedNodeID >= 0)
                    {
                        gm.TranslateNode(selectedNodeID, (scaledPoints[selectedNodeID] - selectedNodeOrigin) * scale);
                    }
                    break;
            }
        }
    }
}
