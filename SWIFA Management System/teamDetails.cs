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
    public partial class teamDetails : Form
    {
        public event Action TeamDeleted;
        private Team _team;
        public teamDetails(Team team)
        {
            InitializeComponent();
            _team = team;
        }

        private void teamDetails_Load(object sender, EventArgs e)
        {
            if (_team != null)
            {
                teamName.UseMnemonic = false;
                teamName.Text = _team.ToString();
                AFencer.Text = _team.AFencer;
                BFencer.Text = _team.BFencer;
                CFencer.Text = _team.CFencer;
                AltFencer.Text = _team.AltFencer;
            }
            else
            {
                MessageBox.Show("No team data was provided.");
                this.Close();
            }
        }

        private void teamName_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            using (var db = new EventsDatabaseContext())
            {
                var teamToDelete = db.Teams.Find(_team.TeamId);
                if (teamToDelete != null)
                {
                    db.Teams.Remove(teamToDelete);
                    db.SaveChanges();
                    MessageBox.Show("Team deleted successfully.");
                    this.Close();
                }
            }
            TeamDeleted?.Invoke();
            this.Close();
        }
    }
}
