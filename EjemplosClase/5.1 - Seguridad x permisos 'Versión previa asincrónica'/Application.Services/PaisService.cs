using Data;
using DTOs;

namespace Application.Services
{
    public class PaisService
    {
        public async Task<IEnumerable<PaisDTO>> GetAllAsync()
        {
            var paisRepository = new PaisRepository();
            var paises = await paisRepository.GetAllAsync();
            return paises.Select(pais => new PaisDTO
            {
                Id = pais.Id,
                Nombre = pais.Nombre
            }).ToList();
        }
    }
}