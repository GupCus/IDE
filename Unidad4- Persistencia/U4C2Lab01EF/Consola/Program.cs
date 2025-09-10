namespace Consola
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("\n--- Menú de Alumnos ---");
                Console.WriteLine("1. Crear Alumno");
                Console.WriteLine("2. Eliminar Alumno");
                Console.WriteLine("3. Listar Alumnos");
                Console.WriteLine("4. Buscar Alumno por ID");
                Console.WriteLine("5. Actualizar Alumno");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");
                string? opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("Apellido: ");
                        string apellido = Console.ReadLine() ?? "";
                        Console.Write("Nombre: ");
                        string nombre = Console.ReadLine() ?? "";
                        Console.Write("Dirección: ");
                        string direccion = Console.ReadLine() ?? "";
                        Console.Write("Legajo: ");
                        int legajo = int.TryParse(Console.ReadLine(), out int l) ? l : 0;
                        CrearAlumno(apellido, nombre, direccion, legajo);
                        Console.WriteLine("Alumno creado.");
                        break;
                    case "2":
                        Console.Write("ID del alumno a eliminar: ");
                        int idEliminar = int.TryParse(Console.ReadLine(), out int e) ? e : 0;
                        EliminarAlumno(idEliminar);
                        Console.WriteLine("Alumno eliminado (si existía).");
                        break;
                    case "3":
                        EncontrarAlumno();
                        break;
                    case "4":
                        Console.Write("ID del alumno a buscar: ");
                        int idBuscar = int.TryParse(Console.ReadLine(), out int b) ? b : 0;
                        Alumno? alum = EncontrarAlumno(idBuscar);
                        if (alum != null)
                            Console.WriteLine(alum.ToString());
                        else
                            Console.WriteLine("Alumno no encontrado.");
                        break;
                    case "5":
                        Console.Write("ID del alumno a actualizar: ");
                        int idActualizar = int.TryParse(Console.ReadLine(), out int a) ? a : 0;
                        ActualizarAlumno(idActualizar);
                        break;
                    case "0":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        } 

        /*
         * ¿Por qué Using?
          El uso de using con llaves ({ ... }) en C# se emplea para asegurar 
          que los recursos que implementan la interfaz IDisposable (como conexiones a bases de datos, archivos, streams, etc.) 
          se liberen correctamente cuando ya no se necesitan.
            •	using crea un bloque de código donde el objeto declarado (en este caso, UniversidadContext bd = new()) 
                solo existe dentro de ese bloque.
            •	Al salir del bloque, C# llama automáticamente al método Dispose() del objeto, 
                liberando recursos como conexiones a la base de datos, memoria, archivos, etc.
        */
        private static void CrearAlumno(string apellido, string nombre, string direccion, int legajo)
        {
            using (UniversidadContext bd = new())
            {
                bd.Alumnos.Add(new Alumno() { Apellido = apellido, Nombre = nombre, Direccion = direccion, Legajo = legajo });
                bd.SaveChanges();
            }
        }

        private static void EliminarAlumno(int idalum)
        {
            using (UniversidadContext bd = new())
            {
                Alumno? alumnoelim = bd.Alumnos.Find(idalum);
                if (alumnoelim != null)
                {
                    bd.Alumnos.Remove(alumnoelim);
                    bd.SaveChanges();
                }
            }
        }

        private static void EncontrarAlumno()
        {
            using (UniversidadContext bd = new())
            {
                foreach (var alum in bd.Alumnos)
                {
                    Console.WriteLine(alum.ToString());
                }
            }
        }
        private static Alumno? EncontrarAlumno(int idalum)
        {
            using (UniversidadContext bd = new())
            {
                return bd.Alumnos.Find(idalum);
            }
        }

        private static void ActualizarAlumno(int idalum)
        {
            using (UniversidadContext bd = new())
            {
                Alumno? alumnomodif = bd.Alumnos.Find(idalum);
                if (alumnomodif != null)
                {
                    Console.Write("\n Ingrese nombre nuevo: ");
                    alumnomodif.Nombre = Console.ReadLine();
                    bd.SaveChanges();
                }
            }
        }
    }
}
