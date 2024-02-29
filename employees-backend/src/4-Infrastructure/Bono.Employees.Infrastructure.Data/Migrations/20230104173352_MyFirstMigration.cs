using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bono.Employees.Infrastructure.Data.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2023, 1, 4, 18, 33, 52, 590, DateTimeKind.Local).AddTicks(9246)),
                    DateUpdated = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2023, 1, 4, 18, 33, 52, 590, DateTimeKind.Local).AddTicks(9389)),
                    DateUpdated = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2023, 1, 4, 18, 33, 52, 590, DateTimeKind.Local).AddTicks(9501)),
                    DateUpdated = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Cpf = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(nullable: false, defaultValue: "bono@teste"),
                    SecurityStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEndDateUtc = table.Column<DateTime>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2023, 1, 4, 18, 33, 52, 588, DateTimeKind.Local).AddTicks(7379)),
                    DateUpdated = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    Title = table.Column<string>(nullable: true),
                    TypeId = table.Column<Guid>(nullable: true),
                    firstName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_EmployeeType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "EmployeeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2023, 1, 4, 18, 33, 52, 590, DateTimeKind.Local).AddTicks(9656)),
                    DateUpdated = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    UserId = table.Column<Guid>(nullable: true),
                    RoleId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRole_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "EmployeeType",
                columns: new[] { "Id", "DateCreated", "DateUpdated", "Type" },
                values: new object[,]
                {
                    { new Guid("2d9d8cf3-80e7-4e0a-b6a4-544b5f169db5"), new DateTime(2023, 1, 4, 18, 33, 52, 595, DateTimeKind.Local).AddTicks(5406), new DateTime(2023, 1, 4, 18, 33, 52, 595, DateTimeKind.Local).AddTicks(5613), "Standard" },
                    { new Guid("a775b466-e19d-424c-b4f8-7d2686f78f06"), new DateTime(2023, 1, 4, 18, 33, 52, 595, DateTimeKind.Local).AddTicks(6508), new DateTime(2023, 1, 4, 18, 33, 52, 595, DateTimeKind.Local).AddTicks(6515), "SaleEmployee" },
                    { new Guid("0a1707e4-9740-4678-8c88-3eee452f187c"), new DateTime(2023, 1, 4, 18, 33, 52, 595, DateTimeKind.Local).AddTicks(6518), new DateTime(2023, 1, 4, 18, 33, 52, 595, DateTimeKind.Local).AddTicks(6519), "PurchaseEmployee" },
                    { new Guid("5dec9c99-ec79-4bcf-bd8f-651b88a90a4f"), new DateTime(2023, 1, 4, 18, 33, 52, 595, DateTimeKind.Local).AddTicks(6522), new DateTime(2023, 1, 4, 18, 33, 52, 595, DateTimeKind.Local).AddTicks(6523), "TransferEmployee" },
                    { new Guid("133ab82e-eb1d-4490-82f9-28809aade78b"), new DateTime(2023, 1, 4, 18, 33, 52, 595, DateTimeKind.Local).AddTicks(6525), new DateTime(2023, 1, 4, 18, 33, 52, 595, DateTimeKind.Local).AddTicks(6527), "ReturnEmployee" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Cpf", "DateUpdated", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEndDateUtc", "Password", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("ad9a02ee-e522-4b8a-844f-50cb95d076ba"), 0, "123.456.456-56", null, null, "richiebono@gmail.com", false, "Richard Bono", "Oliveira", false, null, "23D42F5F3F66498B2C8FF4C20B8C5AC826E47146", "+55 11-98547-3851", false, null, false, "Richard Bono" });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_TypeId",
                table: "Employee",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_UserId",
                table: "Employee",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "EmployeeType");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
