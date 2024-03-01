using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bono.Employees.Infrastructure.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2024, 2, 29, 22, 29, 38, 277, DateTimeKind.Local).AddTicks(5951)),
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
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2024, 2, 29, 22, 29, 38, 277, DateTimeKind.Local).AddTicks(6190)),
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
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2024, 2, 29, 22, 29, 38, 277, DateTimeKind.Local).AddTicks(6338)),
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
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2024, 2, 29, 22, 29, 38, 276, DateTimeKind.Local).AddTicks(3828)),
                    DateUpdated = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    TypeId = table.Column<Guid>(nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    JobTitle = table.Column<string>(nullable: false),
                    DateOfJoining = table.Column<string>(nullable: false),
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
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2024, 2, 29, 22, 29, 38, 277, DateTimeKind.Local).AddTicks(6492)),
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
                    { new Guid("d37266d0-6f16-4eac-8489-687485f05816"), new DateTime(2024, 2, 29, 22, 29, 38, 283, DateTimeKind.Local).AddTicks(6920), new DateTime(2024, 2, 29, 22, 29, 38, 283, DateTimeKind.Local).AddTicks(7245), "Trainee" },
                    { new Guid("63ee719c-0fee-4671-8a3b-5029a6cac4dd"), new DateTime(2024, 2, 29, 22, 29, 38, 283, DateTimeKind.Local).AddTicks(8019), new DateTime(2024, 2, 29, 22, 29, 38, 283, DateTimeKind.Local).AddTicks(8022), "Assistant" },
                    { new Guid("a4900ccf-aced-410a-a955-d5fe00e5a9b8"), new DateTime(2024, 2, 29, 22, 29, 38, 283, DateTimeKind.Local).AddTicks(8025), new DateTime(2024, 2, 29, 22, 29, 38, 283, DateTimeKind.Local).AddTicks(8026), "Analyst" },
                    { new Guid("d26d8f45-e2f4-401c-bab3-be3655873d23"), new DateTime(2024, 2, 29, 22, 29, 38, 283, DateTimeKind.Local).AddTicks(8028), new DateTime(2024, 2, 29, 22, 29, 38, 283, DateTimeKind.Local).AddTicks(8029), "Leader" },
                    { new Guid("ebcfaebb-4ee2-49bb-a70a-0720cf0c2470"), new DateTime(2024, 2, 29, 22, 29, 38, 283, DateTimeKind.Local).AddTicks(8031), new DateTime(2024, 2, 29, 22, 29, 38, 283, DateTimeKind.Local).AddTicks(8032), "Manager" },
                    { new Guid("e90864ba-a7b1-47aa-9405-b32dc579e78c"), new DateTime(2024, 2, 29, 22, 29, 38, 283, DateTimeKind.Local).AddTicks(8034), new DateTime(2024, 2, 29, 22, 29, 38, 283, DateTimeKind.Local).AddTicks(8035), "CEO" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Cpf", "DateUpdated", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEndDateUtc", "Password", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("371e3928-8931-48e6-aaa6-7ec96270d3db"), 0, "123.456.456-56", null, null, "richiebono@gmail.com", false, "Richard Bono", "Oliveira", false, null, "23D42F5F3F66498B2C8FF4C20B8C5AC826E47146", "+55 11-98547-3851", false, null, false, "Richard Bono" });

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
