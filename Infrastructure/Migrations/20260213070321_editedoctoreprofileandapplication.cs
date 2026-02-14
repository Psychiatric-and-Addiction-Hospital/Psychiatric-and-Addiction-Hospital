using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editedoctoreprofileandapplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "DoctorProfiles");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "DoctorApplications");

            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "DoctorApplications");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "DoctorProfiles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ClinicAddress",
                table: "DoctorProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "DoctorProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "DoctorProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Qualifications",
                table: "DoctorProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "DoctorApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Degree",
                table: "DoctorApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "DoctorApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NationalId",
                table: "DoctorApplications",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClinicAddress",
                table: "DoctorProfiles");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "DoctorProfiles");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "DoctorProfiles");

            migrationBuilder.DropColumn(
                name: "Qualifications",
                table: "DoctorProfiles");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "DoctorApplications");

            migrationBuilder.DropColumn(
                name: "Degree",
                table: "DoctorApplications");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "DoctorApplications");

            migrationBuilder.DropColumn(
                name: "NationalId",
                table: "DoctorApplications");

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "DoctorProfiles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "DoctorProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "DoctorApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "DoctorApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
