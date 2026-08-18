using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditeEntitypostion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Position_EmployeeCodePrefix",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "EmployeeCodePrefix",
                table: "Positions");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Positions",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Positions",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCodePrefix",
                table: "Positions",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Position_EmployeeCodePrefix",
                table: "Positions",
                sql: "LEN(LTRIM(RTRIM([EmployeeCodePrefix]))) > 0");
        }
    }
}
