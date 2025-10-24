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
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<GrupoPermiso> GruposPermisos { get; set; }

        public TPIContext(DbContextOptions<TPIContext> options) : base(options)
        {
            this.Database.EnsureCreated();
            SeedInitialData();
        }

        internal TPIContext()
        {
            this.Database.EnsureCreated();
            SeedInitialData();
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

                // Relación con GrupoPermiso (opcional)
                entity.HasOne(e => e.Grupo)
                    .WithMany()
                    .HasForeignKey(e => e.GrupoPermisoId)
                    .OnDelete(DeleteBehavior.SetNull);

                // Usuarios iniciales
                var adminUser = new Domain.Model.Usuario(1, "admin", "admin@tpi.com", "admin123", DateTime.Now);
                var vendedorUser = new Domain.Model.Usuario(2, "vendedor", "vendedor@tpi.com", "vendedor123", DateTime.Now);
                
                entity.HasData(
                    new { 
                        Id = adminUser.Id, 
                        Username = adminUser.Username, 
                        Email = adminUser.Email,
                        PasswordHash = adminUser.PasswordHash,
                        Salt = adminUser.Salt,
                        FechaCreacion = adminUser.FechaCreacion,
                        Activo = adminUser.Activo
                    },
                    new { 
                        Id = vendedorUser.Id, 
                        Username = vendedorUser.Username, 
                        Email = vendedorUser.Email,
                        PasswordHash = vendedorUser.PasswordHash,
                        Salt = vendedorUser.Salt,
                        FechaCreacion = vendedorUser.FechaCreacion,
                        Activo = vendedorUser.Activo
                    }
                );
            });

            // Configuración de Permiso
            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.ToTable("Permisos");
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();
                
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(200);
                
                entity.Property(e => e.Categoria)
                    .IsRequired()
                    .HasMaxLength(30);
                
                entity.Property(e => e.Activo)
                    .IsRequired();

                // Índice único para evitar permisos duplicados
                entity.HasIndex(e => new { e.Nombre, e.Categoria })
                    .IsUnique();
            });

            // Configuración de GrupoPermiso
            modelBuilder.Entity<GrupoPermiso>(entity =>
            {
                entity.ToTable("GruposPermisos");
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();
                
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
                
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(200);
                
                entity.Property(e => e.Activo)
                    .IsRequired();
                
                entity.Property(e => e.FechaCreacion)
                    .IsRequired();

                // Índice único para nombres de grupo
                entity.HasIndex(e => e.Nombre)
                    .IsUnique();
            });

            // Configuración de relación Many-to-Many: GrupoPermiso ↔ Permiso
            modelBuilder.Entity<GrupoPermiso>()
                .HasMany(g => g.Permisos)
                .WithMany(p => p.Grupos)
                .UsingEntity<Dictionary<string, object>>(
                    "GrupoPermisoPermiso", 
                    j => j.HasOne<Permiso>().WithMany().HasForeignKey("PermisosId"),
                    j => j.HasOne<GrupoPermiso>().WithMany().HasForeignKey("GruposId"),
                    j =>
                    {
                        j.HasKey("GruposId", "PermisosId");
                    });

            // SEED DATA - Permisos iniciales
            modelBuilder.Entity<Permiso>().HasData(
                // Permisos para Países
                new { Id = 1, Nombre = "leer", Descripcion = "Leer países", Categoria = "paises", Activo = true },
                new { Id = 2, Nombre = "agregar", Descripcion = "Agregar países", Categoria = "paises", Activo = true },
                new { Id = 3, Nombre = "actualizar", Descripcion = "Actualizar países", Categoria = "paises", Activo = true },
                new { Id = 4, Nombre = "eliminar", Descripcion = "Eliminar países", Categoria = "paises", Activo = true },

                // Permisos para Clientes
                new { Id = 5, Nombre = "leer", Descripcion = "Leer clientes", Categoria = "clientes", Activo = true },
                new { Id = 6, Nombre = "agregar", Descripcion = "Agregar clientes", Categoria = "clientes", Activo = true },
                new { Id = 7, Nombre = "actualizar", Descripcion = "Actualizar clientes", Categoria = "clientes", Activo = true },
                new { Id = 8, Nombre = "eliminar", Descripcion = "Eliminar clientes", Categoria = "clientes", Activo = true },

                // Permisos para Pedidos
                new { Id = 9, Nombre = "leer", Descripcion = "Leer pedidos", Categoria = "pedidos", Activo = true },
                new { Id = 10, Nombre = "agregar", Descripcion = "Agregar pedidos", Categoria = "pedidos", Activo = true },
                new { Id = 11, Nombre = "actualizar", Descripcion = "Actualizar pedidos", Categoria = "pedidos", Activo = true },
                new { Id = 12, Nombre = "eliminar", Descripcion = "Eliminar pedidos", Categoria = "pedidos", Activo = true },

                // Permisos para Usuarios
                new { Id = 13, Nombre = "leer", Descripcion = "Leer usuarios", Categoria = "usuarios", Activo = true },
                new { Id = 14, Nombre = "agregar", Descripcion = "Agregar usuarios", Categoria = "usuarios", Activo = true },
                new { Id = 15, Nombre = "actualizar", Descripcion = "Actualizar usuarios", Categoria = "usuarios", Activo = true },
                new { Id = 16, Nombre = "eliminar", Descripcion = "Eliminar usuarios", Categoria = "usuarios", Activo = true },

                // Permisos para Productos
                new { Id = 17, Nombre = "leer", Descripcion = "Leer productos", Categoria = "productos", Activo = true }
            );

            // SEED DATA - Grupos de permisos iniciales
            var fechaCreacion = DateTime.Now;
            modelBuilder.Entity<GrupoPermiso>().HasData(
                new { Id = 1, Nombre = "Administrador", Descripcion = "Acceso completo a todas las funcionalidades", Activo = true, FechaCreacion = fechaCreacion },
                new { Id = 2, Nombre = "Vendedor", Descripcion = "Acceso a pedidos y consulta de clientes", Activo = true, FechaCreacion = fechaCreacion }
            );
        }

        private void SeedInitialData()
        {
            try
            {
                // Verificar si ya existen las relaciones
                if (!Usuarios.Any(u => u.GrupoPermisoId != null) &&
                    Usuarios.Any() && 
                    GruposPermisos.Any() && 
                    Permisos.Any())
                {
                    // Cargar datos necesarios
                    var adminUser = Usuarios.Include(u => u.Grupo).FirstOrDefault(u => u.Username == "admin");
                    var vendedorUser = Usuarios.Include(u => u.Grupo).FirstOrDefault(u => u.Username == "vendedor");
                    var grupoAdmin = GruposPermisos.Include(g => g.Permisos).FirstOrDefault(g => g.Nombre == "Administrador");
                    var grupoVendedor = GruposPermisos.Include(g => g.Permisos).FirstOrDefault(g => g.Nombre == "Vendedor");
                    var todosLosPermisos = Permisos.ToList();

                    if (grupoAdmin != null && grupoVendedor != null && todosLosPermisos.Any())
                    {
                        // Asignar TODOS los permisos al grupo Administrador
                        foreach (var permiso in todosLosPermisos)
                        {
                            if (!grupoAdmin.Permisos.Contains(permiso))
                            {
                                grupoAdmin.Permisos.Add(permiso);
                            }
                        }

                        // Asignar permisos específicos al grupo Vendedor
                        var permisosVendedor = todosLosPermisos.Where(p => 
                            (p.Categoria == "pedidos") || // Todos los permisos de pedidos
                            (p.Categoria == "clientes" && p.Nombre == "leer") || // Solo leer clientes
                            (p.Categoria == "productos" && p.Nombre == "leer") // Solo leer productos
                        ).ToList();

                        foreach (var permiso in permisosVendedor)
                        {
                            if (!grupoVendedor.Permisos.Contains(permiso))
                            {
                                grupoVendedor.Permisos.Add(permiso);
                            }
                        }

                        // Asignar usuario admin al grupo Administrador (relación simple)
                        if (adminUser != null)
                        {
                            adminUser.SetGrupo(grupoAdmin);
                        }

                        // Asignar usuario vendedor al grupo Vendedor (relación simple)
                        if (vendedorUser != null)
                        {
                            vendedorUser.SetGrupo(grupoVendedor);
                        }

                        SaveChanges();
                    }
                }
            }
            catch
            {
                // Si hay algún error en el seed, lo ignoramos
                // Esto evita problemas en la inicialización
            }
        }
    }
}