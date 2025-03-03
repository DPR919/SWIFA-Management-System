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
    public partial class poolGeneration : Form
    {
        private int _eventId;
        public poolGeneration(int eventId)
        {
            InitializeComponent();
            _eventId = eventId;
        }

        private void poolGeneration_Load(object sender, EventArgs e)
        {
            selectedBladeList.Visible = false;
        }

        private void bladeSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedBlade = bladeSelection.SelectedItem.ToString();

            using (var db = new EventsDatabaseContext())
            {
                var teams = db.Teams.Where(t=>t.EventId==_eventId && t.Blade==selectedBlade)
                    .OrderBy(t=>t.School).ThenBy(t=>t.suffix).ToList();
                selectedBladeList.DataSource = teams;
            }
            selectedBladeList.Visible = true;
        }
    }
}
