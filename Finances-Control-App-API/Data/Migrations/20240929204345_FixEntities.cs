using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finances_Control_App_API.Migrations
{
    /// <inheritdoc />
    public partial class FixEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdAccountFlag",
                table: "Conta",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Conta_IdAccountFlag",
                table: "Conta",
                column: "IdAccountFlag");

            migrationBuilder.AddForeignKey(
                name: "FK_Conta_AccountFlag_IdAccountFlag",
                table: "Conta",
                column: "IdAccountFlag",
                principalTable: "AccountFlag",
                principalColumn: "IdAccountFlag");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conta_AccountFlag_IdAccountFlag",
                table: "Conta");

            migrationBuilder.DropIndex(
                name: "IX_Conta_IdAccountFlag",
                table: "Conta");

            migrationBuilder.DropColumn(
                name: "IdAccountFlag",
                table: "Conta");
        }
    }
}
