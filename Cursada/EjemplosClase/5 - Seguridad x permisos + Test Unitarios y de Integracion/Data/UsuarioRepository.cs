using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class UsuarioRepository
    {
        private TPIContext CreateContext()
        {
            return new TPIContext();
        }

        public void Add(Usuario usuario)
        {
            using var context = CreateContext();
            context.Usuarios.Add(usuario);
            context.SaveChanges();
        }

        public bool Delete(int id)
        {
            using var context = CreateContext();
            var usuario = context.Usuarios.Find(id);
            if (usuario != null)
            {
                context.Usuarios.Remove(usuario);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public Usuario? Get(int id)
        {
            using var context = CreateContext();
            return context.Usuarios
                .Include(u => u.Grupo)
                    .ThenInclude(g => g.Permisos.Where(p => p.Activo))
                .FirstOrDefault(u => u.Id == id);
        }

        public Usuario? GetByUsername(string username)
        {
            using var context = CreateContext();
            return context.Usuarios
                .Include(u => u.Grupo)
                    .ThenInclude(g => g.Permisos.Where(p => p.Activo))
                .FirstOrDefault(u => u.Username == username && u.Activo);
        }

        public IEnumerable<Usuario> GetAll()
        {
            using var context = CreateContext();
            return context.Usuarios.ToList();
        }

        public bool Update(Usuario usuario)
        {
            using var context = CreateContext();
            context.Usuarios.Update(usuario);
            context.SaveChanges();
            return true;
        }
    }
}