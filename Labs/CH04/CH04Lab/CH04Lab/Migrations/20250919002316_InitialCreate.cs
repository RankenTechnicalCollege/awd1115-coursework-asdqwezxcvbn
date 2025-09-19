using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CH04Lab.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "ContactId",
                keyValue: 1,
                column: "CategoryId",
                value: 1);

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "ContactId", "CategoryId", "DateAdded", "Email", "FirstName", "LastName", "Phone" },
                values: new object[,]
                {
                    { 2, 2, new DateTime(2022, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "delores@hotmail.com", "Delores", "Del Rio", "555-987-6543" },
                    { 3, 3, new DateTime(1998, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "efren@aol.com", "Efran", "Hererra", "555-456-7890" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "ContactId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "ContactId",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "ContactId",
                keyValue: 1,
                column: "CategoryId",
                value: 3);
        }
    }
}
