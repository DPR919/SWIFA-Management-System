using SWIFA_Management_System.Models;
using SWIFA_Management_System.Utilities;
using System.Data;
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Windows.Forms.VisualStyles;

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
                        string tempFolder = Path.Combine(outputFolder, Guid.NewGuid().ToString());
                        Directory.CreateDirectory(tempFolder);
                        Pool currPool = pools[i - 1];
                        var squadsInPool = db.Teams
                            .Where(t => t.PoolId == currPool.PoolId && t.Blade == selectedBlade)
                            .OrderBy(t => t.SeedinPool)
                            .ToList();
                        int numSquads = squadsInPool.Count;

                        // Generate the pool summary PDF using the provided output folder
                        generatePoolSummary(selectedBlade, i, squadsInPool, tempFolder, numSquads);

                        // Generate the bout sheets for each squad in the pool
                        if (poolBoutSequenceProvider.Sequences.TryGetValue(numSquads, out var matchList))
                        {
                            for (int k = 0; k < matchList.Count; k += 2)
                            {
                                if (k +1 < matchList.Count)
                                {
                                    var (leftSeed1, rightSeed1) = matchList[k];
                                    var (leftSeed2, rightSeed2) = matchList[k + 1];
                                    Team team1 = squadsInPool[leftSeed1 - 1];
                                    Team team2 = squadsInPool[rightSeed1 - 1];
                                    Team team3 = squadsInPool[leftSeed2 - 1];
                                    Team team4 = squadsInPool[rightSeed2 - 1];

                                    generateBoutSheetForFour(selectedBlade, i, team1, team2, team3, team4, tempFolder, k / 2);
                                } else
                                {
                                    var (leftSeed, rightSeed) = matchList[k];
                                    Team team1 = squadsInPool[leftSeed - 1];
                                    Team team2 = squadsInPool[rightSeed - 1];

                                    generateBoutSheetForTwo(selectedBlade, i, team1, team2, tempFolder);
                                }
                            }
                        }
                        // write to original output folder
                        mergeAllPdfs(tempFolder, outputFolder, selectedBlade, i);
                        Directory.Delete(tempFolder, true);
                    }

                }
            }
        }

        private void mergeAllPdfs(string tempFolder, string outputFolder, string blade, int poolNum)
        {
            // Get all PDF files in the temporary folder
            string[] pdfFiles = Directory.GetFiles(tempFolder, "*.pdf");
            // Define the merged file's output path
            string mergedFilePath = Path.Combine(outputFolder, $"{blade}-Pool{poolNum}.pdf");

            using (FileStream stream = new FileStream(mergedFilePath, FileMode.Create))
            using (iTextSharp.text.Document document = new iTextSharp.text.Document())
            using (PdfCopy pdfCopy = new PdfCopy(document, stream))
            {
                document.Open();
                foreach (string file in pdfFiles)
                {
                    using (PdfReader reader = new PdfReader(file))
                    {
                        for (int i = 1; i <= reader.NumberOfPages; i++)
                        {
                            pdfCopy.AddPage(pdfCopy.GetImportedPage(reader, i));
                        }
                    }
                }
            }
        }

        private void generatePoolSummary(string blade, int poolNum, List<Team> teams, string outputFolder, int numSquads)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            List<string> teamNames = teams.Select(t => t.ToString()).ToList();


            QuestPDF.Fluent.Document
                .Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.Letter);
                        page.Margin(0.5f, Unit.Inch);

                        page.Header()
                            .Text($"{blade} Pool #{poolNum} Summary")
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

        private void generateBoutSheetForFour(string blade, int poolNum, Team team1, Team team2, Team team3, Team team4, string outputFolder, int iter)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            QuestPDF.Fluent.Document
                .Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.Letter);
                        page.Margin(0.2f, Unit.Inch);

                        page.Content().Column(column =>
                        {
                            column.Item().Row(row =>
                            {
                                row.RelativeItem()
                                    .Text("Encounter Summary")
                                    .FontSize(15)
                                    .Bold()
                                    .AlignLeft();

                                row.RelativeItem()
                                    .Text($"{blade} Pool #{poolNum}")
                                    .FontSize(15)
                                    .Bold()
                                    .AlignRight();
                            });

                            column.Item().Row(row =>
                            {
                                row.RelativeItem()
                                    .Text($"{team1} vs. {team2}")
                                    .FontSize(15)
                                    .AlignLeft();

                                row.RelativeItem()
                                    .Text($"{team1.SeedinPool} vs. {team2.SeedinPool}")
                                    .FontSize(15)
                                    .AlignRight();
                            });

                            column.Item().Height(20);

                            column.Item().Row(row =>
                            {
                                row.RelativeItem();

                                row.ConstantItem(560).Table(table =>
                                {
                                    static QuestPDF.Infrastructure.IContainer CellStyle_Header(QuestPDF.Infrastructure.IContainer container)
                                => container
                                    .BorderBottom(2)
                                    .AlignBottom()
                                    .AlignCenter();
                                    static QuestPDF.Infrastructure.IContainer NoTopBottomBorder(QuestPDF.Infrastructure.IContainer container)
                            => container
                                .BorderLeft(1)
                                .BorderRight(1)
                                .AlignCenter()
                                .AlignMiddle()
                                .Height(30);
                                    static QuestPDF.Infrastructure.IContainer NoTopBorder(QuestPDF.Infrastructure.IContainer container)
                            => container
                                .BorderLeft(1)
                                .BorderRight(1)
                                .BorderBottom(1)
                                .AlignCenter()
                                .AlignMiddle()
                                .Height(30);
                                    static QuestPDF.Infrastructure.IContainer NoBorder(QuestPDF.Infrastructure.IContainer container)
                            => container
                                .AlignCenter()
                                .AlignMiddle()
                                .Height(30);
                                    static QuestPDF.Infrastructure.IContainer FullBorder(QuestPDF.Infrastructure.IContainer container)
                            => container
                                .Border(1)
                                .AlignCenter()
                                .AlignMiddle()
                                .Height(30);
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.ConstantColumn(17);
                                        columns.ConstantColumn(96);
                                        columns.ConstantColumn(86);
                                        columns.ConstantColumn(17);

                                        for (int i = 0; i < 4; i++)
                                            columns.ConstantColumn(32);

                                        columns.ConstantColumn(17);
                                        columns.ConstantColumn(86);
                                        columns.ConstantColumn(96);
                                        columns.ConstantColumn(17);
                                    });

                                    table.Header(header =>
                                    {
                                        header.Cell().Element(CellStyle_Header).Text("#").Bold().FontSize(12).AlignCenter();
                                        header.Cell().Element(CellStyle_Header).Text("Squad Left").Bold().FontSize(12).AlignCenter();
                                        header.Cell().Element(CellStyle_Header).Text("Fencers").Bold().FontSize(12).AlignCenter();
                                        header.Cell().Element(CellStyle_Header).Text("SI.").Bold().FontSize(12).AlignCenter();
                                        header.Cell().Element(CellStyle_Header).Text("Score").Bold().FontSize(12).AlignCenter();

                                        header.Cell().ColumnSpan(2).Element(CellStyle_Header)
                                            .Text("Team Victories").Bold().FontSize(12).AlignCenter();

                                        header.Cell().Element(CellStyle_Header).Text("Score").Bold().FontSize(12).AlignCenter();
                                        header.Cell().Element(CellStyle_Header).Text("SI").Bold().FontSize(12).AlignCenter();
                                        header.Cell().Element(CellStyle_Header).Text("Fencers").Bold().FontSize(12).AlignCenter();
                                        header.Cell().Element(CellStyle_Header).Text("Squad Right").Bold().FontSize(12).AlignCenter();
                                        header.Cell().Element(CellStyle_Header).Text("#").Bold().FontSize(12).AlignCenter();
                                    });
                                    // first three row
                                    List<string> strips = new List<string> { "C", "B", "A" };
                                    string[] team1Fencers = new string[] { team1.CFencer, team1.BFencer, team1.AFencer };
                                    string[] team2Fencers = new string[] { team2.CFencer, team2.BFencer, team2.AFencer };
                                    for (int i = 0; i < 3; i++)
                                    {
                                        if (i == 2)
                                        {
                                            table.Cell().Element(NoTopBottomBorder).Text($"{team1.SeedinPool}");
                                            table.Cell().Element(NoTopBottomBorder).Text($"{team1.ToString()}");
                                        }
                                        else
                                        {
                                            table.Cell().Element(NoTopBottomBorder).Text(" ");
                                            table.Cell().Element(NoTopBottomBorder).Text(" ");
                                        }
                                        table.Cell().Element(FullBorder).Text($"{team1Fencers[i]}");
                                        table.Cell().Element(FullBorder).Text($"{strips[i]}");
                                        for (int j = 0; j < 4; j++)
                                        {
                                            table.Cell().Element(FullBorder).Text(" ");
                                        }
                                        table.Cell().Element(FullBorder).Text($"{strips[i]}");
                                        table.Cell().Element(FullBorder).Text($"{team2Fencers[i]}");
                                        if (i == 2)
                                        {
                                            table.Cell().Element(NoTopBottomBorder).Text($"{team2.ToString()}");
                                            table.Cell().Element(NoTopBottomBorder).Text($"{team2.SeedinPool}");
                                        }
                                        else
                                        {
                                            table.Cell().Element(NoTopBottomBorder).Text(" ");
                                            table.Cell().Element(NoTopBottomBorder).Text(" ");
                                        }
                                    }

                                    // fourth row
                                    table.Cell().Element(NoTopBottomBorder).Text(" ");
                                    table.Cell().Element(NoTopBottomBorder).Text(" ");
                                    for (int i = 0; i < 8; i++)
                                    {
                                        table.Cell().Element(NoBorder).Text(" ");
                                    }
                                    table.Cell().Element(NoTopBottomBorder).Text(" ");
                                    table.Cell().Element(NoTopBottomBorder).Text(" ");

                                    // fifth row
                                    table.Cell().Element(NoTopBorder).Text(" ");
                                    table.Cell().Element(NoTopBorder).Text(" ");
                                    table.Cell().Element(FullBorder).Text($"{team1.AltFencer}");
                                    table.Cell().Element(FullBorder).Text("Alt");
                                    for (int i = 0; i < 4; i++)
                                    {
                                        table.Cell().Element(NoBorder).Text(" ");
                                    }
                                    table.Cell().Element(FullBorder).Text("Alt");
                                    table.Cell().Element(FullBorder).Text($"{team2.AltFencer}");
                                    table.Cell().Element(NoTopBorder).Text(" ");
                                    table.Cell().Element(NoTopBorder).Text(" ");
                                });
                                row.RelativeItem();
                            });
                            column.Item().Height(20);
                            column.Item().Image("./signatureFields.png");
                            column.Item().Height(30);
                            column.Item().LineHorizontal(2).LineColor(Colors.Black);

                            column.Item().Height(20);
                            column.Item().Row(row =>
                            {
                                row.RelativeItem()
                                    .Text("Encounter Summary")
                                    .FontSize(15)
                                    .Bold()
                                    .AlignLeft();

                                row.RelativeItem()
                                    .Text($"{blade} Pool #{poolNum}")
                                    .FontSize(15)
                                    .Bold()
                                    .AlignRight();
                            });

                            column.Item().Row(row =>
                            {
                                row.RelativeItem()
                                    .Text($"{team3.ToString()} vs. {team4.ToString()}")
                                    .FontSize(15)
                                    .AlignLeft();

                                row.RelativeItem()
                                    .Text($"{team3.SeedinPool} vs. {team4.SeedinPool}")
                                    .FontSize(15)
                                    .AlignRight();
                            });

                            column.Item().Height(20);
                            column.Item().Row(row =>
                            {
                                row.RelativeItem();

                                row.ConstantItem(560).Table(table =>
                                {
                                    static QuestPDF.Infrastructure.IContainer CellStyle_Header(QuestPDF.Infrastructure.IContainer container)
                                => container
                                    .BorderBottom(2)
                                    .AlignBottom()
                                    .AlignCenter();
                                    static QuestPDF.Infrastructure.IContainer NoTopBottomBorder(QuestPDF.Infrastructure.IContainer container)
                            => container
                                .BorderLeft(1)
                                .BorderRight(1)
                                .AlignCenter()
                                .AlignMiddle()
                                .Height(30);
                                    static QuestPDF.Infrastructure.IContainer NoTopBorder(QuestPDF.Infrastructure.IContainer container)
                            => container
                                .BorderLeft(1)
                                .BorderRight(1)
                                .BorderBottom(1)
                                .AlignCenter()
                                .AlignMiddle()
                                .Height(30);
                                    static QuestPDF.Infrastructure.IContainer NoBorder(QuestPDF.Infrastructure.IContainer container)
                            => container
                                .AlignCenter()
                                .AlignMiddle()
                                .Height(30);
                                    static QuestPDF.Infrastructure.IContainer FullBorder(QuestPDF.Infrastructure.IContainer container)
                            => container
                                .Border(1)
                                .AlignCenter()
                                .AlignMiddle()
                                .Height(30);
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.ConstantColumn(17);
                                        columns.ConstantColumn(96);
                                        columns.ConstantColumn(86);
                                        columns.ConstantColumn(17);

                                        for (int i = 0; i < 4; i++)
                                            columns.ConstantColumn(32);

                                        columns.ConstantColumn(17);
                                        columns.ConstantColumn(86);
                                        columns.ConstantColumn(96);
                                        columns.ConstantColumn(17);
                                    });

                                    table.Header(header =>
                                    {
                                        header.Cell().Element(CellStyle_Header).Text("#").Bold().FontSize(12).AlignCenter();
                                        header.Cell().Element(CellStyle_Header).Text("Squad Left").Bold().FontSize(12).AlignCenter();
                                        header.Cell().Element(CellStyle_Header).Text("Fencers").Bold().FontSize(12).AlignCenter();
                                        header.Cell().Element(CellStyle_Header).Text("SI.").Bold().FontSize(12).AlignCenter();
                                        header.Cell().Element(CellStyle_Header).Text("Score").Bold().FontSize(12).AlignCenter();

                                        header.Cell().ColumnSpan(2).Element(CellStyle_Header)
                                            .Text("Team Victories").Bold().FontSize(12).AlignCenter();

                                        header.Cell().Element(CellStyle_Header).Text("Score").Bold().FontSize(12).AlignCenter();
                                        header.Cell().Element(CellStyle_Header).Text("SI").Bold().FontSize(12).AlignCenter();
                                        header.Cell().Element(CellStyle_Header).Text("Fencers").Bold().FontSize(12).AlignCenter();
                                        header.Cell().Element(CellStyle_Header).Text("Squad Right").Bold().FontSize(12).AlignCenter();
                                        header.Cell().Element(CellStyle_Header).Text("#").Bold().FontSize(12).AlignCenter();
                                    });
                                    // first three row
                                    List<string> strips = new List<string> { "C", "B", "A" };
                                    string[] team3Fencers = new string[] { team3.CFencer, team3.BFencer, team3.AFencer };
                                    string[] team4Fencers = new string[] { team4.CFencer, team4.BFencer, team4.AFencer };
                                    for (int i = 0; i < 3; i++)
                                    {
                                        if (i == 2)
                                        {
                                            table.Cell().Element(NoTopBottomBorder).Text($"{team3.SeedinPool}");
                                            table.Cell().Element(NoTopBottomBorder).Text($"{team3.ToString()}");
                                        }
                                        else
                                        {
                                            table.Cell().Element(NoTopBottomBorder).Text(" ");
                                            table.Cell().Element(NoTopBottomBorder).Text(" ");
                                        }
                                        table.Cell().Element(FullBorder).Text($"{team3Fencers[i]}");
                                        table.Cell().Element(FullBorder).Text($"{strips[i]}");
                                        for (int j = 0; j < 4; j++)
                                        {
                                            table.Cell().Element(FullBorder).Text(" ");
                                        }
                                        table.Cell().Element(FullBorder).Text($"{strips[i]}");
                                        table.Cell().Element(FullBorder).Text($"{team4Fencers[i]}");
                                        if (i == 2)
                                        {
                                            table.Cell().Element(NoTopBottomBorder).Text($"{team4.ToString()}");
                                            table.Cell().Element(NoTopBottomBorder).Text($"{team4.SeedinPool}");
                                        }
                                        else
                                        {
                                            table.Cell().Element(NoTopBottomBorder).Text(" ");
                                            table.Cell().Element(NoTopBottomBorder).Text(" ");
                                        }
                                    }

                                    // fourth row
                                    table.Cell().Element(NoTopBottomBorder).Text(" ");
                                    table.Cell().Element(NoTopBottomBorder).Text(" ");
                                    for (int i = 0; i < 8; i++)
                                    {
                                        table.Cell().Element(NoBorder).Text(" ");
                                    }
                                    table.Cell().Element(NoTopBottomBorder).Text(" ");
                                    table.Cell().Element(NoTopBottomBorder).Text(" ");

                                    // fifth row
                                    table.Cell().Element(NoTopBorder).Text(" ");
                                    table.Cell().Element(NoTopBorder).Text(" ");
                                    table.Cell().Element(FullBorder).Text($"{team3.AltFencer}");
                                    table.Cell().Element(FullBorder).Text("Alt");
                                    for (int i = 0; i < 4; i++)
                                    {
                                        table.Cell().Element(NoBorder).Text(" ");
                                    }
                                    table.Cell().Element(FullBorder).Text("Alt");
                                    table.Cell().Element(FullBorder).Text($"{team4.AltFencer}");
                                    table.Cell().Element(NoTopBorder).Text(" ");
                                    table.Cell().Element(NoTopBorder).Text(" ");
                                });
                                row.RelativeItem();
                            });
                            column.Item().Height(20);
                            column.Item().Image("./signatureFields.png");
                        });
                    });
                }).GeneratePdf(Path.Combine(outputFolder, $"encounterSummary4Teams-{poolNum}-{iter}.pdf"));
        }

        private void generateBoutSheetForTwo(string blade, int poolNum, Team team1, Team team2, string outputFolder)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            QuestPDF.Fluent.Document
                .Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.Letter);
                        page.Margin(0.2f, Unit.Inch);

                        page.Content().Column(column =>
                        {
                            column.Item().Row(row =>
                            {
                                row.RelativeItem()
                                    .Text("Encounter Summary")
                                    .FontSize(15)
                                    .Bold()
                                    .AlignLeft();

                                row.RelativeItem()
                                    .Text($"{blade} Pool #{poolNum}")
                                    .FontSize(15)
                                    .Bold()
                                    .AlignRight();
                            });

                            column.Item().Row(row =>
                            {
                                row.RelativeItem()
                                    .Text($"{team1} vs. {team2}")
                                    .FontSize(15)
                                    .AlignLeft();

                                row.RelativeItem()
                                    .Text($"{team1.SeedinPool} vs. {team2.SeedinPool}")
                                    .FontSize(15)
                                    .AlignRight();
                            });

                            column.Item().Height(20);

                            column.Item().Row(row =>
                            {
                                row.RelativeItem();

                                row.ConstantItem(560).Table(table =>
                                {
                                    static QuestPDF.Infrastructure.IContainer CellStyle_Header(QuestPDF.Infrastructure.IContainer container)
                                => container
                                    .BorderBottom(2)
                                    .AlignBottom()
                                    .AlignCenter();
                                    static QuestPDF.Infrastructure.IContainer NoTopBottomBorder(QuestPDF.Infrastructure.IContainer container)
                            => container
                                .BorderLeft(1)
                                .BorderRight(1)
                                .AlignCenter()
                                .AlignMiddle()
                                .Height(30);
                                    static QuestPDF.Infrastructure.IContainer NoTopBorder(QuestPDF.Infrastructure.IContainer container)
                            => container
                                .BorderLeft(1)
                                .BorderRight(1)
                                .BorderBottom(1)
                                .AlignCenter()
                                .AlignMiddle()
                                .Height(30);
                                    static QuestPDF.Infrastructure.IContainer NoBorder(QuestPDF.Infrastructure.IContainer container)
                            => container
                                .AlignCenter()
                                .AlignMiddle()
                                .Height(30);
                                    static QuestPDF.Infrastructure.IContainer FullBorder(QuestPDF.Infrastructure.IContainer container)
                            => container
                                .Border(1)
                                .AlignCenter()
                                .AlignMiddle()
                                .Height(30);
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.ConstantColumn(17);
                                        columns.ConstantColumn(96);
                                        columns.ConstantColumn(86);
                                        columns.ConstantColumn(17);

                                        for (int i = 0; i < 4; i++)
                                            columns.ConstantColumn(32);

                                        columns.ConstantColumn(17);
                                        columns.ConstantColumn(86);
                                        columns.ConstantColumn(96);
                                        columns.ConstantColumn(17);
                                    });

                                    table.Header(header =>
                                    {
                                        header.Cell().Element(CellStyle_Header).Text("#").Bold().FontSize(12).AlignCenter();
                                        header.Cell().Element(CellStyle_Header).Text("Squad Left").Bold().FontSize(12).AlignCenter();
                                        header.Cell().Element(CellStyle_Header).Text("Fencers").Bold().FontSize(12).AlignCenter();
                                        header.Cell().Element(CellStyle_Header).Text("SI.").Bold().FontSize(12).AlignCenter();
                                        header.Cell().Element(CellStyle_Header).Text("Score").Bold().FontSize(12).AlignCenter();

                                        header.Cell().ColumnSpan(2).Element(CellStyle_Header)
                                            .Text("Team Victories").Bold().FontSize(12).AlignCenter();

                                        header.Cell().Element(CellStyle_Header).Text("Score").Bold().FontSize(12).AlignCenter();
                                        header.Cell().Element(CellStyle_Header).Text("SI").Bold().FontSize(12).AlignCenter();
                                        header.Cell().Element(CellStyle_Header).Text("Fencers").Bold().FontSize(12).AlignCenter();
                                        header.Cell().Element(CellStyle_Header).Text("Squad Right").Bold().FontSize(12).AlignCenter();
                                        header.Cell().Element(CellStyle_Header).Text("#").Bold().FontSize(12).AlignCenter();
                                    });
                                    // first three row
                                    List<string> strips = new List<string> { "C", "B", "A" };
                                    string[] team1Fencers = new string[] { team1.CFencer, team1.BFencer, team1.AFencer };
                                    string[] team2Fencers = new string[] { team2.CFencer, team2.BFencer, team2.AFencer };
                                    for (int i = 0; i < 3; i++)
                                    {
                                        if (i == 2)
                                        {
                                            table.Cell().Element(NoTopBottomBorder).Text($"{team1.SeedinPool}");
                                            table.Cell().Element(NoTopBottomBorder).Text($"{team1.ToString()}");
                                        }
                                        else
                                        {
                                            table.Cell().Element(NoTopBottomBorder).Text(" ");
                                            table.Cell().Element(NoTopBottomBorder).Text(" ");
                                        }
                                        table.Cell().Element(FullBorder).Text($"{team1Fencers[i]}");
                                        table.Cell().Element(FullBorder).Text($"{strips[i]}");
                                        for (int j = 0; j < 4; j++)
                                        {
                                            table.Cell().Element(FullBorder).Text(" ");
                                        }
                                        table.Cell().Element(FullBorder).Text($"{strips[i]}");
                                        table.Cell().Element(FullBorder).Text($"{team2Fencers[i]}");
                                        if (i == 2)
                                        {
                                            table.Cell().Element(NoTopBottomBorder).Text($"{team2.ToString()}");
                                            table.Cell().Element(NoTopBottomBorder).Text($"{team2.SeedinPool}");
                                        }
                                        else
                                        {
                                            table.Cell().Element(NoTopBottomBorder).Text(" ");
                                            table.Cell().Element(NoTopBottomBorder).Text(" ");
                                        }
                                    }

                                    // fourth row
                                    table.Cell().Element(NoTopBottomBorder).Text(" ");
                                    table.Cell().Element(NoTopBottomBorder).Text(" ");
                                    for (int i = 0; i < 8; i++)
                                    {
                                        table.Cell().Element(NoBorder).Text(" ");
                                    }
                                    table.Cell().Element(NoTopBottomBorder).Text(" ");
                                    table.Cell().Element(NoTopBottomBorder).Text(" ");

                                    // fifth row
                                    table.Cell().Element(NoTopBorder).Text(" ");
                                    table.Cell().Element(NoTopBorder).Text(" ");
                                    table.Cell().Element(FullBorder).Text($"{team1.AltFencer}");
                                    table.Cell().Element(FullBorder).Text("Alt");
                                    for (int i = 0; i < 4; i++)
                                    {
                                        table.Cell().Element(NoBorder).Text(" ");
                                    }
                                    table.Cell().Element(FullBorder).Text("Alt");
                                    table.Cell().Element(FullBorder).Text($"{team2.AltFencer}");
                                    table.Cell().Element(NoTopBorder).Text(" ");
                                    table.Cell().Element(NoTopBorder).Text(" ");
                                });
                                row.RelativeItem();
                            });
                            column.Item().Height(20);
                            column.Item().Image("./signatureFields.png");
                            column.Item().Height(30);
                        });
                    });
                }).GeneratePdf(Path.Combine(outputFolder, $"encounterSummary5Teams-{poolNum}.pdf"));
        }
    }
}

