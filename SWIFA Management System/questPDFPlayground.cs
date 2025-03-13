using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Companion;
using System.Diagnostics;
using System.Collections.Generic;
using System.Reflection.Metadata;

QuestPDF.Settings.License = LicenseType.Community;

string bladeSelection = "Foil";
int num_pools = 3;
List<string> squads = new List<string> { "UT Austin A", "UT Austin C", "UT Dallas A", "Texas A&M A", "TXST B", "TXST C", "UNT", "OU" };
int num_squads = squads.Count;

QuestPDF.Fluent.Document
    .Create(container =>
    {
        container.Page(page =>
        {
            page.Size(PageSizes.Letter);
            page.Margin(0.5f, Unit.Inch);

            page.Header()
                .Text($"{bladeSelection} Pool #{num_pools} Summary")
                .FontSize(28)
                .Bold();

            page.Content().Column(column =>
            {
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(20);
                        columns.ConstantColumn(120);
                        for (int i = 0; i < num_squads; i++)
                        {
                            columns.RelativeColumn(1);
                        }
                    });

                    table.Cell().Element(CellStyle).Text("#").Bold().AlignCenter();
                    table.Cell().Element(CellStyle).Text("Squad").Bold().AlignCenter();

                    for (int i = 0; i < num_squads; i++)
                        table.Cell().Element(CellStyle).Text($"{i + 1}").Bold().AlignCenter();

                    for (int i = 0; i < num_squads; i++)
                    {
                        table.Cell().Element(CellStyle).Text($"{i + 1}").Bold().AlignCenter();
                        table.Cell().Element(CellStyle).Text(squads[i]).AlignCenter();

                        for (int j = 0; j < num_squads; j++)
                        {
                            if (i == j)
                                table.Cell().Border(1).Background(Colors.Grey.Darken1).AlignCenter();
                            else
                                table.Cell().Element(CellStyle).Text(" ").AlignCenter();
                        }
                    }

                    static IContainer CellStyle(IContainer container)
                        => container.Border(1).Padding(8);
                });
                column.Item().Height(20);
                column.Item().Image("./poolBoutSequence.png");
            });
        });
    })
    .ShowInCompanion();
