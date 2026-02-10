using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ2Objects1_2_3
{
    public static class Ejercicio2
    {
        public static void Ej2()
        {
            List<int> nums = new List<int>();
            int num=1;

            while (num != 0)
            {
                Console.Clear();
                Console.WriteLine("Ingrese números enteros (0 para terminar)");
                Console.Write("Numero: ");
                string? op = Console.ReadLine();

                while (!(int.TryParse(op, out num)))
                {
                    Console.Clear();
                    Console.Write("No es un número, ingrese un numero: ");
                    op = Console.ReadLine();
                }

                nums.Add(num);
            }

            var mayores = nums
                .Where(n => n > 20)
                .Select(n => n);

            Console.WriteLine("Los numeros mayores a 20 ingresados son: ");
            foreach (var item in mayores)
            {
                Console.WriteLine(item);
            }
        }
    }
}
