using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace temp
{
    enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

    abstract class MovingEntity
    {
        List<Modifier> modifiers;
        public int X { get; set; }
        public int Y { get; set; }
        public float Speed { get; set; }
        public Direction AnimationDirection { get; set; }
        private Direction ScheduledMove { get; set; }
        public float AnimationCompleteness { get; private set; }
        private Image image;
        int imageSize;

        public MovingEntity(int x, int y, string imagePath, int imageSize) {
            X = x;
            Y = y;
            Speed = 10;
            AnimationDirection = Direction.UP;
            ScheduledMove = Direction.UP;
            AnimationCompleteness = 1;
            image = Image.FromFile(imagePath);
            this.imageSize = imageSize;
            modifiers = new List<Modifier>();
        }

        public void AddModifier(Modifier modifier)
        {
            modifiers.Add(modifier);
            modifier.Apply(this);
        }

        public void UpdateModifiers()
        {
            for(int i = modifiers.Count - 1; i >= 0; i--)
            {
                modifiers[i].Duration--;
                if(modifiers[i].Duration == 0)
                {
                    modifiers[i].Undo(this);
                    modifiers.RemoveAt(i);
                }
            }
        }

        public void Turn(Direction direction)
        {
            ScheduledMove = direction;
        }

        public void ForceTurn(Direction direction)
        {
            ScheduledMove = direction;
            AnimationDirection = direction;
        }

        public void ProgressAnimation(Cell[,] map)
        {
            const float tick = (float)1.0 / (float)60.0;

            if(AnimationCompleteness < 1)
            {
                AnimationCompleteness += tick * Speed;
            }
            else // AnimationCompleteness było >= 1 jeszcze przed zmianą
            {
                AnimationDirection = ScheduledMove;
                AnimationCompleteness = 0;
            }

            // AnimationCompletness przekroczyło 1 dopiero w ifie
            if(AnimationCompleteness >= 1)
            {
                switch (AnimationDirection)
                {
                    case Direction.LEFT: --X; break;
                    case Direction.RIGHT: ++X; break;
                    case Direction.UP: --Y; break;
                    case Direction.DOWN: ++Y; break;
                }

                AnimationDirection = ScheduledMove;
                AnimationCompleteness = 0;
            }

            // zablokowanie animacji przy wejściu w ścianę

            int nextX = X;
            int nextY = Y;

            switch (AnimationDirection)
            {
                case Direction.LEFT: --nextX; break;
                case Direction.RIGHT: ++nextX; break;
                case Direction.UP: --nextY; break;
                case Direction.DOWN: ++nextY; break;
            }

            if (map[nextY, nextX].type == Cell.Type.WALL)
            {
                AnimationCompleteness = 0;
                AnimationDirection = ScheduledMove;
            }
        }

        public void Draw(Graphics g)
        {
            int tileX = X * imageSize;
            int tileY = Y * imageSize;

            int absOffset = (int)Math.Floor(AnimationCompleteness * imageSize);

            int offsetX = 0;
            int offsetY = 0;

            switch(AnimationDirection)
            {
                case Direction.LEFT: offsetX = -absOffset; break;
                case Direction.RIGHT: offsetX = absOffset; break;
                case Direction.UP: offsetY = -absOffset; break;
                case Direction.DOWN: offsetY = absOffset; break;
            }

            int finalX = tileX + offsetX;
            int finalY = tileY + offsetY;

            g.DrawImage(image, finalX, finalY, imageSize, imageSize);
        }
    }
}
