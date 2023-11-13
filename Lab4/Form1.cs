using System;
using System.Windows.Forms;
using System.Threading;

namespace Lab4
{
    public partial class Form1 : Form
    {
        private int squareSize = 50;
        private int maxSize = 100;
        private int minSize = 20;
        private int step = 5;
        private bool isGrowing = true;
        private bool isPaused = false; 

        public Form1()
        {
            InitializeComponent();
        }
        public Thread thread1;

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 100;
            timer1.Start();
            thread1 = new Thread(new ThreadStart(TimerThread));
            thread1.IsBackground = true;
            thread1.Start();
        }

        private void TimerThread()
        {
            while (true)
            {
                if (!isPaused)
                {
                    if (isGrowing)
                    {
                        squareSize += step;
                        if (squareSize >= maxSize)
                        {
                            squareSize = maxSize;
                            isGrowing = false;
                        }
                    }
                    else
                    {
                        squareSize -= step;
                        if (squareSize <= minSize)
                        {
                            squareSize = minSize;
                            isGrowing = true;
                        }
                    }
                }

                if (!this.IsHandleCreated || this.IsDisposed || this.Disposing)
                {
                    break; 
                }

                if (panel1.InvokeRequired)
                {
                    panel1.Invoke((MethodInvoker)delegate
                    {
                        UpdatePanelSize();
                    });
                }
                else
                {
                    UpdatePanelSize();
                }

                Thread.Sleep(100);
            }
        }

        private void UpdatePanelSize()
        {
            if (!this.IsHandleCreated || this.IsDisposed || this.Disposing)
            {
                return; 
            }

            panel1.Width = squareSize;
            panel1.Height = squareSize;

            panel1.Left = (this.ClientSize.Width - panel1.Width) / 2;
            panel1.Top = (this.ClientSize.Height - panel1.Height) / 2;
        }



        private void Pause1_Click(object sender, EventArgs e)
        {
            isPaused = true; 
            Pause1.Enabled = false;
            Resume1.Enabled = true;
        }

        private void Resume1_Click(object sender, EventArgs e)
        {
            isPaused = false;
            Pause1.Enabled = true;
            Resume1.Enabled = false;
        }

        private void Thread1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                thread1.Abort();
            }
            catch { }

        }

    }
}
