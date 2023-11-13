namespace Lab4
{
    partial class Form4
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.Pause4 = new System.Windows.Forms.Button();
            this.Resume4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(262, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 15);
            this.label1.TabIndex = 0;
            // 
            // Pause4
            // 
            this.Pause4.BackColor = System.Drawing.Color.Red;
            this.Pause4.Location = new System.Drawing.Point(279, 400);
            this.Pause4.Name = "Pause4";
            this.Pause4.Size = new System.Drawing.Size(75, 23);
            this.Pause4.TabIndex = 1;
            this.Pause4.Text = "Stop";
            this.Pause4.UseVisualStyleBackColor = false;
            this.Pause4.Click += new System.EventHandler(this.Pause4_Click);
            // 
            // Resume4
            // 
            this.Resume4.BackColor = System.Drawing.Color.Yellow;
            this.Resume4.Location = new System.Drawing.Point(391, 400);
            this.Resume4.Name = "Resume4";
            this.Resume4.Size = new System.Drawing.Size(75, 23);
            this.Resume4.TabIndex = 2;
            this.Resume4.Text = "Resume";
            this.Resume4.UseVisualStyleBackColor = false;
            this.Resume4.Click += new System.EventHandler(this.Resume4_Click);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 435);
            this.Controls.Add(this.Resume4);
            this.Controls.Add(this.Pause4);
            this.Controls.Add(this.label1);
            this.Name = "Form4";
            this.Text = "Form4";
            this.Load += new System.EventHandler(this.Form4_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private Label label1;
        private Button Pause4;
        private Button Resume4;
    }
}