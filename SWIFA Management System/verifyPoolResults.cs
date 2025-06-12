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
            }
            else
            {
                MessageBox.Show("All integrity checks passed.", "Integrity Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void printBoolResult_Click(object sender, EventArgs e)
        {

        }

        private static List<FencerPoolResult> GetFencerResultsForPool(int poolId)
        {
            using var db = new EventsDatabaseContext();

            var raw =
            (
                from m in db.Matches
                where m.PoolId == poolId
                join leftT in db.Teams on m.TeamLeftId equals leftT.TeamId
                join rightT in db.Teams on m.TeamRightId equals rightT.TeamId
                select new
                {
                    LeftSeed = leftT.SeedinPool,
                    LeftName = leftT.ToString(),
                    StripLeft = m.FencerLeftStrip,
                    FencerLeft = m.FencerLeft,
                    ScoreLf = m.ScoreLeft,
                    ScoreRf = m.ScoreRight,

                    RightSeed = rightT.SeedinPool,
                    RightName = rightT.ToString(),
                    StripRight = m.FencerRightStrip,
                    FencerRight = m.FencerRight,
                    ScoreRl = m.ScoreRight,
                    ScoreLl = m.ScoreLeft

                }
            ).ToList();

            var all = raw.Select(r => new
            {
                Seed = r.LeftSeed,
                Squad = r.LeftName,
                Strip = r.StripLeft,
                Name = r.FencerLeft,
                Scored = r.ScoreLf,
                Received = r.ScoreRf,
                Won = r.ScoreLf[0] == 'V'
            })
            .Concat(raw.Select(r => new
            {
                Seed = r.RightSeed,
                Squad = r.RightName,
                Strip = r.StripRight,
                Name = r.FencerRight,
                Scored = r.ScoreRl,
                Received = r.ScoreLl,
                Won = r.ScoreRl[0] == 'V'
            })).ToList();

            var results = all
                .GroupBy(x => new {x.Seed, x.Squad, x.Strip, x.Name })
                .Select(g =>
                {
                    var wins = g.Count(x => x.Won);
                    var bouts = g.Count();
                    var scored = g.Sum(x => int.Parse(x.Scored.Substring(0, 1)));
                    var received = g.Sum(x => int.Parse(x.Received.Substring(0, 1)));

                    return new FencerPoolResult
                    {
                        SquadSeed = (int)g.Key.Seed,
                        SquadName = g.Key.Squad,
                        FencerStrip = g.Key.Strip,
                        FencerName = g.Key.Name,
                        Wins = wins,
                        Losses = bouts - wins,
                        WinPct = bouts > 0 ? (double)wins / bouts : 0.0,
                        TouchesScored = scored,
                        TouchesReceived = received
                    };
                })
                .OrderBy(r => r.SquadSeed)
                .ThenBy(r => r.FencerStrip)
                .ToList();

            return results;
        }
    }
}
