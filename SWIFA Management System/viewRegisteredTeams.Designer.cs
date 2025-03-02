namespace SWIFA_Management_System
{
    partial class viewRegisteredTeams
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
            listBoxFoil = new ListBox();
            listBoxEpee = new ListBox();
            listBoxSabre = new ListBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(77, 48);
            label1.TabIndex = 0;
            label1.Text = "Foil";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(262, 9);
            label2.Name = "label2";
            label2.Size = new Size(97, 48);
            label2.TabIndex = 1;
            label2.Text = "Epee";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(532, 9);
            label3.Name = "label3";
            label3.Size = new Size(110, 48);
            label3.TabIndex = 2;
            label3.Text = "Sabre";
            // 
            // listBoxFoil
            // 
            listBoxFoil.FormattingEnabled = true;
            listBoxFoil.ItemHeight = 25;
            listBoxFoil.Location = new Point(12, 60);
            listBoxFoil.Name = "listBoxFoil";
            listBoxFoil.Size = new Size(227, 379);
            listBoxFoil.TabIndex = 3;
            listBoxFoil.SelectedIndexChanged += listBoxFoil_SelectedIndexChanged;
            listBoxFoil.DoubleClick += listBoxFoil_DoubleClick;
            // 
            // listBoxEpee
            // 
            listBoxEpee.FormattingEnabled = true;
            listBoxEpee.ItemHeight = 25;
            listBoxEpee.Location = new Point(262, 60);
            listBoxEpee.Name = "listBoxEpee";
            listBoxEpee.Size = new Size(227, 379);
            listBoxEpee.TabIndex = 4;
            listBoxEpee.SelectedIndexChanged += listBoxEpee_SelectedIndexChanged;
            listBoxEpee.DoubleClick += listBoxEpee_DoubleClick;
            // 
            // listBoxSabre
            // 
            listBoxSabre.FormattingEnabled = true;
            listBoxSabre.ItemHeight = 25;
            listBoxSabre.Location = new Point(532, 60);
            listBoxSabre.Name = "listBoxSabre";
            listBoxSabre.Size = new Size(227, 379);
            listBoxSabre.TabIndex = 5;
            listBoxSabre.SelectedIndexChanged += listBoxSabre_SelectedIndexChanged;
            listBoxSabre.DoubleClick += listBoxSabre_DoubleClick;
            // 
            // viewRegisteredTeams
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(listBoxSabre);
            Controls.Add(listBoxEpee);
            Controls.Add(listBoxFoil);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "viewRegisteredTeams";
            Text = "viewRegisteredTeams";
            Load += viewRegisteredTeams_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private ListBox listBoxFoil;
        private ListBox listBoxEpee;
        private ListBox listBoxSabre;
    }
}