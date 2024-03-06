using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Dominio.Contexto
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(){}

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
           : base(options)
        {
        }
        public virtual DbSet<Ciudad> Ciudades { get; set; }
        public virtual DbSet<Vehiculo> Vehiculos { get; set; }
        public virtual DbSet<TipoVehiculo> TipoVehiculos { get; set; }
        public virtual DbSet<Viaje> Viajes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Viaje>();
            modelBuilder.Entity<Vehiculo>();
            modelBuilder.Entity<TipoVehiculo>();
            modelBuilder.Entity<Ciudad>();
        }

    }
}
