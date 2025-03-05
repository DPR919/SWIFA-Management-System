namespace SWIFA_Management_System
{
    partial class poolAssignments
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
            poolsLayout = new TableLayoutPanel();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(301, 32);
            label1.TabIndex = 0;
            label1.Text = "View pool assignments for:";
            // 
            // bladeSelection
            // 
            bladeSelection.FormattingEnabled = true;
            bladeSelection.Items.AddRange(new object[] { "Foil", "Epee", "Sabre" });
            bladeSelection.Location = new Point(319, 12);
            bladeSelection.Name = "bladeSelection";
            bladeSelection.Size = new Size(96, 33);
            bladeSelection.TabIndex = 1;
            bladeSelection.SelectedIndexChanged += bladeSelection_SelectedIndexChanged;
            // 
            // poolsLayout
            // 
            poolsLayout.ColumnCount = 3;
            poolsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            poolsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            poolsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            poolsLayout.Location = new Point(12, 56);
            poolsLayout.Name = "poolsLayout";
            poolsLayout.RowCount = 3;
            poolsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            poolsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            poolsLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            poolsLayout.Size = new Size(776, 382);
            poolsLayout.TabIndex = 2;
            // 
            // poolAssignments
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(poolsLayout);
            Controls.Add(bladeSelection);
            Controls.Add(label1);
            Name = "poolAssignments";
            Text = "poolAssignments";
            Load += poolAssignments_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox bladeSelection;
        private TableLayoutPanel poolsLayout;
    }
}