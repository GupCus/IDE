using System.Diagnostics;
using System.Threading.Tasks;

namespace LabAsincronia1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await CompararSincronoVsAsincrono();
            //RTA A: el codigo asincronico efectivamente no bloquea la ejecucion de cualquier tarea que siga despues.
            //RTA B: El inconveniente es que se puede llegar a bloquear todo el programa si se utiliza mal. 
        }
        public static void SimularOperacionPesada()
        {
            Thread.Sleep(3000);
        }

        public async static Task SimularOperacionPesadaAsync()
        {
            await Task.Delay(3000);
        }

        public async static Task CompararSincronoVsAsincrono()
        {
            Stopwatch cronometro = new Stopwatch();
            Stopwatch total = new Stopwatch();
            total.Start();
            
            Console.Clear();
            Console.WriteLine("Comenzó el sincrono");
            cronometro.Start();
            SimularOperacionPesada();
            cronometro.Stop();
            Console.WriteLine("\nTerminó el síncrono en "+cronometro.ToString()+" segundos");
            Console.ReadKey();


            cronometro.Reset();
            Console.WriteLine("Comenzó el Asincrono");

            cronometro.Start();
            var tarea= SimularOperacionPesadaAsync();
            Console.Write("\n escribi mientras esperas: ");
            Console.ReadLine();
            await tarea;

            cronometro.Stop();
            total.Stop();
            Console.WriteLine("\nTerminó el Asincrono en " + cronometro.ToString() + " segundos");
            Console.WriteLine("\nTerminó todo en " + total.ToString() + " segundos");
            Console.ReadKey();



        }


    }
}
