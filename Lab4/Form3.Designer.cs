namespace Lab4
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.Pause3 = new System.Windows.Forms.Button();
            this.Resume3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(14, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(343, 233);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // timer3
            // 
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(224, 92);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(371, 255);
            this.panel1.TabIndex = 1;
            // 
            // Pause3
            // 
            this.Pause3.BackColor = System.Drawing.Color.Red;
            this.Pause3.Location = new System.Drawing.Point(424, 402);
            this.Pause3.Name = "Pause3";
            this.Pause3.Size = new System.Drawing.Size(87, 36);
            this.Pause3.TabIndex = 2;
            this.Pause3.Text = "Stop";
            this.Pause3.UseVisualStyleBackColor = false;
            this.Pause3.Click += new System.EventHandler(this.Pause3_Click);
            // 
            // Resume3
            // 
            this.Resume3.BackColor = System.Drawing.Color.Yellow;
            this.Resume3.Location = new System.Drawing.Point(544, 402);
            this.Resume3.Name = "Resume3";
            this.Resume3.Size = new System.Drawing.Size(90, 36);
            this.Resume3.TabIndex = 3;
            this.Resume3.Text = "Resume";
            this.Resume3.UseVisualStyleBackColor = false;
            this.Resume3.Click += new System.EventHandler(this.Resume3_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Resume3);
            this.Controls.Add(this.Pause3);
            this.Controls.Add(this.panel1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer3;
        private Panel panel1;
        private Button Pause3;
        private Button Resume3;
    }
}