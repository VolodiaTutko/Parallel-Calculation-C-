namespace Lab4
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Pause1 = new System.Windows.Forms.Button();
            this.Resume1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Location = new System.Drawing.Point(302, 134);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(120, 111);
            this.panel1.TabIndex = 0;
            // 
            // Pause1
            // 
            this.Pause1.BackColor = System.Drawing.Color.Red;
            this.Pause1.Location = new System.Drawing.Point(241, 312);
            this.Pause1.Name = "Pause1";
            this.Pause1.Size = new System.Drawing.Size(100, 30);
            this.Pause1.TabIndex = 1;
            this.Pause1.Text = "Stop";
            this.Pause1.UseVisualStyleBackColor = false;
            this.Pause1.Click += new System.EventHandler(this.Pause1_Click);
            // 
            // Resume1
            // 
            this.Resume1.BackColor = System.Drawing.Color.Yellow;
            this.Resume1.Location = new System.Drawing.Point(377, 312);
            this.Resume1.Name = "Resume1";
            this.Resume1.Size = new System.Drawing.Size(100, 30);
            this.Resume1.TabIndex = 2;
            this.Resume1.Text = "Resume";
            this.Resume1.UseVisualStyleBackColor = false;
            this.Resume1.Click += new System.EventHandler(this.Resume1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 393);
            this.Controls.Add(this.Resume1);
            this.Controls.Add(this.Pause1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private System.Windows.Forms.Timer timer1;
        private Button Pause1;
        private Button Resume1;
    }
}