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
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Companion;
using System.Diagnostics;

namespace SWIFA_Management_System
{
    public partial class poolAssignments : Form
    {
        private int _eventId;
        public poolAssignments(int eventId)
        {
            InitializeComponent();
            _eventId = eventId;
        }

        private void poolAssignments_Load(object sender, EventArgs e)
        {

        }

        private void bladeSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedBlade = bladeSelection.SelectedItem.ToString();

            poolsLayout.Controls.Clear();

            using (var db = new EventsDatabaseContext())
            {
                var pools = db.Pools.Where(p => p.EventId == _eventId && p.Blade == selectedBlade)
                    .OrderBy(p => p.PoolNum).ToList();

                for (int i = 0; i < pools.Count; i++)
                {
                    var pool = pools[i];
                    var lb = new ListBox
                    {
                        Dock = DockStyle.Fill
                    };

                    var teamsInPool = db.Teams
                    .Where(t => t.PoolId == pool.PoolId)
                    .OrderBy(t => t.School)
                    .ThenBy(t => t.suffix)
                    .ToList();

                    foreach (var team in teamsInPool)
                    {
                        lb.Items.Add(team);
                    }

                    int row = i / 3;
                    int col = i % 3;
                    poolsLayout.Controls.Add(lb, col, row);
                }
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Content().Text("SWIFA Management System");
                });
            }).GeneratePdf("output.pdf");
            Process.Start(new ProcessStartInfo { FileName = "output.pdf", UseShellExecute = true });
        }
    }
}
