using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABInBev.Employees.Data.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeDocumentNumberUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Employees_DocumentNumber",
                table: "Employees",
                column: "DocumentNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_DocumentNumber",
                table: "Employees");
        }
    }
}
