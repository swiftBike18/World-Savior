using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace World_Savior
{
    public class Player 
    {
        public double Health;
        public int Speed { get; set; }
        public Direction direction { get; set ; }
        public int masksQuantity { get; set; }
        public bool goUp;
        public bool goDown;
        public bool goLeft;
        public bool goRight;

        public Image ChooseImage()
        {
            if (Health<2)
                return Properties.Resources.infected;
            switch (direction)
            {
                case Direction.Down:
                    return Properties.Resources.down;
                case Direction.Up:
                    return Properties.Resources.up;
                case Direction.Left:
                    return Properties.Resources.left;
                case Direction.Right:
                    return Properties.Resources.right;
            }
            return Properties.Resources.right;
        }
        public Player(double health, int speed, int maskQuantity)
        {
            direction = Direction.Up;
            Health = health;
            Speed = speed;
            masksQuantity = maskQuantity;
        }

    }
}
