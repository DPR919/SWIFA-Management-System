﻿namespace SWIFA_Management_System
{
    partial class verifyPoolResults
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
            bladeSelection = new ComboBox();
            label2 = new Label();
            poolSelection = new ComboBox();
            verifyButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(242, 32);
            label1.TabIndex = 1;
            label1.Text = "Verify pool results for";
            // 
            // bladeSelection
            // 
            bladeSelection.FormattingEnabled = true;
            bladeSelection.Items.AddRange(new object[] { "Foil", "Epee", "Sabre" });
            bladeSelection.Location = new Point(260, 12);
            bladeSelection.Name = "bladeSelection";
            bladeSelection.Size = new Size(86, 33);
            bladeSelection.TabIndex = 2;
            bladeSelection.SelectedIndexChanged += bladeSelection_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(352, 9);
            label2.Name = "label2";
            label2.Size = new Size(81, 32);
            label2.TabIndex = 3;
            label2.Text = "Pool #";
            // 
            // poolSelection
            // 
            poolSelection.FormattingEnabled = true;
            poolSelection.Location = new Point(439, 12);
            poolSelection.Name = "poolSelection";
            poolSelection.Size = new Size(86, 33);
            poolSelection.TabIndex = 4;
            poolSelection.SelectedIndexChanged += poolSelection_SelectedIndexChanged;
            // 
            // verifyButton
            // 
            verifyButton.Location = new Point(12, 44);
            verifyButton.Name = "verifyButton";
            verifyButton.Size = new Size(242, 66);
            verifyButton.TabIndex = 5;
            verifyButton.Text = "Verify Pool Results Integrity";
            verifyButton.UseVisualStyleBackColor = true;
            verifyButton.Click += verifyButton_Click;
            // 
            // verifyPoolResults
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(verifyButton);
            Controls.Add(poolSelection);
            Controls.Add(label2);
            Controls.Add(bladeSelection);
            Controls.Add(label1);
            Name = "verifyPoolResults";
            Text = "verifyPoolResults";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox bladeSelection;
        private Label label2;
        private ComboBox poolSelection;
        private Button verifyButton;
    }
}