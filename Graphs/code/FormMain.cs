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

        private GraphManager graphManager;
        Graphics graphPanelGraphics;
        Graphics chartPanelGraphics;
        Graphics graphDrawerGraphics;
        Graphics chartDrawerGraphics;
        Graphics graphPanelDrawerGraphics;

        List<Double2> scaledPoints;
        Double2 mousePosition;
        MouseButtons mouseButtonPressed;
        double graphScaling = 1;

        int selectedColumnID = -1; 
        int selectedNodeID = -1;
        Double2 selectedNodeOrigin;

        List<Rectangle> chartColumns;

        Bitmap textureGraph;
        Bitmap textureChart;
        Bitmap textureGraphPanel;

        FormWindowState lastWindowState;

        private static double CHART_COLUMN_WIDTH_RATIO = 0.6;
        private static int CHART_VERTICAL_VALUES_WIDTH = 20;
        private static int CHART_HORIZONTAL_VALUES_HEIGHT = 17;
        private static int NODE_SIZE = 8;

        private const int NODE_COUNT_MAX = 100;
        private const int NODE_COUNT_MIN = 3;
        private const int NODE_COUNT_INIT = 13;
        private const int NODE_COUNT_STEP = 1;
        private const int PROBABILITY_MAX = 100;
        private const int PROBABILITY_MIN = 0;
        private const int PROBABILITY_INIT = 50;
        private const int PROBABILITY_STEP = 1;
        private const int POWER_MAX = 1000;
        private const int POWER_MIN = 0;
        private const int POWER_INIT = 100;
        private const int POWER_STEP = 5;

        public static string FONT = "Segoe UI";

        public FormMain()
        {
            Console.WriteLine($"Version: {Environment.Version}");

            InitializeComponent();
            SetupGraphics();
            lVersion.Text = VERSION;

            graphManager = new GraphManager();

            timer.Start();
            timer.Enabled = true;
            timer.Interval = 1;

            scaledPoints = new List<Double2>();
            chartColumns = new List<Rectangle>();

            lastWindowState = WindowState;

            Double2 p1 = new Double2(5, 4);
            Double2 p2 = new Double2(0, 0);
            lMaxF.Text = p1.Normalize().DistanceFrom(new Double2(0, 0)).ToString();

            SetupUI();
        }

        private void SetupGraphics()
        {
            //int graphSize = Math.Min(panelGraph.Width, panelGraph.Height);

            if (graphPanelGraphics != null) graphPanelGraphics.Dispose();
            if (chartPanelGraphics != null) chartPanelGraphics.Dispose();
            if (graphDrawerGraphics != null) graphDrawerGraphics.Dispose();
            if (chartDrawerGraphics != null) chartDrawerGraphics.Dispose();
            if (graphPanelDrawerGraphics != null) graphPanelDrawerGraphics.Dispose();
            if (textureChart != null) textureChart.Dispose();

            if (panelGraph.Width > 0 && panelGraph.Height > 0)
            {
                if (textureGraphPanel != null) textureGraphPanel.Dispose();
                if (textureGraph != null) textureGraph.Dispose();
                textureGraphPanel = new Bitmap(panelGraph.Width, panelGraph.Height);
                textureGraph = new Bitmap(panelGraph.Width, panelGraph.Height);//graphSize, graphSize);
            }

            textureChart = new Bitmap(panelChart.Width, panelChart.Height);
            graphPanelGraphics = panelGraph.CreateGraphics();
            chartPanelGraphics = panelChart.CreateGraphics();
            graphDrawerGraphics = Graphics.FromImage(textureGraph);
            chartDrawerGraphics = Graphics.FromImage(textureChart);
            graphPanelDrawerGraphics = Graphics.FromImage(textureGraphPanel);
            graphDrawerGraphics.SmoothingMode = SmoothingMode.AntiAlias;
        }

        private void SetupUI()
        {
            SetupTrackBars();
            SetupToggleButtons();
        }

        private void SetupTrackBars()
        {
            trackBarNodes.Minimum = NODE_COUNT_MIN;
            trackBarNodes.Maximum = (NODE_COUNT_MAX - NODE_COUNT_MIN) / NODE_COUNT_STEP + NODE_COUNT_MIN;
            trackBarNodes.Value = Math.Max(Math.Min((NODE_COUNT_INIT - NODE_COUNT_MIN) / NODE_COUNT_STEP, trackBarNodes.Maximum), trackBarNodes.Minimum); ;

            trackBarProbability.Minimum = PROBABILITY_MIN;
            trackBarProbability.Maximum = (PROBABILITY_MAX - PROBABILITY_MIN) / PROBABILITY_STEP + PROBABILITY_MIN;
            trackBarProbability.Value = Math.Max(Math.Min((PROBABILITY_INIT - PROBABILITY_MIN) / PROBABILITY_STEP, trackBarProbability.Maximum), trackBarProbability.Minimum);

            trackBarPower.Minimum = POWER_MIN;
            trackBarPower.Maximum = (POWER_MAX - POWER_MIN) / POWER_STEP + POWER_MIN;
            trackBarPower.Value = Math.Max(Math.Min((POWER_INIT - POWER_MIN) / POWER_STEP, trackBarPower.Maximum), trackBarPower.Minimum);
        }

        private void SetupToggleButtons()
        {
            bShowDegree.BackColor = getToggleButtonColor(Options.showDegree);
            bShowNodes.BackColor = getToggleButtonColor(Options.showNodes);
            bGradient.BackColor = getToggleButtonColor(Options.gradient);
            bSort.BackColor = getToggleButtonColor(Options.sortChart);
            bStretch.BackColor = getToggleButtonColor(Options.stretchChart);
            bShowValues.BackColor = getToggleButtonColor(Options.showChartValues);
            bArrangement.BackColor = getToggleButtonColor(Options.forceDirectedArrangement);
            bGenerateSamples.BackColor = getToggleButtonColor(Options.generateSamples);
        }

        public void SetProgressBarPercent(double p)
        {
            if (p > 1) p = 1;
            if (p < 0) p = 0;

            pProgressBar.Width = (int) (p * panel1.Width);

            pProgressBar.Refresh();
        }

        private int valueOfNodes() => (trackBarNodes.Minimum + (trackBarNodes.Value - trackBarNodes.Minimum) * NODE_COUNT_STEP) * 1;
        private double valueOfProbability() => (trackBarProbability.Minimum + (trackBarProbability.Value - trackBarProbability.Minimum) * PROBABILITY_STEP) * 0.01;
        private double valueOfPower() => (trackBarPower.Minimum + (trackBarPower.Value - trackBarPower.Minimum) * POWER_STEP) * 0.01;

        private void trackBarNodes_Scroll(object sender, EventArgs e)
        {
            int nodes = valueOfNodes();
            lValueOfNodes.Text = nodes.ToString();
            lValueOfNodes.Update();
        }

        private void trackBarProbability_Scroll(object sender, EventArgs e)
        {
            double probability = valueOfProbability();
            lValueOfProbability.Text = probability.ToString("0.00");
            lValueOfProbability.Update();
        }

        private void trackBarPower_Scroll(object sender, EventArgs e)
        {
            double power = valueOfPower();
            lValueOfPower.Text = power.ToString("0.00");
            lValueOfPower.Update();
        }

        private void lValueOfNodes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    int enteredValue = Int32.Parse(lValueOfNodes.Text.ToString());
                    int cappedValue = Math.Max(Math.Min(enteredValue, NODE_COUNT_MAX), NODE_COUNT_MIN);
                    int convertedValue = (cappedValue - NODE_COUNT_MIN) / NODE_COUNT_STEP + NODE_COUNT_MIN;

                    trackBarNodes.Value = convertedValue;
                    lValueOfNodes.Text = cappedValue.ToString();
                    bRandom.Focus();
                }
                catch (FormatException)
                {
                    lValueOfNodes.Text = valueOfNodes().ToString();
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
                    double enteredValue = 100 * Double.Parse(lValueOfProbability.Text.ToString());
                    int cappedValue = Math.Max(Math.Min((int)enteredValue, PROBABILITY_MAX), PROBABILITY_MIN);
                    int convertedValue = (cappedValue - PROBABILITY_MIN) / PROBABILITY_STEP + PROBABILITY_MIN;

                    trackBarProbability.Value = convertedValue;
                    lValueOfProbability.Text = (cappedValue / 100.0).ToString("0.00");
                    bRandom.Focus();
                }
                catch (FormatException)
                {
                    lValueOfProbability.Text = valueOfProbability().ToString("0.00");
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
                    double enteredValue = 100 * Double.Parse(lValueOfPower.Text.ToString());
                    int cappedValue = Math.Max(Math.Min((int)enteredValue, POWER_MAX), POWER_MIN);
                    int convertedValue = (cappedValue - POWER_MIN) / POWER_STEP + POWER_MIN;

                    trackBarPower.Value = convertedValue;
                    lValueOfPower.Text = (cappedValue / 100.0).ToString("0.00");
                    bRandom.Focus();
                }
                catch (FormatException)
                {
                    lValueOfPower.Text = valueOfPower().ToString("0.00");
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }



        private void timer_Tick(object sender, EventArgs e)
        {
            if (graphManager.Graph == null) return;

            if (Options.generateSamples)
            {
                GenerateGraph(selectedGraphType);
            } 
            else if (Options.forceDirectedArrangement)
            {
                if (mouseButtonPressed != MouseButtons.None && selectedNodeID >= 0) return;

                graphManager.AdvanceForceDirectedArrangement();

                if (hoveredPanel == Panels.Graph)
                {
                    selectedNodeID = GetSelectedNodeID(mousePosition);
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
            graphManager.GenerateGraph(graphType, valueOfNodes(), valueOfProbability(), valueOfPower());

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
            graphPanelDrawerGraphics.DrawImage(textureGraph, textureGraphPanel.Width / 2 - textureGraph.Width / 2, textureGraphPanel.Height / 2 - textureGraph.Height / 2);
            graphPanelGraphics.DrawImage(textureGraphPanel, 0, 0);

            //graphPanelDrawerGraphics.CompositingMode = oldCMForGPD;
            //graphDrawerGraphics.CompositingMode = oldCMForGD;
        }

        private void UpdateChart()
        {
            chartPanelGraphics.DrawImage(textureChart, 0, 0);
        }

        private void FillStatistics()
        {
            if (graphManager.Graph == null) return;

            int maxEdgeCount = graphManager.Graph.NodeCount * (graphManager.Graph.NodeCount - 1) / 2;
            lAverageDegreeValue.Text = graphManager.Graph.CalculateAverageDegree().ToString("0.00");
            lAverageDegreeSamples.Text = graphManager.AverageSamples.ToString();
            lEdgeCount.Text = graphManager.Graph.EdgeCount.ToString() + " / " + maxEdgeCount + "   [" + ((double)graphManager.Graph.EdgeCount / maxEdgeCount * 100).ToString("0.0") + "%]";

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
            if (graphManager.Graph == null) return;
            if (graphManager.Graph.NeighbourMatrix == null) return;

            int radius = Math.Min(textureGraph.Width, textureGraph.Height) / 2 - 22;
            graphManager.ArrangeInCircle(radius, new Double2(textureGraph.Width / 2, textureGraph.Height / 2));

            if (Options.forceDirectedArrangement)
            {
                graphManager.ResetForceDirectedArrangement();
                timer.Enabled = true;
                timer.Start();
            }
        }

        private int GetGraphPadding() => Options.forceDirectedArrangement & !Options.generateSamples || !Options.showDegree ? NODE_SIZE / 2 : 22;

        private void FitToCanvas(List<Double2> ps, int padding = 0, bool skipLonelyNodes = false)
        {
            if (ps == null) return;

            int skip = 0;

            if (skipLonelyNodes)
            {
                while (skip < ps.Count && graphManager.Graph.CalculateDegree(skip) == 0)
                {
                    skip++;
                }
                if (skip == ps.Count) return;
            }

            Double2 min = new Double2(ps[skip]);
            Double2 max = new Double2(ps[skip]);

            for (int i = skip; i < ps.Count; i++)
            {
                if (skipLonelyNodes && graphManager.Graph.CalculateDegree(i) == 0) continue;
                if (ps[i].X < min.X) min.X = ps[i].X;
                if (ps[i].Y < min.Y) min.Y = ps[i].Y;
                if (ps[i].X > max.X) max.X = ps[i].X;
                if (ps[i].Y > max.Y) max.Y = ps[i].Y;
            }

            graphScaling = Math.Max(
                (max.X - min.X) / (textureGraph.Width  - padding * 2 - 1), 
                (max.Y - min.Y) / (textureGraph.Height - padding * 2 - 1));

            if (graphScaling == 0) graphScaling = 1;

            for (int i = 0; i < ps.Count; i++)
            {
                ps[i] -= (max + min) / 2;
                ps[i] = ps[i] / graphScaling;
                ps[i] += new Double2(textureGraph.Width / 2, textureGraph.Height / 2);
            }
        }

        public void DrawGraph(bool scale = true)
        {
            if (graphManager.Graph == null) return;
            if (graphManager.Graph.NeighbourMatrix == null) return;
            if (textureGraph.Width <= GetGraphPadding() * 2 + 1) return;

            Font font = new Font(FONT, 10);
            SolidBrush brush = new SolidBrush(Colors.main);
            SolidBrush selectionBrush = new SolidBrush(Colors.selection);
            Pen pen = new Pen(Colors.main);
            Pen sPen = new Pen(Colors.selection);

            graphDrawerGraphics.Clear(Colors.background);

            int radius = Math.Min(textureGraph.Width, textureGraph.Height) / 2 - 22;
            
            if (scale)
            {
                scaledPoints = new List<Double2>(graphManager.Points);
                FitToCanvas(scaledPoints, GetGraphPadding(), Options.forceDirectedArrangement && !Options.generateSamples);
            }
            
            // Text
            if (Options.showDegree && (!Options.forceDirectedArrangement || Options.generateSamples))
            {
                List<Double2> textPositions = graphManager.GetCircularArrangement(graphManager.Graph.NodeCount, radius + 15, new Double2(textureGraph.Width / 2, textureGraph.Height / 2));
                FitToCanvas(textPositions, 7, Options.forceDirectedArrangement && !Options.generateSamples);
                for (int i = 0; i < graphManager.Graph.NodeCount; i++)
                {
                    graphDrawerGraphics.DrawString(graphManager.Graph.CalculateDegree(i).ToString(), font, brush, 
                        (int)textPositions[i].X - (graphManager.Graph.CalculateDegree(i) < 10 ? 5 : 9), 
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
                        if (graphManager.Graph.NeighbourMatrix[i][j])
                        {
                            int posX1 = (int)scaledPoints[i].X;
                            int posY1 = (int)scaledPoints[i].Y;
                            int posX2 = (int)scaledPoints[j].X;
                            int posY2 = (int)scaledPoints[j].Y;

                            LinearGradientBrush gradBrush = new LinearGradientBrush(
                                new Point(posX1 + (posX2 > posX1 ? -1 :  1), posY1 + (posY2 > posY1 ? -1 :  1)),
                                new Point(posX2 + (posX2 > posX1 ?  1 : -1), posY2 + (posY2 > posY1 ?  1 : -1)),
                                Color.FromArgb(
                                    Options.gradient ? (int)(255.0 * Math.Pow((double)graphManager.Graph.CalculateDegree(i) / graphManager.Graph.MaxDegree, 2.5)) : 255, 
                                    i == selectedNodeID ? sPen.Color.R : pen.Color.R,
                                    i == selectedNodeID ? sPen.Color.G : pen.Color.G,
                                    i == selectedNodeID ? sPen.Color.B : pen.Color.B),
                                Color.FromArgb(
                                    Options.gradient ? (int)(255.0 * Math.Pow((double)graphManager.Graph.CalculateDegree(j) / graphManager.Graph.MaxDegree, 2.5)) : 255, 
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
                        if (graphManager.Graph.NeighbourMatrix.Count == 0) Debug.WriteLine("Neighbour Matrix is empty");
                        Debug.WriteLine($"i: {i}, j: {j}, size: {graphManager.Graph.NeighbourMatrix.Count}x{graphManager.Graph.NeighbourMatrix[0].Count}");
                    }
                }
            }

            // Nodes
            if (Options.showNodes)
            {
                for (int i = 0; i < scaledPoints.Count; i++)
                {
                    if (Options.forceDirectedArrangement && !Options.generateSamples && graphManager.Graph.CalculateDegree(i) == 0) continue;

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
            return (int)(values[i] / (Options.stretchChart ? maxValue : maxOrdinate) * chartHeight);
        }

        private void DrawChart()
        {
            if (graphManager.Graph == null) return;
            if (graphManager.Graph.NeighbourMatrix == null) return;

            chartDrawerGraphics.Clear(Colors.background);

            if (!Options.showChart)
            {
                UpdateChart();
                return;
            }

            List<double> values = null;
            int maxOrdinate = 0;

            switch (selectedChartType)
            {
                case Chart.Types.Node: values = GetNodeList(); maxOrdinate = graphManager.Graph.NodeCount - 1; break;
                case Chart.Types.Degree: values = GetDegreeList(); maxOrdinate = graphManager.Graph.NodeCount; break;
                case Chart.Types.AverageDegree: values = GetAverageDegreeList(); maxOrdinate = graphManager.Graph.NodeCount; break;
            }

            if (values == null || values.Count == 0)
            {
                UpdateChart();
                return;
            }

            if (Options.sortChart) values.Sort((double a, double b) => a > b ? -1 : 1);


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

            int startX = Options.showChartValues 
                ? CHART_VERTICAL_VALUES_WIDTH + horizontalOffset 
                : 0;
            int chartWidth = Options.showChartValues 
                ? panelChart.Width - (CHART_VERTICAL_VALUES_WIDTH + horizontalOffset) 
                : panelChart.Width;
            int chartHeight = (Options.showChartValues && !Options.sortChart && (selectedChartType == Chart.Types.Degree || selectedChartType == Chart.Types.AverageDegree)) 
                ? panelChart.Height - CHART_HORIZONTAL_VALUES_HEIGHT 
                : panelChart.Height - 1;

            double columnFactor = values.Count * CHART_COLUMN_WIDTH_RATIO;
            double gapFactor = (values.Count - 1) * (1 - CHART_COLUMN_WIDTH_RATIO);
            double totalWidth = columnFactor + gapFactor;
            double columnWidth = (chartWidth * (columnFactor / totalWidth)) / values.Count;
            double gapWidth = (chartWidth * (gapFactor / totalWidth)) / (values.Count - 1);

            // Values

            if (Options.showChartValues)
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

                if (!Options.sortChart && (selectedChartType == Chart.Types.Degree || selectedChartType == Chart.Types.AverageDegree))
                {
                    double textX = startX;
                    for (int i = 0; i < values.Count * 2 - 1; i++)
                    {
                        double w = i % 2 == 0 ? columnWidth : gapWidth;

                        if (i % 2 == 0)
                        {
                            int column = i / 2;

                            if (values.Count >= 30 && column == values.Count - 1)
                            {
                                // This text would not fit entirely inside the panel if displayed, so do not draw it
                                break;
                            }

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
                    hitbox.Height = panelChart.Height - 1;
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
            for (int i = 0; i < graphManager.Graph.NodeCount; i++)
                degrees.Add(graphManager.Graph.CalculateDegree(i));

            return degrees;
        }

        private List<double> GetDegreeList()
        {
            List<double> counts = new List<double>(new double[graphManager.Graph.NodeCount]);
            for (int i = 0; i < graphManager.Graph.NodeCount; i++)
            {
                int degree = graphManager.Graph.CalculateDegree(i);
                counts[degree]++;
            }

            return counts;
        }

        private List<double> GetAverageDegreeList()
        {
            return graphManager.AverageDegreeDistribution;
        }

        

        private void bStretch_Click(object sender, EventArgs e)
        {
            Options.stretchChart = !Options.stretchChart;
            bStretch.BackColor = getToggleButtonColor(Options.stretchChart);
            DrawChart();
        }

        private void bGradient_Click(object sender, EventArgs e)
        {
            Options.gradient = !Options.gradient;
            bGradient.BackColor = getToggleButtonColor(Options.gradient);
            DrawGraph();
        }

        private void bShowDegree_Click(object sender, EventArgs e)
        {
            Options.showDegree = !Options.showDegree;
            bShowDegree.BackColor = getToggleButtonColor(Options.showDegree);
            DrawGraph();
        }

        private void bSort_Click(object sender, EventArgs e)
        {
            Options.sortChart = !Options.sortChart;
            bSort.BackColor = getToggleButtonColor(Options.sortChart);
            DrawChart();
        }

        private void bShowNodes_Click(object sender, EventArgs e)
        {
            Options.showNodes = !Options.showNodes;
            bShowNodes.BackColor = getToggleButtonColor(Options.showNodes);
            DrawGraph();
        }

        private void bShowValues_Click(object sender, EventArgs e)
        {
            Options.showChartValues = !Options.showChartValues;
            bShowValues.BackColor = getToggleButtonColor(Options.showChartValues);
            DrawChart();
        }

        private void bArrangement_Click(object sender, EventArgs e)
        {
            Options.forceDirectedArrangement = !Options.forceDirectedArrangement;
            bArrangement.BackColor = getToggleButtonColor(Options.forceDirectedArrangement);
            //timer.Enabled = forceDirectedArrangement;
            ArrangePoints();
            DrawGraph();
        }

        private void bGenerateSamples_Click(object sender, EventArgs e)
        {
            Options.generateSamples = !Options.generateSamples;
            bGenerateSamples.BackColor = getToggleButtonColor(Options.generateSamples);
        }

        private void UpdateChartButtons()
        {
            bChart1.BackColor = Colors.background;
            bChart2.BackColor = Colors.background;
            bChart3.BackColor = Colors.background;

            if (Options.showChart)
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
                Options.showChart = !Options.showChart;
            }
            else
            {
                Options.showChart = true;
            }
            selectedChartType = Chart.Types.Node;
            UpdateChartButtons();
            DrawChart();
        }

        private void bChart2_Click(object sender, EventArgs e)
        {
            if (selectedChartType == Chart.Types.Degree)
            {
                Options.showChart = !Options.showChart;
            }
            else
            {
                Options.showChart = true;
            }
            selectedChartType = Chart.Types.Degree;
            UpdateChartButtons();
            DrawChart();
        }

        private void bChart3_Click(object sender, EventArgs e)
        {
            if (selectedChartType == Chart.Types.AverageDegree)
            {
                Options.showChart = !Options.showChart;
            }
            else
            {
                Options.showChart = true;
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
                    if (textureGraphPanel != null) textureGraphPanel.Dispose();
                    textureGraphPanel = new Bitmap(panelGraph.Width, panelGraph.Height);
                }

                graphPanelGraphics = panelGraph.CreateGraphics();
                graphPanelDrawerGraphics = Graphics.FromImage(textureGraphPanel);

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
            if (mouseButtonPressed == MouseButtons.Left) return selectedNodeID;

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
            mousePosition = e.Location;
            selectedNodeID = GetSelectedNodeID(e.Location);

            if (mouseButtonPressed == MouseButtons.Left && selectedNodeID >= 0)
            {
                scaledPoints[selectedNodeID] = mousePosition;
            }

            if (!Options.generateSamples && (!Options.forceDirectedArrangement || mouseButtonPressed != MouseButtons.None && selectedNodeID >= 0))
            {
                DrawGraph(false);
            }
        }

        private void panelGraph_MouseLeave(object sender, EventArgs e)
        {
            hoveredPanel = Panels.None;
            mouseButtonPressed = MouseButtons.None;
            selectedNodeID = -1;

            if (!Options.generateSamples && (!Options.forceDirectedArrangement || mouseButtonPressed != MouseButtons.None && selectedNodeID >= 0))
            {
                DrawGraph(false);
            }
        }

        private void panelGraph_MouseDown(object sender, MouseEventArgs e)
        {
            mouseButtonPressed = e.Button;
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
            mouseButtonPressed = MouseButtons.None;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (selectedNodeID >= 0)
                    {
                        graphManager.TranslateNode(selectedNodeID, (scaledPoints[selectedNodeID] - selectedNodeOrigin) * graphScaling);
                    }
                    break;
                case MouseButtons.Middle:

                    break;
                case MouseButtons.Right:
                    if (selectedNodeID >= 0)
                    {
                        scaledPoints.RemoveAt(selectedNodeID);
                        graphManager.RemoveNode(selectedNodeID);
                        FillStatistics();
                        UpdateChart();
                        DrawChart();
                        DrawGraph();
                        mouseButtonPressed = MouseButtons.None;
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

            if (!Options.generateSamples && !Options.forceDirectedArrangement)
            {
                DrawChart();
            }
        }
        
        private void panelChart_MouseLeave(object sender, EventArgs e)
        {
            hoveredPanel = Panels.None;
            mouseButtonPressed = MouseButtons.None;
            selectedColumnID = -1;

            if (!Options.generateSamples && !Options.forceDirectedArrangement)
            {
                DrawChart();
            }
        }        
        
        private void panelChart_MouseDown(object sender, MouseEventArgs e)
        {
            mouseButtonPressed = e.Button;
        }

        private void panelChart_MouseUp(object sender, MouseEventArgs e)
        {
            mouseButtonPressed = MouseButtons.None;
        }
    }
}
