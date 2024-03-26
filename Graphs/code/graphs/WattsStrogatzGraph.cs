using System;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Graphs
{
    [SupportedOSPlatform("windows")]
    public class WattsStrogatzGraph : Graph
    {
        private ParameterEditor probabilityEditor;
        private double probability = 0;
        public double Probability { get => probability; }

        private ParameterEditor meanDegreeEditor;
        private int meanDegree = 0;
        public int MeanDegree { get => meanDegree; }

        public WattsStrogatzGraph() : base()
        {
            name = "Watts-Strogatz graph";
            probabilityEditor = new ParameterEditor(
                name: "Probability",
                minimumValue: 0,
                maximumValue: 1,
                initialValue: 0.5,
                valueStep: 0.01,
                valueResolution: 2
                );
            probabilityEditor.valueChanged += (value) => { OnParameterChanged(); };
            meanDegreeEditor = new ParameterEditor(
                name: "Mean degree",
                minimumValue: 0,
                maximumValue: 100,
                initialValue: 2,
                valueStep: 2,
                valueResolution: 0
                );
            meanDegreeEditor.valueChanged += (value) => { OnParameterChanged(); };
        }
        public WattsStrogatzGraph(WattsStrogatzGraph other) : base(other)
        {
            probabilityEditor = new ParameterEditor(other.probabilityEditor);
            meanDegreeEditor = new ParameterEditor(other.meanDegreeEditor);
            meanDegree = other.meanDegree;
            probability = other.probability;
        }

        private void RewriteEdge(int node, int other)
        {
            int degree = CalculateDegree(node);
            if (degree == nodeCount - 1) return;

            RemoveEdge(node, other);

            int nodesToSkip = random.Next(nodeCount - 1 - degree);
            int nodesSkipped = 0;
            for (int potentialNewNode = 0; potentialNewNode < nodeCount; potentialNewNode++)
            {
                if (potentialNewNode == node) continue;

                if (!HasEdge(node, potentialNewNode))
                {
                    if (nodesToSkip == nodesSkipped)
                    {
                        AddEdge(node, potentialNewNode);
                    }
                    nodesSkipped++;
                }
            }
        }

        public override void Generate()
        {
            nodeCount = (int)nodeCountEditor.ValueAndSave;
            probability = probabilityEditor.ValueAndSave;
            meanDegree = (int)meanDegreeEditor.ValueAndSave;
            edgeCount = 0;
            neighbourMatrix = CreateMatrix(nodeCount);

            int modulo = nodeCount - 1 - meanDegree / 2;

            if (modulo == 0) return;

            for (int node = 0; node < NodeCount; node++)
            {
                for (int other = 0; other < NodeCount; other++)
                {
                    int nodeDifference = Math.Abs(node - other) % modulo;
                    if (0 < nodeDifference && nodeDifference <= meanDegree / 2)
                    {
                        AddEdge(node, other);
                    }
                }
            }

            for (int node = 0; node < nodeCount; node++)
            {
                for (int turn = 1; turn <= meanDegree / 2; turn++)
                {
                    int otherNode = (node + turn) % nodeCount;
                    if (probability > random.NextDouble())
                    {
                        RewriteEdge(node, otherNode);
                    }
                }
            }

            maxDegree = CalculateMaxDegree();
        }

        public override Graph Clone()
        {
            return new WattsStrogatzGraph(this);
        }

        public override bool Equals(Graph other)
        {
            return other != null
                && GetType() == other.GetType()
                && probabilityEditor.SavedValue == ((WattsStrogatzGraph)other).probabilityEditor.SavedValue
                && meanDegreeEditor.SavedValue == ((WattsStrogatzGraph)other).meanDegreeEditor.SavedValue
                && base.Equals(other);
        }

        public override Color Theme() => Colors.red;

        public override void AddParameterEditorsToControl(Control control, Point position)
        {
            base.AddParameterEditorsToControl(control, position);
            probabilityEditor.AddToControl(control, position + new Size(0, 41));
            meanDegreeEditor.AddToControl(control, position + new Size(0, 82));
        }

        public override void RemoveParameterEditorsFromControl(Control control)
        {
            base.RemoveParameterEditorsFromControl(control);
            probabilityEditor.RemoveFromControl(control);
            meanDegreeEditor.RemoveFromControl(control);
        }

        public override string ToString()
        {
            return
                $"ErdosRenyiGraph: {{\n" +
                $"\tNodes: {nodeCount}, \n" +
                $"\tEdges: {edgeCount}, \n" +
                $"\tMatrix: {neighbourMatrix.Count}x{(NeighbourMatrix.Count == 0 ? "?" : NeighbourMatrix[0].Count.ToString())}, \n" +
                $"\tParameters: {{\n" +
                    $"\t\tNodes: {nodeCountEditor.SavedValue}, \n" +
                    $"\t\tProbability: {probabilityEditor.SavedValue}, \n" +
                    $"\t\tMeanDegree: {meanDegreeEditor.SavedValue}\n" +
                $"\t}}\n" +
                $"}}\n";
        }
    }
}
