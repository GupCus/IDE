using BarrosoBolleroAgustin.Dominio;
namespace BarrosoBolleroAgustin.Consola
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Vehiculo> lista = new() { new Auto("ABC111", 4, 1999, "Azul"), 
                                           new Auto("CCC111", 4, 2012, "Verde"), 
                                           new Auto("DDD222", 4, 1989, "Rojo") };

            var v1 = ListaVehiculo.BuscarPatenteLinq(lista, "ABC111");
            var v2 = ListaVehiculo.BuscarPatenteIterativa(lista, "ABC111");

            Console.WriteLine(v1.ToString());
            Console.WriteLine(v2.ToString());
        }
    }
}
