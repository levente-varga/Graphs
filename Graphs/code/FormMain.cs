using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;

namespace Graphs
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

        private GraphManager SelectedGraphManager { get => graphManagers[selectedTab]; }

        int selectedTab = 0;
        List<GraphManager> graphManagers = new List<GraphManager>();
        List<Button> tabButtons = new List<Button>();
        List<Button> chartButtons = new List<Button>();

        Graphics graphPanelGraphics;
        Graphics chartPanelGraphics;
        Graphics graphDrawerGraphics;
        Graphics chartDrawerGraphics;
        Graphics graphPanelDrawerGraphics;

        List<Double2> scaledPoints = new List<Double2>();
        Double2 mousePosition;
        MouseButtons mouseButtonPressed;
        double graphScaling = 1;
        Double2 graphOffset = 0;

        int selectedNodeID = -1;
        int selectedColumnID = -1;
        int connectionNodeID = -1;
        int newNodeID = -1;
        Double2 selectedNodeOrigin;

        List<Rectangle> chartColumnsHitboxes = new List<Rectangle>();

        Bitmap textureGraph;
        Bitmap textureChart;
        Bitmap textureGraphPanel;

        SolidBrush brushMain = new SolidBrush(Colors.main);
        SolidBrush brushChartDegree = new SolidBrush(Colors.main);
        SolidBrush brushChartDistribution = new SolidBrush(Colors.blue);
        SolidBrush brushChartAverageDistribution = new SolidBrush(Colors.orange);
        SolidBrush brushHighlight = new SolidBrush(Colors.highlight);
        Pen penMain = new Pen(Colors.main);
        Pen penEraser = new Pen(Colors.nothing);
        Pen penHighlight = new Pen(Colors.highlight);
        Pen penChartText = new Pen(Colors.foreground);

        Font valuesFont = new Font("Segoe UI", 10);

        FormWindowState lastWindowState;

        private static double CHART_COLUMN_GAP_RATIO = 0.6;
        private static int CHART_VERTICAL_VALUES_WIDTH = 20;
        private static int CHART_HORIZONTAL_VALUES_HEIGHT = 17;
        private static int NODE_SIZE = 8;

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

            float[] dashValues = { 4, 4 };
            penChartText.DashPattern = dashValues;

            Double2 p1 = new Double2(5, 4);
            Double2 p2 = new Double2(0, 0);
            lMaxF.Text = p1.Normalize().DistanceFrom(new Double2(0, 0)).ToString();

            graphManagers.Add(new GraphManager(new ErdosRenyiGraph()));
            graphManagers.Add(new GraphManager(new BarabasiAlbertGraph()));
            graphManagers.Add(new GraphManager(new WattsStrogatzGraph()));
            graphManagers.Add(new GraphManager(new BianconiBarabasiGraph()));

            tabButtons.Add(bTabErdosRenyi);
            tabButtons.Add(bTabBarabasiAlbert);
            tabButtons.Add(bTabWattsStrogatz);
            tabButtons.Add(bTabBianconiBarabasi);
            
            chartButtons.Add(bChartDegrees);
            chartButtons.Add(bChartDegreeDistribution);
            chartButtons.Add(bChartAverageDegreeDistribution);

            foreach (GraphManager manager in graphManagers) 
            { 
                manager.parameterChanged += GenerateGraph; 
            }

            SelectGraphPage(0);

            SetupUI();

            DrawUI();
        }
        
        private void OnGenerateRequested()
        {
            if (Options.autoGenerateOnChange)
            {
                GenerateGraph();
            }
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
            SetupToggleButtons();
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
            bAutoGenerateOnChange.BackColor = GetToggleButtonColor(Options.autoGenerateOnChange);
        }

        private void Update(object sender, EventArgs e)
        {
            if (SelectedGraphManager.Graph == null) return;

            if (Options.generateSamples)
            {
                GenerateGraph();
            } 
            else if (Options.forceDirectedArrangement)
            {
                if (FreezeArrangement()) return;

                if (!Options.pauseForceDirectedArrangement)
                {
                    SelectedGraphManager.AdvanceForceDirectedArrangement();
                }

                if (hoveredPanel == Panels.Graph)
                {
                    selectedNodeID = GetSelectedNodeID(mousePosition);
                }

                DrawGraphicalUI();
            }
        }


        private void SelectGraphPage(int pageNumber)
        {
            selectedTab = pageNumber;
            
            Colors.UpdateMainColor(SelectedGraphManager.Graph.Theme());
            ChangeBrushesAndPens(Colors.main);

            UpdateTabButtons();
            UpdateChartButtons();
            bGenerateGraph.BackColor = Colors.main;
            bGenerateGraph.Update();
            pProgressBar.BackColor = Colors.main;
            pProgressBar.Update();

            foreach (GraphManager graphManager in graphManagers)
            {
                graphManager.Graph.RemoveParameterEditorsFromControl(panelParameters);
            }

            SelectedGraphManager.Graph.AddParameterEditorsToControl(panelParameters, new Point(30, 30));

            scaledPoints.Clear();

            Debug.WriteLine(SelectedGraphManager.Graph);
            
            if (SelectedGraphManager.SampleCount == 0) GenerateGraph();

            DrawGraphicalUI();
        }

        private void ChangeBrushesAndPens(Color color)
        {
            brushMain.Dispose();
            brushChartDegree.Dispose();
            penMain.Dispose();

            brushMain = new SolidBrush(color);
            brushChartDegree = new SolidBrush(color);
            penMain = new Pen(color);
        }

        private void buttonTabErdosRenyi_Click(object sender, EventArgs e) => SelectGraphPage(0);
        private void buttonTabBarabasiAlbert_Click(object sender, EventArgs e) => SelectGraphPage(1);
        private void buttonTabWattsStrogatz_Click(object sender, EventArgs e) => SelectGraphPage(2);
        private void buttonTabBianconiBarabasi_Click(object sender, EventArgs e) => SelectGraphPage(3);

        private void bGenerateGraph_Click(object sender, EventArgs e)
        {
            GenerateGraph();
        }

        private void GenerateGraph() 
        {
            SelectedGraphManager.GenerateGraph();

            Colors.UpdateMainColor(SelectedGraphManager.Graph.Theme());

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
            CompositingMode oldCMForGD  = graphDrawerGraphics.CompositingMode;
            CompositingMode oldCMForGPD = graphPanelDrawerGraphics.CompositingMode;

            graphPanelDrawerGraphics.CompositingMode = CompositingMode.SourceCopy;
            graphPanelDrawerGraphics.Clear(Colors.background);
            graphPanelDrawerGraphics.DrawImage(textureGraph, textureGraphPanel.Width / 2 - textureGraph.Width / 2, textureGraphPanel.Height / 2 - textureGraph.Height / 2);
            graphPanelGraphics.DrawImage(textureGraphPanel, 0, 0);

            graphPanelDrawerGraphics.CompositingMode = oldCMForGPD;
            graphDrawerGraphics.CompositingMode = oldCMForGD;
        }

        private void UpdateChart()
        {
            chartPanelGraphics.DrawImage(textureChart, 0, 0);
        }

        private void FillStatistics()
        {
            if (SelectedGraphManager.Graph == null) return;

            int maxEdgeCount = SelectedGraphManager.Graph.NodeCount * (SelectedGraphManager.Graph.NodeCount - 1) / 2;
            lAverageDegreeValue.Text = SelectedGraphManager.Graph.CalculateAverageDegree().ToString("0.00");
            lAverageDegreeSamples.Text = SelectedGraphManager.SampleCount.ToString();
            lEdgeCount.Text = SelectedGraphManager.Graph.EdgeCount.ToString() + "/" + maxEdgeCount + "  [" + 
                (maxEdgeCount != 0 
                ? ((double)SelectedGraphManager.Graph.EdgeCount / maxEdgeCount * 100).ToString("0.0") + "%"
                : "-")   
                + "]";

            lAverageDegreeSamples.Refresh();
            lAverageDegreeValue.Refresh();
            lEdgeCount.Refresh();
        }

        private void ArrangePoints()
        {
            if (SelectedGraphManager.Graph == null) return;
            if (SelectedGraphManager.Graph.NeighbourMatrix == null) return;

            int radius = Math.Min(textureGraph.Width, textureGraph.Height) / 2 - 22;
            SelectedGraphManager.ArrangeInCircle(radius, new Double2(textureGraph.Width / 2, textureGraph.Height / 2));

            if (Options.forceDirectedArrangement)
            {
                SelectedGraphManager.ResetForceDirectedArrangement();
                timer.Enabled = true;
                timer.Start();
            }
        }

        private int GetGraphPadding() => Options.forceDirectedArrangement & !Options.generateSamples || !Options.showDegree ? NODE_SIZE / 2 : 22;

        private void FitToCanvas(List<Double2> nodes, int padding = 0, bool skipLonelyNodes = false)
        {
            if (nodes == null || nodes.Count == 0) return;

            int startFromIndex = 0;

            if (skipLonelyNodes)
            {
                while (startFromIndex < nodes.Count && SelectedGraphManager.Graph.CalculateDegree(startFromIndex) == 0)
                {
                    startFromIndex++;
                }
                if (startFromIndex == nodes.Count) return;
            }

            Double2 min = new Double2(nodes[startFromIndex]);
            Double2 max = new Double2(nodes[startFromIndex]);

            for (int i = startFromIndex; i < nodes.Count; i++)
            {
                if (skipLonelyNodes && SelectedGraphManager.Graph.CalculateDegree(i) == 0) continue;
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

            //graphOffset = new Double2(textureGraph.Width / 2, textureGraph.Height / 2) - (max + min) / 2;
        }

        private bool IsNodeHighlighted(int nodeID, List<int> highlightedNodeIDs) => highlightedNodeIDs.Contains(nodeID);
        private bool IsColumnHighlighted(int columnID, List<int> highlightedColumnIDs) => columnID == selectedColumnID || highlightedColumnIDs.Contains(columnID);

        private Point NodePosition(int node) => new Point((int)scaledPoints[node].X, (int)scaledPoints[node].Y);

        private void DrawDashedEdge(int nodeA, int nodeB) => DrawDashedEdge(
                new Point((int)scaledPoints[nodeA].X, (int)scaledPoints[nodeA].Y),
                new Point((int)scaledPoints[nodeB].X, (int)scaledPoints[nodeB].Y));
        private void DrawDashedEdge(int node, Point position) => DrawDashedEdge(
                new Point((int)scaledPoints[node].X, (int)scaledPoints[node].Y),
                position);
        private void DrawDashedEdge(Point positionA, Point positionB)
        {

        }



        private void DrawEdge(int nodeA, int nodeB, bool highlightA = false, bool highlightB = false) => DrawEdge(nodeA, nodeB, penMain, highlightA, highlightB);
        private void DrawEdge(int nodeA, int nodeB, Pen pen, bool highlightA, bool highlightB)
        {
            Point positionA = NodePosition(nodeA);
            Point positionB = NodePosition(nodeB);

            double degreeRatioA = (double)SelectedGraphManager.Graph.CalculateDegree(nodeA) / SelectedGraphManager.Graph.MaxDegree;
            double degreeRatioB = (double)SelectedGraphManager.Graph.CalculateDegree(nodeB) / SelectedGraphManager.Graph.MaxDegree;

            int alphaA = Options.gradient ? (int)(255.0 * Math.Pow(degreeRatioA, 2.5)) : 255;
            int alphaB = Options.gradient ? (int)(255.0 * Math.Pow(degreeRatioB, 2.5)) : 255;

            DrawEdge(positionA, positionB, pen, highlightA, highlightB, alphaA, alphaB);
        }

        private void DrawEdge(Point positionA, Point positionB, Pen pen, bool highlightA = false, bool highlightB = false, int alphaA = 255, int alphaB = 255)
        {
            if (Options.gradient || highlightA || highlightB)
            {
                Color colorA = Color.FromArgb(alphaA, highlightA ? penHighlight.Color : penMain.Color);
                Color colorB = Color.FromArgb(alphaB, highlightB ? penHighlight.Color : penMain.Color);

                Point gradientStart = new Point(positionA.X + (positionB.X > positionA.X ? -1 :  1), positionA.Y + (positionB.Y > positionA.Y ? -1 :  1));
                Point gradientEnd   = new Point(positionB.X + (positionB.X > positionA.X ?  1 : -1), positionB.Y + (positionB.Y > positionA.Y ?  1 : -1));

                LinearGradientBrush gradBrush = new LinearGradientBrush(gradientStart, gradientEnd, colorA, colorB);
                Pen gradPen = new Pen(gradBrush);

                graphDrawerGraphics.DrawLine(gradPen, positionA.X, positionA.Y, positionB.X, positionB.Y);

                gradPen.Dispose();
                gradBrush.Dispose();
            }
            else
            {
                graphDrawerGraphics.DrawLine(penMain, positionA.X, positionA.Y, positionB.X, positionB.Y);
            }
        }

        private void DrawEdgeHighlight(int nodeA, int nodeB, bool highlightA, bool highlightB)
        {
            Point positionA = new Point((int)scaledPoints[nodeA].X, (int)scaledPoints[nodeA].Y);
            Point positionB = new Point((int)scaledPoints[nodeB].X, (int)scaledPoints[nodeB].Y);

            LinearGradientBrush gradBrush = new LinearGradientBrush(
            new Point(positionA.X + (positionB.X > positionA.X ? -1 : 1), positionA.Y + (positionB.Y > positionA.Y ? -1 : 1)),
            new Point(positionB.X + (positionB.X > positionA.X ? 1 : -1), positionB.Y + (positionB.Y > positionA.Y ? 1 : -1)),
            Color.FromArgb(
                highlightA ? 255 : 0,
                penHighlight.Color),
            Color.FromArgb(
                highlightB ? 255 : 0,
                penHighlight.Color)
            );
            Pen gradPen = new Pen(gradBrush);

            graphDrawerGraphics.DrawLine(gradPen, positionA.X, positionA.Y, positionB.X, positionB.Y);

            gradPen.Dispose();
            gradBrush.Dispose();
        }

        /*
        private void DrawGraph()
        {
            switch (Options.graphDisplayMode)
            {
                case Options.GraphDisplayMode.Graph: 
                        break;
                case Options.GraphDisplayMode.Matrix:
                    break;
            }
        }
        */

        public void DrawGraph(bool scaleGraph = true)
        {
            graphDrawerGraphics.Clear(Colors.background);

            if (SelectedGraphManager.Graph == null
                || SelectedGraphManager.Graph.NeighbourMatrix == null
                || textureGraph.Width <= GetGraphPadding() * 2 + 1)
            {
                UpdateGraph();
                return;
            }

            int radius = Math.Min(textureGraph.Width, textureGraph.Height) / 2 - 22;
            
            if (scaleGraph)
            {
                scaledPoints = new List<Double2>(SelectedGraphManager.Points);
                FitToCanvas(scaledPoints, GetGraphPadding(), Options.forceDirectedArrangement && !Options.generateSamples);
            }

            List<int> highlightedNodeIDs = GetHighlightedNodeIDs(GetChartValues());
            
            // Draw texts
            if (Options.showDegree && (!Options.forceDirectedArrangement || Options.generateSamples))
            {
                List<Double2> textPositions = SelectedGraphManager.GetCircularArrangement(SelectedGraphManager.Graph.NodeCount, radius + 15, new Double2(textureGraph.Width / 2, textureGraph.Height / 2));
                FitToCanvas(textPositions, 7, Options.forceDirectedArrangement && !Options.generateSamples);
                for (int i = 0; i < SelectedGraphManager.Graph.NodeCount; i++)
                {
                    bool isHighlighted = IsNodeHighlighted(i, highlightedNodeIDs);
                    graphDrawerGraphics.DrawString(SelectedGraphManager.Graph.CalculateDegree(i).ToString(), valuesFont, isHighlighted ? brushHighlight : brushMain, 
                        (int)textPositions[i].X - (SelectedGraphManager.Graph.CalculateDegree(i) < 10 ? 5 : 9), 
                        (int)textPositions[i].Y - 8);
                }
            }

            // Draw non-highlighted edges first
            for (int nodeA = 0; nodeA < scaledPoints.Count - 1; nodeA++)
            {
                for (int nodeB = nodeA + 1; nodeB < scaledPoints.Count; nodeB++)
                {
                    //if (highlightedNodeIDs.Contains(nodeA) || highlightedNodeIDs.Contains(nodeB)) continue;
                    try
                    {
                        if (SelectedGraphManager.Graph.NeighbourMatrix[nodeA][nodeB])
                        {
                            DrawEdge(nodeA, nodeB, false, false);
                        }
                    }
                    catch
                    {
                        if (SelectedGraphManager.Graph.NeighbourMatrix.Count == 0) Debug.WriteLine("Neighbour Matrix is empty");
                        Debug.WriteLine($"i: {nodeA}, j: {nodeB}, scaledPoints: {scaledPoints.Count}, size: {SelectedGraphManager.Graph.NeighbourMatrix.Count}x{SelectedGraphManager.Graph.NeighbourMatrix[0].Count}");
                    }
                }
            }

            // Draw highlighted edges
            if (highlightedNodeIDs.Count > 0)
            {
                foreach (int nodeA in highlightedNodeIDs)
                {
                    for (int nodeB = 0; nodeB < scaledPoints.Count; nodeB++)
                    {
                        if (highlightedNodeIDs.Contains(nodeB) && nodeB <= nodeA) continue;
                        try
                        {
                            if (SelectedGraphManager.Graph.NeighbourMatrix[nodeA][nodeB])
                            {
                                DrawEdgeHighlight(nodeA, nodeB, highlightedNodeIDs.Contains(nodeA), highlightedNodeIDs.Contains(nodeB));
                            }
                        }
                        catch
                        {
                            if (SelectedGraphManager.Graph.NeighbourMatrix.Count == 0) Debug.WriteLine("Neighbour Matrix is empty");
                            Debug.WriteLine($"i: {nodeA}, j: {nodeB}, scaledPoints: {scaledPoints.Count}, size: {SelectedGraphManager.Graph.NeighbourMatrix.Count}x{SelectedGraphManager.Graph.NeighbourMatrix[0].Count}");
                        }
                    }
                }
            }

            // Draw nodes
            if (Options.showNodes)
            {
                for (int i = 0; i < scaledPoints.Count; i++)
                {
                    if (Options.forceDirectedArrangement && !Options.generateSamples && SelectedGraphManager.Graph.CalculateDegree(i) == 0) continue;

                    int posX = (int)scaledPoints[i].X - NODE_SIZE / 2;
                    int posY = (int)scaledPoints[i].Y - NODE_SIZE / 2;

                    bool isHighlighted = IsNodeHighlighted(i, highlightedNodeIDs);
                    graphDrawerGraphics.FillEllipse(isHighlighted ? brushHighlight : brushMain, new Rectangle(posX, posY, NODE_SIZE, NODE_SIZE));
                }
            }

            UpdateGraph();
        }

        private double RoundDouble(double number, int decimals) 
        {
            if (decimals < 0) return 0;
            double multiplier = Math.Pow(10, decimals);
            return Convert.ToInt32(number * multiplier) / multiplier; 
        }

        private int CalculateColumnHeight(List<double> values, int index, double maxValue, int maxOrdinate, int chartHeight)
        {
            return (int)(values[index] / Math.Max(Options.stretchChart ? maxValue : maxOrdinate, 1) * chartHeight);
        }

        private List<int> GetHighlightedNodeIDs(List<double> values)
        {
            List<int> highlightedNodeIDs = new List<int>();
            if (selectedColumnID >= 0)
            {
                switch (selectedChartType)
                {
                    case Chart.Types.Degrees:
                        highlightedNodeIDs.Add(selectedColumnID);
                        break;
                    case Chart.Types.DegreeDistribution:
                        for (int i = 0; i < SelectedGraphManager.Graph.NodeCount; i++)
                        {
                            if (SelectedGraphManager.Graph.CalculateDegree(i) == selectedColumnID)
                            {
                                highlightedNodeIDs.Add(i);
                            }
                        }
                        break;
                    case Chart.Types.AverageDegreeDistribution:
                        for (int i = 0; i < SelectedGraphManager.Graph.NodeCount; i++)
                        {
                            if (SelectedGraphManager.Graph.CalculateDegree(i) == selectedColumnID)
                            {
                                highlightedNodeIDs.Add(i);
                            }
                        }
                        break;
                }
            }
            if (selectedNodeID >= 0 && !highlightedNodeIDs.Contains(selectedNodeID)) highlightedNodeIDs.Add(selectedNodeID);
            if (connectionNodeID >= 0 && !highlightedNodeIDs.Contains(connectionNodeID)) highlightedNodeIDs.Add(connectionNodeID);
            return highlightedNodeIDs;
        }

        private List<int> GetHighlightedColumnIDs(List<double> values)
        {
            List<int> highlightedColumnIDs = new List<int>();
            if (selectedNodeID >= 0)
            {
                switch (selectedChartType)
                {
                    case Chart.Types.Degrees:
                        highlightedColumnIDs.Add(selectedNodeID);
                        break;
                    case Chart.Types.DegreeDistribution:
                        highlightedColumnIDs.Add(SelectedGraphManager.Graph.CalculateDegree(selectedNodeID));
                        break;
                    case Chart.Types.AverageDegreeDistribution:
                        highlightedColumnIDs.Add(SelectedGraphManager.Graph.CalculateDegree(selectedNodeID));
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
                case Chart.Types.Degrees: values = GetDegreeList(); break;
                case Chart.Types.DegreeDistribution: values = GetDegreeDistibution(); break;
                case Chart.Types.AverageDegreeDistribution: values = GetAverageDegreeDistribution(); break;
            }
            return values;
        }

        private int GetMaxOrdinate()
        {
            int maxOrdinate = 0;
            switch (selectedChartType)
            {
                case Chart.Types.Degrees: maxOrdinate = SelectedGraphManager.Graph.NodeCount - 1; break;
                case Chart.Types.DegreeDistribution: maxOrdinate = SelectedGraphManager.Graph.NodeCount; break;
                case Chart.Types.AverageDegreeDistribution: maxOrdinate = SelectedGraphManager.Graph.NodeCount; break;
            }
            return maxOrdinate;
        }

        private void DrawChart()
        {
            chartDrawerGraphics.Clear(Colors.background);

            if (SelectedGraphManager.Graph == null
                || SelectedGraphManager.Graph.NeighbourMatrix == null
                || !Options.showChart)
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

            Brush brushChart;

            switch (selectedChartType)
            {   
                case Chart.Types.DegreeDistribution: brushChart = new SolidBrush(Colors.blue); break;
                case Chart.Types.AverageDegreeDistribution: brushChart = new SolidBrush(Colors.orange); break;
                case Chart.Types.Degrees:
                default: brushChart = new SolidBrush(Colors.main); break;
            }

            int horizontalOffset = 0;
            switch (selectedChartType)
            {
                case Chart.Types.Degrees: horizontalOffset = 0; break;
                case Chart.Types.DegreeDistribution: horizontalOffset = 0;  break;
                case Chart.Types.AverageDegreeDistribution: horizontalOffset = 10; break;
            }

            int startX = Options.showChartValues 
                ? CHART_VERTICAL_VALUES_WIDTH + horizontalOffset 
                : 0;
            int chartWidth = Options.showChartValues 
                ? panelChart.Width - (CHART_VERTICAL_VALUES_WIDTH + horizontalOffset) 
                : panelChart.Width;
            int chartHeight = (Options.showChartValues && !Options.sortChart && (selectedChartType == Chart.Types.DegreeDistribution || selectedChartType == Chart.Types.AverageDegreeDistribution)) 
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
                int measuredColumnHeight = CalculateColumnHeight(values, measuredColumnID, maxValue, maxOrdinate, chartHeight);
                int linePosY = chartHeight - measuredColumnHeight;

                chartDrawerGraphics.DrawLine(penChartText, 0, linePosY, panelChart.Width, linePosY);

                string tMeasuredValue = measuredValue != RoundDouble(measuredValue, 0)
                    ? tMeasuredValue = RoundDouble(measuredValue, 1).ToString("0.0")
                    : tMeasuredValue = RoundDouble(measuredValue, 0).ToString();

                chartDrawerGraphics.DrawString(tMeasuredValue, valuesFont, brushChart, 0, chartHeight - linePosY > 17 ? linePosY : linePosY - 17);

                if (!Options.sortChart && (selectedChartType == Chart.Types.DegreeDistribution || selectedChartType == Chart.Types.AverageDegreeDistribution))
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
                                    valuesFont, isHighlighted ? brushHighlight : brushChart,
                                    (int)(textX + columnWidth / 2 - (columnID < 10 ? 5 : 9.5)), 
                                    chartHeight + 1);
                            }
                        }

                        textX += w;
                    }
                }
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
                    chartDrawerGraphics.FillRectangle(isHighlighted ? brushHighlight : brushChart, rectangle);
                    
                    Rectangle hitbox = new Rectangle();
                    hitbox.Height = panelChart.Height - 1;
                    hitbox.Y = 0;
                    hitbox.X = (int)(columnID > 0 ? x - gapWidth * 0.5 : x);
                    hitbox.Width = (int)(columnWidth + ((columnID > 0 ? 0.5 : 0.0) + (columnID < values.Count - 1 ? 0.5 : 0.0)) * gapWidth) + 1;

                    chartColumnsHitboxes.Add(hitbox);
                }

                x += width;
            }

            UpdateChart();
        }

        private List<double> GetDegreeList()
        {
            List<double> degrees = new List<double>();
            for (int i = 0; i < SelectedGraphManager.Graph.NodeCount; i++)
                degrees.Add(SelectedGraphManager.Graph.CalculateDegree(i));

            return degrees;
        }

        private List<double> GetDegreeDistibution()
        {
            List<double> degreeList = new List<double>(new double[SelectedGraphManager.Graph.NodeCount]);
            for (int i = 0; i < SelectedGraphManager.Graph.NodeCount; i++)
            {
                int degree = SelectedGraphManager.Graph.CalculateDegree(i);
                degreeList[degree]++;
            }

            return degreeList;
        }

        private List<double> GetAverageDegreeDistribution()
        {
            return SelectedGraphManager.AverageDegreeDistribution;
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

        private void bAutoGenerateOnChange_Click(object sender, EventArgs e)
        {
            Options.autoGenerateOnChange = !Options.autoGenerateOnChange;
            bAutoGenerateOnChange.BackColor = GetToggleButtonColor(Options.autoGenerateOnChange);
        }

        private void UpdateTabButtons()
        {
            tabButtons.ForEach(tabButton =>
            {
                tabButton.BackColor = Colors.background;
                //button.ForeColor = Colors.blackText;
                tabButton.Enabled = true;
            });

            if (Options.showGraph)
            {
                tabButtons[selectedTab].Enabled = false;
                tabButtons[selectedTab].BackColor = Colors.foreground;
                //tabButtons[selectedTab].ForeColor = Colors.whiteText;   
            }

            tabButtons.ForEach(tabButton => { tabButton.Refresh(); });
        }

        private void UpdateChartButtons()
        {
            chartButtons.ForEach(chartButton => { chartButton.BackColor = Colors.background; });

            if (Options.showChart)
            {
                switch (selectedChartType)
                {
                    case Chart.Types.Degrees: bChartDegrees.BackColor = Colors.main; break;
                    case Chart.Types.DegreeDistribution: bChartDegreeDistribution.BackColor = Colors.blue; break;
                    case Chart.Types.AverageDegreeDistribution: bChartAverageDegreeDistribution.BackColor = Colors.orange; break;
                }
            }

            chartButtons.ForEach(chartButtons=> { chartButtons.Refresh(); });
        }

        private void SelectChart(Chart.Types chartType)
        {
            if (selectedChartType == chartType)
            {
                Options.showChart = !Options.showChart;
            }
            else
            {
                Options.showChart = true;
            }
            selectedChartType = chartType;
            UpdateChartButtons();
            DrawChart();
        }

        private void bChartDegrees_Click(object sender, EventArgs e) => SelectChart(Chart.Types.Degrees);
        private void bChartDegreeDistribution_Click(object sender, EventArgs e) => SelectChart(Chart.Types.DegreeDistribution);
        private void bChartAverageDegreeDistribution_Click(object sender, EventArgs e) => SelectChart(Chart.Types.AverageDegreeDistribution);

        Color GetToggleButtonColor(bool value)
        {
            return value ? Colors.darkGrey : Colors.foregroundDark;
        }

        private void Form_SizeChanged(object sender, EventArgs e)
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

        private void Form_ResizeEnd(object sender, EventArgs e)
        {
            SetupGraphics();
            DrawUI();
        }

        private bool IsInsideRect(Double2 position, Rectangle rectangle)
        {
            return rectangle.X <= position.X && position.X < rectangle.X + rectangle.Width
                && rectangle.Y <= position.Y && position.Y < rectangle.Y + rectangle.Height;
        }

        private int GetSelectedColumnID(Double2 mouse)
        {
            if (chartColumnsHitboxes == null) return -1;

            for (int i = 0; i < chartColumnsHitboxes.Count; i++)
            {
                if (IsInsideRect(mouse, chartColumnsHitboxes[i])) return i;
            }

            return -1;
        }

        private int GetSelectedNodeID(Double2 mouse) => GetSelectedNodeID(mouse, new List<int>());
        private int GetSelectedNodeID(Double2 mouse, List<int> exclude)
        {
            if (scaledPoints == null || scaledPoints.Count == 0) return -1;

            double minDistance = scaledPoints[0].DistanceFrom(mouse);
            int id = 0;

            for (int i = 1; i < scaledPoints.Count; i++)
            {
                if (exclude.Contains(i)) continue;

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

            if (mouseButtonPressed == MouseButtons.None)
            {
                selectedNodeID = GetSelectedNodeID(e.Location);
            }

            if (DraggingNode())
            {
                scaledPoints[selectedNodeID] = mousePosition;
            }

            if (AddingEdge())
            {
                scaledPoints[scaledPoints.Count - 1] = mousePosition;
                connectionNodeID = GetSelectedNodeID(mousePosition, new List<int>() { scaledPoints.Count - 1 });
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
                    if (selectedNodeID >= 0)
                    {
                        newNodeID = scaledPoints.Count;
                        scaledPoints.Add(mousePosition);
                        SelectedGraphManager.AddNode(scaledPoints[newNodeID] * graphScaling);
                        SelectedGraphManager.AddEdge(selectedNodeID, newNodeID);
                    }
                    break;
                case MouseButtons.Right:
                    if (selectedNodeID >= 0)
                    {
                        //selectedNodeOrigin = scaledPoints[selectedNodeID];
                    }
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
                        SelectedGraphManager.TranslateNode(selectedNodeID, (scaledPoints[selectedNodeID] - selectedNodeOrigin) * graphScaling);
                    }
                    break;
                case MouseButtons.Middle:
                    if (connectionNodeID >= 0)
                    {
                        SelectedGraphManager.RemoveNode(scaledPoints.Count - 1);
                        scaledPoints.RemoveAt(scaledPoints.Count - 1);
                        
                        if (connectionNodeID != selectedNodeID)
                        {
                            SelectedGraphManager.AddEdge(selectedNodeID, connectionNodeID);
                        }
                        connectionNodeID = -1;
                        newNodeID = -1;
                    }
                    break;
                case MouseButtons.Right:
                    if (selectedNodeID >= 0)
                    {
                        scaledPoints.RemoveAt(selectedNodeID);
                        SelectedGraphManager.RemoveNode(selectedNodeID);
                        selectedNodeID = GetSelectedNodeID(mousePosition);
                        FillStatistics();
                        UpdateChart();
                        DrawGraphicalUI();
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

        private bool AutoDrawingIsOn() => Options.generateSamples || (Options.forceDirectedArrangement && !FreezeArrangement());
        private bool DraggingNode() => mouseButtonPressed == MouseButtons.Left && selectedNodeID >= 0;
        private bool AddingEdge() => mouseButtonPressed == MouseButtons.Middle && selectedNodeID >= 0;
        private bool FreezeArrangement()
        {
            return DraggingNode()
                || AddingEdge();
        }

        private void bResetSamples_Click(object sender, EventArgs e)
        {
            SelectedGraphManager.ResetDistributionSamples();
            FillStatistics();
            DrawChart();
        }
    }
}
