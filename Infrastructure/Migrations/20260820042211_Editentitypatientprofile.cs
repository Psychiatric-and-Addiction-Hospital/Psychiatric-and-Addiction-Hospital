using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Editentitypatientprofile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "PatientProfiles");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "PatientProfiles");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "PatientProfiles");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "PatientProfiles");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "PatientProfiles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "PatientProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "PatientProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "PatientProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "PatientProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "PatientProfiles",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
