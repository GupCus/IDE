using Microsoft.EntityFrameworkCore;
using Domain.Model;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class TPIContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

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

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();
                
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(500);
                
                entity.Property(e => e.Precio)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");
                
                entity.Property(e => e.Stock)
                    .IsRequired();

                // Datos iniciales de prueba
                entity.HasData(
                    new { Id = 1, Nombre = "Laptop Dell XPS 13", Descripcion = "Ultrabook de alto rendimiento", Precio = 1200.00m, Stock = 10 },
                    new { Id = 2, Nombre = "Mouse Logitech MX Master 3", Descripcion = "Mouse inalámbrico ergonómico", Precio = 89.99m, Stock = 25 },
                    new { Id = 3, Nombre = "Teclado Mecánico Corsair K70", Descripcion = "Teclado mecánico RGB", Precio = 149.99m, Stock = 15 },
                    new { Id = 4, Nombre = "Monitor Samsung 27 4K", Descripcion = "Monitor 4K de 27 pulgadas", Precio = 349.99m, Stock = 8 },
                    new { Id = 5, Nombre = "Auriculares Sony WH-1000XM4", Descripcion = "Auriculares con cancelación de ruido", Precio = 279.99m, Stock = 20 }
                );
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();
                
                entity.Property(e => e.ClienteId)
                    .IsRequired()
                    .HasField("_clienteId");
                
                entity.Navigation(e => e.Cliente)
                    .HasField("_cliente");
                
                entity.Property(e => e.FechaPedido)
                    .IsRequired();
                
                entity.HasOne(e => e.Cliente)
                    .WithMany()
                    .HasForeignKey(e => e.ClienteId);
                
                entity.OwnsMany(e => e.ItemsPedido, item =>
                {
                    item.WithOwner().HasForeignKey(i => i.PedidoId);
                    
                    item.Property(i => i.ProductoId)
                        .IsRequired()
                        .HasField("_productoId");
                    
                    item.Navigation(i => i.Producto)
                        .HasField("_producto");
                    
                    item.Property(i => i.Cantidad)
                        .IsRequired();
                    
                    item.Property(i => i.PrecioUnitario)
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");
                    
                    item.HasOne(i => i.Producto)
                        .WithMany()
                        .HasForeignKey(i => i.ProductoId);
                });
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();
                
                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(255);
                
                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(255);
                
                entity.Property(e => e.FechaCreacion)
                    .IsRequired();
                
                entity.Property(e => e.Activo)
                    .IsRequired();
                
                // Restricciones únicas
                entity.HasIndex(e => e.Username)
                    .IsUnique();
                
                entity.HasIndex(e => e.Email)
                    .IsUnique();

                // Usuario administrador inicial
                var adminUser = new Domain.Model.Usuario(1, "admin", "admin@tpi.com", "admin123", DateTime.Now);
                entity.HasData(new { 
                    Id = adminUser.Id, 
                    Username = adminUser.Username, 
                    Email = adminUser.Email,
                    PasswordHash = adminUser.PasswordHash,
                    Salt = adminUser.Salt,
                    FechaCreacion = adminUser.FechaCreacion,
                    Activo = adminUser.Activo
                });
            });
        }
    }
}