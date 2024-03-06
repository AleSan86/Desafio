using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dominio.Migrations
{
    /// <inheritdoc />
    public partial class InicialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "Migrations",
              "20240303155718_Inicial_Data.sql");
            migrationBuilder.Sql(File.ReadAllText(filePath));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "Migrations",
                "20240303155718_Inicial_Data.sql");
            migrationBuilder.Sql(File.ReadAllText(filePath));
        }
    }
}
