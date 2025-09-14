using Microsoft.EntityFrameworkCore;

namespace Lab01.Components.Clases
{
    internal class UniversidadContext: DbContext
    {
        public DbSet<Alumno> Alumnos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;
                                        Initial Catalog=Universidad;
                                        Integrated Security=true;
                                        TrustServerCertificate=True");

            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }

        public UniversidadContext()
        {
            Database.EnsureCreated();
        }

        /*
            TIP: instanciar la clase UniversidadContext anteponiendo la palabra clave using a la
            declaración de la variable; de esta manera, nos aseguraremos de cerrar la conexión con la base
            de datos y liberar recursos cuando finalice su utilización.
        */
    }
}

