using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ2Objects1_2_3
{
    public static class Ejercicio1
    {

        public static void Ej1()
        {

            string[] provincias = [
                "Ciudad de Buenos Aires",
                "Buenos Aires",
                "Catamarca",
                "Chaco",
                "Chubut",
                "Córdoba",
                "Corrientes",
                "Entre Ríos",
                "Formosa",
                "Jujuy",
                "La Pampa",
                "La Rioja",
                "Mendoza",
                "Misiones",
                "Neuquén",
                "Río Negro",
                "Salta",
                "San Juan",
                "San Luis",
                "Santa Cruz",
                "Santa Fe",
                "Santiago del Estero",
                "Tierra del Fuego",
                "Tucumán"
            ];

            var prov = provincias
                .Where(p => p.StartsWith("S") || p.StartsWith("T"))
                .Select(p => p);

            Console.WriteLine("Las provincias que arrancan con S o T son: ");

            foreach (string p in prov)
            {
                Console.WriteLine(p);
            }

        }

    }
}
