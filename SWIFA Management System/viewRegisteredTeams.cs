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
                    .Where(t => t.EventId == _eventId && t.Blade == "Foil").OrderBy(t=>t.School).ThenBy(t=>t.suffix).ToList();
                listBoxFoil.DataSource = foilTeams;

                var epeeTeams = db.Teams
                    .Where(t => t.EventId == _eventId && t.Blade == "Epee").OrderBy(t=>t.School).ThenBy(t=>t.suffix).ToList();
                listBoxEpee.DataSource = epeeTeams;

                var sabreTeams = db.Teams
                    .Where(t => t.EventId == _eventId && t.Blade == "Sabre").OrderBy(t=>t.School).ThenBy(t=>t.suffix).ToList();
                listBoxSabre.DataSource = sabreTeams;
            }
        }

        private void listBoxFoil_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxFoil.SelectedItem is Team selectedTeam)
            {
                var detailsForm = new teamDetails(selectedTeam);
                detailsForm.TeamDeleted += OnTeamDeleted;
                detailsForm.Show();
            }
        }

        private void listBoxEpee_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxEpee.SelectedItem is Team selectedTeam)
            {
                var detailsForm = new teamDetails(selectedTeam);
                detailsForm.TeamDeleted += OnTeamDeleted;
                detailsForm.Show();
            }
        }

        private void listBoxSabre_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxSabre.SelectedItem is Team selectedTeam)
            {
                var detailsForm = new teamDetails(selectedTeam);
                detailsForm.TeamDeleted += OnTeamDeleted;
                detailsForm.Show();
            }
        }

        private void OnTeamDeleted()
        {
            viewRegisteredTeams_Load(this, EventArgs.Empty);
        }
    }
}
