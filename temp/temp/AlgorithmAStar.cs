using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace temp
{
    internal class AlgorithmAStar
    {
        Cell[,] map;
        public Cell start;
        public Cell finish;

        public AlgorithmAStar(int startX, int startY, int finishX, int finishY, Cell[,] map)
        {
            this.map = map;
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j].parent = null;
                    map[i, j].localDistance = float.PositiveInfinity;
                    map[i, j].globalDistance = float.PositiveInfinity;
                    map[i, j].neighbors = new List<Cell>();
                    map[i, j].visited = false;
                }
            start = map[startY,startX];
            finish = map[finishY,finishX];
        }

        private void Initialize()
        {
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (i > 0)
                        map[i,j].neighbors.Add(map[i - 1,j]);
                    if (i < map.GetLength(0) - 1)
                        map[i,j].neighbors.Add(map[i + 1,j]);
                    if (j > 0)
                        map[i,j].neighbors.Add(map[i,j - 1]);
                    if (j < map.GetLength(1) - 1)
                        map[i,j].neighbors.Add(map[i,j + 1]);
                }
        }

        private double Distance(Cell a, Cell b)
        {
            return Math.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
        }

        public void Run()
        {
            Initialize();

            Cell current = start;
            start.localDistance = 0;
            start.globalDistance = (float)Distance(start, finish);

            List<Cell> notTestedCells = new List<Cell>();
            notTestedCells.Add(start);

            while (notTestedCells.Count > 0 && current != finish)
            {
                List<Cell> sortedNotTestedCells = notTestedCells.OrderBy(cell => cell.globalDistance).ToList();
                notTestedCells = sortedNotTestedCells;

                while (notTestedCells.Count > 0 && notTestedCells.First().visited == true)
                    notTestedCells.RemoveAt(0);

                if (notTestedCells.Count == 0)
                    break;

                current = notTestedCells.First();
                current.visited = true;

                for (int i = 0; i < current.neighbors.Count; i++)
                {
                    if (!current.neighbors[i].visited && current.neighbors[i].type != Cell.Type.WALL)
                        notTestedCells.Add(current.neighbors[i]);

                    float possiblyLowerDistance = current.localDistance + (float)Distance(current, current.neighbors[i]);

                    if (possiblyLowerDistance < current.neighbors[i].localDistance)
                    {
                        current.neighbors[i].parent = current;
                        current.neighbors[i].localDistance = possiblyLowerDistance;
                        current.neighbors[i].globalDistance = current.neighbors[i].localDistance +
                                                              (float)Distance(current.neighbors[i], finish);
                    }
                }
            }
        }
    }
}
