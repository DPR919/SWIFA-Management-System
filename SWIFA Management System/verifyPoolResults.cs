using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
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
    public partial class verifyPoolResults : Form
    {
        private int _eventId;
        private string _selectedBlade;
        private int _selectedPoolId;
        public verifyPoolResults(int eventId)
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
            _selectedPoolId = ((Pool)poolSelection.SelectedItem).PoolId;
        }

        private void verifyButton_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            using (var db = new EventsDatabaseContext())
            {
                var duplicatePairs = db.Matches
                    .Where(m => m.PoolId == _selectedPoolId)
                    .Select(m => new
                    {
                        T1 = m.TeamLeftId < m.TeamRightId ? m.TeamLeftId : m.TeamRightId,
                        T2 = m.TeamLeftId < m.TeamRightId ? m.TeamRightId : m.TeamLeftId
                    })
                    .GroupBy(x => new { x.T1, x.T2 })
                    .Select(g => new
                    {
                        TeamPair = g.Key,
                        MatchCount = g.Count()
                    })
                    .Where(x => x.MatchCount > 3)
                    .ToList();

                if (duplicatePairs.Any())
                {
                    sb.AppendLine("Duplicate match count violation:");
                    foreach (var item in duplicatePairs)
                    {
                        sb.AppendLine($"Team pair ({item.TeamPair.T1}, {item.TeamPair.T2}): {item.MatchCount} matches");
                    }
                }

                var pool = db.Pools.Find(_selectedPoolId);
                int twoFencers = pool.TwoFencerSquadCount;
                var squadsInPool = db.Teams.Where(t => t.PoolId == _selectedPoolId).ToList();
                int numSquads = squadsInPool.Count;

                // Calculate the expected number of matches using your formula:
                int expectedMatches = (numSquads * (numSquads - 1) / 2 * 3) - (twoFencers * (numSquads - 1));

                // Retrieve the actual number of matches for the selected pool.
                int actualMatches = db.Matches.Count(m => m.PoolId == _selectedPoolId);

                if (actualMatches != expectedMatches)
                {
                    sb.AppendLine($"Pool {_selectedPoolId} integrity error: {actualMatches} matches found, but expected {expectedMatches}.");
                }
            }
            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString(), "Inconsistencies Detected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } else
            {
                MessageBox.Show("All integrity checks passed.", "Integrity Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
