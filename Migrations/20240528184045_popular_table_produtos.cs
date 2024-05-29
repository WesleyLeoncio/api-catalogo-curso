using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_catalogo_curso.Migrations
{
    /// <inheritdoc />
    public partial class popular_table_produtos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO produtos(nome, descricao,preco,imagem_url,estoque,data_cadastro,categoria_id)" +
                   " VALUES('Coca cola', 'Refrigerante de cola 350ml', 5.45, 'cocacola.png',50,now(),1)");
            
            mb.Sql("INSERT INTO produtos(nome, descricao,preco,imagem_url,estoque,data_cadastro,categoria_id)" +
                   " VALUES('Hamburgue', 'Hamburgue Xtudo', 18.45, 'hamburgue.png',50,now(),2)");
           
            mb.Sql("INSERT INTO produtos(nome, descricao,preco,imagem_url,estoque,data_cadastro,categoria_id)" +
                   " VALUES('Sorvete', 'Sorvete de Bombom', 15.45, 'sorvete.png',50,now(),3)");
           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM produtos");
        }
    }
}
