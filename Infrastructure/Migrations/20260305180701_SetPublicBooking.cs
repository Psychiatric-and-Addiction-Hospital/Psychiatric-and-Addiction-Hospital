using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SetPublicBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublicBooking_DoctorProfiles_DoctorId",
                table: "PublicBooking");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PublicBooking",
                table: "PublicBooking");

            migrationBuilder.RenameTable(
                name: "PublicBooking",
                newName: "PublicBookings");

            migrationBuilder.RenameIndex(
                name: "IX_PublicBooking_DoctorId",
                table: "PublicBookings",
                newName: "IX_PublicBookings_DoctorId");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "PublicBookings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PublicBookings",
                table: "PublicBookings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicBookings_DoctorProfiles_DoctorId",
                table: "PublicBookings",
                column: "DoctorId",
                principalTable: "DoctorProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PublicBookings_DoctorProfiles_DoctorId",
                table: "PublicBookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PublicBookings",
                table: "PublicBookings");

            migrationBuilder.RenameTable(
                name: "PublicBookings",
                newName: "PublicBooking");

            migrationBuilder.RenameIndex(
                name: "IX_PublicBookings_DoctorId",
                table: "PublicBooking",
                newName: "IX_PublicBooking_DoctorId");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "PublicBooking",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PublicBooking",
                table: "PublicBooking",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PublicBooking_DoctorProfiles_DoctorId",
                table: "PublicBooking",
                column: "DoctorId",
                principalTable: "DoctorProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
