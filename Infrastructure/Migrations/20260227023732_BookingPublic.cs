using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BookingPublic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSchedules_Sessions_SessionId",
                table: "DoctorSchedules");

            migrationBuilder.DropIndex(
                name: "IX_DoctorSchedules_SessionId",
                table: "DoctorSchedules");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "DoctorSchedules");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "DoctorSchedules");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "DoctorSchedules");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "DoctorSchedules",
                newName: "Date");

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "DoctorSchedules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PublicBooking",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PreferredTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublicBooking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublicBooking_DoctorProfiles_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "DoctorProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PublicBooking_DoctorId",
                table: "PublicBooking",
                column: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublicBooking");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "DoctorSchedules");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "DoctorSchedules",
                newName: "StartTime");

            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "DoctorSchedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "DoctorSchedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "SessionId",
                table: "DoctorSchedules",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_SessionId",
                table: "DoctorSchedules",
                column: "SessionId",
                unique: true,
                filter: "[SessionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSchedules_Sessions_SessionId",
                table: "DoctorSchedules",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
