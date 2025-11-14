using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CH08Lab.Migrations
{
    /// <inheritdoc />
    public partial class AddAccommodation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accommodation",
                table: "Trips");

            migrationBuilder.AddColumn<int>(
                name: "AccommodationId",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Accommodations",
                columns: table => new
                {
                    AccommodationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accommodations", x => x.AccommodationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trips_AccommodationId",
                table: "Trips",
                column: "AccommodationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Accommodations_AccommodationId",
                table: "Trips",
                column: "AccommodationId",
                principalTable: "Accommodations",
                principalColumn: "AccommodationId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Accommodations_AccommodationId",
                table: "Trips");

            migrationBuilder.DropTable(
                name: "Accommodations");

            migrationBuilder.DropIndex(
                name: "IX_Trips_AccommodationId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "AccommodationId",
                table: "Trips");

            migrationBuilder.AddColumn<string>(
                name: "Accommodation",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
