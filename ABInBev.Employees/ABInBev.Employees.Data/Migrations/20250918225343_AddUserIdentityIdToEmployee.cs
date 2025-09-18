using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABInBev.Employees.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdentityIdToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Employees");

            migrationBuilder.AddColumn<Guid>(
                name: "UserIdentityId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserIdentityId",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Employees",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");
        }
    }
}
