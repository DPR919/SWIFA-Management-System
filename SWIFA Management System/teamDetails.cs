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

        }
    }
}
