using System;
using System.Collections.Generic;
using System.Text;

namespace Graphs_Framework
{
    public class Double2
    {
        double x;
        double y;

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public Double2(double x = 0, double y = 0)
        {
            this.x = x;
            this.y = y;
        }

        public Double2(Double2 p)
        {
            x = p.x;
            y = p.y;
        }

        public static Double2 operator +(Double2 p1, Double2 p2) => new Double2(p1.x + p2.x, p1.y + p2.y);
        public static Double2 operator -(Double2 p1, Double2 p2) => new Double2(p1.x - p2.x, p1.y - p2.y);
        public static Double2 operator *(Double2 p, double n) => new Double2(p.x * n, p.y * n);
        public static Double2 operator *(double n, Double2 p) => new Double2(p.x * n, p.y * n);
        public static Double2 operator /(Double2 p, double n)
        {
            if (n == 0)
            {
                throw new DivideByZeroException();
            }
            return new Double2(p.x / n, p.y / n);
        }

        public static Double2 operator /(double n, Double2 p)
        {
            if (p.x == 0 || p.y == 0)
            {
                throw new DivideByZeroException();
            }
            return new Double2(n / p.x, n / p.y);
        }

        public Double2 Normalize()
        {
            double length = Math.Sqrt(x * x + y * y);
            x /= length;
            y /= length;

            return this;
        }

        public double DistanceFrom(Double2 p)
        {
            return Math.Sqrt((x - p.x) * (x - p.x) + (y - p.y) * (y - p.y));
        }

        public double Length()
        {
            return DistanceFrom(new Double2(0, 0));
        }

        public override string ToString()
        {
            return "[x: " + x + ", y: " + y + "]";
        }

        public Double2 DirectionTowards(Double2 other)
        {
            return (other - this).Normalize();
        }
    }
}