using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ2Objects1_2_3
{   
    public class Ciudad
    {
        public string Nombre { get; set; }
        public int CP { get; set; }

        public Ciudad(string nom, int cp)
        {
            Nombre = nom;
            CP = cp;
        }

    }
    public static class Ejercicio3
    {
        public static void Ej3()
        {

            ArrayList ciudades = new ArrayList();

            ciudades.Add(new Ciudad("Buenos Aires", 1000));
            ciudades.Add(new Ciudad("Córdoba", 5000));
            ciudades.Add(new Ciudad("Rosario", 2000));
            ciudades.Add(new Ciudad("Mendoza", 5500));
            ciudades.Add(new Ciudad("La Plata", 1900));
            ciudades.Add(new Ciudad("San Miguel de Tucumán", 4000));
            ciudades.Add(new Ciudad("Mar del Plata", 7600));
            ciudades.Add(new Ciudad("Salta", 4400));
            ciudades.Add(new Ciudad("Santa Fe", 3000));
            ciudades.Add(new Ciudad("San Juan", 5400));

            Console.WriteLine("Ingrese nombre de ciudad para obtener su CP");
            Console.Write("Ciudad consultada: ");
            string? op = Console.ReadLine();

            var consulta = ciudades
                .OfType<Ciudad>()
                .Where(c => c.Nombre.Contains(op, StringComparison.OrdinalIgnoreCase))
                .Select(c => c.CP);

            while (op == null || op.Length < 3 || !consulta.Any())
            {
                Console.Clear();
                Console.WriteLine("Ingrese nombre de ciudad para obtener su CP");
                Console.Write("No puedo adivinar esa ciudad '" + op + "' pruebe con 3 letras al menos: ");
                op = Console.ReadLine();

                consulta = ciudades
                    .OfType<Ciudad>()
                    .Where(c => c.Nombre.Contains(op, StringComparison.OrdinalIgnoreCase))
                    .Select(c => c.CP);
            }

            Console.WriteLine("El código postal de '" +op+ "' es el siguiente: "+ consulta.First() );




        }
    }
}
