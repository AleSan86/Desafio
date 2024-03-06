using Dominio.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("Ciudad")]
    public class Ciudad : IEntidad
    {
        [Key] public int Id { get; set; }
        [StringLength(50), Required] public string Descripcion { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public byte Activo { get; set; } = 1;
    }
}
