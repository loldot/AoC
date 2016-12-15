using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class MazeDrawer
    {
        const int offset_x = 1;
        const int offset_y = 1;
        const int max_x = 100;
        const int max_y = 100;

        private char[,] explored_maze = new char[max_x, max_y];
        private MazeExplorer explorer;

        public MazeDrawer(MazeExplorer explorer)
        {
            this.explorer = explorer;
            explorer.MazePartDiscovered += OnMazePartDiscovered;
            for (int x = 0; x < max_x; x++)
            {
                for (int y = 0; y < max_y; y++)
                {
                    explored_maze[x, y] = ' ';
                }
            }
        }

        private void OnMazePartDiscovered(object sender, MazeDiscoveryEventArgs e)
        {
            Point p = e.Discovered;
            if (p.X < max_x && p.Y < max_y)
            {
                explored_maze[p.X, p.Y] = (e.IsWall) ? '#' : '.';
            }
        }

        public void Render()
        {
            var bytes = GetBytes().ToArray();
            File.WriteAllBytes(@"C:\temp\maze_output.txt", bytes);
        }

        private IEnumerable<byte> GetBytes()
        {
            for (int x = 0; x < max_x; x++)
            {
                for (int y = 0; y < max_y; y++)
                {
                    yield return (byte)explored_maze[x, y];
                }
                yield return (byte)'\n';
            }
        }
    }
}
