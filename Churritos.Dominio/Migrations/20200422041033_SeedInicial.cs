using Microsoft.EntityFrameworkCore.Migrations;

namespace Churritos.Dominio.Migrations
{
    public partial class SeedInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cobertura",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 1, "Confete" });

            migrationBuilder.InsertData(
                table: "Cobertura",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 2, "Coco" });

            migrationBuilder.InsertData(
                table: "Recheio",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 1, "Doce de leite" });

            migrationBuilder.InsertData(
                table: "Recheio",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 2, "Chocolate" });

            migrationBuilder.InsertData(
                table: "TipoItem",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 1, "Doces Tradicionais" });

            migrationBuilder.InsertData(
                table: "TipoItem",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 2, "Doces Especiais" });

            migrationBuilder.InsertData(
                table: "TipoItem",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 3, "Doces Gelados" });

            migrationBuilder.InsertData(
                table: "TipoItem",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 4, "Salgados" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cobertura",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cobertura",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Recheio",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Recheio",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TipoItem",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TipoItem",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TipoItem",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TipoItem",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
