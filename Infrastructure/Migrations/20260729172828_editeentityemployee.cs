using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editeentityemployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TerminationDate",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TerminationReason",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TerminationDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TerminationReason",
                table: "Employees");
        }
    }
}
