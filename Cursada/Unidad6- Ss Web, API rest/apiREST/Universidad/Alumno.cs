using System.Runtime.CompilerServices;

namespace Dominio
{
    public class Alumno
    {
        public static readonly List<Alumno> Lista = new();
        private static int UltimoId = 0;
        public int? Id { get; set; }
        public required string Apellido { get; set; }
        public required string Nombre { get; set; }
        public required int Legajo { get; set; }
        public required string Direccion { get; set; }

        public Alumno(int id, string apellido, string nombre, int legajo, string direccion)
        {
            Id = id;
            Apellido = apellido;
            Nombre = nombre;
            Legajo = legajo;
            Direccion = direccion;
        }
        public Alumno() { }

        public new string ToString()
        {
            return $"Alumno: {Apellido}, {Nombre}  || Id Interno: {Id}\nLegajo: {Legajo}\nDireccion: {Direccion}";
        }
        public static int ObtenerProximoId()
        {
            UltimoId += 1;
            return UltimoId - 1;
        }
    }
}
