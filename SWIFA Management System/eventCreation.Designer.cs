namespace SWIFA_Management_System
{
    partial class eventCreation
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
            textBox1 = new TextBox();
            label3 = new Label();
            dateTimePicker1 = new DateTimePicker();
            save_button = new Button();
            label4 = new Label();
            label5 = new Label();
            textBox2 = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(214, 25);
            label1.TabIndex = 0;
            label1.Text = "You are creating an event.";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F);
            label2.Location = new Point(12, 44);
            label2.Name = "label2";
            label2.Size = new Size(180, 38);
            label2.TabIndex = 1;
            label2.Text = "Event Name: ";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(198, 52);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(150, 31);
            textBox1.TabIndex = 2;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14F);
            label3.Location = new Point(12, 122);
            label3.Name = "label3";
            label3.Size = new Size(164, 38);
            label3.TabIndex = 3;
            label3.Text = "Event Date: ";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(182, 128);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(300, 31);
            dateTimePicker1.TabIndex = 4;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // save_button
            // 
            save_button.Location = new Point(12, 259);
            save_button.Name = "save_button";
            save_button.Size = new Size(112, 34);
            save_button.TabIndex = 5;
            save_button.Text = "Create";
            save_button.UseVisualStyleBackColor = true;
            save_button.Click += saveButton_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(29, 206);
            label4.Name = "label4";
            label4.Size = new Size(0, 25);
            label4.TabIndex = 6;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14F);
            label5.Location = new Point(12, 193);
            label5.Name = "label5";
            label5.Size = new Size(210, 38);
            label5.TabIndex = 7;
            label5.Text = "Event Location: ";
            label5.Click += label5_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(228, 201);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(150, 31);
            textBox2.TabIndex = 8;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // eventCreation
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBox2);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(save_button);
            Controls.Add(dateTimePicker1);
            Controls.Add(label3);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "eventCreation";
            Text = "Create an Event";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private Label label3;
        private DateTimePicker dateTimePicker1;
        private Button save_button;
        private Label label4;
        private Label label5;
        private TextBox textBox2;
    }
}