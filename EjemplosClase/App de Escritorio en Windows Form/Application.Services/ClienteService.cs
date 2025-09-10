using Domain.Model;
using Data;
using DTOs;

namespace Application.Services
{
    public class ClienteService 
    {
        public ClienteDTO Add(ClienteDTO dto)
        {
            // Validar que el email no esté duplicado
            if (ClienteInMemory.Clientes.Any(c => c.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException($"Ya existe un cliente con el Email '{dto.Email}'.");
            }

            var id = GetNextId();
            var fechaAlta = DateTime.Now;

            Cliente cliente = new Cliente(id, dto.Nombre, dto.Apellido, dto.Email, fechaAlta);

            ClienteInMemory.Clientes.Add(cliente);

            dto.Id = cliente.Id;
            dto.FechaAlta = cliente.FechaAlta;

            return dto;
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

        public ClienteDTO Get(int id)
        {
            Cliente? cliente = ClienteInMemory.Clientes.Find(x => x.Id == id);
            
            if (cliente == null)
                return null;
            
            return new ClienteDTO
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Email = cliente.Email,
                FechaAlta = cliente.FechaAlta
            };
        }

        public IEnumerable<ClienteDTO> GetAll()
        {
            return ClienteInMemory.Clientes.Select(cliente => new ClienteDTO
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Email = cliente.Email,
                FechaAlta = cliente.FechaAlta
            }).ToList();
        }

        public bool Update(ClienteDTO dto)
        {
            Cliente? clienteToUpdate = ClienteInMemory.Clientes.Find(x => x.Id == dto.Id);

            if (clienteToUpdate != null)
            {
                // Validar que el email no esté duplicado (excluyendo el cliente actual)
                if (ClienteInMemory.Clientes.Any(c => c.Id != dto.Id && c.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new ArgumentException($"Ya existe otro cliente con el Email '{dto.Email}'.");
                }

                clienteToUpdate.SetNombre(dto.Nombre);
                clienteToUpdate.SetApellido(dto.Apellido);
                clienteToUpdate.SetEmail(dto.Email);

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
