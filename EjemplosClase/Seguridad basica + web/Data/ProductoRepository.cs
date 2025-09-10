using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ProductoRepository
    {
        private TPIContext CreateContext()
        {
            return new TPIContext();
        }

        public Producto? Get(int id)
        {
            using var context = CreateContext();
            return context.Productos.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Producto> GetAll()
        {
            using var context = CreateContext();
            return context.Productos.ToList();
        }
    }
}