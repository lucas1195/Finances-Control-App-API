using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finances_Control_App_API.Migrations
{
    /// <inheritdoc />
    public partial class EntityUpgrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountFlag",
                columns: table => new
                {
                    IdAccountFlag = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NmAccountFlag = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ImgAccountFlag = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountFlag", x => x.IdAccountFlag);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transferencia_IdCategoria",
                table: "Transferencia",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Transferencia_IdConta",
                table: "Transferencia",
                column: "IdConta");

            migrationBuilder.CreateIndex(
                name: "IX_Transferencia_IdUsuario",
                table: "Transferencia",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Conta_IdUsuario",
                table: "Conta",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Conta_Usuario_IdUsuario",
                table: "Conta",
                column: "IdUsuario",
                principalTable: "Usuario",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transferencia_Categoria_IdCategoria",
                table: "Transferencia",
                column: "IdCategoria",
                principalTable: "Categoria",
                principalColumn: "IdCategoria",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transferencia_Conta_IdConta",
                table: "Transferencia",
                column: "IdConta",
                principalTable: "Conta",
                principalColumn: "IdConta",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transferencia_Usuario_IdUsuario",
                table: "Transferencia",
                column: "IdUsuario",
                principalTable: "Usuario",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conta_Usuario_IdUsuario",
                table: "Conta");

            migrationBuilder.DropForeignKey(
                name: "FK_Transferencia_Categoria_IdCategoria",
                table: "Transferencia");

            migrationBuilder.DropForeignKey(
                name: "FK_Transferencia_Conta_IdConta",
                table: "Transferencia");

            migrationBuilder.DropForeignKey(
                name: "FK_Transferencia_Usuario_IdUsuario",
                table: "Transferencia");

            migrationBuilder.DropTable(
                name: "AccountFlag");

            migrationBuilder.DropIndex(
                name: "IX_Transferencia_IdCategoria",
                table: "Transferencia");

            migrationBuilder.DropIndex(
                name: "IX_Transferencia_IdConta",
                table: "Transferencia");

            migrationBuilder.DropIndex(
                name: "IX_Transferencia_IdUsuario",
                table: "Transferencia");

            migrationBuilder.DropIndex(
                name: "IX_Conta_IdUsuario",
                table: "Conta");
        }
    }
}
