namespace SWIFA_Management_System
{
    partial class teamRegistration
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            schoolCombo = new ComboBox();
            button1 = new Button();
            label7 = new Label();
            bladeCombo = new ComboBox();
            manualSchoolInput = new TextBox();
            AFencerName = new TextBox();
            BFencerName = new TextBox();
            CFencerName = new TextBox();
            AltFencerName = new TextBox();
            label8 = new Label();
            suffixText = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(91, 32);
            label1.TabIndex = 0;
            label1.Text = "School:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(521, 9);
            label2.Name = "label2";
            label2.Size = new Size(78, 32);
            label2.TabIndex = 1;
            label2.Text = "Blade:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(12, 132);
            label3.Name = "label3";
            label3.Size = new Size(112, 32);
            label3.TabIndex = 2;
            label3.Text = "Fencer A:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(12, 188);
            label4.Name = "label4";
            label4.Size = new Size(111, 32);
            label4.TabIndex = 3;
            label4.Text = "Fencer B:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(12, 244);
            label5.Name = "label5";
            label5.Size = new Size(112, 32);
            label5.TabIndex = 4;
            label5.Text = "Fencer C:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(69, 300);
            label6.Name = "label6";
            label6.Size = new Size(55, 32);
            label6.TabIndex = 5;
            label6.Text = "Alt: ";
            // 
            // schoolCombo
            // 
            schoolCombo.FormattingEnabled = true;
            schoolCombo.Location = new Point(109, 12);
            schoolCombo.Name = "schoolCombo";
            schoolCombo.Size = new Size(182, 33);
            schoolCombo.TabIndex = 6;
            schoolCombo.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // button1
            // 
            button1.Location = new Point(12, 404);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 7;
            button1.Text = "Register";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(12, 73);
            label7.Name = "label7";
            label7.Size = new Size(468, 32);
            label7.TabIndex = 8;
            label7.Text = "Don't see school in dropdown? Enter here:";
            // 
            // bladeCombo
            // 
            bladeCombo.FormattingEnabled = true;
            bladeCombo.Items.AddRange(new object[] { "Epee", "Foil", "Sabre" });
            bladeCombo.Location = new Point(605, 12);
            bladeCombo.Name = "bladeCombo";
            bladeCombo.Size = new Size(182, 33);
            bladeCombo.TabIndex = 9;
            // 
            // manualSchoolInput
            // 
            manualSchoolInput.Location = new Point(486, 76);
            manualSchoolInput.Name = "manualSchoolInput";
            manualSchoolInput.Size = new Size(302, 31);
            manualSchoolInput.TabIndex = 10;
            // 
            // AFencerName
            // 
            AFencerName.Location = new Point(130, 135);
            AFencerName.Name = "AFencerName";
            AFencerName.Size = new Size(172, 31);
            AFencerName.TabIndex = 11;
            AFencerName.TextChanged += AFencerName_TextChanged;
            // 
            // BFencerName
            // 
            BFencerName.Location = new Point(129, 191);
            BFencerName.Name = "BFencerName";
            BFencerName.Size = new Size(172, 31);
            BFencerName.TabIndex = 12;
            BFencerName.TextChanged += textBox3_TextChanged;
            // 
            // CFencerName
            // 
            CFencerName.Location = new Point(130, 247);
            CFencerName.Name = "CFencerName";
            CFencerName.Size = new Size(172, 31);
            CFencerName.TabIndex = 13;
            // 
            // AltFencerName
            // 
            AltFencerName.Location = new Point(130, 303);
            AltFencerName.Name = "AltFencerName";
            AltFencerName.Size = new Size(172, 31);
            AltFencerName.TabIndex = 14;
            AltFencerName.TextChanged += textBox5_TextChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(297, 9);
            label8.Name = "label8";
            label8.Size = new Size(79, 32);
            label8.TabIndex = 15;
            label8.Text = "Suffix:";
            // 
            // suffixText
            // 
            suffixText.Location = new Point(382, 12);
            suffixText.Name = "suffixText";
            suffixText.Size = new Size(47, 31);
            suffixText.TabIndex = 16;
            // 
            // teamRegistration
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(suffixText);
            Controls.Add(label8);
            Controls.Add(AltFencerName);
            Controls.Add(CFencerName);
            Controls.Add(BFencerName);
            Controls.Add(AFencerName);
            Controls.Add(manualSchoolInput);
            Controls.Add(bladeCombo);
            Controls.Add(label7);
            Controls.Add(button1);
            Controls.Add(schoolCombo);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "teamRegistration";
            Text = "teamRegistration";
            Load += teamRegistration_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private ComboBox schoolCombo;
        private Button button1;
        private Label label7;
        private ComboBox bladeCombo;
        private TextBox manualSchoolInput;
        private TextBox AFencerName;
        private TextBox BFencerName;
        private TextBox CFencerName;
        private TextBox AltFencerName;
        private Label label8;
        private TextBox suffixText;
    }
}