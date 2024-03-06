using Dominio.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("Vehiculo")]
    public class Vehiculo : IEntidad
    {
        [Key] public int Id { get; set; }
        [StringLength(50), Required] public string Patente { get; set; }
        [StringLength(50), Required] public string Marca { get; set; }
        [Required] public int Anio { get; set; }
        public int Tara { get; set; }
        [ForeignKey("TipoVehiculo")] public int IdTipoVehiculo { get; set; }
        public virtual TipoVehiculo TipoVehiculo { get; set; }
        public byte Activo { get; set; } = 1;

    }
}
