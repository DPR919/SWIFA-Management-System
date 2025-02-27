using SWIFA_Management_System.Models;
using System;
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
    public partial class eventCreation : Form
    {
        public eventCreation()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string eventName = textBox1.Text;
            DateTime eventDate = dateTimePicker1.Value;

            var newEvent = new Event
            {
                EventName = eventName,
                EventDate = eventDate
            };

            using (var db = new EventsDatabaseContext())
            {
                db.Events.Add(newEvent);
                db.SaveChanges();
            }

            MessageBox.Show("Event created successfully!");
        }
    }
}
