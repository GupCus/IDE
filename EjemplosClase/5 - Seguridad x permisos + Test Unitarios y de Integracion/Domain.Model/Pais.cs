namespace Domain.Model
{
    public class Pais
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }

        public Pais(int id, string nombre)
        {
            SetId(id);
            SetNombre(nombre);
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
                throw new ArgumentException("El nombre del país no puede ser nulo o vacío.", nameof(nombre));
            Nombre = nombre;
        }
    }
}