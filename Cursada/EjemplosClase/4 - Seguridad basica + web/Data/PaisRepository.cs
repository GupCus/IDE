using Domain.Model;

namespace Data
{
    public class PaisRepository
    {
        private TPIContext CreateContext()
        {
            return new TPIContext();
        }

        public IEnumerable<Pais> GetAll()
        {
            using var context = CreateContext();
            return context.Paises.OrderBy(p => p.Nombre).ToList();
        }
    }
}