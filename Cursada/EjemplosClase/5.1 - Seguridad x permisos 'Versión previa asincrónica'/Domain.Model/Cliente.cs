using System.Text.RegularExpressions;

namespace Domain.Model
{
    public class Cliente
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }

        public string Email { get; private set; }
        
        private int _paisId;
        private Pais? _pais;
        
        public int PaisId 
        { 
            get => _pais?.Id ?? _paisId;
            private set => _paisId = value; 
        }
        
        public Pais? Pais 
        { 
            get => _pais;
            private set 
            {
                _pais = value;
                if (value != null && _paisId != value.Id)
                {
                    _paisId = value.Id; // Sincronizar automáticamente
                }
            }
        }
        
        public DateTime FechaAlta { get; private set; }

        public Cliente(int id, string nombre, string apellido, string email, int paisId, DateTime fechaAlta)
        {
            SetId(id);
            SetNombre(nombre);
            SetApellido(apellido);
            SetEmail(email);
            SetPaisId(paisId);
            SetFechaAlta(fechaAlta);
        }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id debe ser mayor que 0.", nameof(id));
            Id = id;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede ser nulo o vacío.", nameof(nombre));
            Nombre = nombre;
        }

        public void SetApellido(string apellido)
        {
            if (string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El apellido no puede ser nulo o vacío.", nameof(apellido));
            Apellido = apellido;
        }

        public void SetEmail(string email)
        {
            if (!EsEmailValido(email))
                throw new ArgumentException("El email no tiene un formato válido.", nameof(email));
            Email = email;
        }

        private static bool EsEmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public void SetPaisId(int paisId)
        {
            if (paisId <= 0)
                throw new ArgumentException("El PaisId debe ser mayor que 0.", nameof(paisId));
        
            _paisId = paisId;
            
            // Solo invalidar si hay inconsistencia
            if (_pais != null && _pais.Id != paisId)
            {
                _pais = null; // Invalidar navigation property
            }
        }

        public void SetPais(Pais pais)
        {
            ArgumentNullException.ThrowIfNull(pais);
            _pais = pais;
            _paisId = pais.Id;
        }

        public void SetFechaAlta(DateTime fechaAlta)
        {
            if (fechaAlta == default)
                throw new ArgumentException("La fecha de alta no puede ser nula.", nameof(fechaAlta));
            FechaAlta = fechaAlta;
        }
    }
}