using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityInLeave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveBalance_Employees_EmployeeId",
                table: "EmployeeLeaveBalance");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveBalance_LeaveTypes_LeaveTypeId",
                table: "EmployeeLeaveBalance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeLeaveBalance",
                table: "EmployeeLeaveBalance");

            migrationBuilder.RenameTable(
                name: "EmployeeLeaveBalance",
                newName: "EmployeeLeaveBalances");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeLeaveBalance_LeaveTypeId",
                table: "EmployeeLeaveBalances",
                newName: "IX_EmployeeLeaveBalances_LeaveTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeLeaveBalance_EmployeeId_LeaveTypeId",
                table: "EmployeeLeaveBalances",
                newName: "IX_EmployeeLeaveBalances_EmployeeId_LeaveTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeLeaveBalances",
                table: "EmployeeLeaveBalances",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveBalances_Employees_EmployeeId",
                table: "EmployeeLeaveBalances",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveBalances_LeaveTypes_LeaveTypeId",
                table: "EmployeeLeaveBalances",
                column: "LeaveTypeId",
                principalTable: "LeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveBalances_Employees_EmployeeId",
                table: "EmployeeLeaveBalances");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveBalances_LeaveTypes_LeaveTypeId",
                table: "EmployeeLeaveBalances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeLeaveBalances",
                table: "EmployeeLeaveBalances");

            migrationBuilder.RenameTable(
                name: "EmployeeLeaveBalances",
                newName: "EmployeeLeaveBalance");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeLeaveBalances_LeaveTypeId",
                table: "EmployeeLeaveBalance",
                newName: "IX_EmployeeLeaveBalance_LeaveTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeLeaveBalances_EmployeeId_LeaveTypeId",
                table: "EmployeeLeaveBalance",
                newName: "IX_EmployeeLeaveBalance_EmployeeId_LeaveTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeLeaveBalance",
                table: "EmployeeLeaveBalance",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveBalance_Employees_EmployeeId",
                table: "EmployeeLeaveBalance",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveBalance_LeaveTypes_LeaveTypeId",
                table: "EmployeeLeaveBalance",
                column: "LeaveTypeId",
                principalTable: "LeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
