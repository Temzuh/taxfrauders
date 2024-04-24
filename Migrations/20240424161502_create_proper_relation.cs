using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace taxfrauders.Migrations
{
    /// <inheritdoc />
    public partial class create_proper_relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Workplace",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Workplace_UserId",
                table: "Workplace",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workplace_User_UserId",
                table: "Workplace",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workplace_User_UserId",
                table: "Workplace");

            migrationBuilder.DropIndex(
                name: "IX_Workplace_UserId",
                table: "Workplace");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Workplace");
        }
    }
}
