using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Churritos.Dominio.Migrations
{
    public partial class MigracaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Cobertura",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cobertura", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataCriacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedido", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recheio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recheio", x => x.Id);
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
                name: "ProdutoCobertura",
                columns: table => new
                {
                    ProdutoId = table.Column<int>(nullable: false),
                    CoberturaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoCobertura", x => new { x.ProdutoId, x.CoberturaId });
                    table.ForeignKey(
                        name: "FK_ProdutoCobertura_Cobertura_CoberturaId",
                        column: x => x.CoberturaId,
                        principalTable: "Cobertura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdutoCobertura_Produto_ProdutoId",
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
                    CoberturaSelecionadaId = table.Column<int>(nullable: true),
                    RecheioSelecionadoId = table.Column<int>(nullable: true),
                    Valor = table.Column<decimal>(nullable: false),
                    PedidoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutoPedido_Cobertura_CoberturaSelecionadaId",
                        column: x => x.CoberturaSelecionadaId,
                        principalTable: "Cobertura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    table.ForeignKey(
                        name: "FK_ProdutoPedido_Recheio_RecheioSelecionadoId",
                        column: x => x.RecheioSelecionadoId,
                        principalTable: "Recheio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoRecheio",
                columns: table => new
                {
                    ProdutoId = table.Column<int>(nullable: false),
                    RecheioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoRecheio", x => new { x.ProdutoId, x.RecheioId });
                    table.ForeignKey(
                        name: "FK_ProdutoRecheio_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProdutoRecheio_Recheio_RecheioId",
                        column: x => x.RecheioId,
                        principalTable: "Recheio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 1, "Doces Tradicionais" });

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 2, "Doces Especiais" });

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 3, "Doces Gelados" });

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 4, "Salgados" });

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 5, "Salgados Especiais" });

            migrationBuilder.InsertData(
                table: "Cobertura",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 18, "Cream cheese" });

            migrationBuilder.InsertData(
                table: "Cobertura",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 17, "Catupiry" });

            migrationBuilder.InsertData(
                table: "Cobertura",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 16, "Cheddar" });

            migrationBuilder.InsertData(
                table: "Cobertura",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 15, "Banana" });

            migrationBuilder.InsertData(
                table: "Cobertura",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 14, "Morango" });

            migrationBuilder.InsertData(
                table: "Cobertura",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 12, "Ovomantine" });

            migrationBuilder.InsertData(
                table: "Cobertura",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 11, "Sonho de valsa" });

            migrationBuilder.InsertData(
                table: "Cobertura",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 10, "Ouro branco" });

            migrationBuilder.InsertData(
                table: "Cobertura",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 13, "Ninho em pó" });

            migrationBuilder.InsertData(
                table: "Cobertura",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 8, "Kit kat preto" });

            migrationBuilder.InsertData(
                table: "Cobertura",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 7, "Oreo" });

            migrationBuilder.InsertData(
                table: "Cobertura",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 6, "Amendoim moído" });

            migrationBuilder.InsertData(
                table: "Cobertura",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 5, "Choco ball" });

            migrationBuilder.InsertData(
                table: "Cobertura",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 4, "Granulado colorido" });

            migrationBuilder.InsertData(
                table: "Cobertura",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 3, "Granulado" });

            migrationBuilder.InsertData(
                table: "Cobertura",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 2, "Confete" });

            migrationBuilder.InsertData(
                table: "Cobertura",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 1, "Coco" });

            migrationBuilder.InsertData(
                table: "Cobertura",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 9, "Kit kat branco" });

            migrationBuilder.InsertData(
                table: "Recheio",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 10, "Carne moída" });

            migrationBuilder.InsertData(
                table: "Recheio",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 9, "Calabresa" });

            migrationBuilder.InsertData(
                table: "Recheio",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 8, "Frango" });

            migrationBuilder.InsertData(
                table: "Recheio",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 7, "Nutella com ninho" });

            migrationBuilder.InsertData(
                table: "Recheio",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 6, "Ninho" });

            migrationBuilder.InsertData(
                table: "Recheio",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 2, "Chocolate" });

            migrationBuilder.InsertData(
                table: "Recheio",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 4, "Misto" });

            migrationBuilder.InsertData(
                table: "Recheio",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 3, "Goiabada" });

            migrationBuilder.InsertData(
                table: "Recheio",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 1, "Doce de leite" });

            migrationBuilder.InsertData(
                table: "Recheio",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 11, "Carne seca" });

            migrationBuilder.InsertData(
                table: "Recheio",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 5, "Nutella" });

            migrationBuilder.InsertData(
                table: "Recheio",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 12, "Pizza" });

            migrationBuilder.InsertData(
                table: "Produto",
                columns: new[] { "Id", "CategoriaId", "Nome", "Valor" },
                values: new object[] { 1, 1, "Churros Doce Tradicional", 8m });

            migrationBuilder.InsertData(
                table: "Produto",
                columns: new[] { "Id", "CategoriaId", "Nome", "Valor" },
                values: new object[] { 2, 2, "Churros Doce Especial", 12m });

            migrationBuilder.InsertData(
                table: "Produto",
                columns: new[] { "Id", "CategoriaId", "Nome", "Valor" },
                values: new object[] { 3, 3, "Churros Doce Gelado", 16m });

            migrationBuilder.InsertData(
                table: "Produto",
                columns: new[] { "Id", "CategoriaId", "Nome", "Valor" },
                values: new object[] { 4, 4, "Churros Salgado", 10m });

            migrationBuilder.InsertData(
                table: "Produto",
                columns: new[] { "Id", "CategoriaId", "Nome", "Valor" },
                values: new object[] { 5, 5, "Churros Salgado Especial", 12m });

            migrationBuilder.CreateIndex(
                name: "IX_Produto_CategoriaId",
                table: "Produto",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoCobertura_CoberturaId",
                table: "ProdutoCobertura",
                column: "CoberturaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoPedido_CoberturaSelecionadaId",
                table: "ProdutoPedido",
                column: "CoberturaSelecionadaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoPedido_PedidoId",
                table: "ProdutoPedido",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoPedido_ProdutoId",
                table: "ProdutoPedido",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoPedido_RecheioSelecionadoId",
                table: "ProdutoPedido",
                column: "RecheioSelecionadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoRecheio_RecheioId",
                table: "ProdutoRecheio",
                column: "RecheioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProdutoCobertura");

            migrationBuilder.DropTable(
                name: "ProdutoPedido");

            migrationBuilder.DropTable(
                name: "ProdutoRecheio");

            migrationBuilder.DropTable(
                name: "Cobertura");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Recheio");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
