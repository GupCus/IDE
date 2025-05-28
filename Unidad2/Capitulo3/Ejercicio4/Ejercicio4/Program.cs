using Ejercicio4;
using static System.Net.Mime.MediaTypeNames;
internal class Program
{
    private static void Main(string[] args)
    {

        List<Empleado> emps = new List<Empleado>();


        ConsoleKeyInfo opcion = default;
        while (opcion.Key != ConsoleKey.D1 || opcion.Key != ConsoleKey.D2 || opcion.Key != ConsoleKey.D3)
        {

            Console.Clear();

            Console.WriteLine("Elija una opción:");
            Console.WriteLine("1. Dar de alta");
            Console.WriteLine("2. Carga magica...");
            Console.WriteLine("3. Presentar de mayor a menor");
            Console.Write("Opción: ");
            opcion = Console.ReadKey();
            Console.WriteLine("");

            Console.Clear();

            switch (opcion.Key)
            {
                case ConsoleKey.D1:

                    ConsoleKeyInfo cont = default;
                    while (cont.Key != ConsoleKey.Escape)
                    {
                        int id = emps.Count();

                        Console.Write("Nombre: ");
                        string? nom = Console.ReadLine();
                        nom = string.IsNullOrEmpty(nom) ? "Empleado" + id: nom;
                        Console.WriteLine("");

                        Console.Write("Sueldo: ");
                        string? s = Console.ReadLine();
                        float sueldo = (string.IsNullOrEmpty(s) || !float.TryParse(s, out float n) )? 1 : float.Parse(s);
                        Console.WriteLine("");

                        emps.Add(new Empleado(id, nom, sueldo));

                        Console.WriteLine("\n\nPresione cualquier tecla para continuar, presione ESCAPE para salir");
                        cont = Console.ReadKey();
                    }

                    break;
                case ConsoleKey.D2:

                    Console.WriteLine("Carga Mágica");
                    for(int i = 0; i <= 10; i++)
                    {
                        int id = emps.Count();
                        string nom = "Empleado" + id;
                        Random rnd = new Random();
                        float sueldo = (float)rnd.NextDouble() * rnd.Next(10000) + 1000;
                        emps.Add(new Empleado(id, nom, sueldo));
                    }
                    Console.WriteLine("Completado...");
                    Console.ReadKey();

                    break;
                case ConsoleKey.D3:

                    var mejoresemp = from e in emps
                                     orderby e.Sueldo descending
                                     select e;

                    Console.WriteLine("Empleados ordenados de mayor a menor sueldo:");
                    foreach (Empleado empleado in mejoresemp)
                    {
                        Console.WriteLine($"ID: {empleado.Id}, Nombre: {empleado.Nombre}, Sueldo: {empleado.Sueldo}");
                    }
                    Console.ReadKey();

                    break;
                default:
                    Console.WriteLine("Lo ingresado no es 1, 2 o 3");
                    break;
            }
            Console.ReadKey();

        }


    }
}