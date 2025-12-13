using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace S3FinalV2.Migrations
{
    /// <inheritdoc />
    public partial class FixError : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedJobsAssignedJobId",
                table: "MechanicAssignments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MechanicAssignments_AssignedJobsAssignedJobId",
                table: "MechanicAssignments",
                column: "AssignedJobsAssignedJobId");

            migrationBuilder.AddForeignKey(
                name: "FK_MechanicAssignments_AssignedJobs_AssignedJobsAssignedJobId",
                table: "MechanicAssignments",
                column: "AssignedJobsAssignedJobId",
                principalTable: "AssignedJobs",
                principalColumn: "AssignedJobId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MechanicAssignments_AssignedJobs_AssignedJobsAssignedJobId",
                table: "MechanicAssignments");

            migrationBuilder.DropIndex(
                name: "IX_MechanicAssignments_AssignedJobsAssignedJobId",
                table: "MechanicAssignments");

            migrationBuilder.DropColumn(
                name: "AssignedJobsAssignedJobId",
                table: "MechanicAssignments");
        }
    }
}
