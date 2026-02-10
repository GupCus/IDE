using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Lab01.Components.Clases
{
    public class Alumno
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public required string Apellido { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public required string Nombre { get; set; }

        [Range(1, 99999, ErrorMessage = "El legajo tiene que ser mayor que 0 y menor a 99999")]
        public required int Legajo { get; set; }
        [Required(ErrorMessage = "La direccion es obligatoria.")]
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
