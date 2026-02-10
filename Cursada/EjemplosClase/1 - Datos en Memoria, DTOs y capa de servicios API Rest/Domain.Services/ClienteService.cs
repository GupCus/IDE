using Domain.Model;
using Data;

namespace Domain.Services
{
    public class ClienteService 
    {
        public void Add(Cliente cliente)
        {
            cliente.SetId(GetNextId());

            ClienteInMemory.Clientes.Add(cliente);
        }

        public bool Delete(int id)
        {
            Cliente? clienteToDelete = ClienteInMemory.Clientes.Find(x => x.Id == id);

            if (clienteToDelete != null)
            {
                ClienteInMemory.Clientes.Remove(clienteToDelete);

                return true;
            }
            else
            {
                return false;
            }
        }

        public Cliente Get(int id)
        {
            //Deberia devolver un objeto cloneado 
            return ClienteInMemory.Clientes.Find(x => x.Id == id);
        }

        public IEnumerable<Cliente> GetAll()
        {
            //Devuelvo una lista nueva cada vez que se llama a GetAll
            //pero idealmente deberia implementar un Deep Clone
            return ClienteInMemory.Clientes.ToList();
        }

        public bool Update(Cliente cliente)
        {
            Cliente? clienteToUpdate = ClienteInMemory.Clientes.Find(x => x.Id == cliente.Id);

            if (clienteToUpdate != null)
            {
                clienteToUpdate.SetNombre(cliente.Nombre);
                clienteToUpdate.SetApellido(cliente.Apellido);
                clienteToUpdate.SetEmail(cliente.Email);
                clienteToUpdate.SetFechaAlta(cliente.FechaAlta);

                return true;
            }
            else
            {
                return false;
            }
        }

        //No es ThreadSafe pero sirve para el proposito del ejemplo        
        private static int GetNextId()
        {
            int nextId;

            if (ClienteInMemory.Clientes.Count > 0)
            {
                nextId = ClienteInMemory.Clientes.Max(x => x.Id) + 1;
            }
            else
            {
                nextId = 1;
            }

            return nextId;
        }
    }
}
