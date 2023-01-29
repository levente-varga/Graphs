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
    public partial class FormMain : Form
    {
        public static string VERSION = "2.0.0";

        public enum Panels
        {
            None,
            Graph,
            Chart,
        }

        Panels hoveredPanel = Panels.None;
        Chart.Types selectedChartType = 0;
        Graph.Types selectedGraphType = 0;

        private GraphManager gm;
        Graphics graphPanelGraphics;
        Graphics chartPanelGraphics;
        Graphics graphDrawerGraphics;
        Graphics chartDrawerGraphics;
        Graphics graphPanelDrawerGraphics;

        List<Double2> scaledPoints;
        Double2 mousePos;
        MouseButtons mouseBtnDown;
        double scale = 1;

        long time = 0;

        bool stretchChart = false;
        bool gradient = false;
        bool showDegree = false;
        bool sortChart = false;
        bool showNodes = true;
        bool showChartValues = true;
        bool forceDirectedArrangement = false;
        bool showChart = true;
        bool generateSamples = false;

        int selectedColumnID = -1; 
        int selectedNodeID = -1;
        Double2 selectedNodeOrigin;

        List<Rectangle> chartColumns;

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

        public static string FONT = "Segoe UI";

        public FormMain()
        {
            Console.WriteLine($"Version: {Environment.Version}");

            InitializeComponent();
            SetupGraphics();
            lVersion.Text = VERSION;

            gm = new GraphManager();

            timer.Start();
            timer.Enabled = true;
            timer.Interval = 1;

            scaledPoints = new List<Double2>();
            chartColumns = new List<Rectangle>();

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



        private void timer_Tick(object sender, EventArgs e)
        {
            time++;

            if (gm.Graph == null) return;

            if (generateSamples)
            {
                GenerateGraph(selectedGraphType);
            } 
            else if (forceDirectedArrangement)
            {
                if (mouseBtnDown != MouseButtons.None && selectedNodeID >= 0) return;

                gm.AdvanceForceDirectedArrangement();

                if (hoveredPanel == Panels.Graph)
                {
                    selectedNodeID = GetSelectedNodeID(mousePos);
                }

                DrawGraph();
                DrawChart();
            }
        }



        private void buttonGenRnd_Click(object sender, EventArgs e) => GenerateGraph(Graph.Types.Random);
        private void buttonGenPop_Click(object sender, EventArgs e) => GenerateGraph(Graph.Types.Popularity);
        private void GenerateGraph(Graph.Types graphType) 
        {
            if (graphType == Graph.Types.None) graphType = Graph.Types.Random;
            gm.GenerateGraph(graphType, valueOfNodes(), valueOfProbability(), valueOfPower());

            Colors.UpdateMainColor(graphType);

            selectedGraphType = graphType;
            pProgressBar.BackColor = Colors.main;
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
            graphPanelDrawerGraphics.Clear(Colors.background);
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

        private void ArrangePoints()
        {
            if (gm.Graph == null) return;
            if (gm.Graph.NeighbourMatrix == null) return;

            int radius = Math.Min(bmGraph.Width, bmGraph.Height) / 2 - 22;
            gm.ArrangeInCircle(radius, new Double2(bmGraph.Width / 2, bmGraph.Height / 2));

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

            if (scale == 0) scale = 1;

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
            SolidBrush brush = new SolidBrush(Colors.main);
            SolidBrush selectionBrush = new SolidBrush(Colors.selection);
            Pen pen = new Pen(Colors.main);
            Pen sPen = new Pen(Colors.selection);

            graphDrawerGraphics.Clear(Colors.background);

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

        private double RoundDouble(double n, int decimals) 
        {
            if (decimals < 0) return 0;
            double multiplier = Math.Pow(10, decimals);
            return Convert.ToInt32(n * multiplier) / multiplier; 
        }

        private int CalculateColumnHeight(List<double> values, int i, double maxValue, int maxOrdinate, int chartHeight)
        {
            return (int)(values[i] / (stretchChart ? maxValue : maxOrdinate) * chartHeight);
        }

        private void DrawChart()
        {
            if (gm.Graph == null) return;
            if (gm.Graph.NeighbourMatrix == null) return;

            chartDrawerGraphics.Clear(Colors.background);

            if (!showChart)
            {
                UpdateChart();
                return;
            }

            List<double> values = null;
            int maxOrdinate = 0;

            switch (selectedChartType)
            {
                case Chart.Types.Node: values = GetNodeList(); maxOrdinate = gm.Graph.NodeCount - 1; break;
                case Chart.Types.Degree: values = GetDegreeList(); maxOrdinate = gm.Graph.NodeCount; break;
                case Chart.Types.AverageDegree: values = GetAverageDegreeList(); maxOrdinate = gm.Graph.NodeCount; break;
            }

            if (values == null || values.Count == 0)
            {
                UpdateChart();
                return;
            }

            if (sortChart) values.Sort((double a, double b) => a > b ? -1 : 1);


            double maxValue = values.Max();
            double measuredValue = selectedColumnID >= 0 ? values[selectedColumnID] : maxValue;
            int maxValueColumnID = values.FindIndex((n) => n == maxValue);
            int measuredColumnID = selectedColumnID >= 0 ? selectedColumnID : maxValueColumnID;


            SolidBrush selectionBrush = new SolidBrush(Colors.selection);
            SolidBrush brush = null;
            switch (selectedChartType)
            {
                case Chart.Types.Node: brush = new SolidBrush(Colors.main); break;
                case Chart.Types.Degree: brush = new SolidBrush(Colors.blue); break;
                case Chart.Types.AverageDegree: brush = new SolidBrush(Colors.orange); break;
            }

            int horizontalOffset = 0;
            switch (selectedChartType)
            {
                case Chart.Types.Node: horizontalOffset = 0; break;
                case Chart.Types.Degree: horizontalOffset = 0;  break;
                case Chart.Types.AverageDegree: horizontalOffset = 10; break;
            }

            int startX = showChartValues 
                ? CHART_VERTICAL_VALUES_WIDTH + horizontalOffset 
                : 0;
            int chartWidth = showChartValues 
                ? panelChart.Width - (CHART_VERTICAL_VALUES_WIDTH + horizontalOffset) 
                : panelChart.Width;
            int chartHeight = (showChartValues && !sortChart && (selectedChartType == Chart.Types.Degree || selectedChartType == Chart.Types.AverageDegree)) 
                ? panelChart.Height - CHART_HORIZONTAL_VALUES_HEIGHT 
                : panelChart.Height - 1;

            double columnFactor = values.Count * CHART_COLUMN_WIDTH_RATIO;
            double gapFactor = (values.Count - 1) * (1 - CHART_COLUMN_WIDTH_RATIO);
            double totalWidth = columnFactor + gapFactor;
            double columnWidth = (chartWidth * (columnFactor / totalWidth)) / values.Count;
            double gapWidth = (chartWidth * (gapFactor / totalWidth)) / (values.Count - 1);

            // Values

            if (showChartValues)
            {
                Font font = new Font(FONT, 10);
                float[] dashValues = { 4, 4 };
                Pen pen = new Pen(Colors.foreground);
                pen.DashPattern = dashValues;

                int measuredColumnHeight = CalculateColumnHeight(values, measuredColumnID, maxValue, maxOrdinate, chartHeight);
                int linePosY = chartHeight - measuredColumnHeight;

                chartDrawerGraphics.DrawLine(pen, 0, linePosY, panelChart.Width, linePosY);

                string tMeasuredValue;
                if (measuredValue != RoundDouble(measuredValue, 0))
                    tMeasuredValue = RoundDouble(measuredValue, 1).ToString("0.0");
                else
                    tMeasuredValue = RoundDouble(measuredValue, 0).ToString();
                chartDrawerGraphics.DrawString(tMeasuredValue, font, brush, 0, chartHeight - linePosY > 17 ? linePosY : linePosY - 17);

                if (!sortChart && (selectedChartType == Chart.Types.Degree || selectedChartType == Chart.Types.AverageDegree))
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
                                    column.ToString(), 
                                    font, brush,
                                    (int)(textX + columnWidth / 2 - (column < 10 ? 5 : 9.5)), 
                                    chartHeight + 1);
                            }
                        }

                        textX += w;
                    }
                }

                font.Dispose();
                pen.Dispose();
            }

            // Columns

            chartColumns.Clear();
            double x = startX;
            for (int i = 0; i < values.Count * 2 - 1; i++)
            {
                double w = i % 2 == 0 ? columnWidth : gapWidth;

                if (i % 2 == 0)
                {
                    int columnID = i / 2;

                    int h = CalculateColumnHeight(values, columnID, maxValue, maxOrdinate, chartHeight);
                    int y = chartHeight - h;

                    Rectangle r = new Rectangle((int)x, y, (int)w, h);
                    chartDrawerGraphics.FillRectangle(selectedColumnID == columnID ? selectionBrush : brush, r);
                    
                    Rectangle hitbox = new Rectangle();
                    hitbox.Height = chartHeight;
                    hitbox.Y = 0;
                    hitbox.X = (int)(columnID > 0 ? x - gapWidth * 0.5 : x);
                    hitbox.Width = (int)(columnWidth + ((columnID > 0 ? 0.5 : 0.0) + (columnID < values.Count - 1 ? 0.5 : 0.0)) * gapWidth) + 1;

                    chartColumns.Add(hitbox);
                }

                x += w;
            }

            brush.Dispose();
            selectionBrush.Dispose();

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
            //timer.Enabled = forceDirectedArrangement;
            ArrangePoints();
            DrawGraph();
        }

        private void bGenerateSamples_Click(object sender, EventArgs e)
        {
            generateSamples = !generateSamples;
            bGenerateSamples.BackColor = getToggleButtonColor(generateSamples);
        }

        private void UpdateChartButtons()
        {
            bChart1.BackColor = Colors.background;
            bChart2.BackColor = Colors.background;
            bChart3.BackColor = Colors.background;

            if (showChart)
            {
                switch (selectedChartType)
                {
                    case Chart.Types.Node: bChart1.BackColor = Colors.main; break;
                    case Chart.Types.Degree: bChart2.BackColor = Colors.blue; break;
                    case Chart.Types.AverageDegree: bChart3.BackColor = Colors.orange; break;
                }
            }

            bChart1.Refresh();
            bChart2.Refresh();
            bChart3.Refresh();
        }

        private void bChart1_Click(object sender, EventArgs e)
        {
            if (selectedChartType == Chart.Types.Node)
            {
                showChart = !showChart;
            }
            else
            {
                showChart = true;
            }
            selectedChartType = Chart.Types.Node;
            UpdateChartButtons();
            DrawChart();
        }

        private void bChart2_Click(object sender, EventArgs e)
        {
            if (selectedChartType == Chart.Types.Degree)
            {
                showChart = !showChart;
            }
            else
            {
                showChart = true;
            }
            selectedChartType = Chart.Types.Degree;
            UpdateChartButtons();
            DrawChart();
        }

        private void bChart3_Click(object sender, EventArgs e)
        {
            if (selectedChartType == Chart.Types.AverageDegree)
            {
                showChart = !showChart;
            }
            else
            {
                showChart = true;
            }
            selectedChartType = Chart.Types.AverageDegree;
            UpdateChartButtons();
            DrawChart();
        }

        Color getToggleButtonColor(bool value)
        {
            return value ? Colors.blue : Colors.background;
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

        private bool isInsideRect(Double2 pos, Rectangle rect)
        {
            return rect.X <= pos.X && pos.X < rect.X + rect.Width
                && rect.Y <= pos.Y && pos.Y < rect.Y + rect.Height;
        }

        private int GetSelectedColumnID(Double2 mouse)
        {
            if (chartColumns == null) return -1;

            for (int i = 0; i < chartColumns.Count; i++)
            {
                if (isInsideRect(mouse, chartColumns[i])) return i;
            }

            return -1;
        }

        private int GetSelectedNodeID(Double2 mouse)
        {
            if (scaledPoints == null || scaledPoints.Count == 0) return -1;
            if (mouseBtnDown == MouseButtons.Left) return selectedNodeID;

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
            hoveredPanel = Panels.Graph;
        }

        private void panelGraph_MouseMove(object sender, MouseEventArgs e)
        {
            hoveredPanel = Panels.Graph;
            mousePos = e.Location;
            selectedNodeID = GetSelectedNodeID(e.Location);

            if (mouseBtnDown == MouseButtons.Left && selectedNodeID >= 0)
            {
                scaledPoints[selectedNodeID] = mousePos;
            }

            if (!generateSamples && (!forceDirectedArrangement || mouseBtnDown != MouseButtons.None && selectedNodeID >= 0))
            {
                DrawGraph(false);
            }
        }

        private void panelGraph_MouseLeave(object sender, EventArgs e)
        {
            hoveredPanel = Panels.None;
            mouseBtnDown = MouseButtons.None;
            selectedNodeID = -1;

            if (!generateSamples && (!forceDirectedArrangement || mouseBtnDown != MouseButtons.None && selectedNodeID >= 0))
            {
                DrawGraph(false);
            }
        }

        private void panelGraph_MouseDown(object sender, MouseEventArgs e)
        {
            mouseBtnDown = e.Button;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (selectedNodeID >= 0)
                    {
                        selectedNodeOrigin = scaledPoints[selectedNodeID];
                    }
                    break;
                case MouseButtons.Middle:
                    
                    break;
                case MouseButtons.Right:
                    
                    break;
            }   
        }

        private void panelGraph_MouseUp(object sender, MouseEventArgs e)
        {
            mouseBtnDown = MouseButtons.None;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (selectedNodeID >= 0)
                    {
                        gm.TranslateNode(selectedNodeID, (scaledPoints[selectedNodeID] - selectedNodeOrigin) * scale);
                    }
                    break;
                case MouseButtons.Middle:

                    break;
                case MouseButtons.Right:
                    if (selectedNodeID >= 0)
                    {
                        scaledPoints.RemoveAt(selectedNodeID);
                        gm.RemoveNode(selectedNodeID);
                        FillStatistics();
                        UpdateChart();
                        DrawChart();
                        DrawGraph();
                        mouseBtnDown = MouseButtons.None;
                    }
                    break;
            }
        }

        

        private void panelChart_MouseEnter(object sender, EventArgs e)
        {
            hoveredPanel = Panels.Chart;
        }
        
        private void panelChart_MouseMove(object sender, MouseEventArgs e)
        {
            hoveredPanel = Panels.Chart;
            selectedColumnID = GetSelectedColumnID(e.Location);

            if (!generateSamples && !forceDirectedArrangement)
            {
                DrawChart();
            }
        }
        
        private void panelChart_MouseLeave(object sender, EventArgs e)
        {
            hoveredPanel = Panels.None;
            mouseBtnDown = MouseButtons.None;
            selectedColumnID = -1;

            if (!generateSamples && !forceDirectedArrangement)
            {
                DrawChart();
            }
        }        
        
        private void panelChart_MouseDown(object sender, MouseEventArgs e)
        {
            mouseBtnDown = e.Button;
        }

        private void panelChart_MouseUp(object sender, MouseEventArgs e)
        {
            mouseBtnDown = MouseButtons.None;
        }
    }
}
