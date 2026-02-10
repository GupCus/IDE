using Barroso.Clases;
using Microsoft.EntityFrameworkCore;

namespace Barroso.Context
{
    public class LiquidacionesContext : DbContext
    {
        public DbSet<Liquidacion> Liquidaciones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;
                                        Initial Catalog=dbLiquidacion;
                                        Integrated Security=true;
                                        TrustServerCertificate=True");
        }

        public LiquidacionesContext()
        {
            Database.EnsureCreated();
        }

    }
}
