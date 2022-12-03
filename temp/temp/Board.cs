using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace temp
{
    internal class Board
    {
        public Cell[,] map;

        public void Clear()
        {
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    map[i,j] = new Cell(j, i);
        }

        public void ReadFromFile(string fileName)
        {
            string[] boardLines = System.IO.File.ReadAllLines(fileName);
            map = new Cell[boardLines.Length, boardLines[0].Split('\t').Length];
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    map[i, j] = new Cell(j, i);
            int k = 0;
            foreach (string line in boardLines)
            {
                string[] cell = line.Split('\t');
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    var a = cell[j];
                    int type = Convert.ToInt32(cell[j]);
                    if (type == 0)
                        map[k, j].type = Cell.Type.WALL;
                    map[k, j].refreshImage();
                }
                k++;
            }
        }

        public void PlaceCandy(Player player, Ghost ghost)
        {
            var candyLocations = new List<(int x, int y)>();
            
            for (var x = 0; x < map.GetLength(0); x++)
            {
                for (var y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y].type == Cell.Type.PATH)
                    {
                        map[x, y].candy = new RegularCandy(player);
                        candyLocations.Add((x,y));
                    }
                }
            }

            Random rnd = new Random();
            var fastLocation = candyLocations[rnd.Next(candyLocations.Count)];
            var slowLocation = fastLocation;
            while (slowLocation == fastLocation)
            {
                slowLocation = candyLocations[rnd.Next(candyLocations.Count)];
            }

            map[fastLocation.x, fastLocation.y].candy = new PlayerSpeedingCandy(player);
            map[slowLocation.x, slowLocation.y].candy = new GhostSlowingCandy(ghost);
        }
    }
}