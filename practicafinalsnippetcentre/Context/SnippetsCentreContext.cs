using Clases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Context
{
    public class SnippetsCentreContext : DbContext
    {
        public DbSet<Lenguaje> Lenguajes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;
                                        Initial Catalog=SnippetCentre;
                                        Integrated Security=true;
                                        TrustServerCertificate=True");

            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }

        public SnippetsCentreContext()
        {
            Database.EnsureCreated();
        }

        
    }
}
