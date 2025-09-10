using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Data
{
    public class ClienteRepository
    {
        private TPIContext CreateContext()
        {
            return new TPIContext();
        }

        public void Add(Cliente cliente)
        {
            using var context = CreateContext();
            context.Clientes.Add(cliente);
            context.SaveChanges();
        }

        public bool Delete(int id)
        {
            using var context = CreateContext();
            var cliente = context.Clientes.Find(id);
            if (cliente != null)
            {
                context.Clientes.Remove(cliente);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public Cliente? Get(int id)
        {
            using var context = CreateContext();
            return context.Clientes
                .Include(c => c.Pais)
                .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Cliente> GetAll()
        {
            using var context = CreateContext();
            return context.Clientes
                .Include(c => c.Pais)
                .ToList();
        }

        public bool Update(Cliente cliente)
        {
            using var context = CreateContext();
            var existingCliente = context.Clientes.Find(cliente.Id);
            if (existingCliente != null)
            {
                existingCliente.SetNombre(cliente.Nombre);
                existingCliente.SetApellido(cliente.Apellido);
                existingCliente.SetEmail(cliente.Email);
                existingCliente.SetPaisId(cliente.PaisId);
                
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool EmailExists(string email, int? excludeId = null)
        {
            using var context = CreateContext();
            var query = context.Clientes.Where(c => c.Email.ToLower() == email.ToLower());
            if (excludeId.HasValue)
            {
                query = query.Where(c => c.Id != excludeId.Value);
            }
            return query.Any();
        }

        public IEnumerable<Cliente> GetByCriteria(ClienteCriteria criteria)
        {
            const string sql = @"
                SELECT c.Id, c.Nombre, c.Apellido, c.Email, c.PaisId, c.FechaAlta,
                       p.Nombre as PaisNombre
                FROM Clientes c
                INNER JOIN Paises p ON c.PaisId = p.Id
                WHERE c.Nombre LIKE @SearchTerm 
                   OR c.Apellido LIKE @SearchTerm 
                   OR c.Email LIKE @SearchTerm
                ORDER BY c.Nombre, c.Apellido";

            var clientes = new List<Cliente>();
            string connectionString = new TPIContext().Database.GetConnectionString();
            string searchPattern = $"%{criteria.Texto}%";

            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(sql, connection);
            
            command.Parameters.AddWithValue("@SearchTerm", searchPattern);

            connection.Open();
            using var reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                var cliente = new Cliente(
                    reader.GetInt32(0),    // Id
                    reader.GetString(1),   // Nombre
                    reader.GetString(2),   // Apellido
                    reader.GetString(3),   // Email
                    reader.GetInt32(4),    // PaisId
                    reader.GetDateTime(5)  // FechaAlta
                );
                
                // Crear y asignar el País
                var pais = new Pais(reader.GetInt32(4), reader.GetString(6)); // PaisId, PaisNombre
                cliente.SetPais(pais);
                
                clientes.Add(cliente);
            }

            return clientes;
        }

    }
}