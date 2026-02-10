using Domain.Model;

namespace Data
{
    public interface IClienteRepository
    {
        void Add(Cliente cliente);
        bool Delete(int id);
        Cliente? Get(int id);
        IEnumerable<Cliente> GetAll();
        bool Update(Cliente cliente);
        bool EmailExists(string email, int? excludeId = null);
        IEnumerable<Cliente> GetByCriteria(ClienteCriteria criteria);
    }
}