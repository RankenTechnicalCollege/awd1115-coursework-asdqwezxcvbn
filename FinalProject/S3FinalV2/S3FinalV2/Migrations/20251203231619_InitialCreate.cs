using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace S3FinalV2.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillLevel = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstCompTime = table.Column<float>(type: "real", nullable: false),
                    AvgCompletionTime = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.JobId);
                });

            migrationBuilder.CreateTable(
                name: "WorkWeeks",
                columns: table => new
                {
                    WorkWeekId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkWeeks", x => x.WorkWeekId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleInfo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mechanics",
                columns: table => new
                {
                    MechanicId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SkillLevel = table.Column<int>(type: "int", nullable: false),
                    WeeklyHourLimit = table.Column<float>(type: "real", nullable: false),
                    TotalHours = table.Column<float>(type: "real", nullable: false),
                    AssignedJobs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompletedJobs = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mechanics", x => x.MechanicId);
                    table.ForeignKey(
                        name: "FK_Mechanics_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignedJobs",
                columns: table => new
                {
                    AssignedJobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobsId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualCompTime = table.Column<float>(type: "real", nullable: true),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedJobs", x => x.AssignedJobId);
                    table.ForeignKey(
                        name: "FK_AssignedJobs_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignedJobs_Jobs_JobsId",
                        column: x => x.JobsId,
                        principalTable: "Jobs",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MechanicAssignments",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MechanicId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    TimeAssigned = table.Column<float>(type: "real", nullable: false),
                    TimeCompleted = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MechanicAssignments", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_MechanicAssignments_AssignedJobs_JobId",
                        column: x => x.JobId,
                        principalTable: "AssignedJobs",
                        principalColumn: "AssignedJobId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MechanicAssignments_Mechanics_MechanicId",
                        column: x => x.MechanicId,
                        principalTable: "Mechanics",
                        principalColumn: "MechanicId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkWeekAssignments",
                columns: table => new
                {
                    WorkWeekAssignmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkWeekId = table.Column<int>(type: "int", nullable: false),
                    AssignedJobsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkWeekAssignments", x => x.WorkWeekAssignmentId);
                    table.ForeignKey(
                        name: "FK_WorkWeekAssignments_AssignedJobs_AssignedJobsId",
                        column: x => x.AssignedJobsId,
                        principalTable: "AssignedJobs",
                        principalColumn: "AssignedJobId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkWeekAssignments_WorkWeeks_WorkWeekId",
                        column: x => x.WorkWeekId,
                        principalTable: "WorkWeeks",
                        principalColumn: "WorkWeekId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "JobId", "AvgCompletionTime", "Description", "EstCompTime", "Name", "Priority", "SkillLevel" },
                values: new object[,]
                {
                    { 1, 0f, "Change oil and oil filter", 0f, "Oil Change", "Low", 1 },
                    { 2, 0f, "Replace Break Pads", 0f, "Breaks", "Low", 1 },
                    { 3, 0f, "Replace Air Filter", 0f, "Air Filter", "Low", 1 },
                    { 4, 0f, "Replace Cabin Filter", 0f, "Cabin Filter", "Low", 1 },
                    { 5, 0f, "Balance And Install/Mount New Tires", 0f, "Tire Mounting And Balancing", "Low", 1 },
                    { 6, 0f, "Replace Windshield Wipers", 0f, "Windshield Wipers", "Low", 1 },
                    { 7, 0f, "Rotate Tires", 0f, "Tire Rotation", "Low", 1 },
                    { 8, 0f, "Remove/Drain And Replace Vehicle's Fluids", 0f, "Change Fluids", "Low", 1 },
                    { 9, 0f, "Replace Catalitic Convertor", 0f, "Catalitic Convertor", "Low", 2 },
                    { 10, 0f, "Replace Of Spark Plugs", 0f, "Spark Plugs", "Low", 2 },
                    { 11, 0f, "Replace Head Gasket", 0f, "Internal Engine Work", "Low", 2 },
                    { 12, 0f, "Replace Water Pump", 0f, "Water Pump", "Low", 2 },
                    { 13, 0f, "Replace Alternator", 0f, "Alternator", "Low", 2 },
                    { 14, 0f, "Replace Intake", 0f, "Intake", "Low  ", 2 },
                    { 15, 0f, "Replace Exhaust Manifold", 0f, "Exhaust Manifold", "Low", 2 },
                    { 16, 0f, "Replace Clutch", 0f, "Clutch", "Low", 3 },
                    { 17, 0f, "Replace Transmission", 0f, "Transmission", "Low", 3 },
                    { 18, 0f, "Fix Electrical Issues", 0f, "Electrical Issues", "Low", 3 },
                    { 19, 0f, "Replace Pistons And Rings", 0f, "Engine Internals", "Low", 3 },
                    { 20, 0f, "Replace Engine", 0f, "Engine Replacement", "Low", 4 },
                    { 21, 0f, "Fix Wiring", 0f, "Fix Wiring", "Low", 4 },
                    { 22, 0f, "Any Work Here", 0f, "Work On A Maserati", null, 4 },
                    { 23, 0f, "Any Work Here", 0f, "Work On A Alpha Romeo", null, 4 }
                });

            migrationBuilder.InsertData(
                table: "Mechanics",
                columns: new[] { "MechanicId", "AssignedJobs", "CompletedJobs", "Name", "SkillLevel", "TotalHours", "UserId", "WeeklyHourLimit" },
                values: new object[,]
                {
                    { 1, "[]", "[]", "Ashtin Gebert", 4, 0f, null, 0f },
                    { 2, "[]", "[]", "Patrick Rodgers", 4, 0f, null, 0f },
                    { 3, "[]", "[]", "Aaron Barkley", 4, 0f, null, 0f },
                    { 4, "[]", "[]", "Bird Ball", 3, 0f, null, 0f },
                    { 5, "[]", "[]", "Kareem Ryan", 3, 0f, null, 0f },
                    { 6, "[]", "[]", "Gabriel Wilt", 3, 0f, null, 0f },
                    { 7, "[]", "[]", "Kelce Baker", 3, 0f, null, 0f },
                    { 8, "[]", "[]", "Stephinie John", 2, 0f, null, 0f },
                    { 9, "[]", "[]", "Beatriz Kareem", 2, 0f, null, 0f },
                    { 10, "[]", "[]", "Bill Patrick", 2, 0f, null, 0f },
                    { 11, "[]", "[]", "Ashtin Peterson", 2, 0f, null, 0f },
                    { 12, "[]", "[]", "Eric Newton", 2, 0f, null, 0f },
                    { 13, "[]", "[]", "Hanna Tyson", 1, 0f, null, 0f },
                    { 14, "[]", "[]", "Diya John", 1, 0f, null, 0f },
                    { 15, "[]", "[]", "Wilt Rodgers", 1, 0f, null, 0f },
                    { 16, "[]", "[]", "LeBron Ali", 1, 0f, null, 0f },
                    { 17, "[]", "[]", "Fatima Ball", 1, 0f, null, 0f },
                    { 18, "[]", "[]", "Aaron Magic", 1, 0f, null, 0f }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedJobs_CustomerId",
                table: "AssignedJobs",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignedJobs_JobsId",
                table: "AssignedJobs",
                column: "JobsId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MechanicAssignments_JobId",
                table: "MechanicAssignments",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_MechanicAssignments_MechanicId",
                table: "MechanicAssignments",
                column: "MechanicId");

            migrationBuilder.CreateIndex(
                name: "IX_Mechanics_UserId",
                table: "Mechanics",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkWeekAssignments_AssignedJobsId",
                table: "WorkWeekAssignments",
                column: "AssignedJobsId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkWeekAssignments_WorkWeekId",
                table: "WorkWeekAssignments",
                column: "WorkWeekId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "MechanicAssignments");

            migrationBuilder.DropTable(
                name: "WorkWeekAssignments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Mechanics");

            migrationBuilder.DropTable(
                name: "AssignedJobs");

            migrationBuilder.DropTable(
                name: "WorkWeeks");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
