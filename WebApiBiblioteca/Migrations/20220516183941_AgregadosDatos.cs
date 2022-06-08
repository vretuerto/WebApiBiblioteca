using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiBiblioteca.Migrations
{
    public partial class AgregadosDatos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Editoriales",
                columns: new[] { "EditorialId", "Nombre" },
                values: new object[] { 1, "Editorial 1" });

            migrationBuilder.InsertData(
                table: "Editoriales",
                columns: new[] { "EditorialId", "Nombre" },
                values: new object[] { 2, "Editorial 2" });

            migrationBuilder.InsertData(
                table: "Libros",
                columns: new[] { "LibroId", "EditorialId", "Nombre", "Paginas" },
                values: new object[,]
                {
                    { 1, 1, "Libro 1", 50 },
                    { 2, 2, "Libro 2", 500 },
                    { 3, 1, "Libro 3", 1500 },
                    { 4, 2, "Libro 4", 850 },
                    { 5, 1, "Libro 5", 700 },
                    { 6, 2, "Libro 6", 4000 },
                    { 7, 1, "Libro 7", 550 },
                    { 8, 2, "Libro 8", 150 },
                    { 9, 1, "Libro 9", 100 },
                    { 10, 2, "Libro 10", 50 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "LibroId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "LibroId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "LibroId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "LibroId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "LibroId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "LibroId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "LibroId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "LibroId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "LibroId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "LibroId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Editoriales",
                keyColumn: "EditorialId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Editoriales",
                keyColumn: "EditorialId",
                keyValue: 2);
        }
    }
}
