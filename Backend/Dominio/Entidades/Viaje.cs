using Dominio.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("Viaje")]
    public class Viaje : IEntidad
    {
        [Key] public int Id { get; set; }
        [StringLength(50), Required] public DateTime Fecha { get; set; }
        public DateTime? FechaAnterior {  get; set; }
        public byte Activo { get; set; } = 1;
        [ForeignKey("Vehiculo")] public int IdVehiculo { get; set; }
        public virtual Vehiculo Vehiculo { get; set; }
        [ForeignKey("Ciudad")] public int IdCiudad { get; set; }
        public virtual Ciudad Ciudad { get; set; }

    }
}
