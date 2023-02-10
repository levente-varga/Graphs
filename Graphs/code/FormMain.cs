using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Runtime.Versioning;

namespace Graphs_Framework
{
    [SupportedOSPlatform("windows")]
    public partial class FormMain : Form
    {
        public static string VERSION = "2.1.0";

        public enum Panels
        {
            None,
            Graph,
            Chart,
        }

        Panels hoveredPanel = Panels.None;
        Chart.Types selectedChartType = 0;
        Graph.Types selectedGraphType = 0;

        private GraphManager graphManager = new GraphManager();
        Graphics graphPanelGraphics;
        Graphics chartPanelGraphics;
        Graphics graphDrawerGraphics;
        Graphics chartDrawerGraphics;
        Graphics graphPanelDrawerGraphics;

        List<Double2> scaledPoints = new List<Double2>();
        Double2 mousePosition;
        MouseButtons mouseButtonPressed;
        double graphScaling = 1;

        int selectedNodeID = -1;
        int selectedColumnID = -1; 
        Double2 selectedNodeOrigin;

        List<Rectangle> chartColumnsHitboxes = new List<Rectangle>();

        Bitmap textureGraph;
        Bitmap textureChart;
        Bitmap textureGraphPanel;

        FormWindowState lastWindowState;

        private static double CHART_COLUMN_GAP_RATIO = 0.6;
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
            Debug.WriteLine($"Version: {Environment.Version}");

            InitializeComponent();
            SetupGraphics();
            lVersion.Text = VERSION;

            timer.Start();
            timer.Enabled = true;
            timer.Interval = 1;

            lastWindowState = WindowState;

            Double2 p1 = new Double2(5, 4);
            Double2 p2 = new Double2(0, 0);
            lMaxF.Text = p1.Normalize().DistanceFrom(new Double2(0, 0)).ToString();

            SetupUI();
        }

        private void SetupGraphics()
        {
            //int graphSize = Math.Min(panelGraph.Width, panelGraph.Height);

            graphPanelGraphics?.Dispose();
            chartPanelGraphics?.Dispose();
            graphDrawerGraphics?.Dispose();
            chartDrawerGraphics?.Dispose();
            graphPanelDrawerGraphics?.Dispose();
            textureChart?.Dispose();

            if (panelGraph.Width > 0 && panelGraph.Height > 0)
            {
                textureGraphPanel?.Dispose();
                textureGraph?.Dispose();
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
            bShowDegree.BackColor = GetToggleButtonColor(Options.showDegree);
            bShowNodes.BackColor = GetToggleButtonColor(Options.showNodes);
            bGradient.BackColor = GetToggleButtonColor(Options.gradient);
            bSort.BackColor = GetToggleButtonColor(Options.sortChart);
            bStretch.BackColor = GetToggleButtonColor(Options.stretchChart);
            bShowValues.BackColor = GetToggleButtonColor(Options.showChartValues);
            bArrangement.BackColor = GetToggleButtonColor(Options.forceDirectedArrangement);
            bGenerateSamples.BackColor = GetToggleButtonColor(Options.generateSamples);
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
                if (DraggingNode()) return;

                if (!Options.pauseForceDirectedArrangement)
                {
                    graphManager.AdvanceForceDirectedArrangement();
                }

                if (hoveredPanel == Panels.Graph)
                {
                    selectedNodeID = GetSelectedNodeID(mousePosition);
                }

                DrawGraphicalUI();
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
            DrawGraphicalUI();
        }

        private void DrawGraphicalUI(bool scaleGraph = true)
        {
            DrawChart();
            DrawGraph(scaleGraph);
        }

        private void UpdateGraphicalUI()
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

        private void FitToCanvas(List<Double2> nodes, int padding = 0, bool skipLonelyNodes = false)
        {
            if (nodes == null) return;

            int startFromIndex = 0;

            if (skipLonelyNodes)
            {
                while (startFromIndex < nodes.Count && graphManager.Graph.CalculateDegree(startFromIndex) == 0)
                {
                    startFromIndex++;
                }
                if (startFromIndex == nodes.Count) return;
            }

            Double2 min = new Double2(nodes[startFromIndex]);
            Double2 max = new Double2(nodes[startFromIndex]);

            for (int i = startFromIndex; i < nodes.Count; i++)
            {
                if (skipLonelyNodes && graphManager.Graph.CalculateDegree(i) == 0) continue;
                if (nodes[i].X < min.X) min.X = nodes[i].X;
                if (nodes[i].Y < min.Y) min.Y = nodes[i].Y;
                if (nodes[i].X > max.X) max.X = nodes[i].X;
                if (nodes[i].Y > max.Y) max.Y = nodes[i].Y;
            }

            graphScaling = Math.Max(
                (max.X - min.X) / (textureGraph.Width  - padding * 2 - 1), 
                (max.Y - min.Y) / (textureGraph.Height - padding * 2 - 1));

            if (graphScaling == 0) graphScaling = 1;

            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i] -= (max + min) / 2;
                nodes[i] = nodes[i] / graphScaling;
                nodes[i] += new Double2(textureGraph.Width / 2, textureGraph.Height / 2);
            }
        }

        private bool IsNodeHighlighted(int nodeID, List<int> highlightedNodeIDs) => nodeID == selectedNodeID || highlightedNodeIDs.Contains(nodeID);
        private bool IsColumnHighlighted(int columnID, List<int> highlightedColumnIDs) => columnID == selectedColumnID || highlightedColumnIDs.Contains(columnID);

        public void DrawGraph(bool scaleGraph = true)
        {
            if (graphManager.Graph == null) return;
            if (graphManager.Graph.NeighbourMatrix == null) return;
            if (textureGraph.Width <= GetGraphPadding() * 2 + 1) return;

            Font font = new Font(FONT, 10);
            SolidBrush brush = new SolidBrush(Colors.main);
            SolidBrush highlightBrush = new SolidBrush(Colors.highlight);
            Pen pen = new Pen(Colors.main);
            Pen highlightPen = new Pen(Colors.highlight);

            graphDrawerGraphics.Clear(Colors.background);

            int radius = Math.Min(textureGraph.Width, textureGraph.Height) / 2 - 22;
            
            if (scaleGraph)
            {
                scaledPoints = new List<Double2>(graphManager.Points);
                FitToCanvas(scaledPoints, GetGraphPadding(), Options.forceDirectedArrangement && !Options.generateSamples);
            }

            List<int> highlightedNodeIDs = GetHighlightedNodeIDs(GetChartValues());
            
            // Draw texts
            if (Options.showDegree && (!Options.forceDirectedArrangement || Options.generateSamples))
            {
                List<Double2> textPositions = graphManager.GetCircularArrangement(graphManager.Graph.NodeCount, radius + 15, new Double2(textureGraph.Width / 2, textureGraph.Height / 2));
                FitToCanvas(textPositions, 7, Options.forceDirectedArrangement && !Options.generateSamples);
                for (int i = 0; i < graphManager.Graph.NodeCount; i++)
                {
                    bool isHighlighted = IsNodeHighlighted(i, highlightedNodeIDs);
                    graphDrawerGraphics.DrawString(graphManager.Graph.CalculateDegree(i).ToString(), font, isHighlighted ? highlightBrush : brush, 
                        (int)textPositions[i].X - (graphManager.Graph.CalculateDegree(i) < 10 ? 5 : 9), 
                        (int)textPositions[i].Y - 8);
                }
            }

            // Draw edges
            for (int nodeA = 0; nodeA < scaledPoints.Count - 1; nodeA++)
            {
                for (int nodeB = nodeA + 1; nodeB < scaledPoints.Count; nodeB++)
                {
                    try
                    {
                        if (graphManager.Graph.NeighbourMatrix[nodeA][nodeB])
                        {
                            Point positionA = new Point((int)scaledPoints[nodeA].X, (int)scaledPoints[nodeA].Y);
                            Point positionB = new Point((int)scaledPoints[nodeB].X, (int)scaledPoints[nodeB].Y);

                            bool isNodeAHighlighted = IsNodeHighlighted(nodeA, highlightedNodeIDs);
                            bool isNodeBHighlighted = IsNodeHighlighted(nodeB, highlightedNodeIDs);

                            LinearGradientBrush gradBrush = new LinearGradientBrush(
                                new Point(positionA.X + (positionB.X > positionA.X ? -1 :  1), positionA.Y + (positionB.Y > positionA.Y ? -1 :  1)),
                                new Point(positionB.X + (positionB.X > positionA.X ?  1 : -1), positionB.Y + (positionB.Y > positionA.Y ?  1 : -1)),
                                Color.FromArgb(
                                    Options.gradient ? (int)(255.0 * Math.Pow((double)graphManager.Graph.CalculateDegree(nodeA) / graphManager.Graph.MaxDegree, 2.5)) : 255,
                                    isNodeAHighlighted ? highlightPen.Color.R : pen.Color.R,
                                    isNodeAHighlighted ? highlightPen.Color.G : pen.Color.G,
                                    isNodeAHighlighted ? highlightPen.Color.B : pen.Color.B),
                                Color.FromArgb(
                                    Options.gradient ? (int)(255.0 * Math.Pow((double)graphManager.Graph.CalculateDegree(nodeB) / graphManager.Graph.MaxDegree, 2.5)) : 255,
                                    isNodeBHighlighted ? highlightPen.Color.R : pen.Color.R,
                                    isNodeBHighlighted ? highlightPen.Color.G : pen.Color.G,
                                    isNodeBHighlighted ? highlightPen.Color.B : pen.Color.B)
                                );
                            Pen gradPen = new Pen(gradBrush);

                            graphDrawerGraphics.DrawLine(gradPen, positionA.X, positionA.Y, positionB.X, positionB.Y);

                            gradPen.Dispose();
                            gradBrush.Dispose();
                        }
                    }
                    catch
                    {
                        if (graphManager.Graph.NeighbourMatrix.Count == 0) Debug.WriteLine("Neighbour Matrix is empty");
                        Debug.WriteLine($"i: {nodeA}, j: {nodeB}, size: {graphManager.Graph.NeighbourMatrix.Count}x{graphManager.Graph.NeighbourMatrix[0].Count}");
                    }
                }
            }

            // Draw nodes
            if (Options.showNodes)
            {
                for (int i = 0; i < scaledPoints.Count; i++)
                {
                    if (Options.forceDirectedArrangement && !Options.generateSamples && graphManager.Graph.CalculateDegree(i) == 0) continue;

                    int posX = (int)scaledPoints[i].X - NODE_SIZE / 2;
                    int posY = (int)scaledPoints[i].Y - NODE_SIZE / 2;

                    bool isHighlighted = IsNodeHighlighted(i, highlightedNodeIDs);
                    graphDrawerGraphics.FillEllipse(isHighlighted ? highlightBrush : brush, new Rectangle(posX, posY, NODE_SIZE, NODE_SIZE));
                }
            }

            UpdateGraph();

            font.Dispose();
            pen.Dispose();
            brush.Dispose();
            highlightBrush.Dispose();
        }

        private double RoundDouble(double number, int decimals) 
        {
            if (decimals < 0) return 0;
            double multiplier = Math.Pow(10, decimals);
            return Convert.ToInt32(number * multiplier) / multiplier; 
        }

        private int CalculateColumnHeight(List<double> values, int index, double maxValue, int maxOrdinate, int chartHeight)
        {
            return (int)(values[index] / (Options.stretchChart ? Math.Max(maxValue, 1) : maxOrdinate) * chartHeight);
        }

        private List<int> GetHighlightedNodeIDs(List<double> values)
        {
            List<int> highlightedNodeIDs = new List<int>();
            if (selectedColumnID >= 0)
            {
                switch (selectedChartType)
                {
                    case Chart.Types.Node:
                        highlightedNodeIDs.Add(selectedColumnID);
                        break;
                    case Chart.Types.Degree:
                        for (int i = 0; i < graphManager.Graph.NodeCount; i++)
                        {
                            if (graphManager.Graph.CalculateDegree(i) == selectedColumnID)
                            {
                                highlightedNodeIDs.Add(i);
                            }
                        }
                        break;
                    case Chart.Types.AverageDegree:
                        for (int i = 0; i < graphManager.Graph.NodeCount; i++)
                        {
                            if (graphManager.Graph.CalculateDegree(i) == selectedColumnID)
                            {
                                highlightedNodeIDs.Add(i);
                            }
                        }
                        break;
                }
            }
            return highlightedNodeIDs;
        }

        private List<int> GetHighlightedColumnIDs(List<double> values)
        {
            List<int> highlightedColumnIDs = new List<int>();
            if (selectedNodeID >= 0)
            {
                switch (selectedChartType)
                {
                    case Chart.Types.Node:
                        highlightedColumnIDs.Add(selectedNodeID);
                        break;
                    case Chart.Types.Degree:
                        highlightedColumnIDs.Add(graphManager.Graph.CalculateDegree(selectedNodeID));
                        break;
                    case Chart.Types.AverageDegree:
                        highlightedColumnIDs.Add(graphManager.Graph.CalculateDegree(selectedNodeID));
                        break;
                }
            }
            return highlightedColumnIDs;
        }

        private List<double> GetChartValues()
        {
            List<double> values = new List<double>();
            switch (selectedChartType)
            {
                case Chart.Types.Node: values = GetDegreeList(); break;
                case Chart.Types.Degree: values = GetDegreeDistibution(); break;
                case Chart.Types.AverageDegree: values = GetAverageDegreeDistribution(); break;
            }
            return values;
        }

        private int GetMaxOrdinate()
        {
            int maxOrdinate = 0;
            switch (selectedChartType)
            {
                case Chart.Types.Node: maxOrdinate = graphManager.Graph.NodeCount - 1; break;
                case Chart.Types.Degree: maxOrdinate = graphManager.Graph.NodeCount; break;
                case Chart.Types.AverageDegree: maxOrdinate = graphManager.Graph.NodeCount; break;
            }
            return maxOrdinate;
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

            List<double> values = GetChartValues();
            int maxOrdinate = GetMaxOrdinate();

            if (values == null || values.Count == 0)
            {
                UpdateChart();
                return;
            }

            if (Options.sortChart) values.Sort((double a, double b) => a > b ? -1 : 1);

            List<int> highlightedColumnIDs = GetHighlightedColumnIDs(values);
            double maxValue = values.Max();
            double measuredValue = selectedColumnID >= 0 ? values[selectedColumnID] : maxValue;
            int maxValueColumnID = values.FindIndex((n) => n == maxValue);
            int measuredColumnID = selectedColumnID >= 0 ? selectedColumnID : maxValueColumnID;


            SolidBrush highlightBrush = new SolidBrush(Colors.highlight);
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

            double columnFactor = values.Count * CHART_COLUMN_GAP_RATIO;
            double gapFactor = (values.Count - 1) * (1 - CHART_COLUMN_GAP_RATIO);
            double totalWidth = columnFactor + gapFactor;
            double columnWidth = (chartWidth * (columnFactor / totalWidth)) / values.Count;
            double gapWidth = (chartWidth * (gapFactor / totalWidth)) / (values.Count - 1);

            // Draw values

            if (Options.showChartValues)
            {
                Font font = new Font(FONT, 10);
                float[] dashValues = { 4, 4 };
                Pen pen = new Pen(Colors.foreground);
                pen.DashPattern = dashValues;

                int measuredColumnHeight = CalculateColumnHeight(values, measuredColumnID, maxValue, maxOrdinate, chartHeight);
                int linePosY = chartHeight - measuredColumnHeight;

                chartDrawerGraphics.DrawLine(pen, 0, linePosY, panelChart.Width, linePosY);

                string tMeasuredValue = measuredValue != RoundDouble(measuredValue, 0)
                    ? tMeasuredValue = RoundDouble(measuredValue, 1).ToString("0.0")
                    : tMeasuredValue = RoundDouble(measuredValue, 0).ToString();

                chartDrawerGraphics.DrawString(tMeasuredValue, font, brush, 0, chartHeight - linePosY > 17 ? linePosY : linePosY - 17);

                if (!Options.sortChart && (selectedChartType == Chart.Types.Degree || selectedChartType == Chart.Types.AverageDegree))
                {
                    double textX = startX;
                    for (int i = 0; i < values.Count * 2 - 1; i++)
                    {
                        double w = i % 2 == 0 ? columnWidth : gapWidth;

                        if (i % 2 == 0)
                        {
                            int columnID = i / 2;

                            if (values.Count >= 30 && columnID == values.Count - 1)
                            {
                                // This text would not fit entirely inside the panel if displayed, so do not draw it
                                break;
                            }

                            if (columnID % (int)Math.Sqrt(values.Count) == 0)
                            {
                                bool isHighlighted = IsColumnHighlighted(columnID, highlightedColumnIDs);
                                chartDrawerGraphics.DrawString(
                                    columnID.ToString(), 
                                    font, isHighlighted ? highlightBrush : brush,
                                    (int)(textX + columnWidth / 2 - (columnID < 10 ? 5 : 9.5)), 
                                    chartHeight + 1);
                            }
                        }

                        textX += w;
                    }
                }

                font.Dispose();
                pen.Dispose();
            }

            // Draw columns

            chartColumnsHitboxes.Clear();
            double x = startX;
            for (int i = 0; i < values.Count * 2 - 1; i++)
            {
                double width = i % 2 == 0 ? columnWidth : gapWidth;

                if (i % 2 == 0)
                {
                    int columnID = i / 2;

                    int height = CalculateColumnHeight(values, columnID, maxValue, maxOrdinate, chartHeight);
                    int y = chartHeight - height;

                    bool isHighlighted = IsColumnHighlighted(columnID, highlightedColumnIDs);
                    Rectangle rectangle = new Rectangle((int)x, y, (int)width, height);
                    chartDrawerGraphics.FillRectangle(isHighlighted ? highlightBrush : brush, rectangle);
                    
                    Rectangle hitbox = new Rectangle();
                    hitbox.Height = panelChart.Height - 1;
                    hitbox.Y = 0;
                    hitbox.X = (int)(columnID > 0 ? x - gapWidth * 0.5 : x);
                    hitbox.Width = (int)(columnWidth + ((columnID > 0 ? 0.5 : 0.0) + (columnID < values.Count - 1 ? 0.5 : 0.0)) * gapWidth) + 1;

                    chartColumnsHitboxes.Add(hitbox);
                }

                x += width;
            }

            brush.Dispose();
            highlightBrush.Dispose();

            UpdateChart();
        }

        private List<double> GetDegreeList()
        {
            List<double> degrees = new List<double>();
            for (int i = 0; i < graphManager.Graph.NodeCount; i++)
                degrees.Add(graphManager.Graph.CalculateDegree(i));

            return degrees;
        }

        private List<double> GetDegreeDistibution()
        {
            List<double> degreeList = new List<double>(new double[graphManager.Graph.NodeCount]);
            for (int i = 0; i < graphManager.Graph.NodeCount; i++)
            {
                int degree = graphManager.Graph.CalculateDegree(i);
                degreeList[degree]++;
            }

            return degreeList;
        }

        private List<double> GetAverageDegreeDistribution()
        {
            return graphManager.AverageDegreeDistribution;
        }

        

        private void bStretch_Click(object sender, EventArgs e)
        {
            Options.stretchChart = !Options.stretchChart;
            bStretch.BackColor = GetToggleButtonColor(Options.stretchChart);
            DrawChart();
        }

        private void bGradient_Click(object sender, EventArgs e)
        {
            Options.gradient = !Options.gradient;
            bGradient.BackColor = GetToggleButtonColor(Options.gradient);
            DrawGraph();
        }

        private void bShowDegree_Click(object sender, EventArgs e)
        {
            Options.showDegree = !Options.showDegree;
            bShowDegree.BackColor = GetToggleButtonColor(Options.showDegree);
            DrawGraph();
        }

        private void bSort_Click(object sender, EventArgs e)
        {
            Options.sortChart = !Options.sortChart;
            bSort.BackColor = GetToggleButtonColor(Options.sortChart);
            DrawChart();
        }

        private void bShowNodes_Click(object sender, EventArgs e)
        {
            Options.showNodes = !Options.showNodes;
            bShowNodes.BackColor = GetToggleButtonColor(Options.showNodes);
            DrawGraph();
        }

        private void bShowValues_Click(object sender, EventArgs e)
        {
            Options.showChartValues = !Options.showChartValues;
            bShowValues.BackColor = GetToggleButtonColor(Options.showChartValues);
            DrawChart();
        }

        private void bArrangement_Click(object sender, EventArgs e)
        {
            Options.forceDirectedArrangement = !Options.forceDirectedArrangement;
            bArrangement.BackColor = GetToggleButtonColor(Options.forceDirectedArrangement);
            bPauseArrangement.BackColor = GetToggleButtonColor(Options.forceDirectedArrangement);
            bResetArrangement.BackColor = GetToggleButtonColor(Options.forceDirectedArrangement);
            bPauseArrangement.BackgroundImage = Properties.Resources.pause;
            Options.pauseForceDirectedArrangement = false;
            //timer.Enabled = forceDirectedArrangement;
            ArrangePoints();
            DrawGraph();
        }

        private void bPauseArrangement_Click(object sender, EventArgs e)
        {
            if (!Options.forceDirectedArrangement) return;

            Options.pauseForceDirectedArrangement = !Options.pauseForceDirectedArrangement;

            bPauseArrangement.BackColor = GetToggleButtonColor(!Options.pauseForceDirectedArrangement);
            bPauseArrangement.BackgroundImage = Options.pauseForceDirectedArrangement
                ? Properties.Resources.play
                : Properties.Resources.pause;
        }

        private void bResetArrangement_Click(object sender, EventArgs e)
        {
            ArrangePoints();
            DrawGraph();
        }

        private void bGenerateSamples_Click(object sender, EventArgs e)
        {
            Options.generateSamples = !Options.generateSamples;
            bGenerateSamples.BackColor = GetToggleButtonColor(Options.generateSamples);
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

        Color GetToggleButtonColor(bool value)
        {
            return value ? Colors.darkGrey : Colors.extrasBackground;
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

                UpdateGraphicalUI();
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

        private bool isInsideRect(Double2 position, Rectangle rectangle)
        {
            return rectangle.X <= position.X && position.X < rectangle.X + rectangle.Width
                && rectangle.Y <= position.Y && position.Y < rectangle.Y + rectangle.Height;
        }

        private int GetSelectedColumnID(Double2 mouse)
        {
            if (chartColumnsHitboxes == null) return -1;

            for (int i = 0; i < chartColumnsHitboxes.Count; i++)
            {
                if (isInsideRect(mouse, chartColumnsHitboxes[i])) return i;
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

            if (DraggingNode())
            {
                scaledPoints[selectedNodeID] = mousePosition;
            }

            if (!AutoDrawingIsOn()) DrawGraphicalUI(false);
        }

        private void panelGraph_MouseLeave(object sender, EventArgs e)
        {
            hoveredPanel = Panels.None;
            mouseButtonPressed = MouseButtons.None;
            selectedNodeID = -1;

            if (!AutoDrawingIsOn()) DrawGraphicalUI(false);
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
                        DrawGraphicalUI();
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

            if (!AutoDrawingIsOn()) DrawGraphicalUI();
        }
        
        private void panelChart_MouseLeave(object sender, EventArgs e)
        {
            hoveredPanel = Panels.None;
            mouseButtonPressed = MouseButtons.None;
            selectedColumnID = -1;

            if (!AutoDrawingIsOn()) DrawGraphicalUI();
        }        
        
        private void panelChart_MouseDown(object sender, MouseEventArgs e)
        {
            mouseButtonPressed = e.Button;
        }

        private void panelChart_MouseUp(object sender, MouseEventArgs e)
        {
            mouseButtonPressed = MouseButtons.None;
        }

        private bool AutoDrawingIsOn() => Options.generateSamples || (Options.forceDirectedArrangement && !DraggingNode());
        private bool DraggingNode() => mouseButtonPressed == MouseButtons.Left && selectedNodeID >= 0;

        private void bResetSamples_Click(object sender, EventArgs e)
        {
            graphManager.ResetDistributionSamples();
            FillStatistics();
            DrawChart();
        }
    }
}
