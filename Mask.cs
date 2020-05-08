using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Windows.Forms;

namespace World_Savior
{
    class Mask
    {
        public Direction direction;
        public int speed = 20; 
        PictureBox mask = new PictureBox();
        Timer tm = new Timer(); 
        public int Left; 
        public int Top; 
        public void makeMask(Form form)
        {
            mask.Tag = "mask"; 
            mask.Image = Properties.Resources.mask;
            mask.SizeMode = PictureBoxSizeMode.Zoom;
            mask.Left = Left;  
            mask.Top = Top; 
            mask.BringToFront(); 
            form.Controls.Add(mask); 
            tm.Interval = speed;
            tm.Tick += new EventHandler(tm_Tick); 
            tm.Start(); 
        }

        public void tm_Tick(object sender, EventArgs e)
        {
            
            if (direction is Direction.Left)
            {
                mask.Left -= speed; 
            }
            
            if (direction == Direction.Right)
            {
                mask.Left += speed; 
            }
            
            if (direction == Direction.Up)
            {
                mask.Top -= speed;
            }
           
            if (direction == Direction.Down)
            {
                mask.Top += speed; 
            }
            if (mask.Left < 16 || mask.Left > 860 || mask.Top < 10 || mask.Top > 616)
            {
                tm.Stop(); 
                tm.Dispose(); 
                mask.Dispose();
                tm = null; 
                mask = null; 
            }
        }
    }
}