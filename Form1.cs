using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace World_Savior
{

    public partial class Form1 : Form
    {
        int score = 0; 
        bool gameOver = false;
        bool firstAidOnField;
        Random random = new Random(); 
        Player newPlayer = new Player(100, 10,10);
        Virus newVirus = new Virus(2);

        public Form1()
        {
            InitializeComponent();
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (gameOver) return;
            switch (e.KeyCode)
            {
                case Keys.Left:
                    newPlayer.goLeft = true; 
                    newPlayer.direction = Direction.Left;
                    player.Image = newPlayer.ChooseImage(); 
                    break;

                case Keys.Right:
                    newPlayer.goRight = true; 
                    newPlayer.direction = Direction.Right;
                    player.Image = newPlayer.ChooseImage(); 
                    break;

                case Keys.Up:
                    newPlayer.goUp = true; 
                    newPlayer.direction = Direction.Up;
                    player.Image = newPlayer.ChooseImage(); 
                    break;

                case Keys.Down:
                    newPlayer.goDown = true; 
                    newPlayer.direction = Direction.Down;
                    player.Image = newPlayer.ChooseImage(); 
                    break;
                default: break;
            }

        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (gameOver) return; 

            switch (e.KeyCode)
            {
                case Keys.Left:
                    newPlayer.goLeft = false;
                    break;
                case Keys.Right:
                    newPlayer.goRight = false;
                    break;
                case Keys.Up:
                    newPlayer.goUp = false;
                    break;
                case Keys.Down:
                    newPlayer.goDown = false;
                    break;
                case Keys.Space:
                    if (newPlayer.masksQuantity > 0)
                    {
                        newPlayer.masksQuantity--;
                        ThrowMask(newPlayer.direction);
                        if (newPlayer.masksQuantity < 1)
                            DropMoreMasks();
                    }
                    break;
            }
        }

        private void gameEngine(object sender, EventArgs e)
        {
            if (newPlayer.Health > 1) 
            {
                progressBar1.Value = Convert.ToInt32(newPlayer.Health); 
            }
            else
            {
                player.Image = newPlayer.ChooseImage(); 
                timer1.Stop(); 
                gameOver = true; 
            }

            label1.Text = "   Masks:  " + newPlayer.masksQuantity; 
            label2.Text = "Kills: " + score; 

            

            if (newPlayer.goLeft && player.Left > 0)
            {
                player.Left -= newPlayer.Speed;
            }

            if (newPlayer.goRight && player.Right < Size.Width)
            {
                player.Left += newPlayer.Speed;
            }

            if (newPlayer.goUp && player.Top > 0)
            {
                player.Top -= newPlayer.Speed;
            }

            if (newPlayer.goDown && player.Top + player.Height < Size.Height - 50)
            {
                player.Top += newPlayer.Speed;
            }

            if ((newPlayer.Health < 30)&&(!firstAidOnField))
            {
                DropFirstAidKit();
                firstAidOnField = true;

            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    switch (x.Tag)
                    {
                        case "FirstAid":
                            if (x.Bounds.IntersectsWith(player.Bounds))
                            {
                                this.Controls.Remove(x);
                                x.Dispose();
                                newPlayer.Health += 30;
                                firstAidOnField = false;
                            }
                            break;

                        case "MoreMasks":
                            if (x.Bounds.IntersectsWith(player.Bounds))
                            {
                                this.Controls.Remove(x);
                                x.Dispose();
                                newPlayer.masksQuantity += 5;
                            }
                            break;

                        case "mask":
                            if (x.Left < 1 || x.Left > Size.Width || x.Top < 10 || x.Top > Size.Height)
                                 this.Controls.Remove(x);
                             break;

                        case "virus":
                            MoveVirus(x);
                            break;
                    }
                }

                foreach (Control j in this.Controls)
                {
                    if ((j is PictureBox && j.Tag is "mask") && (x is PictureBox && x.Tag is "virus"))
                    {
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++; 
                            this.Controls.Remove(j); 
                            j.Dispose(); 
                            this.Controls.Remove(x); 
                            x.Dispose(); 
                            makeVirus(); 
                        }
                    }
                }
            }
        }

        public void MoveVirus(Control x)
        {
            if (x.Bounds.IntersectsWith(player.Bounds))
            {
                newPlayer.Health -= 1;
            }

            if (x.Left > player.Left)
            {
                x.Left -= newVirus.Speed;
            }

            if (x.Top > player.Top)
            {
                x.Top -= newVirus.Speed;
            }

            if (x.Left < player.Left)
            {
                x.Left += newVirus.Speed;
            }

            if (x.Top < player.Top)
            {
                x.Top += newVirus.Speed;
            }
        }
        private void DropFirstAidKit()
        {
            PictureBox firstAid = new PictureBox();
            firstAid.Image = Properties.Resources.aid; 
            firstAid.SizeMode = PictureBoxSizeMode.Zoom; 
            firstAid.Left = random.Next(0, Size.Width - 100); 
            firstAid.Top = random.Next(0, Size.Height - 100); 
            firstAid.Tag = "FirstAid"; 
            this.Controls.Add(firstAid);
            firstAid.BringToFront(); 
            player.BringToFront(); 
        }

        private void DropMoreMasks()
        {
            PictureBox moreMasks = new PictureBox();
            moreMasks.Image = Properties.Resources.mask;
            moreMasks.SizeMode = PictureBoxSizeMode.Zoom;
            moreMasks.Left = random.Next(0, Size.Width - 100);
            moreMasks.Top = random.Next(0, Size.Height - 100);
            moreMasks.Tag = "MoreMasks";
            this.Controls.Add(moreMasks);
            moreMasks.BringToFront();
            player.BringToFront();
        }

        private void ThrowMask(Direction direct)
        {
            Mask mask = new Mask(); 
            mask.direction = direct; 
            mask.Left = player.Left + (player.Width / 2); 
            mask.Top = player.Top + (player.Height / 2); 
            mask.makeMask(this);
        }

        private void makeVirus()
        {
            PictureBox virus = new PictureBox();
            virus.Tag = "virus"; 
            virus.Image = Properties.Resources.virus1; 
            virus.Left = random.Next(0, Size.Width); 
            virus.Top = random.Next(0, Size.Height); 
            virus.SizeMode = PictureBoxSizeMode.Zoom; 
            this.Controls.Add(virus); 
            player.BringToFront(); 
        }
    }
}