using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SWIFA_Management_System.Models;

namespace SWIFA_Management_System
{
    public partial class pastEvents : Form
    {
        public pastEvents()
        {
            InitializeComponent();
        }

        private void pastEvents_Load(object sender, EventArgs e)
        {
            using (var db = new EventsDatabaseContext())
            {
                var pastEvents = db.Events
                    .Where(ev => ev.Current == false)
                    .ToList();
                flowLayoutPanel1.Controls.Clear();
                foreach (var ev in pastEvents)
                {
                    var card = new pastEventCard
                    {
                        EventID = ev.Id,
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
