using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Graphs_Framework
{
    public class GraphManager 
    {
        private const double IDEAL_SPRING_LENGTH = 10.0;
        double cooling = 1;
        int arrangementStep = 1;
        
        private Graph prevGeneratedGraph;
        private Graph currGeneratedGraph;
        private Graph graph;
        public Graph Graph
        {
            get { return graph; }
        }

        private List<Double2> points;
        public List<Double2> Points
        {
            get { return points; }
        }

        private List<double> averageDegreeDistribution;
        public List<double> AverageDegreeDistribution
        {
            get { return new List<double>(averageDegreeDistribution); }
        }

        private int sampleCount = 0;
        public int AverageSamples
        {
            get { return sampleCount; }
        }


        
        public GraphManager() {
            averageDegreeDistribution = new List<double>();
            points = new List<Double2>();
        }

        public void GenerateGraph(Graph.Types type, int nodes, double probability, double power)
        {
            prevGeneratedGraph = currGeneratedGraph;
            graph = Graph.GenerateGraph(type, nodes, probability, power);
            currGeneratedGraph = graph.Clone();

            if (GraphParametersChanged())
            {
                // start over counting the average degree distribution
                averageDegreeDistribution = new List<double>(new double[graph.NodeCount]);
                sampleCount = 0;
            }

            UpdateAverageDegreeDistribution();
        }

        /*
         * Compare the current and previous graph's generating parameters
         */
        private bool GraphParametersChanged()
        {
            if (prevGeneratedGraph == null) return true;
            return currGeneratedGraph.NodeCount != prevGeneratedGraph.NodeCount
                || currGeneratedGraph.Probability != prevGeneratedGraph.Probability
                || currGeneratedGraph.Power != prevGeneratedGraph.Power
                || currGeneratedGraph.Type != prevGeneratedGraph.Type;
        }

        private void UpdateAverageDegreeDistribution()
        {
            if (averageDegreeDistribution == null) return;

            sampleCount++;
            for (int i = 0; i < averageDegreeDistribution.Count; i++)
            {
                averageDegreeDistribution[i] *= (double)(sampleCount - 1) / (double)sampleCount;
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
            arrangementStep = 1;
            cooling = 1;
            ArrangeInCircle(1);
        }

        public void AdvanceForceDirectedArrangement() => AdvanceForceDirectedArrangement(new List<int> {});
        public void AdvanceForceDirectedArrangement(int fixedPoint) => AdvanceForceDirectedArrangement(new List<int> { fixedPoint });
        public void AdvanceForceDirectedArrangement(List<int> fixedPoints) 
        {
            if (graph == null) return;

            double maxF = 0;
            double coolingFactor = 1;
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

                    // Repulsive force
                    Double2 repulsive = IDEAL_SPRING_LENGTH / Math.Pow(distance, 1.5) * awayFromOther;

                    // Attractive (spring) force
                    Double2 attractive = new Double2();

                    if (graph.HasEdge(node, other))
                    {
                        attractive = distance / Math.Pow(IDEAL_SPRING_LENGTH, 1.5) * towardsOther;
                    }

                    Double2 force = repulsive + attractive;

                    if (force.Length() > maxF) maxF = force.Length();

                    forces[node] += force;
                }
            }

            // Applying force to nodes
            for (int i = 0; i < graph.NodeCount; i++) 
            {
                if (fixedPoints.Contains(i)) continue;
                points[i] += cooling * forces[i];
            }

            cooling *= coolingFactor;
            arrangementStep++;
        }


        public void TranslateNode(int i, Double2 t)
        {
            points[i] += t;
        }

        public void RemoveNode(int i)
        {
            points.RemoveAt(i);
            graph.RemoveNode(i);
        }

        public void RemoveEdge(int node1, int node2)
        {
            graph.RemoveEdge(node1, node2);
        }

        public void AddNode(Double2 at)
        {
            points.Add(at);
            graph.AddNode();
        }
    }
}
