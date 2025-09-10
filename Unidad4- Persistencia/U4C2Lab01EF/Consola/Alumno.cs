using System.Runtime.CompilerServices;

namespace Consola
{
    public class Alumno
    {
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
    }
}
