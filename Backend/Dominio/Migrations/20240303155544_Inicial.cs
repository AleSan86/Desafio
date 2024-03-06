using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dominio.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ciudad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Latitud = table.Column<double>(type: "double", nullable: false),
                    Longitud = table.Column<double>(type: "double", nullable: false),
                    Activo = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoVehiculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoVehiculo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Patente = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Anio = table.Column<int>(type: "int", nullable: false),
                    Tara = table.Column<int>(type: "int", nullable: false),
                    IdTipoVehiculo = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehiculo_TipoVehiculo_IdTipoVehiculo",
                        column: x => x.IdTipoVehiculo,
                        principalTable: "TipoVehiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Viaje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime", maxLength: 50, nullable: false),
                    FechaAnterior = table.Column<DateTime>(type: "datetime", nullable: true),
                    Activo = table.Column<byte>(type: "tinyint", nullable: false),
                    IdVehiculo = table.Column<int>(type: "int", nullable: false),
                    IdCiudad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viaje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Viaje_Ciudad_IdCiudad",
                        column: x => x.IdCiudad,
                        principalTable: "Ciudad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Viaje_Vehiculo_IdVehiculo",
                        column: x => x.IdVehiculo,
                        principalTable: "Vehiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_IdTipoVehiculo",
                table: "Vehiculo",
                column: "IdTipoVehiculo");

            migrationBuilder.CreateIndex(
                name: "IX_Viaje_IdCiudad",
                table: "Viaje",
                column: "IdCiudad");

            migrationBuilder.CreateIndex(
                name: "IX_Viaje_IdVehiculo",
                table: "Viaje",
                column: "IdVehiculo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Viaje");

            migrationBuilder.DropTable(
                name: "Ciudad");

            migrationBuilder.DropTable(
                name: "Vehiculo");

            migrationBuilder.DropTable(
                name: "TipoVehiculo");
        }
    }
}
