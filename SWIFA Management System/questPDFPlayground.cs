using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Companion;
using System.Diagnostics;

QuestPDF.Settings.License = LicenseType.Community;

string bladeSelection = "Foil";
int num_pools = 3;
int num_teams = 6;

Document.Create(container =>
{
    container.Page(page =>
    {
        page.Size(PageSizes.Letter);
        page.Margin(0.5f, Unit.Inch);

        page.Header()
        .Text($"{bladeSelection} Pool #{num_pools} Summary")
        .FontSize(28)
        .Bold();

        page.Content().Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(20);
                for (int i = 0; i < num_teams + 1; i++)
                {
                    columns.RelativeColumn(1);
                }
            });

            table.Cell().Element(CellStyle).Text("#");
            for (int i = 0; i < num_teams + 1; i++)
            {
                table.Cell().Element(CellStyle).Text("Relative");

            }
            static IContainer CellStyle(IContainer container)
                => container.Border(1).Padding(8); 
        });
    });
}).ShowInCompanion();
