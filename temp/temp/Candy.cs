using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace temp
{
    public abstract class Candy
    {
        public Image image;

        public void Show(Graphics e, float x, float y, float cell)
        {
            e.DrawImage(image, x, y, cell, cell);
        }

        public abstract void CauseEffect();
    }
}
