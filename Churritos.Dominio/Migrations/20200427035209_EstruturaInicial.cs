using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Churritos.Dominio.Migrations
{
    public partial class EstruturaInicial : Migration
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
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoriaId = table.Column<int>(nullable: true),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoriaCobertura",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(nullable: false),
                    CoberturaId = table.Column<int>(nullable: false),
                    CategoriaId1 = table.Column<int>(nullable: true),
                    CoberturaId2 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaCobertura", x => new { x.CategoriaId, x.CoberturaId });
                    table.ForeignKey(
                        name: "FK_CategoriaCobertura_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoriaCobertura_Categoria_CategoriaId1",
                        column: x => x.CategoriaId1,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoriaCobertura_Cobertura_CoberturaId",
                        column: x => x.CoberturaId,
                        principalTable: "Cobertura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoriaCobertura_Cobertura_CoberturaId2",
                        column: x => x.CoberturaId2,
                        principalTable: "Cobertura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoriaRecheio",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(nullable: false),
                    RecheioId = table.Column<int>(nullable: false),
                    CategoriaId1 = table.Column<int>(nullable: true),
                    RecheioId2 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaRecheio", x => new { x.CategoriaId, x.RecheioId });
                    table.ForeignKey(
                        name: "FK_CategoriaRecheio_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoriaRecheio_Categoria_CategoriaId1",
                        column: x => x.CategoriaId1,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoriaRecheio_Recheio_RecheioId",
                        column: x => x.RecheioId,
                        principalTable: "Recheio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoriaRecheio_Recheio_RecheioId2",
                        column: x => x.RecheioId2,
                        principalTable: "Recheio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemPedido",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ItemId = table.Column<int>(nullable: true),
                    CoberturaSelecionadaId = table.Column<int>(nullable: true),
                    RecheioSelecionadoId = table.Column<int>(nullable: true),
                    PedidoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPedido_Cobertura_CoberturaSelecionadaId",
                        column: x => x.CoberturaSelecionadaId,
                        principalTable: "Cobertura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemPedido_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemPedido_Pedido_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemPedido_Recheio_RecheioSelecionadoId",
                        column: x => x.RecheioSelecionadoId,
                        principalTable: "Recheio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaCobertura_CategoriaId1",
                table: "CategoriaCobertura",
                column: "CategoriaId1");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaCobertura_CoberturaId",
                table: "CategoriaCobertura",
                column: "CoberturaId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaCobertura_CoberturaId2",
                table: "CategoriaCobertura",
                column: "CoberturaId2");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaRecheio_CategoriaId1",
                table: "CategoriaRecheio",
                column: "CategoriaId1");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaRecheio_RecheioId",
                table: "CategoriaRecheio",
                column: "RecheioId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriaRecheio_RecheioId2",
                table: "CategoriaRecheio",
                column: "RecheioId2");

            migrationBuilder.CreateIndex(
                name: "IX_Item_CategoriaId",
                table: "Item",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedido_CoberturaSelecionadaId",
                table: "ItemPedido",
                column: "CoberturaSelecionadaId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedido_ItemId",
                table: "ItemPedido",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedido_PedidoId",
                table: "ItemPedido",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedido_RecheioSelecionadoId",
                table: "ItemPedido",
                column: "RecheioSelecionadoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoriaCobertura");

            migrationBuilder.DropTable(
                name: "CategoriaRecheio");

            migrationBuilder.DropTable(
                name: "ItemPedido");

            migrationBuilder.DropTable(
                name: "Cobertura");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Recheio");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
