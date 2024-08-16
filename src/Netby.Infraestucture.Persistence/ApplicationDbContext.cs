using Microsoft.EntityFrameworkCore;
using Netby.Domain;

namespace Netby.Infraestucture.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Formulario> Formularios { get; set; }
        public DbSet<Campo> Campos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Permiso>()
            //    .HasOne(p => p.TipoPermiso)
            //    .WithMany(tp => tp.Permisos)
            //    .HasForeignKey(p => p.TipoPermisoId);

            // Configuración de las relaciones y restricciones

            modelBuilder.Entity<Formulario>()
                .HasMany(f => f.Campos)
                .WithOne(c => c.Formulario)
                .HasForeignKey(c => c.FormularioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Opcional: Semilla de datos inicial
            modelBuilder.Entity<Formulario>().HasData(
                    new Formulario { Id = 1, Nombre = "Datos Personales", Descripcion = "Formulario para ingresar datos básicos de una persona" },
                    new Formulario { Id = 2, Nombre = "Datos de la Mascota", Descripcion = "Formulario para ingresar datos de la mascota" }
                );

            modelBuilder.Entity<Campo>().HasData(
                new Campo { Id = 1, FormularioId = 1, NombreCampo = "Nombre", TipoCampo = "text", EsRequerido = true },
                new Campo { Id = 2, FormularioId = 1, NombreCampo = "Fecha de Nacimiento", TipoCampo = "date", EsRequerido = true },
                new Campo { Id = 3, FormularioId = 1, NombreCampo = "Estatura", TipoCampo = "number", EsRequerido = false },
                new Campo { Id = 4, FormularioId = 2, NombreCampo = "Especie", TipoCampo = "text", EsRequerido = true },
                new Campo { Id = 5, FormularioId = 2, NombreCampo = "Raza", TipoCampo = "text", EsRequerido = false },
                new Campo { Id = 6, FormularioId = 2, NombreCampo = "Color", TipoCampo = "text", EsRequerido = false }
            );
        }
    }
}