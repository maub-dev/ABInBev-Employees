using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABInBev.Employees.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeePhone1AndPhone2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Phonebook");

            migrationBuilder.AddColumn<string>(
                name: "Phone1",
                table: "Employees",
                type: "varchar(30)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone2",
                table: "Employees",
                type: "varchar(30)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone1",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Phone2",
                table: "Employees");

            migrationBuilder.CreateTable(
                name: "Phonebook",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(50)", nullable: false),
                    Type = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phonebook", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phonebook_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Phonebook_EmployeeId",
                table: "Phonebook",
                column: "EmployeeId");
        }
    }
}
