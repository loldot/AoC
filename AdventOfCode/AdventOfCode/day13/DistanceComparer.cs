using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    internal class DistanceComparer : IComparer<Point>
    {
        private readonly Point target;

        public DistanceComparer(Point target)
        {
            this.target = target;
        }
        public int Compare(Point a, Point b)
        {
            return a.Distance(target).CompareTo(b.Distance(target));
        }
    }
}