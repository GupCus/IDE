using Data;
using DTOs;

namespace Application.Services
{
    public class PaisService
    {
        public IEnumerable<PaisDTO> GetAll()
        {
            var paisRepository = new PaisRepository();
            return paisRepository.GetAll().Select(pais => new PaisDTO
            {
                Id = pais.Id,
                Nombre = pais.Nombre
            }).ToList();
        }
    }
}