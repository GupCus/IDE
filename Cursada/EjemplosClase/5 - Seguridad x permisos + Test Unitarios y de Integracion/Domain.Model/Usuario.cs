using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Model
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string Salt { get; private set; }
        public DateTime FechaCreacion { get; private set; }
        public bool Activo { get; private set; }

        // Simplificación: Usuario tiene UN grupo (relación simple)
        public int? GrupoPermisoId { get; private set; }
        public virtual GrupoPermiso? Grupo { get; private set; }

        public Usuario(int id, string username, string email, string password, DateTime fechaCreacion, bool activo = true)
        {
            SetId(id);
            SetUsername(username);
            SetEmail(email);
            SetPassword(password);
            SetFechaCreacion(fechaCreacion);
            SetActivo(activo);
        }

        // Constructor privado sin parámetros requerido por Entity Framework
        // EF no puede usar el constructor público porque 'password' no existe como propiedad en BD
        // - En BD guardamos: PasswordHash y Salt
        // - El Domain Model recibe: password (y lo hashea internamente)
        // Por eso EF necesita este constructor para crear instancias desde la BD
        private Usuario() { }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id debe ser mayor que 0.", nameof(id));
            Id = id;
        }

        public void SetUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("El nombre de usuario no puede ser nulo o vacío.", nameof(username));
            
            if (username.Length < 3)
                throw new ArgumentException("El nombre de usuario debe tener al menos 3 caracteres.", nameof(username));
                
            Username = username;
        }

        public void SetEmail(string email)
        {
            if (!EsEmailValido(email))
                throw new ArgumentException("El email no tiene un formato válido.", nameof(email));
            Email = email;
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña no puede ser nula o vacía.", nameof(password));

            if (password.Length < 6)
                throw new ArgumentException("La contraseña debe tener al menos 6 caracteres.", nameof(password));

            Salt = GenerateSalt();
            PasswordHash = HashPassword(password, Salt);
        }

        public void SetFechaCreacion(DateTime fechaCreacion)
        {
            if (fechaCreacion == default)
                throw new ArgumentException("La fecha de creación no puede ser nula.", nameof(fechaCreacion));
            FechaCreacion = fechaCreacion;
        }

        public void SetActivo(bool activo)
        {
            Activo = activo;
        }

        public bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            string hashedInput = HashPassword(password, Salt);
            return PasswordHash == hashedInput;
        }

        private static bool EsEmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private static string GenerateSalt()
        {
            byte[] saltBytes = new byte[32];
            RandomNumberGenerator.Fill(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        private static string HashPassword(string password, string salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 10000, HashAlgorithmName.SHA256);
            byte[] hashBytes = pbkdf2.GetBytes(32);
            return Convert.ToBase64String(hashBytes);
        }

        // Métodos para manejo de grupo y permisos (simplificado a UN grupo)
        public void SetGrupo(GrupoPermiso? grupo)
        {
            Grupo = grupo;
            GrupoPermisoId = grupo?.Id;
        }

        public bool TienePermiso(string nombrePermiso)
        {
            if (!Activo || Grupo == null || !Grupo.Activo)
                return false;
                
            return Grupo.TienePermiso(nombrePermiso);
        }

        public IEnumerable<string> ObtenerTodosLosPermisos()
        {
            if (Grupo == null || !Grupo.Activo)
                return new List<string>();
                
            return Grupo.ObtenerNombresPermisos();
        }

        public string? ObtenerNombreGrupo()
        {
            return Grupo?.Nombre;
        }
    }
}