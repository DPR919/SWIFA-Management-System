﻿namespace SWIFA_Management_System
{
    partial class inputPoolBouts
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
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            rightCScore = new TextBox();
            leftCScore = new TextBox();
            leftBScore = new TextBox();
            rightBScore = new TextBox();
            rightAScore = new TextBox();
            leftAScore = new TextBox();
            label8 = new Label();
            teamLeftBox = new ComboBox();
            teamRightBox = new ComboBox();
            leftC = new ComboBox();
            leftB = new ComboBox();
            leftA = new ComboBox();
            rightC = new ComboBox();
            rightB = new ComboBox();
            rightA = new ComboBox();
            submitButton = new Button();
            label9 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(248, 32);
            label1.TabIndex = 0;
            label1.Text = "Input a bout sheet for";
            // 
            // bladeSelection
            // 
            bladeSelection.FormattingEnabled = true;
            bladeSelection.Items.AddRange(new object[] { "Foil", "Epee", "Sabre" });
            bladeSelection.Location = new Point(266, 12);
            bladeSelection.Name = "bladeSelection";
            bladeSelection.Size = new Size(86, 33);
            bladeSelection.TabIndex = 1;
            bladeSelection.SelectedIndexChanged += bladeSelection_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(358, 9);
            label2.Name = "label2";
            label2.Size = new Size(81, 32);
            label2.TabIndex = 2;
            label2.Text = "Pool #";
            // 
            // poolSelection
            // 
            poolSelection.FormattingEnabled = true;
            poolSelection.Location = new Point(445, 12);
            poolSelection.Name = "poolSelection";
            poolSelection.Size = new Size(86, 33);
            poolSelection.TabIndex = 3;
            poolSelection.SelectedIndexChanged += poolSelection_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(358, 137);
            label3.Name = "label3";
            label3.Size = new Size(84, 32);
            label3.TabIndex = 4;
            label3.Text = "C Strip";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(358, 226);
            label4.Name = "label4";
            label4.Size = new Size(83, 32);
            label4.TabIndex = 5;
            label4.Text = "B Strip";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(358, 315);
            label5.Name = "label5";
            label5.Size = new Size(84, 32);
            label5.TabIndex = 6;
            label5.Text = "A Strip";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(12, 91);
            label6.Name = "label6";
            label6.Size = new Size(118, 32);
            label6.TabIndex = 7;
            label6.Text = "Team Left";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(654, 91);
            label7.Name = "label7";
            label7.Size = new Size(134, 32);
            label7.TabIndex = 8;
            label7.Text = "Team Right";
            // 
            // rightCScore
            // 
            rightCScore.Location = new Point(448, 140);
            rightCScore.Name = "rightCScore";
            rightCScore.Size = new Size(47, 31);
            rightCScore.TabIndex = 9;
            // 
            // leftCScore
            // 
            leftCScore.Location = new Point(305, 140);
            leftCScore.Name = "leftCScore";
            leftCScore.Size = new Size(47, 31);
            leftCScore.TabIndex = 10;
            // 
            // leftBScore
            // 
            leftBScore.Location = new Point(305, 229);
            leftBScore.Name = "leftBScore";
            leftBScore.Size = new Size(47, 31);
            leftBScore.TabIndex = 11;
            // 
            // rightBScore
            // 
            rightBScore.Location = new Point(447, 229);
            rightBScore.Name = "rightBScore";
            rightBScore.Size = new Size(47, 31);
            rightBScore.TabIndex = 12;
            // 
            // rightAScore
            // 
            rightAScore.Location = new Point(448, 318);
            rightAScore.Name = "rightAScore";
            rightAScore.Size = new Size(47, 31);
            rightAScore.TabIndex = 13;
            // 
            // leftAScore
            // 
            leftAScore.Location = new Point(305, 318);
            leftAScore.Name = "leftAScore";
            leftAScore.Size = new Size(47, 31);
            leftAScore.TabIndex = 14;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(358, 91);
            label8.Name = "label8";
            label8.Size = new Size(83, 32);
            label8.TabIndex = 15;
            label8.Text = "Scores";
            // 
            // teamLeftBox
            // 
            teamLeftBox.FormattingEnabled = true;
            teamLeftBox.Location = new Point(136, 94);
            teamLeftBox.Name = "teamLeftBox";
            teamLeftBox.Size = new Size(142, 33);
            teamLeftBox.TabIndex = 16;
            teamLeftBox.SelectedIndexChanged += teamLeftBox_SelectedIndexChanged;
            // 
            // teamRightBox
            // 
            teamRightBox.FormattingEnabled = true;
            teamRightBox.Location = new Point(506, 94);
            teamRightBox.Name = "teamRightBox";
            teamRightBox.Size = new Size(142, 33);
            teamRightBox.TabIndex = 17;
            teamRightBox.SelectedIndexChanged += teamRightBox_SelectedIndexChanged;
            // 
            // leftC
            // 
            leftC.FormattingEnabled = true;
            leftC.Location = new Point(109, 140);
            leftC.Name = "leftC";
            leftC.Size = new Size(190, 33);
            leftC.TabIndex = 18;
            // 
            // leftB
            // 
            leftB.FormattingEnabled = true;
            leftB.Location = new Point(109, 229);
            leftB.Name = "leftB";
            leftB.Size = new Size(190, 33);
            leftB.TabIndex = 19;
            // 
            // leftA
            // 
            leftA.FormattingEnabled = true;
            leftA.Location = new Point(109, 318);
            leftA.Name = "leftA";
            leftA.Size = new Size(190, 33);
            leftA.TabIndex = 20;
            // 
            // rightC
            // 
            rightC.FormattingEnabled = true;
            rightC.Location = new Point(501, 140);
            rightC.Name = "rightC";
            rightC.Size = new Size(190, 33);
            rightC.TabIndex = 21;
            // 
            // rightB
            // 
            rightB.FormattingEnabled = true;
            rightB.Location = new Point(500, 229);
            rightB.Name = "rightB";
            rightB.Size = new Size(190, 33);
            rightB.TabIndex = 22;
            // 
            // rightA
            // 
            rightA.FormattingEnabled = true;
            rightA.Location = new Point(501, 318);
            rightA.Name = "rightA";
            rightA.Size = new Size(190, 33);
            rightA.TabIndex = 23;
            // 
            // submitButton
            // 
            submitButton.Location = new Point(331, 380);
            submitButton.Name = "submitButton";
            submitButton.Size = new Size(138, 58);
            submitButton.TabIndex = 24;
            submitButton.Text = "Submit";
            submitButton.UseVisualStyleBackColor = true;
            submitButton.Click += submitButton_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(173, 66);
            label9.Name = "label9";
            label9.Size = new Size(455, 25);
            label9.TabIndex = 25;
            label9.Text = "If there is a squad with 2 fencers, input VF/DF for C strip";
            // 
            // inputPoolBouts
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label9);
            Controls.Add(submitButton);
            Controls.Add(rightA);
            Controls.Add(rightB);
            Controls.Add(rightC);
            Controls.Add(leftA);
            Controls.Add(leftB);
            Controls.Add(leftC);
            Controls.Add(teamRightBox);
            Controls.Add(teamLeftBox);
            Controls.Add(label8);
            Controls.Add(leftAScore);
            Controls.Add(rightAScore);
            Controls.Add(rightBScore);
            Controls.Add(leftBScore);
            Controls.Add(leftCScore);
            Controls.Add(rightCScore);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(poolSelection);
            Controls.Add(label2);
            Controls.Add(bladeSelection);
            Controls.Add(label1);
            Name = "inputPoolBouts";
            Text = "inputPoolBouts";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox bladeSelection;
        private Label label2;
        private ComboBox poolSelection;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox rightCScore;
        private TextBox leftCScore;
        private TextBox leftBScore;
        private TextBox rightBScore;
        private TextBox rightAScore;
        private TextBox leftAScore;
        private Label label8;
        private ComboBox teamLeftBox;
        private ComboBox teamRightBox;
        private ComboBox leftC;
        private ComboBox leftB;
        private ComboBox leftA;
        private ComboBox rightC;
        private ComboBox rightB;
        private ComboBox rightA;
        private Button submitButton;
        private Label label9;
    }
}