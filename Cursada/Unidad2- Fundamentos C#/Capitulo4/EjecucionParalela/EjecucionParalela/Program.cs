using System.Diagnostics;

namespace EjecucionParalela
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Stopwatch cronometro = new Stopwatch();
            Stopwatch cronometro2 = new Stopwatch();

            Console.Clear();
            Console.WriteLine("Comenzó el Paralelo");
            cronometro.Start();
            await EjecutarTareasParalelasAsync();
            cronometro.Stop();
            Console.WriteLine("\nTerminó el Paralelo en " + cronometro.ToString() + " segundos");



            Console.WriteLine("\nComenzó el Secuencial");

            cronometro2.Start();
            await OperacionCortaAsync();
            await OperacionMediaAsync();
            await OperacionLargaAsync();
            cronometro2.Stop();

            Console.WriteLine("\nTerminó el Secuencial en " + cronometro2.ToString() + " segundos");
            Console.ReadKey();


        }

        public async static Task OperacionMediaAsync()
        {
            await Task.Delay(2000);
        }
        public async static Task OperacionLargaAsync()
        {
            await Task.Delay(3000);
        }
        public async static Task OperacionCortaAsync()
        {
            await Task.Delay(1000);
        }

        public async static Task EjecutarTareasParalelasAsync()
        {
            Task[] tareas = new Task[] { OperacionCortaAsync(), OperacionMediaAsync(), OperacionLargaAsync() };

            await Task.WhenAll(tareas);
        }
    }
}
