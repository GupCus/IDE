namespace Juego
{
    public class Juego
    {
        private int? _record;

        public Juego()
        {
            _record = null;
        }

        public void ComenzarJuego()
        {

            do
            {
                Console.Clear();
                Console.WriteLine("Comenzó el Juego \nIngrese un numero máximo y adivina cual toca");
                Console.WriteLine("Record Actual: " + _record);

                int max = this.PreguntarMaximo();
                JugadaConAyuda j = new JugadaConAyuda(max);

                do
                {
                    Console.Clear();
                    Console.WriteLine("Comenzó el Juego, el máximo es: " + max);
                    Console.WriteLine("Este es su intento n: " + j.Intento + " Record Actual: " + _record);


                    j.Comparar(this.PreguntarNumero());
                    if (j.Adivino)
                    {
                        this.CompararRecord(j.Intento);
                        Console.WriteLine("\n Muy bien!! adivinaste!!. Intentos: " +j.Intento + " Record Actual: " + _record);
                    }
                    else
                    {
                        Console.WriteLine("\nTe equivocaste!, seguí intentando");
                    }
                } while (this.Continuar() && !(j.Adivino));
                Console.Clear();
                Console.WriteLine( "Record Actual: " + _record);
                Console.WriteLine("Deseas jugar de vuelta? \nVamos a sacar un nuevo record!!");
           
            } while (this.Continuar());
    
        }

        public void CompararRecord(int actual)
        {
            if((_record ==  null) || (_record > actual))
            {
                _record = actual;
            }
        }

        public bool Continuar()
        {
            Console.WriteLine("\n\nPresione cualquier tecla para continuar, presione ESCAPE para salir");
            ConsoleKeyInfo cont = Console.ReadKey();
            return cont.Key != ConsoleKey.Escape;
        }

        public int PreguntarMaximo()
        {
            Console.Write("Numero: ");
            int opcion = int.Parse(Console.ReadLine());
            Console.WriteLine("");
            return opcion; 
        }

        public int PreguntarNumero()
        {
            Console.Write("Cual número tocó???: ");
            int adivinar = int.Parse(Console.ReadLine());
            return adivinar;
        }
    }
}
