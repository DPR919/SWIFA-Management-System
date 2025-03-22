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
            if (bladeSelection.SelectedItem == null)
            {
                MessageBox.Show("Please select a blade to export", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Prompt the user for the output folder first
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Select a folder to store the output files";
                if (fbd.ShowDialog() != DialogResult.OK || string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    return; // User cancelled or did not select a valid folder
                }
                string outputFolder = fbd.SelectedPath;

                // Now query the database and generate the files using the selected folder
                var selectedBlade = bladeSelection.SelectedItem.ToString();

                using (var db = new EventsDatabaseContext())
                {
                    var pools = db.Pools
                                  .Where(p => p.EventId == _eventId && p.Blade == selectedBlade)
                                  .OrderBy(p => p.PoolNum)
                                  .ToList();

                    int numPools = pools.Count;

                    for (int i = 1; i <= numPools; i++)
                    {
                        Pool currPool = pools[i - 1];
                        var squadsInPool = db.Teams
                            .Where(t => t.PoolId == currPool.PoolId && t.Blade == selectedBlade)
                            .OrderBy(t => t.SeedinPool)
                            .ToList();

                        // Generate the pool summary PDF using the provided output folder
                        generatePoolSummary(selectedBlade, i, squadsInPool, outputFolder);
                    }
                }
            }
        }

        private void generatePoolSummary(string blade, int poolNum, List<Team> teams, string outputFolder)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            List<string> teamNames = teams.Select(t=>t.ToString()).ToList();
            int numSquads = teams.Count;


            QuestPDF.Fluent.Document
                .Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.Letter);
                        page.Margin(0.5f, Unit.Inch);

                        page.Header()
                            .Text($"{bladeSelection} Pool #{poolNum} Summary")
                            .FontSize(28)
                            .Bold();

                        page.Content().Column(column =>
                        {
                            column.Item().Height(20);
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(20);
                                    columns.ConstantColumn(120);
                                    for (int i = 0; i < numSquads; i++)
                                    {
                                        columns.RelativeColumn(1);
                                    }
                                });

                                table.Cell().Element(CellStyle).Text("#").Bold().AlignCenter();
                                table.Cell().Element(CellStyle).Text("Squad").Bold().AlignCenter();

                                for (int i = 0; i < numSquads; i++)
                                    table.Cell().Element(CellStyle).Text($"{i + 1}").Bold().AlignCenter();

                                for (int i = 0; i < numSquads; i++)
                                {
                                    table.Cell().Element(CellStyle).Text($"{i + 1}").Bold().AlignCenter();
                                    table.Cell().Element(CellStyle).Text(teamNames[i]).AlignCenter();

                                    for (int j = 0; j < numSquads; j++)
                                    {
                                        if (i == j)
                                            table.Cell().Border(1).Background(Colors.Grey.Darken1).AlignCenter();
                                        else
                                            table.Cell().Element(CellStyle).Text(" ").AlignCenter();
                                    }
                                }

                                static QuestPDF.Infrastructure.IContainer CellStyle(QuestPDF.Infrastructure.IContainer container)
                                    => container.Border(1).Padding(8);
                            });
                            column.Item().Height(20);
                            column.Item().Row(row =>
                            {
                                row.RelativeItem();
                                row.ConstantItem(522).Image("./poolBoutSequence.png");
                                row.RelativeItem();
                            });
                        });
                    });
                }).GeneratePdf(Path.Combine(outputFolder, $"poolSummary-{poolNum}.pdf"));
        }
    }
}
