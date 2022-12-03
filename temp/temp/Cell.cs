using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;

namespace temp
{
    internal class Cell
    {
        public enum Type
        {
            WALL,
            PATH
        }

        public Type type;
        public bool visited;
        public float globalDistance;
        public float localDistance;
        public int x, y;
        public List<Cell> neighbors;
        public Cell parent;
        public Candy candy;
        public Image image;

        public Cell(int x, int y)
        {
            type = Type.PATH;
            visited = false;
            globalDistance = float.PositiveInfinity;
            localDistance = float.PositiveInfinity;
            this.x = x;
            this.y = y;
            neighbors = new List<Cell>();
            parent = null;
        }

        public void refreshImage()
        {
            if (type == Type.WALL)
                image = Image.FromFile("../../Images/path.png");
            else if (type == Type.PATH)
            {
                image = Image.FromFile("../../Images/path.png");
                //candy = new Candy();
            }
        }
    }
}
