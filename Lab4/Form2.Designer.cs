namespace Lab4
{
    partial class Form2
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
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Pause2 = new System.Windows.Forms.Button();
            this.Resume2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(68, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(655, 376);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // Pause2
            // 
            this.Pause2.BackColor = System.Drawing.Color.Red;
            this.Pause2.Location = new System.Drawing.Point(456, 416);
            this.Pause2.Name = "Pause2";
            this.Pause2.Size = new System.Drawing.Size(75, 30);
            this.Pause2.TabIndex = 1;
            this.Pause2.Text = "Stop";
            this.Pause2.UseVisualStyleBackColor = false;
            this.Pause2.Click += new System.EventHandler(this.Pause2_Click);
            // 
            // Resume2
            // 
            this.Resume2.BackColor = System.Drawing.Color.Yellow;
            this.Resume2.Location = new System.Drawing.Point(570, 416);
            this.Resume2.Name = "Resume2";
            this.Resume2.Size = new System.Drawing.Size(75, 30);
            this.Resume2.TabIndex = 2;
            this.Resume2.Text = "Resume";
            this.Resume2.UseVisualStyleBackColor = false;
            this.Resume2.Click += new System.EventHandler(this.Resume2_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Resume2);
            this.Controls.Add(this.Pause2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer2;
        private PictureBox pictureBox1;
        private Button Pause2;
        private Button Resume2;
    }
}