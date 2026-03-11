using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditVaildation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorApplications_Departments_DepartmentId",
                table: "DoctorApplications");

            migrationBuilder.DropIndex(
                name: "IX_DoctorApplications_DepartmentId",
                table: "DoctorApplications");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "DoctorApplications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "DoctorApplications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DoctorApplications_DepartmentId",
                table: "DoctorApplications",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorApplications_Departments_DepartmentId",
                table: "DoctorApplications",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
