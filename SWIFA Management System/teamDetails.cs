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
                teamName.Text = _team.ToString();
                AFencer.Text = _team.AFencer;
                BFencer.Text = _team.BFencer;
                CFencer.Text = _team.CFencer;
                AltFencer.Text = _team.AltFencer;
            } else
            {
                MessageBox.Show("No team data was provided.");
                this.Close();
            }
        }
    }
}
