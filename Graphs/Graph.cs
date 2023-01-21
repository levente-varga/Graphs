using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms.VisualStyles;

namespace Graphs_Framework
{
    public class Graph
    {
        public enum Types 
        {
            Unknown,
            Random,
            Popularity,
        }

        private Types type;
        public Types Type 
        { 
            get { return type; } 
        }
        
        private List<List<bool>> neighbourMatrix;
        public List<List<bool>> NeighbourMatrix 
        { 
            get { return neighbourMatrix; }
        }

        private int nodeCount = 0;
        public int NodeCount 
        {
            get { return nodeCount; }
        }

        private int edgeCount = 0;
        public int EdgeCount 
        { 
            get { return edgeCount; }
        }

        private int maxDegree = 0;
        public int MaxDegree 
        {
            get { return maxDegree; }
        }

        private double probability = 0;
        public double Probability
        {
            get { return probability; }
        }

        private double power = 0;
        public double Power
        {
            get { return power; }
        }



        public static Graph GenerateGraph(Types type, int nodes, double probability, double power)
        {
            switch (type)
            {
                case Types.Random:
                    return GenerateRandomGraph(nodes, probability);
                case Types.Popularity:
                    return GeneratePopularityGraph(nodes, power);
            }
            return new Graph();
        }

        private static Graph GenerateRandomGraph(int nodes, double probability)
        {
            Random random = new Random();
            Graph g = new Graph();
            g.nodeCount = nodes;
            g.probability = probability;
            g.edgeCount = 0;
            g.neighbourMatrix = g.CreateMatrix(g.nodeCount);

            for (int node = 0; node < g.nodeCount - 1; node++)
            {
                for (int other = node + 1; other < g.nodeCount; other++)
                {
                    if (probability > random.NextDouble())
                    {
                        g.AddEdge(node, other);
                    }
                }
            }
            g.maxDegree = g.CalculateMaxDegree();
            g.type = Types.Random;

            return g;
        }

        private static Graph GeneratePopularityGraph(int nodes, double power)
        {
            Random random = new Random();
            Graph g = new Graph();
            g.nodeCount = nodes;
            g.power = power;
            g.neighbourMatrix = g.CreateMatrix(g.nodeCount);

            int startNode1 = random.Next(0, g.nodeCount);
            int startNode2 = random.Next(0, g.nodeCount - 1);
            if (startNode2 >= startNode1) startNode2++;
            if (startNode2 == g.nodeCount) startNode2 = 0;

            g.edgeCount = 0;
            g.AddEdge(startNode1, startNode2);

            for (int i = 0; i < g.nodeCount; i++)
            {
                if (i == startNode1 || i == startNode2) continue;

                for (int j = 0; j < g.nodeCount; j++)
                {
                    if (j == i) continue;
                    double probability = (double)g.CalculateDegree(j) / (g.edgeCount * 2);
                    probability = Math.Pow(probability, 1 / power);
                    double dice = random.NextDouble();
                    if (dice < probability)
                    {
                        g.AddEdge(i, j);
                    }
                }
            }

            g.maxDegree = g.CalculateMaxDegree();
            g.type = Types.Popularity;

            return g;
        }



        private int CalculateMaxDegree()
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

        private List<List<bool>> CreateMatrix(int n)
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

        public Graph Clone()
        {
            Graph clone = new Graph();
            clone.neighbourMatrix = neighbourMatrix;
            clone.edgeCount = edgeCount;
            clone.nodeCount = nodeCount;
            clone.maxDegree = maxDegree;
            clone.power = power;
            clone.probability= probability;
            clone.type = type;

            return clone;
        }

        public void AddNode()
        {
            for (int i = 0; i < nodeCount; i++)
            {
                neighbourMatrix[i].Add(false);
            }
            nodeCount++;
            neighbourMatrix.Add(new List<bool>(nodeCount));
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
            return $"[N: {nodeCount}, E: {edgeCount}, M: {neighbourMatrix.Count}x{(NeighbourMatrix.Count == 0 ? "?" : NeighbourMatrix[0].Count.ToString())}]";
        }
    }
}
