﻿using Microsoft.EntityFrameworkCore.Query.Internal;
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
        private int _selectedPoolId;
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
            _selectedPoolId = ((Pool)poolSelection.SelectedItem).PoolId;

            using (var db = new EventsDatabaseContext())
            {
                var teamsInPools = db.Teams
                    .Where(t => t.PoolId == _selectedPoolId)
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
                // probably will never reach this part
                rightA.DataSource = null;
                rightB.DataSource = null;
                rightC.DataSource = null;
            }
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            var leftTeam = (Team)teamLeftBox.SelectedValue;
            var rightTeam = (Team)teamRightBox.SelectedValue;
            if (leftTeam == null || rightTeam == null)
            {
                MessageBox.Show("Please select both teams before submitting.");
                return;
            }

            int leftTeamId = leftTeam.TeamId;
            int rightTeamId = rightTeam.TeamId;
            if (leftTeamId == rightTeamId)
            {
                MessageBox.Show("A team should not fence itself.");
                return;
            }



            // C strip
            string cLeftFencer = leftC.SelectedItem?.ToString() ?? "";
            string cRightFencer = rightC.SelectedItem?.ToString() ?? "";
            string cLeftScore = leftCScore.Text;
            string cRightScore = rightCScore.Text;

            // B strip
            string bLeftFencer = leftB.SelectedItem?.ToString() ?? "";
            string bRightFencer = rightB.SelectedItem?.ToString() ?? "";
            string bLeftScore = leftBScore.Text;
            string bRightScore = rightBScore.Text;

            // A strip
            string aLeftFencer = leftA.SelectedItem?.ToString() ?? "";
            string aRightFencer = rightA.SelectedItem?.ToString() ?? "";
            string aLeftScore = leftAScore.Text;
            string aRightScore = rightAScore.Text;

            using (var db = new EventsDatabaseContext())
            {
                var existingMatch = db.Matches
                    .FirstOrDefault(m => m.PoolId == _selectedPoolId && ((m.TeamLeftId == leftTeamId && m.TeamRightId == rightTeamId)
                 || (m.TeamLeftId == rightTeamId && m.TeamRightId == leftTeamId)));

                if (existingMatch != null)
                {
                    MessageBox.Show("This matchup is already recorded.");
                    return;
                }

                var matchC = new Match
                {
                    TeamLeftId = leftTeamId,
                    TeamRightId = rightTeamId,
                    FencerLeft = cLeftFencer,
                    FencerLeftStrip = "C",
                    FencerRight = cRightFencer,
                    FencerRightStrip = "C",
                    ScoreLeft = cLeftScore,
                    ScoreRight = cRightScore,
                    PoolMatch = true,
                    DEMatch = false,
                    PoolId = _selectedPoolId
                };
                db.Matches.Add(matchC);

                var matchB = new Match
                {
                    TeamLeftId = leftTeamId,
                    TeamRightId = rightTeamId,
                    FencerLeft = bLeftFencer,
                    FencerLeftStrip = "B",
                    FencerRight = bRightFencer,
                    FencerRightStrip = "B",
                    ScoreLeft = bLeftScore,
                    ScoreRight = bRightScore,
                    PoolMatch = true,
                    DEMatch = false,
                    PoolId = _selectedPoolId
                };
                db.Matches.Add(matchB);

                var matchA = new Match
                {
                    TeamLeftId = leftTeamId,
                    TeamRightId = rightTeamId,
                    FencerLeft = aLeftFencer,
                    FencerLeftStrip = "A",
                    FencerRight = aRightFencer,
                    FencerRightStrip = "A",
                    ScoreLeft = aLeftScore,
                    ScoreRight = aRightScore,
                    PoolMatch = true,
                    DEMatch = false,
                    PoolId = _selectedPoolId
                };
                db.Matches.Add(matchA);

                db.SaveChanges();
            }
            MessageBox.Show("Encounter recorded successfully!");
            // clear fields
            clearFields();
        }

        private void clearFields()
        {
            leftCScore.Clear();
            rightCScore.Clear();
            leftBScore.Clear();
            rightBScore.Clear();
            leftAScore.Clear();
            rightAScore.Clear();

            teamLeftBox.SelectedIndex = -1;
            teamRightBox.SelectedIndex = -1;
        }
    }
}
