namespace Lab02TXTADO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ManejadorArchivo manejadorArch;
            Console.WriteLine("Elija el modo:");
            Console.WriteLine("1 - TXT");
            Console.WriteLine("2 - XML");
            if (Console.ReadLine() == "2")
            {
                manejadorArch = new ManejadorArchivoXml();
            }
            else
            {
                manejadorArch = new ManejadorArchivoTxt();
            }
            manejadorArch.Listar();
            Menu(manejadorArch);
        }

        static void Menu(ManejadorArchivo manejadorArch)
        {
            string rta = "";
            do
            {
                Console.WriteLine("1 - Listar");
                Console.WriteLine("2 - Agregar");
                Console.WriteLine("3 - Modificar");
                Console.WriteLine("4 - Eliminar");
                Console.WriteLine("5 - Guardar Cambios");
                Console.WriteLine("6 - Salir");
                rta = Console.ReadLine();
                switch (rta)
                {
                    case "1":
                        manejadorArch.Listar();
                        break;
                    case "2":
                        manejadorArch.NuevaFila();
                        break;
                    case "3":
                        manejadorArch.EditarFila();
                        break;
                    case "4":
                        manejadorArch.EliminarFila();
                        break;
                    case "5":
                        manejadorArch.AplicaCambios();
                        break;
                    default:
                        break;
                }
            } while (rta != "6");
        }

    }
}
/*
  - El manejador de archivos XML no modifica el archivo original, sino que lo elimina y lo crea de nuevo.
  - Existen diferentes formas de manipular XML; aquí se usó una, pero también es común usar XMLDocument o XMLDataDocument, que trabajan con nodos en vez de tablas.
  - Manipular XML como árbol permite mayor flexibilidad para estructuras no tabulares.
  - El manejador de archivos de texto solo soporta listar y agregar filas nuevas; OleDB no permite modificar ni borrar filas porque los archivos planos no tienen índices.
  - En este laboratorio, las modificaciones y eliminaciones se agregaron para mostrar cómo serían si se trabajara con una base de datos real.
*/