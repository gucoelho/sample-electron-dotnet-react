using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Churritos.Dominio.Migrations
{
    public partial class EstruturaInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "TipoItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TipoId = table.Column<int>(nullable: true),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_TipoItem_TipoId",
                        column: x => x.TipoId,
                        principalTable: "TipoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TipoItemCobertura",
                columns: table => new
                {
                    TipoItemId = table.Column<int>(nullable: false),
                    CoberturaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoItemCobertura", x => new { x.TipoItemId, x.CoberturaId });
                    table.ForeignKey(
                        name: "FK_TipoItemCobertura_Cobertura_CoberturaId",
                        column: x => x.CoberturaId,
                        principalTable: "Cobertura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TipoItemCobertura_TipoItem_TipoItemId",
                        column: x => x.TipoItemId,
                        principalTable: "TipoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TipoItemRecheio",
                columns: table => new
                {
                    TipoItemId = table.Column<int>(nullable: false),
                    RecheioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoItemRecheio", x => new { x.TipoItemId, x.RecheioId });
                    table.ForeignKey(
                        name: "FK_TipoItemRecheio_Recheio_RecheioId",
                        column: x => x.RecheioId,
                        principalTable: "Recheio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TipoItemRecheio_TipoItem_TipoItemId",
                        column: x => x.TipoItemId,
                        principalTable: "TipoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "IX_Item_TipoId",
                table: "Item",
                column: "TipoId");

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

            migrationBuilder.CreateIndex(
                name: "IX_TipoItemCobertura_CoberturaId",
                table: "TipoItemCobertura",
                column: "CoberturaId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoItemRecheio_RecheioId",
                table: "TipoItemRecheio",
                column: "RecheioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemPedido");

            migrationBuilder.DropTable(
                name: "TipoItemCobertura");

            migrationBuilder.DropTable(
                name: "TipoItemRecheio");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Cobertura");

            migrationBuilder.DropTable(
                name: "Recheio");

            migrationBuilder.DropTable(
                name: "TipoItem");
        }
    }
}
