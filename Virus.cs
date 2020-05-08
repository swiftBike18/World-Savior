using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace World_Savior
{
    public class Virus
    {
        public int Speed { get; set; }
        public Virus(int velocity)
        {
            Speed = velocity;
        }
    }
}
