using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace temp
{
    public partial class Form1 : Form
    {
        Thread th;
        Board board;
        Image title;
        Player player;
        Ghost ghost;
        //List<IPlayer> player; 
        //row 38 col 30
        float cell = 18;
        int tick = 60;

        public Form1()
        {
            //33 ,14
            //
            player = new Player(14, 33, (int)cell);
            board = new Board();
            board.ReadFromFile("../../board.txt");
            title = Image.FromFile("../../Images/title.png");
            ghost = new Ghost(14, 10, (int)cell, board.map);
            board.PlaceCandy(player, ghost);
            //players = new List<IPlayer>(4);
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        { //11 //12 /8-w 4-h
            Graphics g = e.Graphics;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            Brush bWall = new SolidBrush(Color.Blue);
            for(int i = 0; i < board.map.GetLength(0); i++)
            {
                for(int j = 0; j < board.map.GetLength(1); j++)
                {
                    if (board.map[i, j].type == Cell.Type.WALL)
                    {
                        g.DrawImage(board.map[i, j].image, j * cell, i * cell, cell, cell);
                    }
                    else if (board.map[i, j].type == Cell.Type.PATH)
                    {
                        //g.DrawImage(board.map[i, j].image, j * cell, i * cell, cell, cell);
                        g.FillRectangle(new SolidBrush(Color.Black), j * cell, i * cell, cell, cell);
                        if (board.map[i, j].candy != null)
                            board.map[i, j].candy.Show(g, j * cell, i * cell, cell);
                    }
                }
            }
            player.Draw(g);
            ghost.Draw(g);
            g.DrawImage(title, 11 * cell, 11 * cell, 8 * cell, 4 * cell);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ghost.ForceTurn(ghost.GetDirection(player, ref tick));
            ghost.ProgressAnimation(board.map);
            player.CheckCandy(board.map,ref title);
            player.ProgressAnimation(board.map);
            player.UpdateModifiers();
            ghost.UpdateModifiers();
            pictureBox1.Invalidate();
            tick++;
            if (player.CandyCount == 410 || ghost.X == player.X && ghost.Y == player.Y)
                timer1.Stop();
        }

        private void Form1_KeyDown_1(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case (Keys)37:
                    player.Turn(Direction.LEFT);
                    break;
                case (Keys)38:
                    player.Turn(Direction.UP);
                    break;
                case (Keys)39:
                    player.Turn(Direction.RIGHT);
                    break;
                case (Keys)40:
                    player.Turn(Direction.DOWN);
                    break;
            }
        }
    }
}
