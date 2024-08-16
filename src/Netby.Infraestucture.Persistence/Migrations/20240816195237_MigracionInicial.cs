using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Netby.Infraestucture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Formularios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formularios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Campos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormularioId = table.Column<int>(type: "int", nullable: false),
                    NombreCampo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoCampo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EsRequerido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campos_Formularios_FormularioId",
                        column: x => x.FormularioId,
                        principalTable: "Formularios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Formularios",
                columns: new[] { "Id", "Descripcion", "Nombre" },
                values: new object[,]
                {
                    { 1, "Formulario para ingresar datos básicos de una persona", "Datos Personales" },
                    { 2, "Formulario para ingresar datos de la mascota", "Datos de la Mascota" }
                });

            migrationBuilder.InsertData(
                table: "Campos",
                columns: new[] { "Id", "EsRequerido", "FormularioId", "NombreCampo", "TipoCampo" },
                values: new object[,]
                {
                    { 1, true, 1, "Nombre", "text" },
                    { 2, true, 1, "Fecha de Nacimiento", "date" },
                    { 3, false, 1, "Estatura", "number" },
                    { 4, true, 2, "Especie", "text" },
                    { 5, false, 2, "Raza", "text" },
                    { 6, false, 2, "Color", "text" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Campos_FormularioId",
                table: "Campos",
                column: "FormularioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Campos");

            migrationBuilder.DropTable(
                name: "Formularios");
        }
    }
}
