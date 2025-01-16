using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectMedii.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TrainingSession",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSession_UserId",
                table: "TrainingSession",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingSession_User_UserId",
                table: "TrainingSession",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingSession_User_UserId",
                table: "TrainingSession");

            migrationBuilder.DropIndex(
                name: "IX_TrainingSession_UserId",
                table: "TrainingSession");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TrainingSession");
        }
    }
}
