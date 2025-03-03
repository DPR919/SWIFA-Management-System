namespace SWIFA_Management_System
{
    partial class poolGeneration
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
            label3 = new Label();
            comboBox1 = new ComboBox();
            selectedBladeList = new ListBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(213, 32);
            label1.TabIndex = 0;
            label1.Text = "Generate pools for";
            // 
            // bladeSelection
            // 
            bladeSelection.FormattingEnabled = true;
            bladeSelection.Items.AddRange(new object[] { "Foil", "Epee", "Sabre" });
            bladeSelection.Location = new Point(231, 12);
            bladeSelection.Name = "bladeSelection";
            bladeSelection.Size = new Size(96, 33);
            bladeSelection.TabIndex = 1;
            bladeSelection.SelectedIndexChanged += bladeSelection_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(333, 9);
            label2.Name = "label2";
            label2.Size = new Size(162, 32);
            label2.TabIndex = 2;
            label2.Text = "with a total of";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(603, 9);
            label3.Name = "label3";
            label3.Size = new Size(72, 32);
            label3.TabIndex = 3;
            label3.Text = "pools";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" });
            comboBox1.Location = new Point(501, 12);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(96, 33);
            comboBox1.TabIndex = 4;
            // 
            // selectedBladeList
            // 
            selectedBladeList.FormattingEnabled = true;
            selectedBladeList.ItemHeight = 25;
            selectedBladeList.Location = new Point(12, 44);
            selectedBladeList.Name = "selectedBladeList";
            selectedBladeList.Size = new Size(180, 379);
            selectedBladeList.TabIndex = 5;
            // 
            // poolGeneration
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(selectedBladeList);
            Controls.Add(comboBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(bladeSelection);
            Controls.Add(label1);
            Name = "poolGeneration";
            Text = "poolGeneration";
            Load += poolGeneration_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox bladeSelection;
        private Label label2;
        private Label label3;
        private ComboBox comboBox1;
        private ListBox selectedBladeList;
    }
}