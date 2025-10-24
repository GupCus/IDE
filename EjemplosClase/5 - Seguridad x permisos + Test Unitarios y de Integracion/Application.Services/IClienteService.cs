using DTOs;

namespace Application.Services
{
    public interface IClienteService
    {
        ClienteDTO Add(ClienteDTO dto);
        bool Delete(int id);
        ClienteDTO? Get(int id);
        IEnumerable<ClienteDTO> GetAll();
        bool Update(ClienteDTO dto);
        IEnumerable<ClienteDTO> GetByCriteria(ClienteCriteriaDTO criteriaDTO);
    }
}