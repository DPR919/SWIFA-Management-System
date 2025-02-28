using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SWIFA_Management_System.Models;

namespace SWIFA_Management_System
{
    public partial class currentEventCard : UserControl
    {
        public int EventID { get; set; }
        public currentEventCard()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var db = new EventsDatabaseContext())
            {
                var ev = db.Events.Find(EventID);
                ev.Current = false;
                db.SaveChanges();
            }
            this.Parent.Controls.Remove(this);
            MessageBox.Show("Event has been removed from the list of ongoing events.");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form = new individualEvent();

            string summary = $"{this.EventName} - {this.EventLocation} - {this.EventDate.ToShortDateString()}";

            form.SetSummary(summary);

            form.Show();
        }

        public string EventName
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        public DateTime EventDate
        {
            get { return DateTime.Parse(label2.Text); }
            set { label2.Text = value.ToString(); }
        }

        public string EventLocation
        {
            get { return label3.Text; }
            set { label3.Text = value; }
        }
    }
}
