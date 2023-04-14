using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.Versioning;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Graphs
{
    [SupportedOSPlatform("windows")]
    public abstract class Graph
    {
        protected List<List<bool>> neighbourMatrix = new List<List<bool>>();
        public List<List<bool>> NeighbourMatrix
        {
            get { return neighbourMatrix; }
        }

        protected string name = "Graph";
        public string Name { get => name; }

        protected int nodeCount = 0;
        public int NodeCount { get => nodeCount; }

        protected int maxDegree = 0;
        public int MaxDegree { get => maxDegree; }

        protected int edgeCount = 0;
        public int EdgeCount { get => edgeCount; }

        protected ParameterEditor nodeCountEditor;

        protected Random random = new Random();

        public delegate void OnParameterChangedEventHandler();
        public event OnParameterChangedEventHandler parameterChange;

        protected virtual void OnParameterChanged()
        {
            parameterChange?.Invoke();
        }

        public Graph()
        {
            nodeCountEditor = new ParameterEditor(
                name: "Node count",
                minimumValue: 3,
                maximumValue: 100,
                initialValue: 13,
                valueStep: 1,
                valueResolution: 0
                );
            nodeCountEditor.valueChanged += (value) => { OnParameterChanged(); };
        }

        public Graph(Graph other)
        {
            nodeCountEditor = new ParameterEditor(other.nodeCountEditor);
            nodeCount = other.nodeCount;
            maxDegree = other.maxDegree;
            edgeCount = other.edgeCount;
            neighbourMatrix = other.neighbourMatrix == null ? null : new List<List<bool>>(other.neighbourMatrix);
        }

        protected int CalculateMaxDegree()
        {
            int max = 0;
            for (int i = 0; i < nodeCount; i++)
            {
                int degree = CalculateDegree(i);
                if (degree > max) max = degree;
            }
            return max;
        }

        public int CalculateDegree(int n)
        {
            if (n < 0 || neighbourMatrix.Count <= n)
            {
                return 0;
            }
            if (neighbourMatrix[0].Count < nodeCount)
            {
                return 0;
            }

            int degree = 0;
            for (int i = 0; i < nodeCount; i++)
            {
                if (neighbourMatrix[n][i]) degree++;
            }
            return degree;
        }

        public double CalculateAverageDegree()
        {
            if (nodeCount == 0) return 0;

            int totalDegree = 0;
            for (int i = 0; i < nodeCount; i++)
            {
                totalDegree += CalculateDegree(i);
            }
            return (double)totalDegree / nodeCount;
        }

        public int CountEdges()
        {
            int edges = 0;
            for (int i = 0; i < nodeCount - 1; i++)
            {
                for (int j = i + 1; j < nodeCount; j++)
                {
                    if (neighbourMatrix[i][j]) edges++;
                }
            }
            return edges;
        }

        protected List<List<bool>> CreateMatrix(int n)
        {
            List<List<bool>> columns = new List<List<bool>>();
            for (int i = 0; i < n; i++)
            {
                columns.Add(new List<bool>(new bool[n]));
            }
            return columns;
        }

        public void AddEdge(int node1, int node2)
        {
            if (node1 == node2) return;
            if (node1 >= nodeCount || node2 >= nodeCount) return;
            if (neighbourMatrix[node1][node2] || neighbourMatrix[node2][node1]) return;

            neighbourMatrix[node1][node2] = true;
            neighbourMatrix[node2][node1] = true;
            maxDegree = CalculateMaxDegree();
            edgeCount++;
        }

        public void RemoveEdge(int node1, int node2)
        {
            if (node1 == node2) return;
            if (!neighbourMatrix[node1][node2] && !neighbourMatrix[node2][node1]) return;

            neighbourMatrix[node1][node2] = false;
            neighbourMatrix[node2][node1] = false;
            edgeCount--;
        }

        public bool HasEdge(int node1, int node2)
        {
            return neighbourMatrix[node1][node2] || neighbourMatrix[node2][node1];
        }

        public void AddNode()
        {
            for (int i = 0; i < nodeCount; i++)
            {
                neighbourMatrix[i].Add(false);
            }
            nodeCount++;
            neighbourMatrix.Add(new List<bool>(new bool[nodeCount]));
        }

        public void RemoveNode(int node)
        {
            edgeCount -= CalculateDegree(node);
            nodeCount--;

            neighbourMatrix.RemoveAt(node);
            for (int i = 0; i < neighbourMatrix.Count; i++)
            {
                neighbourMatrix[i].RemoveAt(node);
            }

            maxDegree = CalculateMaxDegree();
        }

        public override string ToString()
        {
            return 
                $"Graph: {{\n" +
                $"\tNodes: {nodeCount}, \n" +
                $"\tEdges: {edgeCount}, \n" +
                $"\tMatrix: {neighbourMatrix.Count}x{(NeighbourMatrix.Count == 0 ? "?" : NeighbourMatrix[0].Count.ToString())}, \n" +
                $"\tParameters: {{\n" +
                    $"\t\tNodes: {nodeCountEditor.SavedValue}\n" +
                $"\t}}\n" +
                $"}}\n";
        }

        public abstract void Generate();

        public abstract Graph Clone();

        public abstract Color Theme();

        public virtual void AddParameterEditorsToControl(Control control, Point position)
        {
            nodeCountEditor.AddToControl(control, position);
        }

        public virtual void RemoveParameterEditorsFromControl(Control control)
        {
            nodeCountEditor.RemoveFromControl(control);
        }

        public virtual bool Equals(Graph other)
        {
            return other != null
                && nodeCountEditor.SavedValue == other.nodeCountEditor.SavedValue;
        }
    }
}
