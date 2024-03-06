namespace BackendAPI.Dto
{
    public class ViajeDto
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime? FechaAnterior { get; set; }
        public byte Activo { get; set; }
        public VehiculoDto Vehiculo { get; set; }
        public CiudadDto Ciudad { get; set; }
    }

    public class ViajeAMDto
    {
        public int? Id { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime? FechaAnterior { get; set; }
        public byte Activo { get; set; }
        public int IdVehiculo { get; set; }
        public int IdCiudad { get; set; }
    }
}
