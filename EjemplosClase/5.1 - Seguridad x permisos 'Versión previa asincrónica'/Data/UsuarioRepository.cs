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

        public async Task AddAsync(Usuario usuario)
        {
            using var context = CreateContext();
            await context.Usuarios.AddAsync(usuario);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var context = CreateContext();
            var usuario = await context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                context.Usuarios.Remove(usuario);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Usuario?> GetAsync(int id)
        {
            using var context = CreateContext();
            return await context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario?> GetByUsernameAsync(string username)
        {
            using var context = CreateContext();
            return await context.Usuarios.FirstOrDefaultAsync(u => u.Username == username && u.Activo);
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            using var context = CreateContext();
            return await context.Usuarios.ToListAsync();
        }

        public async Task<bool> UpdateAsync(Usuario usuario)
        {
            using var context = CreateContext();
            var existingUsuario = await context.Usuarios.FindAsync(usuario.Id);
            if (existingUsuario != null)
            {
                existingUsuario.SetUsername(usuario.Username);
                existingUsuario.SetEmail(usuario.Email);
                existingUsuario.SetActivo(usuario.Activo);
                
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}