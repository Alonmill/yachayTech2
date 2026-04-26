using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YachayTech_p_cov.Migrations
{
    /// <inheritdoc />
    public partial class SeedPreguntas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Preguntas",
                columns: new[] { "Id", "Enunciado", "TituloEtapa" },
                values: new object[,]
                {
                    { 1, "¿Qué te apasiona más de la tecnología?", "Alineación de Perfil Tecnológico" },
                    { 2, "¿Qué lenguaje prefieres para Backend?", "Estrategia de Desarrollo" },
                    { 3, "¿Cuál es tu herramienta de IA favorita?", "Estrategia de Innovación" },
                    { 4, "¡Has completado el Nivel 2!", "Final" },
                    { 5, "¿Con qué ecosistema de tecnologías emergentes prefieres experimentar para resolver problemas complejos?", "Exploración e I+D" }
                });

            migrationBuilder.InsertData(
                table: "Opciones",
                columns: new[] { "Id", "PreguntaId", "Puntos", "SiguientePreguntaId", "Texto" },
                values: new object[,]
                {
                    { 1, 1, 3, 2, "Arquitectura y creación de aplicaciones escalables" },
                    { 2, 1, 3, 3, "Entrenamiento de modelos predictivos y automatización (IA)" },
                    { 3, 1, 3, 5, "Investigación e integración de tecnologías emergentes (I+D)" },
                    { 4, 2, 5, 4, "C# / .NET" },
                    { 5, 2, 4, 4, "Java" },
                    { 6, 3, 5, 4, "Python / PyTorch" },
                    { 7, 3, 3, 4, "Modelos Pre-entrenados" },
                    { 8, 5, 5, 4, "Internet de las Cosas (IoT), Robótica y Hardware" },
                    { 9, 5, 4, 4, "Blockchain, Web3 y Sistemas Descentralizados" },
                    { 10, 5, 5, 4, "Computación Cuántica o simulación de nuevos materiales" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Opciones",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Opciones",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Opciones",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Opciones",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Opciones",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Opciones",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Opciones",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Opciones",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Opciones",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Opciones",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Preguntas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Preguntas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Preguntas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Preguntas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Preguntas",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
