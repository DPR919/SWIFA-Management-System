namespace SWIFA_Management_System
{
    partial class teamDetails
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
            teamName = new Label();
            AFencer = new Label();
            BFencer = new Label();
            CFencer = new Label();
            AltFencer = new Label();
            SuspendLayout();
            // 
            // teamName
            // 
            teamName.AutoSize = true;
            teamName.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            teamName.Location = new Point(12, 9);
            teamName.Name = "teamName";
            teamName.Size = new Size(132, 32);
            teamName.TabIndex = 0;
            teamName.Text = "teamName";
            // 
            // AFencer
            // 
            AFencer.AutoSize = true;
            AFencer.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            AFencer.Location = new Point(12, 80);
            AFencer.Name = "AFencer";
            AFencer.Size = new Size(78, 32);
            AFencer.TabIndex = 1;
            AFencer.Text = "label1";
            // 
            // BFencer
            // 
            BFencer.AutoSize = true;
            BFencer.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BFencer.Location = new Point(12, 147);
            BFencer.Name = "BFencer";
            BFencer.Size = new Size(78, 32);
            BFencer.TabIndex = 2;
            BFencer.Text = "label2";
            // 
            // CFencer
            // 
            CFencer.AutoSize = true;
            CFencer.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            CFencer.Location = new Point(12, 214);
            CFencer.Name = "CFencer";
            CFencer.Size = new Size(78, 32);
            CFencer.TabIndex = 3;
            CFencer.Text = "label3";
            // 
            // AltFencer
            // 
            AltFencer.AutoSize = true;
            AltFencer.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            AltFencer.Location = new Point(12, 281);
            AltFencer.Name = "AltFencer";
            AltFencer.Size = new Size(78, 32);
            AltFencer.TabIndex = 4;
            AltFencer.Text = "label4";
            // 
            // teamDetails
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(AltFencer);
            Controls.Add(CFencer);
            Controls.Add(BFencer);
            Controls.Add(AFencer);
            Controls.Add(teamName);
            Name = "teamDetails";
            Text = "teamDetails";
            Load += teamDetails_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label teamName;
        private Label AFencer;
        private Label BFencer;
        private Label CFencer;
        private Label AltFencer;
    }
}