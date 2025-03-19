using Microsoft.EntityFrameworkCore.Query.Internal;
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
    public partial class inputPoolBouts : Form
    {
        private int _eventId;
        private string _selectedBlade;
        public inputPoolBouts(int eventId)
        {
            InitializeComponent();
            _eventId = eventId;
        }

        private void bladeSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedBlade = bladeSelection.SelectedItem.ToString();
            using (var db = new EventsDatabaseContext())
            {
                var pools = db.Pools
                        .Where(p => p.Blade == _selectedBlade && p.EventId == _eventId)
                        .OrderBy(p => p.PoolNum)
                        .ToList();

                poolSelection.DataSource = pools;
                poolSelection.DisplayMember = "PoolNum";
                poolSelection.ValueMember = "PoolId";
            }
        }

        private void poolSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedPoolId = ((Pool)poolSelection.SelectedItem).PoolId;

            using (var db = new EventsDatabaseContext())
            {
                var teamsInPools = db.Teams
                    .Where(t => t.PoolId == selectedPoolId)
                    .OrderBy(t => t.TeamId)
                    .ToList();
                teamLeftBox.DataSource = teamsInPools;
                teamRightBox.DataSource = teamsInPools;

                teamLeftBox.SelectedIndex = -1;
                teamRightBox.SelectedIndex = -1;

                teamRightBox.BindingContext = new BindingContext();
            }
        }

        private void teamLeftBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (teamLeftBox.SelectedItem is Team selectedTeam)
            {
                // For each combobox, include the primary fencer and the alternate fencer.
                if (selectedTeam.AltFencer != null)
                {
                    leftA.DataSource = new List<string> { selectedTeam.AFencer, selectedTeam.AltFencer };
                    leftB.DataSource = new List<string> { selectedTeam.BFencer, selectedTeam.AltFencer };
                    leftC.DataSource = new List<string> { selectedTeam.CFencer, selectedTeam.AltFencer };
                }
                else
                {
                    leftA.DataSource = new List<string> { selectedTeam.AFencer };
                    leftB.DataSource = new List<string> { selectedTeam.BFencer };
                    leftC.DataSource = new List<string> { selectedTeam.CFencer };
                }
            }
            else
            {
                leftA.DataSource = null;
                leftB.DataSource = null;
                leftC.DataSource = null;
            }
        }

        private void teamRightBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (teamRightBox.SelectedItem is Team selectedTeam)
            {
                // For each combobox, include the primary fencer and the alternate fencer.
                if (selectedTeam.AltFencer != null)
                {
                    rightA.DataSource = new List<string> { selectedTeam.AFencer, selectedTeam.AltFencer };
                    rightB.DataSource = new List<string> { selectedTeam.BFencer, selectedTeam.AltFencer };
                    rightC.DataSource = new List<string> { selectedTeam.CFencer, selectedTeam.AltFencer };
                }
                else
                {
                    rightA.DataSource = new List<string> { selectedTeam.AFencer };
                    rightB.DataSource = new List<string> { selectedTeam.BFencer };
                    rightC.DataSource = new List<string> { selectedTeam.CFencer };
                }
            }
            else
            {
                rightA.DataSource = null;
                rightB.DataSource = null;
                rightC.DataSource = null;
            }
        }
    }
}
