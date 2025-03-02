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
    public partial class viewRegisteredTeams : Form
    {
        private int _eventId;
        public viewRegisteredTeams(int eventId)
        {
            InitializeComponent();
            _eventId = eventId;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBoxSabre_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBoxEpee_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBoxFoil_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void viewRegisteredTeams_Load(object sender, EventArgs e)
        {
            using (var db = new EventsDatabaseContext())
            {
                var foilTeams = db.Teams
                    .Where(t => t.EventId == _eventId && t.Blade == "Foil")
                    .Select(t => t.School + " " + t.suffix)
                    .ToList();

                var epeeTeams = db.Teams
                    .Where(t => t.EventId == _eventId && t.Blade == "Epee")
                    .Select(t => t.School + " " + t.suffix)
                    .ToList();

                var sabreTeams = db.Teams
                    .Where(t => t.EventId == _eventId && t.Blade == "Sabre")
                    .Select(t => t.School + " " + t.suffix)
                    .ToList();

                listBoxFoil.DataSource = foilTeams;
                listBoxEpee.DataSource = epeeTeams;
                listBoxSabre.DataSource = sabreTeams;
            }
        }
    }
}
