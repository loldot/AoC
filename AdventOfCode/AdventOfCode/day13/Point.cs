using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    [DebuggerDisplay("({X},{Y})")]
    public struct Point
    {
        public readonly int X;
        public readonly int Y;

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public double Distance(Point target)
        {
            int diffX = X > target.X ? X - target.X : target.X - X;
            int diffY = Y > target.Y ? Y - target.Y : target.Y - Y;

            return Math.Sqrt(diffX * diffX + diffY * diffY);
        }

        public static bool operator ==(Point a, Point b)
        {
            return a.X == b.X && a.Y == b.Y;
        }
        public static bool operator !=(Point a, Point b)
        {
            return a.X != b.X || a.Y != b.Y;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return this == (Point)obj;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return unchecked(31 * X + Y);
        }
    }
}
