using Dominio.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("TipoVehiculo")]
    public class TipoVehiculo : IEntidad
    {
        [Key] public int Id { get; set; }
        [StringLength(50), Required] public string Descripcion { get; set; }
    }
}
