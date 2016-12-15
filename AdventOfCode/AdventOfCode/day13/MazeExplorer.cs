using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode
{
    public class MazeExplorer
    {
        private int magicNumber;
        public event EventHandler<MazeDiscoveryEventArgs> MazePartDiscovered;

        public MazeExplorer(int magicNumber)
        {
            this.magicNumber = magicNumber;
        }

        public int Explore(Point start, Point target)
        {
            Dictionary<Point, int> score = new Dictionary<Point, int>();
            Queue<Point> Q = new Queue<Point>();

            Q.Enqueue(start);
            score[start] = 0;
            while (Q.Any())
            {
                Point current = Q.Dequeue();
                int currentScore = GetScore(score, current);
                if (current == target)
                    return currentScore;
                foreach (var n in ComputeNeighbours(current))
                {
                    if (!score.ContainsKey(n))
                    {
                        score[n] = currentScore + 1;
                        Q.Enqueue(n);
                    }
                }
            }
            return -1;
        }

        private Point[] ComputeNeighbours(Point p)
        {
            List<Point> allNeighbours = new List<Point>()
            {
                new Point(p.X - 1, p.Y),
                new Point(p.X + 1, p.Y),
                new Point(p.X, p.Y - 1),
                new Point(p.X, p.Y + 1)
            };
            List<Point> openNeighbours = new List<Point>();
            foreach (var neighbour in allNeighbours)
            {
                if (p.X > 0 && p.Y > 0)
                {
                    bool isWall = IsWall(neighbour);
                    RaiseMazePartDiscovered(neighbour, isWall);
                    if (!isWall)
                        openNeighbours.Add(neighbour);
                }
            }
            return openNeighbours.ToArray();
        }

        private bool IsWall(Point p)
        {
            if (p.X < 0 || p.Y < 0)
                return true;
            int m = p.X * p.X + 3 * p.X + 2 * p.X * p.Y + p.Y + p.Y * p.Y + magicNumber;
            return BitParity(m) == 1;
        }
        private int BitParity(int n)
        {
            uint v = (uint)n;
            v ^= v >> 16;
            v ^= v >> 8;
            v ^= v >> 4;
            v &= 0xf;
            uint c = (0x6996U >> (int)v) & 1U;
            return (int)c;
        }

        public void FindAll(int max_x, int max_y)
        {
            for (int x = 0; x < max_x; x++)
            {
                for (int y = 0; y < max_y; y++)
                {
                    Point p = new Point(x, y);
                    RaiseMazePartDiscovered(p, IsWall(p));
                }
            }
        }

        private int GetScore(Dictionary<Point, int> scores, Point p)
        {
            int score;
            if (scores.TryGetValue(p, out score))
            {
                return score;
            }
            return int.MaxValue - 1;
        }

        private void RaiseMazePartDiscovered(Point p, bool isWall)
        {

            MazePartDiscovered?.Invoke(this, new MazeDiscoveryEventArgs
            {
                Discovered = p,
                IsWall = isWall
            });
        }
    }
}