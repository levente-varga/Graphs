using System;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Graphs
{
    [SupportedOSPlatform("windows")]
    public class BianconiBarabasiGraph : Graph
    {
        private ParameterEditor powerEditor;
        private double power = 0;
        public double Power { get { return power; } }

        public BianconiBarabasiGraph() : base()
        {
            name = "Barabasi-Albert graph";
            powerEditor = new ParameterEditor(
                name: "Fitness",
                minimumValue: 0,
                maximumValue: 10,
                initialValue: 1,
                valueStep: 0.05,
                valueResolution: 2
                );
            powerEditor.valueChanged += (value) => { OnParameterChanged(); };
        }
        public BianconiBarabasiGraph(BianconiBarabasiGraph other) : base(other)
        {
            powerEditor = new ParameterEditor(other.powerEditor);
            power = other.power;
        }

        public override void Generate()
        {
            Random random = new Random();
            nodeCount = (int)nodeCountEditor.ValueAndSave;
            power = powerEditor.ValueAndSave;
            neighbourMatrix = CreateMatrix(nodeCount);

            int startNode1 = random.Next(0, nodeCount);
            int startNode2 = random.Next(0, nodeCount - 1);
            if (startNode2 >= startNode1) startNode2++;
            if (startNode2 == nodeCount) startNode2 = 0;

            edgeCount = 0;
            AddEdge(startNode1, startNode2);

            for (int i = 0; i < nodeCount; i++)
            {
                if (i == startNode1 || i == startNode2) continue;

                for (int j = 0; j < nodeCount; j++)
                {
                    if (j == i) continue;
                    double probability = (double)CalculateDegree(j) / (edgeCount * 2);
                    probability = Math.Pow(probability, 1 / power);
                    double dice = random.NextDouble();
                    if (dice < probability)
                    {
                        AddEdge(i, j);
                    }
                }
            }
            maxDegree = CalculateMaxDegree();
        }

        public override Graph Clone()
        {
            return new BianconiBarabasiGraph(this);
        }

        public override bool Equals(Graph other)
        {
            return other != null
                && GetType() == other.GetType()
                && powerEditor.SavedValue == ((BianconiBarabasiGraph)other).powerEditor.SavedValue
                && base.Equals(other);
        }

        public override Color Theme() => Colors.purple;

        public override void AddParameterEditorsToControl(Control control, Point position)
        {
            base.AddParameterEditorsToControl(control, position);
            powerEditor.AddToControl(control, position + new Size(0, 41));
        }

        public override void RemoveParameterEditorsFromControl(Control control)
        {
            base.RemoveParameterEditorsFromControl(control);
            powerEditor.RemoveFromControl(control);
        }

        public override string ToString()
        {
            return
                $"BianconiBarabasiGraph: {{\n" +
                $"\tNodes: {nodeCount}, \n" +
                $"\tEdges: {edgeCount}, \n" +
                $"\tMatrix: {neighbourMatrix.Count}x{(NeighbourMatrix.Count == 0 ? "?" : NeighbourMatrix[0].Count.ToString())}, \n" +
                $"\tParameters: {{\n" +
                    $"\t\tNodes: {nodeCountEditor.SavedValue}, \n" +
                    $"\t\tPower: {powerEditor.SavedValue}\n" +
                $"\t}}\n" +
                $"}}\n";
        }
    }
}
