using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class PaisRepository
    {
        private TPIContext CreateContext()
        {
            return new TPIContext();
        }

        public async Task<IEnumerable<Pais>> GetAllAsync()
        {
            using var context = CreateContext();
            return await context.Paises.OrderBy(p => p.Nombre).ToListAsync();
        }
    }
}