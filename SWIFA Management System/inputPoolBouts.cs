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
    }
}
