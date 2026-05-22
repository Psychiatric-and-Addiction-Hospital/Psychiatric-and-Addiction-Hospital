using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadeDeleteIssue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Employees_EmployeeId1",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Employees_EmployeeId1",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId1",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicBookings_DoctorProfiles_DoctorId",
                table: "PublicBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Departments_DepartmentId1",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_DepartmentId1",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Employees_Email",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UserId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Contracts_EmployeeId1",
                table: "Contracts");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_EmployeeId1",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "DepartmentId1",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "EmployeeId1",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "EmployeeId1",
                table: "Attendances");

            migrationBuilder.RenameColumn(
                name: "DepartmentId1",
                table: "Employees",
                newName: "ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_DepartmentId1",
                table: "Employees",
                newName: "IX_Employees_ManagerId");

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "PublicBookings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ScheduleId",
                table: "PublicBookings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Employees",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employees",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Departments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ManagerId",
                table: "Departments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PublicBookings_ScheduleId",
                table: "PublicBookings",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_ManagerId",
                table: "Employees",
                column: "ManagerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicBookings_DoctorProfiles_DoctorId",
                table: "PublicBookings",
                column: "DoctorId",
                principalTable: "DoctorProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PublicBookings_DoctorSchedules_ScheduleId",
                table: "PublicBookings",
                column: "ScheduleId",
                principalTable: "DoctorSchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_ManagerId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicBookings_DoctorProfiles_DoctorId",
                table: "PublicBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_PublicBookings_DoctorSchedules_ScheduleId",
                table: "PublicBookings");

            migrationBuilder.DropIndex(
                name: "IX_PublicBookings_ScheduleId",
                table: "PublicBookings");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UserId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "PublicBookings");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "PublicBookings");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Departments");

            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "Employees",
                newName: "DepartmentId1");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_ManagerId",
                table: "Employees",
                newName: "IX_Employees_DepartmentId1");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId1",
                table: "Services",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employees",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId1",
                table: "Contracts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId1",
                table: "Attendances",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_DepartmentId1",
                table: "Services",
                column: "DepartmentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_EmployeeId1",
                table: "Contracts",
                column: "EmployeeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_EmployeeId1",
                table: "Attendances",
                column: "EmployeeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Employees_EmployeeId1",
                table: "Attendances",
                column: "EmployeeId1",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Employees_EmployeeId1",
                table: "Contracts",
                column: "EmployeeId1",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId1",
                table: "Employees",
                column: "DepartmentId1",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicBookings_DoctorProfiles_DoctorId",
                table: "PublicBookings",
                column: "DoctorId",
                principalTable: "DoctorProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Departments_DepartmentId1",
                table: "Services",
                column: "DepartmentId1",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
