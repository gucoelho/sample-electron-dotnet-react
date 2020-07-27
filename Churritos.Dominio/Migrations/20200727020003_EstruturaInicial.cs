using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Churritos.Dominio.Migrations
{
    public partial class EstruturaInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adicional",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(nullable: true),
                    Valor = table.Column<decimal>(nullable: false),
                    Tipo = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adicional", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(nullable: true),
                    Cpf = table.Column<string>(nullable: true),
                    Telefone = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Origem = table.Column<string>(nullable: true),
                    Tipo = table.Column<string>(nullable: true),
                    MeioDePagamento = table.Column<string>(nullable: true),
                    TaxaDeEntrega = table.Column<decimal>(nullable: false),
                    TempoEstimado = table.Column<int>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    Desconto = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoriaId = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produto_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Endereço",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Estado = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: true),
                    Logradouro = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: true),
                    Complemento = table.Column<string>(nullable: true),
                    ClienteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereço", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endereço_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdicionalProduto",
                columns: table => new
                {
                    ProdutoId = table.Column<int>(nullable: false),
                    AdicionalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdicionalProduto", x => new { x.ProdutoId, x.AdicionalId });
                    table.ForeignKey(
                        name: "FK_AdicionalProduto_Adicional_AdicionalId",
                        column: x => x.AdicionalId,
                        principalTable: "Adicional",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdicionalProduto_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoPedido",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProdutoId = table.Column<int>(nullable: true),
                    Valor = table.Column<decimal>(nullable: false),
                    PedidoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutoPedido_Pedido_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProdutoPedido_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdicionalProdutoPedido",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AdicionalId = table.Column<int>(nullable: false),
                    ProdutoPedidoId = table.Column<int>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdicionalProdutoPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdicionalProdutoPedido_Adicional_AdicionalId",
                        column: x => x.AdicionalId,
                        principalTable: "Adicional",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdicionalProdutoPedido_ProdutoPedido_ProdutoPedidoId",
                        column: x => x.ProdutoPedidoId,
                        principalTable: "ProdutoPedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 1, "Coco", "Cobertura", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 32, "Teste", "Extra", 2.34m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 31, "Bacon", "Extra", 2m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 30, "Pizza", "Recheio", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 29, "Carne seca", "Recheio", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 28, "Carne moída", "Recheio", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 27, "Calabresa", "Recheio", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 26, "Frango", "Recheio", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 25, "Nutella com ninho", "Recheio", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 24, "Ninho", "Recheio", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 23, "Nutella", "Recheio", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 22, "Misto", "Recheio", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 21, "Goiabada", "Recheio", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 20, "Chocolate", "Recheio", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 19, "Doce de leite", "Recheio", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 18, "Cream cheese", "Cobertura", 2m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 17, "Catupiry", "Cobertura", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 16, "Cheddar", "Cobertura", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 2, "Confete", "Cobertura", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 3, "Granulado", "Cobertura", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 4, "Granulado colorido", "Cobertura", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 5, "Choco ball", "Cobertura", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 6, "Amendoim moído", "Cobertura", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 7, "Oreo", "Cobertura", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 8, "Kit kat preto", "Cobertura", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 10, "Ouro branco", "Cobertura", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 11, "Sonho de valsa", "Cobertura", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 12, "Ovomantine", "Cobertura", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 13, "Ninho em pó", "Cobertura", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 14, "Morango", "Cobertura", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 15, "Banana", "Cobertura", 0m });

            migrationBuilder.InsertData(
                table: "Adicional",
                columns: new[] { "Id", "Nome", "Tipo", "Valor" },
                values: new object[] { 9, "Kit kat branco", "Cobertura", 0m });

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 1, "Churros" });

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 2, "Bebidas" });

            migrationBuilder.InsertData(
                table: "Produto",
                columns: new[] { "Id", "CategoriaId", "Nome", "Valor" },
                values: new object[] { 1, 1, "Churros Doce Tradicional", 8m });

            migrationBuilder.InsertData(
                table: "Produto",
                columns: new[] { "Id", "CategoriaId", "Nome", "Valor" },
                values: new object[] { 2, 1, "Churros Doce Especial", 12m });

            migrationBuilder.InsertData(
                table: "Produto",
                columns: new[] { "Id", "CategoriaId", "Nome", "Valor" },
                values: new object[] { 3, 1, "Churros Doce Gelado", 16m });

            migrationBuilder.InsertData(
                table: "Produto",
                columns: new[] { "Id", "CategoriaId", "Nome", "Valor" },
                values: new object[] { 4, 1, "Churros Salgado", 10m });

            migrationBuilder.InsertData(
                table: "Produto",
                columns: new[] { "Id", "CategoriaId", "Nome", "Valor" },
                values: new object[] { 5, 2, "Coca-Cola Lata 269ml", 4.99m });

            migrationBuilder.InsertData(
                table: "Produto",
                columns: new[] { "Id", "CategoriaId", "Nome", "Valor" },
                values: new object[] { 6, 2, "Guaraná Lata 269ml", 4.99m });

            migrationBuilder.InsertData(
                table: "Produto",
                columns: new[] { "Id", "CategoriaId", "Nome", "Valor" },
                values: new object[] { 7, 2, "Suco Dell Valle 250ml", 4.50m });

            migrationBuilder.InsertData(
                table: "AdicionalProduto",
                columns: new[] { "ProdutoId", "AdicionalId" },
                values: new object[] { 1, 19 });

            migrationBuilder.InsertData(
                table: "AdicionalProduto",
                columns: new[] { "ProdutoId", "AdicionalId" },
                values: new object[] { 4, 16 });

            migrationBuilder.InsertData(
                table: "AdicionalProduto",
                columns: new[] { "ProdutoId", "AdicionalId" },
                values: new object[] { 4, 32 });

            migrationBuilder.InsertData(
                table: "AdicionalProduto",
                columns: new[] { "ProdutoId", "AdicionalId" },
                values: new object[] { 4, 31 });

            migrationBuilder.InsertData(
                table: "AdicionalProduto",
                columns: new[] { "ProdutoId", "AdicionalId" },
                values: new object[] { 4, 30 });

            migrationBuilder.InsertData(
                table: "AdicionalProduto",
                columns: new[] { "ProdutoId", "AdicionalId" },
                values: new object[] { 4, 29 });

            migrationBuilder.InsertData(
                table: "AdicionalProduto",
                columns: new[] { "ProdutoId", "AdicionalId" },
                values: new object[] { 4, 28 });

            migrationBuilder.InsertData(
                table: "AdicionalProduto",
                columns: new[] { "ProdutoId", "AdicionalId" },
                values: new object[] { 4, 27 });

            migrationBuilder.InsertData(
                table: "AdicionalProduto",
                columns: new[] { "ProdutoId", "AdicionalId" },
                values: new object[] { 4, 26 });

            migrationBuilder.InsertData(
                table: "AdicionalProduto",
                columns: new[] { "ProdutoId", "AdicionalId" },
                values: new object[] { 1, 6 });

            migrationBuilder.InsertData(
                table: "AdicionalProduto",
                columns: new[] { "ProdutoId", "AdicionalId" },
                values: new object[] { 1, 5 });

            migrationBuilder.InsertData(
                table: "AdicionalProduto",
                columns: new[] { "ProdutoId", "AdicionalId" },
                values: new object[] { 1, 4 });

            migrationBuilder.InsertData(
                table: "AdicionalProduto",
                columns: new[] { "ProdutoId", "AdicionalId" },
                values: new object[] { 1, 3 });

            migrationBuilder.InsertData(
                table: "AdicionalProduto",
                columns: new[] { "ProdutoId", "AdicionalId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "AdicionalProduto",
                columns: new[] { "ProdutoId", "AdicionalId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "AdicionalProduto",
                columns: new[] { "ProdutoId", "AdicionalId" },
                values: new object[] { 1, 22 });

            migrationBuilder.InsertData(
                table: "AdicionalProduto",
                columns: new[] { "ProdutoId", "AdicionalId" },
                values: new object[] { 1, 21 });

            migrationBuilder.InsertData(
                table: "AdicionalProduto",
                columns: new[] { "ProdutoId", "AdicionalId" },
                values: new object[] { 1, 20 });

            migrationBuilder.InsertData(
                table: "AdicionalProduto",
                columns: new[] { "ProdutoId", "AdicionalId" },
                values: new object[] { 4, 17 });

            migrationBuilder.InsertData(
                table: "AdicionalProduto",
                columns: new[] { "ProdutoId", "AdicionalId" },
                values: new object[] { 4, 18 });

            migrationBuilder.CreateIndex(
                name: "IX_AdicionalProduto_AdicionalId",
                table: "AdicionalProduto",
                column: "AdicionalId");

            migrationBuilder.CreateIndex(
                name: "IX_AdicionalProdutoPedido_AdicionalId",
                table: "AdicionalProdutoPedido",
                column: "AdicionalId");

            migrationBuilder.CreateIndex(
                name: "IX_AdicionalProdutoPedido_ProdutoPedidoId",
                table: "AdicionalProdutoPedido",
                column: "ProdutoPedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereço_ClienteId",
                table: "Endereço",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_CategoriaId",
                table: "Produto",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoPedido_PedidoId",
                table: "ProdutoPedido",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoPedido_ProdutoId",
                table: "ProdutoPedido",
                column: "ProdutoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdicionalProduto");

            migrationBuilder.DropTable(
                name: "AdicionalProdutoPedido");

            migrationBuilder.DropTable(
                name: "Endereço");

            migrationBuilder.DropTable(
                name: "Adicional");

            migrationBuilder.DropTable(
                name: "ProdutoPedido");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
