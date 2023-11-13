using System;
using System.Threading;
using System.Windows.Forms;

namespace Lab4
{
    public partial class MainForm : Form
    {
       public MainForm()
        {
            InitializeComponent();
        }
        private void StartThread1_Click(object sender, EventArgs e)
        {
            Form1 thread1 = new Form1();
            thread1.Show();
        }

        private void StartThread2_Click(object sender, EventArgs e)
        {
            Form2 thread2 = new Form2();
            thread2.Show();
        }

        private void StartThread3_Click(object sender, EventArgs e)
        {
            Form3 thread3 = new Form3();
            thread3.Show();
        }

        private void StartThread4_Click(object sender, EventArgs e)
        {
            Form4 thread4 = new Form4();
            thread4.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        

    }
}
