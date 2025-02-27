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
    public partial class currentEvents : Form
    {
        public currentEvents()
        {
            InitializeComponent();
        }

        private void OngoingEvents_Load(object sender, EventArgs e)
        {
            using (var db = new EventsDatabaseContext())
            {
                var ongoingEvents = db.Events
                    .Where(ev => ev.Current==true)
                    .ToList();
                flowLayoutPanel1.Controls.Clear();

                foreach (var ev in ongoingEvents)
                {
                    var card = new EventCard
                    {
                        EventName = ev.EventName,
                        EventDate = ev.EventDate,
                        EventLocation = ev.EventLocation
                    };
                    flowLayoutPanel1.Controls.Add(card);
                }
            }
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
