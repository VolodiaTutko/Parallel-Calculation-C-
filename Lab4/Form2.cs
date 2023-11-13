using System;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace Lab4
{
    public partial class Form2 : Form
    {
        private Thread thread2;
        private int amplitude = 50;
        private int frequency = 5;
        private int xOffset = 0;
        private int yOffset = 100;
        private int pixelStep = 5;
        private bool isPaused = false;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer2.Interval = 50;
            timer2.Start();
            thread2 = new Thread(new ThreadStart(Thread2Worker));
            thread2.IsBackground = true;
            thread2.Start();
        }

        private void Thread2Worker()
        {
            while (true)
            {
                if (!isPaused)
                {
                    timer2_Tick(null, null);
                }
                Thread.Sleep(50);
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int width = pictureBox1.Width;
            int height = pictureBox1.Height;

            g.Clear(Color.White);

            for (int x = 0; x < width; x += pixelStep)
            {
                int y = yOffset + (int)(amplitude * Math.Sin(2 * Math.PI * frequency * (x + xOffset) / width));
                g.FillRectangle(Brushes.Red, x, y, 4, 4);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (!isPaused)
            {
                xOffset += 5;
                pictureBox1.Invalidate();
            }
        }

        private void Pause2_Click(object sender, EventArgs e)
        {
            isPaused = true;
            Pause2.Enabled = false;
            Resume2.Enabled = true;
        }

        private void Resume2_Click(object sender, EventArgs e)
        {
            isPaused = false;
            Pause2.Enabled = true;
            Resume2.Enabled = false;
        }

        private void Thread2_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                thread2.Abort();
            }
            catch { }
        }
    }
}
