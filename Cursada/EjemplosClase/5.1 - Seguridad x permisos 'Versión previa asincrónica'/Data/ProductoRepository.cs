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

        public async Task<Producto?> GetAsync(int id)
        {
            using var context = CreateContext();
            return await context.Productos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            using var context = CreateContext();
            return await context.Productos.ToListAsync();
        }
    }
}