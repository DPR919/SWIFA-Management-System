﻿namespace SWIFA_Management_System
{
    partial class splashPage
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(splashPage));
            label1 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 27F);
            label1.Location = new Point(52, 24);
            label1.Name = "label1";
            label1.Size = new Size(697, 72);
            label1.TabIndex = 0;
            label1.Text = "SWIFA Management System";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Click += label1_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.None;
            button1.AutoSize = true;
            button1.Location = new Point(312, 128);
            button1.Name = "button1";
            button1.Size = new Size(177, 35);
            button1.TabIndex = 1;
            button1.Text = "Create A New Event";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.None;
            button2.AutoSize = true;
            button2.Location = new Point(285, 186);
            button2.Name = "button2";
            button2.Size = new Size(237, 35);
            button2.TabIndex = 2;
            button2.Text = "Manage An Ongoing Event";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.None;
            button3.AutoSize = true;
            button3.Location = new Point(312, 244);
            button3.Name = "button3";
            button3.Size = new Size(177, 35);
            button3.TabIndex = 3;
            button3.Text = "See Past Events";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.Image = Properties.Resources.SWIFA_Black_Base_Export_Small;
            pictureBox1.Location = new Point(303, 304);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(195, 119);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // splashPage
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(800, 450);
            Controls.Add(pictureBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "splashPage";
            Text = "SWIFA Management System";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button button1;
        private Button button2;
        private Button button3;
        private PictureBox pictureBox1;
    }
}
