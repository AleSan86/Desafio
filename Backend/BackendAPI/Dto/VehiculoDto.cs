using Dominio.Entidades;

namespace BackendAPI.Dto
{
    public class VehiculoDto
    {
        public int Id { get; set; }
        public string Patente { get; set; }
        public string Marca { get; set; }
        public int Anio { get; set; }
        public int Tara { get; set; }
        public TipoVehiculoDto TipoVehiculo { get; set; }
        public byte Activo { get; set; }
    }

    public class VehiculoAltaDto
    {
        public string Patente { get; set; }
        public string Marca { get; set; }
        public int Anio { get; set; }
        public int Tara { get; set; }
        public int IdTipoVehiculo { get; set; }
        public byte Activo { get; set; }
    }
    public class VehiculoEditarDto
    {
        public int? Id { get; set; }
        public string Patente { get; set; }
        public string Marca { get; set; }
        public int Anio { get; set; }
        public int Tara { get; set; }
        public int IdTipoVehiculo { get; set; }
        public byte Activo { get; set; }
    }
}
