using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace temp
{
    class Player : MovingEntity
    {
        public int CandyCount;
        public int HP;
        public Player(int x, int y, int imageSize) : base(x, y, "../../Images/DYNIA.png", imageSize)
        {
            HP = 3;
            CandyCount = 0;
        }

        public void CheckCandy(Cell[,] map, ref Image title)
        {
            if (map[Y, X].candy != null)
            {
                map[Y, X].candy.CauseEffect();
                map[Y, X].candy = null;
            }
            if(CandyCount >= 410)
            {
                title = Image.FromFile("../../Images/win.png");
            }
        }

    }
}
