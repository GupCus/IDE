namespace Domain.Model
{
    public class Permiso
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string Descripcion { get; private set; }
        public string Categoria { get; private set; }
        public bool Activo { get; private set; }

        // Navigation properties
        public virtual ICollection<GrupoPermiso> Grupos { get; private set; } = new List<GrupoPermiso>();

        public Permiso(int id, string nombre, string descripcion, string categoria, bool activo = true)
        {
            SetId(id);
            SetNombre(nombre);
            SetDescripcion(descripcion);
            SetCategoria(categoria);
            SetActivo(activo);
        }

        // Constructor privado para Entity Framework
        private Permiso() { }

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

        public void SetCategoria(string categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria))
                throw new ArgumentException("La categoría no puede ser nula o vacía.", nameof(categoria));
                
            if (categoria.Length > 30)
                throw new ArgumentException("La categoría no puede exceder 30 caracteres.", nameof(categoria));
                
            Categoria = categoria;
        }

        public void SetActivo(bool activo)
        {
            Activo = activo;
        }

        public override string ToString()
        {
            return $"{Categoria}.{Nombre}";
        }
    }
}