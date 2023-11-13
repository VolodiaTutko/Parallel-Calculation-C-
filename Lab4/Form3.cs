using System;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace Lab4
{
    public partial class Form3 : Form
    {
        private int ballSize = 20;
        private int ballSpeedX = 5;
        private int ballSpeedY = 5;
        private int pictureBoxWidth;
        private int pictureBoxHeight;
        private Point ballPosition;
        private bool isPaused = false;
        private Thread thread3;

        public Form3()
        {
            InitializeComponent();
            pictureBoxWidth = pictureBox1.Width;
            pictureBoxHeight = pictureBox1.Height;
            ballPosition = new Point(pictureBoxWidth / 2, pictureBoxHeight / 2);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            timer3.Interval = 50;
            timer3.Start();
            thread3 = new Thread(Thread3Worker);
            thread3.IsBackground = true;
            thread3.Start();
        }

        private void Thread3Worker()
        {
            while (true)
            {
                if (!isPaused)
                {
                    timer3_Tick(null, null);
                }
                Thread.Sleep(50);
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillEllipse(Brushes.Red, ballPosition.X, ballPosition.Y, ballSize, ballSize);
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (!isPaused)
            {
                ballPosition.X += ballSpeedX;
                ballPosition.Y += ballSpeedY;

                if (ballPosition.X <= 0 || ballPosition.X + ballSize >= pictureBoxWidth)
                {
                    ballSpeedX = -ballSpeedX;
                }

                if (ballPosition.Y <= 0 || ballPosition.Y + ballSize >= pictureBoxHeight)
                {
                    ballSpeedY = -ballSpeedY;
                }

                pictureBox1.Invalidate();
            }
        }

        private void Pause3_Click(object sender, EventArgs e)
        {
            isPaused = true;
            Pause3.Enabled = false;
            Resume3.Enabled = true;
        }

        private void Resume3_Click(object sender, EventArgs e)
        {
            isPaused = false;
            Pause3.Enabled = true;
            Resume3.Enabled = false;
        }

        private void Thread3_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                thread3.Abort();
            }
            catch { }
        }
    }
}
