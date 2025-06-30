namespace Universidad
{
    public class Alumno
    {
        public static readonly List<Alumno> Lista = new();
        public int Id { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public int Legajo { get; set; }
        public string Direccion { get; set; }

        public Alumno(int id, string apellido, string nombre, int legajo, string direccion)
        {
            Id = id;
            Apellido = apellido;
            Nombre = nombre;
            Legajo = legajo;
            Direccion = direccion;
        }

    }
}
