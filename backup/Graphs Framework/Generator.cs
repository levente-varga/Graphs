using System;
using System.Collections.Generic;
using System.Text;

namespace Graphs_Framework
{
    class Generator
    {
        private const int MAX_NODES = 100;
        private const int MIN_NODES = 3;
        private const double MAX_POWER = 2.0;
        private const double MIN_POWER = 0.0;
        public const int NODES_INIT = 13;
        public const double PROBABILITY_INIT = 0.5;
        public const double POWER_INIT = 1.0;

        Random random = new Random();

        public enum Graph
        {
            Unknown,
            Random,
            Popularity,
        }

        private Graph lastGraphType;
        private Graph graphType;

        private List<double> averageDegreeDistribution;
        private int averageSamples = 0;

        public int MaxDegree { get { return maxDegree; } }
        private int maxDegree = 0;

        public int Edges { get { return edges; } }
        private int edges = 0;

        public List<List<bool>> NeighbourMatrix { get { return neighbourMatrix; } }
        private List<List<bool>> neighbourMatrix;

        public List<double> AverageDegreeDistribution
        {
            get { return new List<double>(averageDegreeDistribution); }
        }

        public int AverageSamples
        {
            get { return averageSamples; }
        }

        public int NodesToGenerate
        {
            get { return nodesToGenerate; }
            set
            {
                if (value > MAX_NODES)
                    nodesToGenerate = MAX_NODES;
                else if (value < MIN_NODES)
                    nodesToGenerate = MIN_NODES;
                else nodesToGenerate = value;
            }
        }
        private int nodesToGenerate = NODES_INIT;


        public int Nodes
        {
            get { return nodes; }
        }
        private int nodes = 0;
        private int lastNodes;

        public double Probability
        {
            get { return probability; }
            set
            {
                if (value > 1.0)
                    probability = 1;
                else if (value < 0.0)
                    probability = 0;
                else probability = value;
            }
        }
        private double probability = PROBABILITY_INIT;
        private double lastProbability;

        public double Power
        {
            get { return power; }
            set
            {
                if (value > MAX_POWER)
                    power = MAX_POWER;
                else if (value < MIN_POWER)
                    power = MIN_POWER;
                else power = value;
            }
        }
        private double power = POWER_INIT;
        private double lastPower;

        public Generator()
        {
            CheckIfValuesChanged();
        }

        private void CheckIfValuesChanged()
        {
            if (nodes != lastNodes || probability != lastProbability || power != lastPower || graphType != lastGraphType)
            {
                // az átlagos fokszámeloszlást elölről kezdjük, mivel változotak a gráfgenerálás paraméterei
                averageDegreeDistribution = new List<double>(new double[nodesToGenerate]);
                averageSamples = 0;
            }

            UpdateLastparameters();
        }

        private void UpdateLastparameters()
        {
            lastNodes = nodes;
            lastProbability = probability;
            lastPower = power;
            lastGraphType = graphType;
        }

        private void UpdateAverageDegreeDistribution()
        {
            if (averageDegreeDistribution == null) return;

            averageSamples++;
            for (int i = 0; i < averageDegreeDistribution.Count; i++)
            {
                averageDegreeDistribution[i] *= (double)(averageSamples - 1) / (double)averageSamples;
            }
            for (int i = 0; i < nodesToGenerate; i++)
            {
                averageDegreeDistribution[CalculateDegree(i)] += 1.0 / averageSamples;
            }
        }

        public void GenerateRandomGraph()
        {
            nodes = nodesToGenerate;

            int edges = 0;
            neighbourMatrix = CreateMatrix(nodesToGenerate);

            for (int node = 0; node < nodesToGenerate - 1; node++)
            {
                for (int other = node + 1; other < nodesToGenerate; other++)
                {
                    if (probability > random.NextDouble())
                    {
                        AddEdge(node, other);
                        edges++;
                    }
                }
            }
            maxDegree = CalculateMaxDegree();
            this.edges = edges;
            graphType = Graph.Random;
            CheckIfValuesChanged();
            UpdateAverageDegreeDistribution();
        }

        public void GeneratePopularityGraph()
        {
            nodes = nodesToGenerate;

            neighbourMatrix = CreateMatrix(nodesToGenerate);

            int startNode1 = random.Next(0, nodesToGenerate);
            int startNode2 = random.Next(0, nodesToGenerate - 1);
            if (startNode2 >= startNode1) startNode2++;
            if (startNode2 == nodesToGenerate) startNode2 = 0;

            AddEdge(startNode1, startNode2);
            int edges = 1;

            for (int i = 0; i < nodesToGenerate; i++)
            {
                if (i == startNode1 || i == startNode2) continue;

                for (int j = 0; j < nodesToGenerate; j++)
                {
                    if (j == i) continue;
                    double probability = (double)CalculateDegree(j) / (edges * 2);
                    probability = Math.Pow(probability, 1 / power);
                    double dice = random.NextDouble();
                    if (dice < probability)
                    {
                        AddEdge(i, j);
                        edges++;
                    }
                }
            }
            maxDegree = CalculateMaxDegree();
            this.edges = edges;
            graphType = Graph.Popularity;
            CheckIfValuesChanged();
            UpdateAverageDegreeDistribution();
        }

        private int CalculateMaxDegree()
        {
            int max = 0;
            for (int i = 0; i < nodes; i++)
            {
                int degree = CalculateDegree(i);
                if (degree > max) max = degree;
            }
            return max;
        }

        public int CalculateDegree(int n)
        {
            int degree = 0;
            for (int i = 0; i < nodes; i++)
            {
                if (neighbourMatrix[n][i]) degree++;
            }
            return degree;
        }

        public double CalculateAverageDegree()
        {
            int totalDegree = 0;
            for (int i = 0; i < nodes; i++)
            {
                totalDegree += CalculateDegree(i);
            }
            return (double)totalDegree / nodes;
        }

        public int CountEdges()
        {
            int edges = 0;
            for (int i = 0; i < nodes - 1; i++)
            {
                for (int j = i + 1; j < nodes; j++)
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

        private void AddEdge(int node1, int node2)
        {
            if (node1 == node2) return;
            neighbourMatrix[node1][node2] = true;
            neighbourMatrix[node2][node1] = true;
        }
    }
}
