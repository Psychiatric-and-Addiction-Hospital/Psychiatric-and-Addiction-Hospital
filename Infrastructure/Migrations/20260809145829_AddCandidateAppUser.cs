using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCandidateAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Candidates",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_AppUserId",
                table: "Candidates",
                column: "AppUserId",
                unique: true,
                filter: "[AppUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_AspNetUsers_AppUserId",
                table: "Candidates",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_AspNetUsers_AppUserId",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_AppUserId",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Candidates");
        }
    }
}
