using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Graphs_Framework 
{
    public class GraphManager 
    {
        private Graph lastGraph;
        private const double IDEAL_SPRING_LENGTH = 10.0;
        double cooling = 1;
        int arrangementStep = 1;

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
            lastGraph = graph;
            graph = Graph.GenerateGraph(type, nodes, probability, power);

            if (GraphParametersChanged())
            {
                // az átlagos fokszámeloszlást elölről kezdjük, mivel változotak a gráfgenerálás paraméterei
                averageDegreeDistribution = new List<double>(new double[graph.NodeCount]);
                sampleCount = 0;
            }

            UpdateAverageDegreeDistribution();
        }

        private bool GraphParametersChanged()
        {
            if (lastGraph == null) return true;
            return graph.NodeCount != lastGraph.NodeCount
                || graph.Probability != lastGraph.Probability
                || graph.Power != lastGraph.Power
                || graph.Type != lastGraph.Type;
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



        public List<Double2> GenerateCircularArrangement(double R) => GenerateCircularArrangement(R, new Double2(0, 0));
        public List<Double2> GenerateCircularArrangement(double R, Double2 Origo) 
        {
            points = new List<Double2>();

            for (int i = 0; i < graph.NodeCount; i++) 
            {
                points.Add(new Double2(
                    Origo.X + CalculateCoordinateX(i, graph.NodeCount, R),
                    Origo.Y + CalculateCoordinateY(i, graph.NodeCount, R)));
            }

            return points;
        }

        private static double CalculateCoordinateX(int N, int maxN, double R)
        {
            return Math.Cos(2 * Math.PI * ((double)N / maxN)) * R;
        }

        private static double CalculateCoordinateY(int N, int maxN, double R)
        {
            return Math.Sin(2 * Math.PI * ((double)N / maxN)) * R;
        }



        public void ResetForceDirectedArrangement() 
        {
            arrangementStep = 1;
            cooling = 1;
            GenerateCircularArrangement(1);
        }

        public List<Double2> AdvanceForceDirectedArrangement() 
        {
            if (graph == null) return new List<Double2>();

            double maxF = 0;
            double coolingFactor = 1;
            List<Double2> positions = points;
            List<Double2> forces = new List<Double2>();

            for (int i = 0; i < graph.NodeCount; i++) forces.Add(new Double2());

            for (int node = 0; node < graph.NodeCount; node++) 
            {
                for (int other = 0; other < graph.NodeCount; other++) 
                {
                    if (node == other) continue;
                    //Debug.WriteLine(graph.ToString());
                    Double2 towardsOther = positions[node].DirectionTowards(positions[other]);
                    Double2 awayFromOther = positions[other].DirectionTowards(positions[node]);
                    double distance = positions[node].DistanceFrom(positions[other]);

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
                positions[i] += cooling * forces[i];
            }

            cooling *= coolingFactor;
            arrangementStep++;
            return points;
        }
    }
}
