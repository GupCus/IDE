using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio4
{
    public class Empleado
    {
        public int Id
        {
            get;set;
        }
        public string Nombre
        {
            get; set;
        }
        public float Sueldo
        {
            get;set;
        }

        public Empleado(int id, string nombre, float sueldo)
        {
            Id = id;
            Nombre = nombre;
            Sueldo = sueldo;
        }
    }
}
