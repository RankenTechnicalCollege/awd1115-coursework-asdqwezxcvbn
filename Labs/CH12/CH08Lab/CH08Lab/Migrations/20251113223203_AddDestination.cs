using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CH08Lab.Migrations
{
    /// <inheritdoc />
    public partial class AddDestination : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destination",
                table: "Trips");

            migrationBuilder.AddColumn<int>(
                name: "DestinationId",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    DestinationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.DestinationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DestinationId",
                table: "Trips",
                column: "DestinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Destinations_DestinationId",
                table: "Trips",
                column: "DestinationId",
                principalTable: "Destinations",
                principalColumn: "DestinationId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Destinations_DestinationId",
                table: "Trips");

            migrationBuilder.DropTable(
                name: "Destinations");

            migrationBuilder.DropIndex(
                name: "IX_Trips_DestinationId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "Trips");

            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
