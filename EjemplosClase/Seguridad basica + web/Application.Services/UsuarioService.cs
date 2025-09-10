using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class UsuarioService
    {
        public IEnumerable<UsuarioDTO> GetAll()
        {
            var usuarioRepository = new UsuarioRepository();
            var usuarios = usuarioRepository.GetAll();
            
            return usuarios.Select(usuario => new UsuarioDTO
            {
                Id = usuario.Id,
                Username = usuario.Username,
                Email = usuario.Email,
                FechaCreacion = usuario.FechaCreacion,
                Activo = usuario.Activo
            });
        }

        public UsuarioDTO? Get(int id)
        {
            var usuarioRepository = new UsuarioRepository();
            Usuario? usuario = usuarioRepository.Get(id);
            
            if (usuario == null)
                return null;
            
            return new UsuarioDTO
            {
                Id = usuario.Id,
                Username = usuario.Username,
                Email = usuario.Email,
                FechaCreacion = usuario.FechaCreacion,
                Activo = usuario.Activo
            };
        }

        public UsuarioDTO Add(UsuarioCreateDTO createDto)
        {
            var usuarioRepository = new UsuarioRepository();

            var fechaCreacion = DateTime.Now;
            Usuario usuario = new Usuario(0, createDto.Username, createDto.Email, createDto.Password, fechaCreacion, true);

            usuarioRepository.Add(usuario);

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Username = usuario.Username,
                Email = usuario.Email,
                FechaCreacion = usuario.FechaCreacion,
                Activo = usuario.Activo
            };
        }

        public bool Update(UsuarioUpdateDTO updateDto)
        {
            var usuarioRepository = new UsuarioRepository();
            var usuario = usuarioRepository.Get(updateDto.Id);
            if (usuario == null)
                return false;

            usuario.SetUsername(updateDto.Username);
            usuario.SetEmail(updateDto.Email);
            usuario.SetActivo(updateDto.Activo);

            // Solo actualizar contraseña si se proporciona
            if (!string.IsNullOrWhiteSpace(updateDto.Password))
            {
                usuario.SetPassword(updateDto.Password);
            }

            return usuarioRepository.Update(usuario);
        }

        public bool Delete(int id)
        {
            var usuarioRepository = new UsuarioRepository();
            return usuarioRepository.Delete(id);
        }
    }
}