using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class editetype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "ScheduledDate",
                table: "Sessions",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "PreferredTime",
                table: "PublicBookings",
                type: "time",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "PreferredDate",
                table: "PublicBookings",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "Time",
                table: "DoctorSchedules",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "DoctorSchedules",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ScheduledDate",
                table: "Sessions",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "PreferredTime",
                table: "PublicBookings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PreferredDate",
                table: "PublicBookings",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "Time",
                table: "DoctorSchedules",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "DoctorSchedules",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
