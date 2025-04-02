namespace SWIFA_Management_System
{
    partial class individualEvent
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
            button1 = new Button();
            label1 = new Label();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.AutoSize = true;
            button1.Location = new Point(12, 44);
            button1.Name = "button1";
            button1.Size = new Size(267, 76);
            button1.TabIndex = 0;
            button1.Text = "Register a Team";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(78, 32);
            label1.TabIndex = 1;
            label1.Text = "label1";
            label1.Click += label1_Click;
            // 
            // button2
            // 
            button2.AutoSize = true;
            button2.Location = new Point(285, 44);
            button2.Name = "button2";
            button2.Size = new Size(267, 76);
            button2.TabIndex = 4;
            button2.Text = "Input a Bout Sheet for Pools";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.AutoSize = true;
            button3.Location = new Point(12, 208);
            button3.Name = "button3";
            button3.Size = new Size(267, 76);
            button3.TabIndex = 5;
            button3.Text = "Generate Pools";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.AutoSize = true;
            button4.Location = new Point(12, 126);
            button4.Name = "button4";
            button4.Size = new Size(267, 76);
            button4.TabIndex = 6;
            button4.Text = "View Registered Teams";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.AutoSize = true;
            button5.Location = new Point(12, 290);
            button5.Name = "button5";
            button5.Size = new Size(267, 76);
            button5.TabIndex = 7;
            button5.Text = "View Pool Assignments";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.AutoSize = true;
            button6.Location = new Point(285, 126);
            button6.Name = "button6";
            button6.Size = new Size(267, 76);
            button6.TabIndex = 8;
            button6.Text = "Verify and Output Pool Results";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // individualEvent
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label1);
            Controls.Add(button1);
            Name = "individualEvent";
            Text = "individualEvent";
            Load += individualEvent_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
    }
}