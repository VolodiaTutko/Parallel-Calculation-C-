using System;
using System.Threading;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form4 : Form
    {
        private int seconds = 0;
        private bool isPaused = false;
        public Thread thread4;

        public Form4()
        {
            InitializeComponent();

            //timer1.Interval = 1000;
            timer1.Tick += new EventHandler(timer1_Tick);
            label1.Size = new Size(50, 100);
            label1.Font = new Font(label1.Font.FontFamily, 56);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!isPaused)
            {
                seconds+= 5;
                UpdateLabel1Text(TimeSpan.FromSeconds(seconds).ToString(@"hh\:mm\:ss"));
            }
        }

        private void UpdateLabel1Text(string text)
        {
            if (label1.InvokeRequired)
            {
                label1.Invoke((Action)(() => label1.Text = text));
            }
            else
            {
                label1.Text = text;
            }
        }


        private void Form4_Load(object sender, EventArgs e)
        {
            thread4 = new Thread(Thread4Worker);
            thread4.IsBackground = true;
            thread4.Start();
        }

        private void Thread4Worker()
        {
            while (true)
            {
                Thread.Sleep(1000); 
                timer1_Tick(null, null);
            }
        }

        private void Pause4_Click(object sender, EventArgs e)
        {
            isPaused = true;
            Pause4.Enabled = false;
            Resume4.Enabled = true;
        }

        private void Resume4_Click(object sender, EventArgs e)
        {
            isPaused = false;
            Pause4.Enabled = true;
            Resume4.Enabled = false;
        }

        private void Thread4_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                thread4.Abort();
            }
            catch { }
        }
    }
}
