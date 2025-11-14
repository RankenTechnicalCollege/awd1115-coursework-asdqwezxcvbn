using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CH08Lab.Migrations
{
    /// <inheritdoc />
    public partial class AddActivityEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activity1",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Activity2",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Activity3",
                table: "Trips");

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.ActivityId);
                });

            migrationBuilder.CreateTable(
                name: "ActivityTrip",
                columns: table => new
                {
                    ActivitiesActivityId = table.Column<int>(type: "int", nullable: false),
                    TripsTripId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTrip", x => new { x.ActivitiesActivityId, x.TripsTripId });
                    table.ForeignKey(
                        name: "FK_ActivityTrip_Activities_ActivitiesActivityId",
                        column: x => x.ActivitiesActivityId,
                        principalTable: "Activities",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityTrip_Trips_TripsTripId",
                        column: x => x.TripsTripId,
                        principalTable: "Trips",
                        principalColumn: "TripId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTrip_TripsTripId",
                table: "ActivityTrip",
                column: "TripsTripId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityTrip");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.AddColumn<string>(
                name: "Activity1",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Activity2",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Activity3",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
