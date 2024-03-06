namespace BackendAPI.Dto
{
    public class CiudadDto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public byte Activo { get; set; }
    }

    public class CiudadCrearDto
    {
        public string Descripcion { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public byte Activo { get; set; }
    }
}
