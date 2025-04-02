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
    public partial class individualEvent : Form
    {
        public int EvntId { get; private set; }
        public individualEvent(int evntId)
        {
            InitializeComponent();
            EvntId = evntId;
        }

        public void SetSummary(string summary)
        {
            label1.Text = summary;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new teamRegistration(EvntId);
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form = new inputPoolBouts(EvntId);
            form.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void individualEvent_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var viewTeams = new viewRegisteredTeams(this.EvntId);
            viewTeams.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var poolGen = new poolGeneration(this.EvntId);
            poolGen.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var viewPools = new poolAssignments(this.EvntId);
            viewPools.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var verifyPoolResults = new verifyPoolResults(this.EvntId);
            verifyPoolResults.Show();
        }
    }
}
