using System;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Graphs
{
    [SupportedOSPlatform("windows")]
    public class ErdosRenyiGraph : Graph
    {
        private ParameterEditor probabilityEditor;
        private double probability = 0;
        public double Probability { get => probability; }

        public ErdosRenyiGraph() : base()
        {
            name = "Erdos-Renyi graph";
            probabilityEditor = new ParameterEditor(
                name: "Probability",
                minimumValue: 0,
                maximumValue: 1,
                initialValue: 0.5,
                valueStep: 0.01,
                valueResolution: 2
                );
            probabilityEditor.valueChanged += (value) => { OnParameterChanged(); };
        }
        public ErdosRenyiGraph(ErdosRenyiGraph other) : base(other)
        {
            probabilityEditor = new ParameterEditor(other.probabilityEditor);
            probability = other.probability;
        }

        public override void Generate()
        {
            Random random = new Random();
            nodeCount = (int)nodeCountEditor.ValueAndSave;
            probability = probabilityEditor.ValueAndSave;
            edgeCount = 0;
            neighbourMatrix = CreateMatrix(nodeCount);

            for (int node = 0; node < nodeCount - 1; node++)
            {
                for (int other = node + 1; other < nodeCount; other++)
                {
                    if (probability > random.NextDouble())
                    {
                        AddEdge(node, other);
                    }
                }
            }
            maxDegree = CalculateMaxDegree();
        }

        public override Graph Clone()
        {
            return new ErdosRenyiGraph(this);
        }

        public override bool Equals(Graph other)
        {
            return other != null
                && GetType() == other.GetType()
                && probabilityEditor.SavedValue == ((ErdosRenyiGraph)other).probabilityEditor.SavedValue
                && base.Equals(other);
        }

        public override Color Theme() => Colors.yellow;

        public override void AddParameterEditorsToControl(Control control, Point position)
        {
            base.AddParameterEditorsToControl(control, position);
            probabilityEditor.AddToControl(control, position + new Size(0, 41));
        }

        public override void RemoveParameterEditorsFromControl(Control control)
        {
            base.RemoveParameterEditorsFromControl(control);
            probabilityEditor.RemoveFromControl(control);
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
                    $"\t\tProbability: {probabilityEditor.SavedValue}\n" +
                $"\t}}\n" +
                $"}}\n";
        }
    }
}
