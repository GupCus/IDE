namespace Persona
{
    public class Persona
    {
        private string nombre;
        private string apellido;
        private int edad;
        private int dni;

        public Persona(string nmbre,string ap,int ed, int doc)
        {
            nombre = nmbre;
            apellido = ap;
            edad = ed;
            dni = doc;
            Console.WriteLine("Persona creada exitosamente");
        }

        ~Persona()
        {
            Console.WriteLine("Persona destruida exitosamente");
        }

        public string Apellido
        {
            get
            {
                return apellido;
            }
            set
            {
                apellido = value;
            }
        }

        public int Dni
        {
            get
            {
                return dni;
            }
            set
            {
                dni = value;
            }
        }

        public int Edad
        {
            get
            {
                return edad;
            }
            set
            {
                edad = value;
            }
        }

        public string Nombre
        {
            get
            {
                return nombre;
            }
            set
            {
                nombre = value;
            }
        }

        public string GetFullName()
        {
            return (nombre + apellido);
        }

        public int GetAge()
        {
            return edad;
        }
    }
}
