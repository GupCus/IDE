using Dominio;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Consola
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            HttpClient httpClient = new()
            {
                BaseAddress = new Uri("http://localhost:5024")
            };

            ConsoleKeyInfo opcion = default;
            while (opcion.Key != ConsoleKey.Escape)
            {

                Console.Clear();

                Console.WriteLine("Elija una opción:");
                Console.WriteLine("1. Postear Alumno básico");
                Console.WriteLine("2. Getear alumnos");
                Console.WriteLine("3. Cargar nuevo alumno");
                Console.WriteLine("4. Eliminar alumno");
                Console.WriteLine("5. Encontrar alumno");
                Console.WriteLine("[ESCAPE] Salir");
                Console.Write("Opción: ");
                opcion = Console.ReadKey();
                Console.WriteLine("");

                Console.Clear();

                switch (opcion.Key)
                {
                    case ConsoleKey.D1:

                        Alumno al1 = new()
                        {
                            Apellido = "Barroso",
                            Nombre = "Agustín",
                            Legajo = 52818,
                            Direccion = "H. YRIGOYEN, San Lorenzo",
                        };

                        var rta = await httpClient.PostAsJsonAsync("alumnos", al1);
                        Console.WriteLine($"Código de estado {((int)rta.StatusCode) + " " + rta.StatusCode.ToString()}");
                        Console.WriteLine($"Contenido: {await rta.Content.ReadAsStringAsync()}");


                        break;


                    case ConsoleKey.D2:

                        IEnumerable<Alumno>? alumnos =
                        await httpClient.GetFromJsonAsync<IEnumerable<Alumno>>("alumnos");

                        Console.WriteLine($"Total de alumnos: {alumnos.Count()}\n");


                        foreach (Alumno alumno in alumnos)
                        {

                            Console.WriteLine(alumno.ToString() + "\n");
                        }

                        break;

                    case ConsoleKey.D3:

                        ConsoleKeyInfo cont = default;

                        while (cont.Key != ConsoleKey.Escape)
                        {

                            Console.Write("Nombre: ");
                            string? nom = Console.ReadLine();
                            nom = string.IsNullOrEmpty(nom) ? "Juan": nom;
                            Console.WriteLine("");

                            Console.Write("Apellido: ");
                            string? ap = Console.ReadLine();
                            ap = string.IsNullOrEmpty(ap) ? "Doe" : ap;
                            Console.WriteLine("");

                            Console.Write("Legajo: ");
                            string? l = Console.ReadLine();
                            int leg = (string.IsNullOrEmpty(l) || !int.TryParse(l, out int n)) ? (new Random()).Next(99999) : int.Parse(l);
                            Console.WriteLine("");

                            Console.Write("Direccion: ");
                            string? dir = Console.ReadLine();
                            dir = string.IsNullOrEmpty(dir) ? "Avenida Pellegrini" : dir;
                            Console.WriteLine("");

                            Alumno al = new()
                            {
                                Apellido = ap,
                                Nombre = nom,
                                Legajo = leg,
                                Direccion = dir,
                            };

                            var rt = await httpClient.PostAsJsonAsync("alumnos", al);
                            Console.WriteLine($"Código de estado {((int)rt.StatusCode) + " " + rt.StatusCode.ToString()}");
                            Console.WriteLine($"Contenido: {await rt.Content.ReadAsStringAsync()}");

                            Console.WriteLine("\n\nPresione cualquier tecla para continuar, presione ESCAPE para salir");
                            cont = Console.ReadKey();
                        }

                        break;

                    case ConsoleKey.D4:

                        Console.Write("Ingrese id a eliminar o deje vacío para volver: ");
                        string? inp = Console.ReadLine();
                        int? id = (!int.TryParse(inp, out int a)) ? null : int.Parse(inp);

                        if(id != null)
                        {
                            var rt = await httpClient.DeleteAsync($"alumnos/{id}");

                            if (rt.IsSuccessStatusCode)
                            {
                                Console.WriteLine($"Éxito!");
                            }
                            else 
                            {
                                Console.WriteLine("Error!");
                            }
                            Console.WriteLine($"Código de estado {((int)rt.StatusCode) + " " + rt.StatusCode.ToString()}");

                        }


                        break;

                    case ConsoleKey.D5:

                        Console.Write("Ingrese id a encontrar o deje vacío para volver: ");
                        string? inpp = Console.ReadLine();
                        int? idd = (!int.TryParse(inpp, out int b)) ? null : int.Parse(inpp);

                        if (idd != null)
                        {
                            var al = await httpClient.GetFromJsonAsync<Alumno>($"alumnos/{idd}");

                            if (al is Alumno)
                            {
                                Console.WriteLine($"Éxito!");
                                Console.WriteLine(al.ToString());
                            }
                            else
                            {
                                Console.WriteLine("Error! No encontrado!");
                            }

                        }
                        break;

                    case ConsoleKey.Escape:
                        break;

                    default:
                        Console.WriteLine("Lo ingresado no es 1 o 2");
                        break;
                }
                if (opcion.Key != ConsoleKey.Escape)
                {
                    Console.WriteLine("\nPresione cualquier tecla para volver...");
                    Console.ReadKey();
                }

            }
            
        }
    }
}
