using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Graphs_Framework
{
    class GraphArranger
    {
        public static List<Double2> GetCircularArrangement(int N, double R) => GetCircularArrangement(N, R, new Double2(0, 0));
        public static List<Double2> GetCircularArrangement(int N, double R, Double2 Origo)
        {
            List<Double2> points = new List<Double2>();

            for (int i = 0; i < N; i++)
            {
                points.Add(new Double2(
                    Origo.X + CalculateCoordinateX(i, N, R),
                    Origo.Y + CalculateCoordinateY(i, N, R)));
            }

            return points;
        }

        public static List<Double2> GetForceDirectedArrangement(List<List<bool>> G, Form1 main)
        {
            if (G == null) return new List<Double2>();

            //const double FORCE_LIMIT = 1.0;
            const int MAX_ITERATIONS = 800;
            const double IDEAL_SPRING_LENGTH = 5.0;

            int t = 1;
            double maxF = 0;
            double cooling = 1;
            double coolingFactor = 1;
            List<Double2> positions = new List<Double2>();

            // Setting up the default node positions
            //int gridSize = (int)Math.Ceiling(Math.Sqrt(G.Count));
            //for (int i = 0; i < G.Count; i++)
            //{
            //    positions.Add(new Double2((i % gridSize) * 70 + 10, (i / gridSize) * 70 + 10));
            //}

            positions = GetCircularArrangement(G.Count, 10);




            
            // Applying forces to nodes
            while (t < MAX_ITERATIONS || t == 1)
            {
                List<Double2> forces = new List<Double2>();
                for (int i = 0; i < G.Count; i++) forces.Add(new Double2());
                maxF = 0;

                for (int node = 0; node < G.Count; node++)
                {
                    for (int other = 0; other < G.Count; other++)
                    {
                        if (node == other) continue;

                        Double2 towardsOther = positions[node].DirectionTowards(positions[other]);
                        Double2 awayFromOther    = positions[other].DirectionTowards(positions[node]);
                        
                        // Repulsive force
                        Double2 repulsive = IDEAL_SPRING_LENGTH / Math.Pow(positions[node].DistanceFrom(positions[other]), 2) * awayFromOther;
                        
                        // Attractive (spring) force
                        Double2 spring = new Double2();
                        if (G[node][other])
                        {
                            spring = positions[node].DistanceFrom(positions[other]) / Math.Pow(IDEAL_SPRING_LENGTH, 2) * towardsOther;
                        }

                        Double2 force = cooling * (repulsive + spring);

                        if (force.Length() > maxF) maxF = force.Length();

                        forces[node] += force;
                    }
                }

                for (int i = 0; i < G.Count; i++)
                {
                    positions[i] += forces[i];
                }

                Debug.WriteLine(string.Join(", ", positions));

                main.SetPoints(positions, maxF, t);
                main.DrawGraph();

                cooling *= coolingFactor;
                t++;
            }
            

            return positions;
        }

        private static double CalculateCoordinateX(int N, int maxN, double R)
        {
            return Math.Cos(2 * Math.PI * ((double)N / maxN)) * R;
        }

        private static double CalculateCoordinateY(int N, int maxN, double R)
        {
            return Math.Sin(2 * Math.PI * ((double)N / maxN)) * R;
        }
    }
}
