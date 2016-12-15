using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Program
    {
        struct Disc
        {
            public readonly int StartPosition;
            public readonly int PositionCount;
            public Disc(int startPos, int totalPos)
            {
                this.StartPosition = startPos;
                this.PositionCount = totalPos;
            }
        }
        static void Main(string[] args)
        {
            int x = Solve();
            Console.WriteLine(x);
            Console.Read();
        }

        private static int Solve()
        {
            Disc[] discs = {
                new Disc(1,13),     // #1
                new Disc(10,19),    // #2
                new Disc(2,3),      // #3
                new Disc(1,7),      // #4
                new Disc(3,5),      // #5
                new Disc(5, 17)     // #6
            };

            for (int t = 0; t < int.MaxValue; t++)
            {
                bool win = true;
                for (int i = 0; i < discs.Length; i++)
                {
                    win &= (t + i + discs[i].StartPosition) % discs[i].PositionCount == 0;
                }
                if (win)
                    return t - 1;
            }
            return -1;
        }
    }
}
