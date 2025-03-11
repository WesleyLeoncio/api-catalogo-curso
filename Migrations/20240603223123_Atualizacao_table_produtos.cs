using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_catalogo_curso.Migrations
{
    /// <inheritdoc />
    public partial class Atualizacao_table_produtos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_produtos_categorias_CategoriaId",
                table: "produtos");

            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "produtos",
                newName: "categoria_id");

            migrationBuilder.RenameIndex(
                name: "IX_produtos_CategoriaId",
                table: "produtos",
                newName: "IX_produtos_categoria_id");

            migrationBuilder.AlterColumn<int>(
                name: "categoria_id",
                table: "produtos",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_produtos_categorias_categoria_id",
                table: "produtos",
                column: "categoria_id",
                principalTable: "categorias",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_produtos_categorias_categoria_id",
                table: "produtos");

            migrationBuilder.RenameColumn(
                name: "categoria_id",
                table: "produtos",
                newName: "CategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_produtos_categoria_id",
                table: "produtos",
                newName: "IX_produtos_CategoriaId");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaId",
                table: "produtos",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_produtos_categorias_CategoriaId",
                table: "produtos",
                column: "CategoriaId",
                principalTable: "categorias",
                principalColumn: "id");
        }
    }
}
