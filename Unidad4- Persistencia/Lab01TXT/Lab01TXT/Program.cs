using System.Data;
using System.Xml;

namespace Lab01TXT
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LecturaXML();
            LecturaTextoPlano();
            Console.ReadKey();
        }

        private static void LecturaXML()
        {
            bool rta = true;
            while (rta)
            {
                Console.Clear();
                Console.Write("\n¿Desea generar un XML con contactos de su agenda? (S/N): ");
                rta = (Console.ReadKey().Key == ConsoleKey.S ? true : false);
                if (rta)
                {
                    EscribirXML();
                    Console.Write("\nArchivo generado correctamente, ¿desea ver su contenido? (S/N): ");
                    rta = (Console.ReadKey().Key == ConsoleKey.S ? true : false);
                    if (rta)
                    {
                        Console.WriteLine();
                        LeerXML();
                        Console.ReadKey();
                    }
                }
            }
        }

        private static void LecturaTextoPlano()
        {
            bool rta = true;
            while (rta)
            {
                Console.Clear();
                Leer();
                Console.Write("\n\n¿Desea ingresar un contacto? (S/N): ");
                rta = (Console.ReadKey().Key == ConsoleKey.S ? true : false);
                if (rta)
                {
                    Escribir();
                }
            }
        }

        // Método para leer byte a byte, menos eficiente FILESTREAM
        private static void Leerbyteabyte()
        {
            FileStream lector = new("agenda.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
            while (lector.Length > lector.Position)
            {
                Console.Write((char)lector.ReadByte());
            }
            lector.Close();
            Console.ReadKey();
        }

        // Método para leer por linea STREAMREADER
        private static void Leer()
        {
            StreamReader lector = File.OpenText("agenda.txt");
            string linea;
            Console.WriteLine("Nombre\tApellido\temail\t\t\tTelefono");
            do
            {
                linea = lector.ReadLine();
                if (linea != null)
                {
                    string[] valores = linea.Split(";");
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", valores[0], valores[1], valores[2], valores[3]);
                }
            } while (linea != null);
            lector.Close();
        }
        private static void Escribir()
        {
            bool rta = true;
            while (rta)
            {
                Console.Clear();
                Leer();
                StreamWriter escritor = File.AppendText("agenda.txt");
                Console.WriteLine("\nIngrese nuevo contacto");
                Console.Write("\nIngrese nombre: ");
                string nom = Console.ReadLine();
                Console.Write("\nIngrese apellido: ");
                string ap = Console.ReadLine();
                Console.Write("\nIngrese email: ");
                string em = Console.ReadLine();
                Console.Write("\nIngrese telefono: ");
                string tel = Console.ReadLine();

                Console.WriteLine("\nContacto: {0};{1};{2};{3}", nom, ap, em, tel);
                escritor.WriteLine("{0};{1};{2};{3}",nom,ap,em,tel);
                Console.Write("\n\n¿Desea ingresar otro contacto? (S/N): ");
                rta = (Console.ReadKey().Key == ConsoleKey.S ? true : false);


                escritor.Close();

                /*Aclaración: Si dentro del Visual Studio revisan el archivo agenda.txt 
                 * desde el explorador de soluciones verán que no ha sido modificado. 
                 * Esto se debe a que no es este el archivo que hemos modificado. 
                 * En el paso 7) Seteamos la propiedad Copy to Output Directory del archivo. 
                 * Esto significa que una copia del archivo se escribe en el directorio donde 
                 * se compila el ejecutable este es el archivo que hemos estado utilizando.

                El mismo se encuentra dentro del directorio de la solución, 
                dentro del directorio del proyecto dentro de la carpeta \bin\Debug\
                */
            }
        }
        private static void EscribirXML()
        {
            XmlTextWriter escritorXML = new("agendaxml.xml", null);
            escritorXML.Formatting = Formatting.Indented;
            escritorXML.WriteStartDocument(true);
            escritorXML.WriteStartElement("DocumentElement");
            StreamReader lector = File.OpenText("agenda.txt");
            string linea;
            do
            {
                linea = lector.ReadLine();
                if (linea != null)
                {
                    string[] valores = linea.Split(";");
                    escritorXML.WriteStartElement("contactos");
                    escritorXML.WriteStartElement("nombre");
                    escritorXML.WriteValue(valores[0]);
                    escritorXML.WriteEndElement();
                    escritorXML.WriteStartElement("apellido");
                    escritorXML.WriteValue(valores[1]);
                    escritorXML.WriteEndElement();
                    escritorXML.WriteStartElement("email");
                    escritorXML.WriteValue(valores[2]);
                    escritorXML.WriteEndElement();
                    escritorXML.WriteStartElement("telefono");
                    escritorXML.WriteValue(valores[3]);
                    escritorXML.WriteEndElement();
                    escritorXML.WriteEndElement();
                }
            } while (linea != null);
            escritorXML.WriteEndElement();
            escritorXML.WriteEndDocument();
            escritorXML.Close();
            lector.Close();

            /* La forma de crear un xml es abriendo, cargandole valores y cerrando las tags
             por ejemplo: 
                escritorXML.WriteStartElement("nombre");
                escritorXML.WriteValue(valores[0]);
                escritorXML.WriteEndElement(); 
            genera:
                <nombre>Jose</nombre>
            */
        }
        private static void LeerXML()
        {
            XmlTextReader lectorXML = new("agendaxml.xml");

            string tagAnt = "";
            while (lectorXML.Read())
            {
                if (lectorXML.NodeType == XmlNodeType.Element)
                {
                    tagAnt = lectorXML.Name;
                }
                else if(lectorXML.NodeType == XmlNodeType.Text)
                {
                    Console.WriteLine(tagAnt + ": " + lectorXML.Value);
                }
            }
            lectorXML.Close();
        }
    }
}
