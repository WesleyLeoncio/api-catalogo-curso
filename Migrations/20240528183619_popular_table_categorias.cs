using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_catalogo_curso.Migrations
{
    /// <inheritdoc />
    public partial class popular_table_categorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO categorias(nome, imagem_url) VALUES('Bebidas', 'bebidas.png')");
            mb.Sql("INSERT INTO categorias(nome, imagem_url) VALUES('Lanches', 'lanches.png')");
            mb.Sql("INSERT INTO categorias(nome, imagem_url) VALUES('Sobremesas', 'sobremesas.png')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM categorias");
        }
    }
}
