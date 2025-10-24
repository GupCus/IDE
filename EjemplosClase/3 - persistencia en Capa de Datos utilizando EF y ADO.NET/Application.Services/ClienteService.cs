using Domain.Model;
using Data;
using DTOs;

namespace Application.Services
{
    public class ClienteService 
    {
        public ClienteDTO Add(ClienteDTO dto)
        {
            var clienteRepository = new ClienteRepository();

            // Validar que el email no esté duplicado
            if (clienteRepository.EmailExists(dto.Email))
            {
                throw new ArgumentException($"Ya existe un cliente con el Email '{dto.Email}'.");
            }

            var fechaAlta = DateTime.Now;
            Cliente cliente = new Cliente(0, dto.Nombre, dto.Apellido, dto.Email, fechaAlta);

            clienteRepository.Add(cliente);

            dto.Id = cliente.Id;
            dto.FechaAlta = cliente.FechaAlta;

            return dto;
        }

        public bool Delete(int id)
        {
            var clienteRepository = new ClienteRepository();
            return clienteRepository.Delete(id);
        }

        public ClienteDTO Get(int id)
        {
            var clienteRepository = new ClienteRepository();
            Cliente? cliente = clienteRepository.Get(id);
            
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
            var clienteRepository = new ClienteRepository();
            return clienteRepository.GetAll().Select(cliente => new ClienteDTO
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
            var clienteRepository = new ClienteRepository();

            // Validar que el email no esté duplicado (excluyendo el cliente actual)
            if (clienteRepository.EmailExists(dto.Email, dto.Id))
            {
                throw new ArgumentException($"Ya existe otro cliente con el Email '{dto.Email}'.");
            }

            Cliente cliente = new Cliente(dto.Id, dto.Nombre, dto.Apellido, dto.Email, dto.FechaAlta);
            return clienteRepository.Update(cliente);
        }

        public IEnumerable<ClienteDTO> GetByCriteria(ClienteCriteriaDTO criteriaDTO)
        {
            var clienteRepository = new ClienteRepository();
            
            // Mapear DTO a Domain Model
            var criteria = new ClienteCriteria(criteriaDTO.Texto);
            
            // Llamar al repositorio
            var clientes = clienteRepository.GetByCriteria(criteria);
            
            // Mapear Domain Model a DTO
            return clientes.Select(c => new ClienteDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Email = c.Email,
                FechaAlta = c.FechaAlta
            });
        }
    }
}
