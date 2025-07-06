using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SWIFA_Management_System.Models;
using SWIFA_Management_System.Utilities;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
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
            if (bladeSelection.SelectedItem == null || poolSelection.SelectedItem == null)
            {
                MessageBox.Show("Please select both a blade and a pool before exporting", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var fbd = new FolderBrowserDialog())
            {
                fbd.Description = "Select a folder to store the output files";
                if (fbd.ShowDialog() != DialogResult.OK || string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    return;
                }
                string outputFolder = fbd.SelectedPath;

                var results = GetFencerResultsForPool(_selectedPoolId);

                using (var db = new EventsDatabaseContext())
                {
                    var squadsInPool = db.Teams.Where(t => t.PoolId == _selectedPoolId).ToList();
                    int numSquads = squadsInPool.Count;
                    var seedLookup = squadsInPool.ToDictionary(t => t.TeamId, t => t.SeedinPool);

                    var boutOrder = poolBoutSequenceProvider.Sequences[numSquads];
                    var numBouts = boutOrder.Count;

                    var blade = bladeSelection.SelectedItem.ToString();
                    var poolNum = ((Pool)poolSelection.SelectedItem).PoolNum;
                    var file = Path.Combine(outputFolder, $"{blade}_Pool{poolNum}_FencerResults.pdf");


                    var matches = db.Matches
                        .Where(m => m.PoolId == _selectedPoolId)
                        .AsEnumerable()
                        .ToList();

                    var boutLookup = new Dictionary<(int Seed, string Strip, int Bout), string>();
                    
                    for (int boutIndex = 0; boutIndex < boutOrder.Count; boutIndex++)
                    {
                        var (leftSeed, rightSeed) = boutOrder[boutIndex];

                        var m = matches.FirstOrDefault(x =>
                            (seedLookup[x.TeamLeftId] == leftSeed && seedLookup[x.TeamRightId] == rightSeed)
                            ||
                            (seedLookup[x.TeamLeftId] == rightSeed && seedLookup[x.TeamRightId] == leftSeed)
                        );

                        if (m == null) continue;

                        var codeL = (m.ScoreLeft.StartsWith("V") ? "V" : "D") + m.ScoreLeft.Substring(1);
                        boutLookup[(leftSeed, m.FencerLeftStrip, boutIndex)] = codeL;

                        var codeR = (m.ScoreRight.StartsWith("V") ? "V" : "D") + m.ScoreRight.Substring(1);
                        boutLookup[(rightSeed, m.FencerRightStrip, boutIndex)] = codeR;
                    }

                    QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
                    QuestPDF.Fluent.Document.Create(container =>
                    {
                        container.Page(page =>
                        {
                            page.Margin(10);
                            page.Size(PageSizes.Letter.Landscape());     // ← horizontal orientation
                            page.DefaultTextStyle(x => x.FontSize(9));

                            // title
                            page.Header().AlignCenter().Text($"{blade} – Pool #{poolNum} – Details")
                                         .SemiBold().FontSize(16);

                            page.Content().Table(tbl =>
                            {
                                // ── column definitions ─────────────────────────────────────────
                                tbl.ColumnsDefinition(cols =>
                                {
                                    cols.ConstantColumn(30);         // "#"
                                    cols.ConstantColumn(80);         // "Squad"
                                    cols.ConstantColumn(30);         // "Strip"
                                    cols.RelativeColumn(3);          // "Name"
                                                                     // now one ConstantColumn for each bout in order:
                                    for (int i = 0; i < boutOrder.Count; i++)
                                    {
                                        cols.RelativeColumn(1);     // A
                                        cols.RelativeColumn(1);     // B
                                        cols.RelativeColumn(1);     // C
                                        cols.RelativeColumn(1);     // D (if applicable)
                                    }
                                });

                                // ── header row ─────────────────────────────────────────────────
                                tbl.Header(h =>
                                {
                                    h.Cell().RowSpan(2).Text("#");
                                    h.Cell().RowSpan(2).Text("Squad");
                                    h.Cell().RowSpan(2).Text("S");
                                    h.Cell().RowSpan(2).Text("Name");
                                    for (int i = 0; i < numBouts; i++)
                                    {
                                        var (l, r) = boutOrder[i];
                                        h.Cell()
                                         .ColumnSpan(4)
                                         .AlignCenter()
                                         .Text($"{l}-{r}");
                                    };

                                    h.Cell();
                                    h.Cell();
                                    h.Cell();
                                    h.Cell();

                                    for (int i = 0; i < boutOrder.Count; i++)
                                    {
                                        h.Cell().AlignCenter().Text("A");
                                        h.Cell().AlignCenter().Text("B");
                                        h.Cell().AlignCenter().Text("C");
                                        h.Cell().AlignCenter().Text("D");
                                    }


                                });

                                // ── one row per fencer ──────────────────────────────────────────
                                foreach (var team in squadsInPool.OrderBy(t => t.SeedinPool))
                                {
                                    int seed = team.SeedinPool.Value;

                                    var fencers = new List<(string Strip, string Name)>
                                    {
                                        ("A", team.AFencer),
                                        ("B", team.BFencer),
                                        ("C", team.CFencer)
                                    };

                                    if (!string.IsNullOrWhiteSpace(team.AltFencer))
                                    {
                                        fencers.Add(("D", team.AltFencer));
                                    };

                                    int rowSpan = fencers.Count;
                                    bool firstRow = true;

                                    foreach (var (strip, name) in fencers)
                                    {
                                        if (firstRow)
                                        {
                                            tbl.Cell().RowSpan((uint)rowSpan).Text(team.SeedinPool.ToString());
                                            tbl.Cell().RowSpan((uint)rowSpan).Text(team.ToString());
                                            firstRow = false;
                                        }
                                        else
                                        {
                                            tbl.Cell().Text("");
                                            tbl.Cell().Text("");
                                        }
                                        tbl.Cell().Text(strip);
                                        tbl.Cell().Text(name);

                                        for (int idx = 0; idx < boutOrder.Count; idx++)
                                        {
                                            if (boutLookup.TryGetValue((team.SeedinPool.Value, strip, idx), out var code))
                                            {
                                                tbl.Cell().AlignCenter().Text(code);
                                            }
                                            else
                                            {
                                                tbl.Cell().AlignCenter().Text("-");
                                            }
                                        }
                                    }
                                }
                            });
                        });
                    }).GeneratePdf(Path.Combine(outputFolder, $"Pool{poolNum}_Details.pdf"));
                    MessageBox.Show($"Pool results PDF written to:\n{file}", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                };
            }
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

                    var scored = g.Sum(x => int.Parse(x.Scored.Substring(1, 1)));
                    var received = g.Sum(x => int.Parse(x.Received.Substring(1, 1)));

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
