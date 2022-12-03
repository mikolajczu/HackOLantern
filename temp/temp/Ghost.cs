using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace temp
{
    class Ghost : MovingEntity
    {
        public AlgorithmAStar algorithm;
        Cell[,] map;
        public Ghost(int x, int y, int imageSize, Cell[,] map) : base(x, y, "../../Images/STRASZNYDUCH.png", imageSize)
        {
            this.map = map;
        }
        public Direction GetDirection(Player player, ref int tick)
        {
            if (tick == 60)
            {
                algorithm = new AlgorithmAStar(player.X, player.Y, X, Y, map);
                algorithm.Run();
                tick = 0;
            }
            if (map[Y, X].parent == null)
                if (map[Y, X].x == player.X && map[Y, X].y == player.Y)
                    //koniec
                    return Direction.UP;
                else
                {
                    algorithm = new AlgorithmAStar(player.X, player.Y, X, Y, map);
                    algorithm.Run();
                }
            int heightdiff = Y - map[Y, X].parent.y;
            int widthdiff = X - map[Y, X].parent.x;
            if (heightdiff <= -1)
            {
                return Direction.DOWN;
            }
            else if (heightdiff >= 1)
            {
                return Direction.UP;
            }
            else if (widthdiff <= -1)
            {
                return Direction.RIGHT;
            }
            else
            {
                return Direction.LEFT;
            }
        }
    }
}
