using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Graphs
{
    [SupportedOSPlatform("windows")]
    public class GraphManager 
    {
        public GraphManager() 
        { 
            graph.parameterChange += OnParameterChange;
        }

        public GraphManager(Graph other)
        {
            graph = other;
            graph.parameterChange += OnParameterChange;
        }

        public delegate void OnParameterChangedEventHandler();
        public event OnParameterChangedEventHandler parameterChanged;

        private const double IDEAL_SPRING_LENGTH = 10.0;
        double cooling = 1;
        
        private Graph previousGeneratedGraph;
        private Graph currentGeneratedGraph;

        private Graph graph;
        public Graph Graph
        {
            get => graph;
        }

        private List<Double2> points = new List<Double2>();
        public List<Double2> Points
        {
            get => points;
        }

        private List<double> averageDegreeDistribution = new List<double>();
        public List<double> AverageDegreeDistribution
        {
            get => new List<double>(averageDegreeDistribution);
        }

        private int sampleCount = 0;
        public int SampleCount
        {
            get => sampleCount;
        }

        public void ResetDistributionSamples()
        {
            if (graph == null) return;

            averageDegreeDistribution = new List<double>(new double[graph.NodeCount]);
            sampleCount = 0;
        }

        public void GenerateGraph()
        {
            previousGeneratedGraph = currentGeneratedGraph?.Clone();
            graph.Generate();
            currentGeneratedGraph = graph.Clone();

            if (!currentGeneratedGraph.Equals(previousGeneratedGraph))
            {
                ResetDistributionSamples();
            }

            UpdateAverageDegreeDistribution();
        }

        private void UpdateAverageDegreeDistribution()
        {
            if (averageDegreeDistribution == null) return;

            sampleCount++;
            for (int i = 0; i < averageDegreeDistribution.Count; i++)
            {
                averageDegreeDistribution[i] *= (sampleCount - 1) / (double)sampleCount;
            }
            for (int i = 0; i < graph.NodeCount; i++)
            {
                averageDegreeDistribution[graph.CalculateDegree(i)] += 1.0 / sampleCount;
            }
        }



        public void ArrangeInCircle(double radius) => ArrangeInCircle(radius, 0);
        public void ArrangeInCircle(double radius, Double2 origo) 
        {
            points = GetCircularArrangement(graph.NodeCount, radius, origo);
        }

        public List<Double2> GetCircularArrangement(int n, double radius) => GetCircularArrangement(n, radius, 0);
        public List<Double2> GetCircularArrangement(int n, double radius, Double2 origo)
        {
            List<Double2> points = new List<Double2>();

            for (int i = 0; i < n; i++)
            {
                points.Add(new Double2(origo + GetPositionInCircle(i, graph.NodeCount, radius)));
            }

            return points;
        }

        private Double2 GetPositionInCircle(int i, int total, double radius)
        {
            return new Double2(
                Math.Cos(2 * Math.PI * ((double)i / total)) * radius,
                Math.Sin(2 * Math.PI * ((double)i / total)) * radius);
        }



        public void ResetForceDirectedArrangement() 
        {
            cooling = 1;
            ArrangeInCircle(1);
        }

        public void AdvanceForceDirectedArrangement() => AdvanceForceDirectedArrangement(new List<int> {});
        public void AdvanceForceDirectedArrangement(int fixedPoint) => AdvanceForceDirectedArrangement(new List<int> { fixedPoint });
        public void AdvanceForceDirectedArrangement(List<int> fixedPoints) 
        {
            if (graph == null) return;

            double maxForce = 0;
            const double coolingFactor = 1;
            const double power = 1.5;
            List<Double2> forces = new List<Double2>();

            for (int i = 0; i < graph.NodeCount; i++) forces.Add(new Double2());

            for (int node = 0; node < graph.NodeCount; node++) 
            {
                for (int other = 0; other < graph.NodeCount; other++) 
                {
                    if (node == other) continue;
                    Double2 towardsOther = points[node].DirectionTowards(points[other]);
                    Double2 awayFromOther = points[other].DirectionTowards(points[node]);
                    double distance = points[node].DistanceFrom(points[other]);

                    Double2 repulsiveForce = IDEAL_SPRING_LENGTH / Math.Pow(distance, power) * awayFromOther;
                    Double2 attractiveForce = new Double2();

                    if (graph.HasEdge(node, other))
                    {
                        attractiveForce = distance / Math.Pow(IDEAL_SPRING_LENGTH, power) * towardsOther;
                    }

                    Double2 totalForce = repulsiveForce + attractiveForce;

                    if (totalForce.Length() > maxForce) maxForce = totalForce.Length();

                    forces[node] += totalForce;
                }
            }

            // Applying force to nodes
            for (int i = 0; i < graph.NodeCount; i++) 
            {
                if (fixedPoints.Contains(i)) continue;
                points[i] += cooling * forces[i];
            }

            cooling *= coolingFactor;
        }


        public void TranslateNode(int node, Double2 translate)
        {
            points[node] += translate;
        }

        public void RemoveNode(int node)
        {
            points.RemoveAt(node);
            graph.RemoveNode(node);
        }

        public void RemoveEdge(int node1, int node2)
        {
            graph.RemoveEdge(node1, node2);
        }

        public void AddEdge(int node1, int node2)
        {
            graph.AddEdge(node1, node2);
        }

        public void AddNode(Double2 at)
        {
            points.Add(at);
            graph.AddNode();
        }

        private void OnParameterChange()
        {
            if (Options.autoGenerateOnChange)
            {
                parameterChanged();
            }
        }
    }
}
