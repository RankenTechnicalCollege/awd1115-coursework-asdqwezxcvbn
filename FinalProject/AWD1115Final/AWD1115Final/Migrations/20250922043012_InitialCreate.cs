using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AWD1115Final.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillLevel = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompletionTime = table.Column<float>(type: "real", nullable: false),
                    AssignedMechanic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.JobId);
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "JobId", "AssignedMechanic", "CompletionTime", "CustomerName", "Description", "IsCompleted", "Name", "Priority", "SkillLevel" },
                values: new object[,]
                {
                    { 1, "", 0.5f, "", "Change oil and oil filter", true, "Oil Change", "Low", 1 },
                    { 2, "", 1f, "", "Replace Break Pads", true, "Breaks", "Low", 1 },
                    { 3, "", 0.25f, "", "Replace Air Filter", true, "Air Filter", "Low", 1 },
                    { 4, "", 0.25f, "", "Replace Cabin Filter", true, "Cabin Filter", "Low", 1 },
                    { 5, "", 2f, "", "Balance And Install/Mount New Tires", true, "Tire Mounting And Balancing", "Low", 1 },
                    { 6, "", 0.1f, "", "Replace Windshield Wipers", true, "Windshield Wipers", "Low", 1 },
                    { 7, "", 1f, "", "Rotate Tires", true, "Tire Rotation", "Low", 1 },
                    { 8, "", 0.5f, "", "Remove/Drain And Replace Vehicle's Fluids", true, "Change Fluids", "Low", 1 },
                    { 9, "", 0.5f, "", "Replace Catalitic Convertor", true, "Catalitic Convertor", "Low", 2 },
                    { 10, "", 0.25f, "", "Replace Of Spark Plugs", true, "Spark Plugs", "Low", 2 },
                    { 11, "", 10f, "", "Replace Head Gasket", true, "Internal Engine Work", "Low", 2 },
                    { 12, "", 0.25f, "", "Replace Water Pump", true, "Water Pump", "Low", 2 },
                    { 13, "", 0.25f, "", "Replace Alternator", true, "Alternator", "Low", 2 },
                    { 14, "", 0.5f, "", "Replace Intake", true, "Intake", "Low", 2 },
                    { 15, "", 1f, "", "Replace Exhaust Manifold", true, "Exhaust Manifold", "Low", 2 },
                    { 16, "", 6f, "", "Replace Clutch", true, "Clutch", "Low", 3 },
                    { 17, "", 5f, "", "Replace Transmission", true, "Transmission", "Low", 3 },
                    { 18, "", 3.25f, "", "Fix Electrical Issues", true, "Electrical Issues", "Low", 3 },
                    { 19, "", 7.75f, "", "Replace Pistons And Rings", true, "Engine Internals", "Low", 3 },
                    { 20, "", 15f, "", "Replace Engine", true, "Engine Replacement", "Low", 4 },
                    { 21, "", 2.5f, "", "Fix Wiring", true, "Fix Wiring", "Low", 4 },
                    { 22, "", 1.5f, "", "Any Work Here", true, "Work On A Maserati", "Low", 4 },
                    { 23, "", 1.5f, "", "Any Work Here", true, "Work On A Alpha Romeo", "Low", 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
