namespace Domain.Model
{
    public class GrupoPermiso
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        public bool Activo { get; private set; }
        public DateTime FechaCreacion { get; private set; }

        // Navigation properties
        public virtual ICollection<Permiso> Permisos { get; private set; } = new List<Permiso>();

        public GrupoPermiso(int id, string nombre, string descripcion, DateTime fechaCreacion, bool activo = true)
        {
            SetId(id);
            SetNombre(nombre);
            SetDescripcion(descripcion);
            SetFechaCreacion(fechaCreacion);
            SetActivo(activo);
        }

        // Constructor privado para Entity Framework
        private GrupoPermiso() { }

        public void SetId(int id)
        {
            if (id < 0)
                throw new ArgumentException("El Id debe ser mayor o igual a 0.", nameof(id));
            Id = id;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede ser nulo o vacío.", nameof(nombre));
            
            if (nombre.Length > 50)
                throw new ArgumentException("El nombre no puede exceder 50 caracteres.", nameof(nombre));
                
            Nombre = nombre;
        }

        public void SetDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new ArgumentException("La descripción no puede ser nula o vacía.", nameof(descripcion));
                
            if (descripcion.Length > 200)
                throw new ArgumentException("La descripción no puede exceder 200 caracteres.", nameof(descripcion));
                
            Descripcion = descripcion;
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

        public void AgregarPermiso(Permiso permiso)
        {
            if (permiso == null)
                throw new ArgumentNullException(nameof(permiso));
                
            if (!Permisos.Contains(permiso))
            {
                Permisos.Add(permiso);
            }
        }

        public void RemoverPermiso(Permiso permiso)
        {
            if (permiso == null)
                throw new ArgumentNullException(nameof(permiso));
                
            Permisos.Remove(permiso);
        }

        public bool TienePermiso(string nombrePermiso)
        {
            return Activo && Permisos.Any(p => p.Activo && p.Nombre.Equals(nombrePermiso, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<string> ObtenerNombresPermisos()
        {
            return Permisos
                .Where(p => p.Activo)
                .Select(p => $"{p.Categoria}.{p.Nombre}")
                .ToList();
        }
    }
}