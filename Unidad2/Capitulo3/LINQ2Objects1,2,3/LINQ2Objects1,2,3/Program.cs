using LINQ2Objects1_2_3;

internal class Program
{
    private static void Main(string[] args)
    {

        ConsoleKeyInfo opcion = default;
        while (opcion.Key != ConsoleKey.D1 || opcion.Key != ConsoleKey.D2 || opcion.Key != ConsoleKey.D3 ) {

            Console.Clear();

            Console.WriteLine("Elija una opción:");
            Console.WriteLine("1. Ejercicio 1");
            Console.WriteLine("2. Ejercicio 2");
            Console.WriteLine("3. Ejercicio 3");
            Console.Write("Opción: ");
            opcion = Console.ReadKey();
            Console.WriteLine("");

            Console.Clear();

            switch (opcion.Key)
            {
                case ConsoleKey.D1:

                    Ejercicio1.Ej1();
                    
                    break;
                case ConsoleKey.D2:

                    Ejercicio2.Ej2();

                    break;
                case ConsoleKey.D3:

                    Ejercicio3.Ej3();

                    break;
                default:
                    Console.WriteLine("Lo ingresado no es 1, 2 o 3");
                    break;
            }
            Console.ReadKey();

        }


    }
}