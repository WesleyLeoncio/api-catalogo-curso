using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_catalogo_curso.Migrations
{
    /// <inheritdoc />
    public partial class alter_table_ajuste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoBd_CategoriaBd_CategoriaId",
                table: "ProdutoBd");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdutoBd",
                table: "ProdutoBd");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriaBd",
                table: "CategoriaBd");

            migrationBuilder.RenameTable(
                name: "ProdutoBd",
                newName: "produtos");

            migrationBuilder.RenameTable(
                name: "CategoriaBd",
                newName: "categorias");

            migrationBuilder.RenameColumn(
                name: "Preco",
                table: "produtos",
                newName: "preco");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "produtos",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Estoque",
                table: "produtos",
                newName: "estoque");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "produtos",
                newName: "descricao");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "produtos",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ImagemUrl",
                table: "produtos",
                newName: "imagem_url");

            migrationBuilder.RenameColumn(
                name: "DataCadastro",
                table: "produtos",
                newName: "data_cadastro");

            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "produtos",
                newName: "categoria_id");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutoBd_CategoriaId",
                table: "produtos",
                newName: "IX_produtos_categoria_id");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "categorias",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "categorias",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ImagemUrl",
                table: "categorias",
                newName: "imagem_url");

            migrationBuilder.AlterColumn<decimal>(
                name: "preco",
                table: "produtos",
                type: "numeric(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "nome",
                table: "produtos",
                type: "varchar(80)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "descricao",
                table: "produtos",
                type: "varchar(300)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "imagem_url",
                table: "produtos",
                type: "varchar(300)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "nome",
                table: "categorias",
                type: "character varying(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "imagem_url",
                table: "categorias",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_produtos",
                table: "produtos",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categorias",
                table: "categorias",
                column: "id");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_produtos",
                table: "produtos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categorias",
                table: "categorias");

            migrationBuilder.RenameTable(
                name: "produtos",
                newName: "ProdutoBd");

            migrationBuilder.RenameTable(
                name: "categorias",
                newName: "CategoriaBd");

            migrationBuilder.RenameColumn(
                name: "preco",
                table: "ProdutoBd",
                newName: "Preco");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "ProdutoBd",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "estoque",
                table: "ProdutoBd",
                newName: "Estoque");

            migrationBuilder.RenameColumn(
                name: "descricao",
                table: "ProdutoBd",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ProdutoBd",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "imagem_url",
                table: "ProdutoBd",
                newName: "ImagemUrl");

            migrationBuilder.RenameColumn(
                name: "data_cadastro",
                table: "ProdutoBd",
                newName: "DataCadastro");

            migrationBuilder.RenameColumn(
                name: "categoria_id",
                table: "ProdutoBd",
                newName: "CategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_produtos_categoria_id",
                table: "ProdutoBd",
                newName: "IX_ProdutoBd_CategoriaId");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "CategoriaBd",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CategoriaBd",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "imagem_url",
                table: "CategoriaBd",
                newName: "ImagemUrl");

            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "ProdutoBd",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(10,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "ProdutoBd",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(80)");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "ProdutoBd",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(300)");

            migrationBuilder.AlterColumn<string>(
                name: "ImagemUrl",
                table: "ProdutoBd",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(300)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "CategoriaBd",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(80)",
                oldMaxLength: 80);

            migrationBuilder.AlterColumn<string>(
                name: "ImagemUrl",
                table: "CategoriaBd",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(300)",
                oldMaxLength: 300);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdutoBd",
                table: "ProdutoBd",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriaBd",
                table: "CategoriaBd",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoBd_CategoriaBd_CategoriaId",
                table: "ProdutoBd",
                column: "CategoriaId",
                principalTable: "CategoriaBd",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
