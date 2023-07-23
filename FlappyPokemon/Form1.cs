using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlappyPokemon
{
    public partial class Form1 : Form
    {
        static int max = 0;
        int pipeSpeed = 6;
        int gravity = 8;
        int score = 0;
        int i = 0;
        int flagp1 = 0;
        int flagp2 = 0;
        int flip = 1;
        int hit1 = 0;
        int hit2 = 0;

        Random rand = new Random();
        public Form1()
        {
            InitializeComponent();
            System.Drawing.Drawing2D.GraphicsPath obj = new System.Drawing.Drawing2D.GraphicsPath();
            obj.AddEllipse(0, 0, Magikarp.Height, Magikarp.Width);

            Region rg = new Region(obj);
            Magikarp.Region = rg;
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            ShowUpdates();

            if (gravity >= 0 && flip > 0)
            {
                Image magi = Magikarp.Image;
                magi.RotateFlip(RotateFlipType.Rotate90FlipNone);
                Magikarp.Image = magi;
                flip = -1;
            }
            else if (gravity <= 0 && flip < 0)
            {
                Image magi = Magikarp.Image;
                magi.RotateFlip(RotateFlipType.Rotate270FlipNone);
                Magikarp.Image = magi;
                flip = 1;
            }


            Magikarp.Top += gravity;

            pipeBottom.Left -= pipeSpeed;
            pipeBottom2.Left -= pipeSpeed;

            pipeTop.Left -= pipeSpeed;
            pipeTop2.Left -= pipeSpeed;


            if (pipeTop.Left < Magikarp.Left && flagp1 == 0)
            {
                score++;
                flagp1 = 1;
            }

            if (pipeTop2.Left < Magikarp.Left && flagp2 == 0)
            {
                score++;
                flagp2 = 1;
            }


            if (pipeTop.Left < -100)
            {
                i = rand.Next(230, 480);

                pipeBottom.Left = 560;
                pipeTop.Left = 560;

                pipeBottom.Top = i;
                pipeTop.Top = i - 700;

                flagp1 = 0;
                hit1 = 0;
            }

            else if(pipeTop2.Left < -100)
            {
                i = rand.Next(230, 480);

                pipeBottom2.Left = 560;
                pipeTop2.Left = 560;

                pipeBottom2.Top = i;
                pipeTop2.Top = i - 700;

                flagp2 = 0;
                hit1 = 0;
            }


            if ( (Magikarp.Bounds.IntersectsWith(pipeBottom.Bounds) ||
                 Magikarp.Bounds.IntersectsWith(pipeTop.Bounds) ||
                 Magikarp.Bounds.IntersectsWith(pipeBottom2.Bounds) ||
                 Magikarp.Bounds.IntersectsWith(pipeTop2.Bounds)) && hit1 == 0)
            {
                hit1 = 1;
                endGame();
            }

            if (Magikarp.Top < -25 || Magikarp.Bottom > 725)
                endGame();
                

        }

        private void endGame()
        {
            if (score > max)
                max = score;

            gameTimer.Stop();
            lblGameOver.Text += "Game Over!!!";
        }

        private void ShowUpdates()
        {
            lblScore.Text = "Score: " + score.ToString();
            lblMax.Text = "Max: " + max.ToString();
        }

        private void keyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                gravity = 8;
            }
            
        }

        private void keyIsDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
            {
                gravity = -8;
            }

            if(gameTimer.Enabled == false && e.KeyCode == Keys.Enter)
            {
                score = 0;
                Magikarp.Top = 250; Magikarp.Left = 50;
                pipeBottom.Top = 450; pipeTop.Top = -250;
                pipeBottom.Left = 279; pipeTop.Left = 279;

                pipeBottom2.Top = 400; pipeTop2.Top = -300;
                pipeBottom2.Left = 560; pipeTop2.Left = 560;

                gameTimer.Start();
                lblGameOver.Text = "";
                hit1 = 0;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
