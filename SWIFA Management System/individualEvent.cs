﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWIFA_Management_System
{
    public partial class individualEvent : Form
    {
        public individualEvent()
        {
            InitializeComponent();
        }

        public void SetSummary(string summary)
        {
            label1.Text = summary;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new teamRegistration();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
