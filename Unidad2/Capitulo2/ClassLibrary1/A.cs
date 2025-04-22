namespace Clases
{
    //Error al ponerlo como private o protected "Los elementos definidos dentro de un espacio de nombres no pueden declararse
    //explicitamente como private, protected, private protected o protected internal"
    public class A
    {
        string _NombreInstancia;

        public A() {
            _NombreInstancia = "Instancia sin nombre";
        }
        public A(string nombreInstancia)
        {
            _NombreInstancia = nombreInstancia;
        }
        public void MostrarNombre()
        {
            Console.WriteLine("El nombre de la instancia es: " + _NombreInstancia);
        }

        public void M1()
        {
            Console.WriteLine("El metodo fue invocado");
        }
        public void M2()
        {
            Console.WriteLine("El metodo fue invocado");
        }
        public void M3()
        {
            Console.WriteLine("El metodo fue invocado");
        }
        
        /*
        public string NombreInstancia {
            get
            {
                return _NombreInstancia;
            } 
            set 
            {
                _NombreInstancia = value;
            } 
        } */

    }
}
