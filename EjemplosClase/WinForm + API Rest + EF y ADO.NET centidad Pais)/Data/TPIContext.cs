using Microsoft.EntityFrameworkCore;
using Domain.Model;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class TPIContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pais> Paises { get; set; }

        internal TPIContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                string connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();
                
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);
                
                // Restricción única para Email
                entity.HasIndex(e => e.Email)
                    .IsUnique();
                
                entity.Property(e => e.FechaAlta)
                    .IsRequired();

                entity.Property(e => e.PaisId)
                    .IsRequired()
                    .HasField("_paisId");
                
                entity.Navigation(e => e.Pais)
                    .HasField("_pais");
                    
                entity.HasOne(e => e.Pais)
                    .WithMany()
                    .HasForeignKey(e => e.PaisId);
            });

            modelBuilder.Entity<Pais>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();
                
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.HasIndex(e => e.Nombre)
                    .IsUnique();

                // Datos iniciales
                entity.HasData(
                    new { Id = 1, Nombre = "Argentina" },
                    new { Id = 2, Nombre = "Brasil" },
                    new { Id = 3, Nombre = "Chile" },
                    new { Id = 4, Nombre = "Uruguay" },
                    new { Id = 5, Nombre = "Paraguay" }
                );
            });
        }
    }
}