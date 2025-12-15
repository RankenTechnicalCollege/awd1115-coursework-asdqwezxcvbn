using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace S3FinalV2.Migrations
{
    /// <inheritdoc />
    public partial class AddPriorityToAssignedJobs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "AssignedJobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "AssignedJobs");
        }
    }
}
