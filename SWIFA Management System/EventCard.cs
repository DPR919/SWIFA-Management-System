using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWIFA_Management_System
{
    public partial class EventCard : UserControl
    {
        public EventCard()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

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
