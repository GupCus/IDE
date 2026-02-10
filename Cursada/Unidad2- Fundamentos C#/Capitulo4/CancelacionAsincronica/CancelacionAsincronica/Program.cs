using System.Threading.Tasks;

namespace CancelacionAsincronica
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                CancellationTokenSource token = new CancellationTokenSource();
                Progress<int> progreso = new Progress<int>((porcentaje) =>
                Console.WriteLine("Progreso " + porcentaje + "%"));
                var tarea = OperacionCancelableAsync(token.Token,progreso);

                var tareaTecla = Task.Run(() =>
                {
                    Console.WriteLine("Presiona una tecla para cancelar la operación...");
                    Console.ReadKey();
                });

                var tareacompletada = await Task.WhenAny(tarea, tareaTecla);

                if (tareacompletada == tareaTecla)
                {
                    token.Cancel();
                    await tarea;
                }

                Console.WriteLine("Completado");
            }
            catch(Exception e)
            {
                Console.Clear();
                Console.WriteLine("Cancelada: ");
                Console.WriteLine(e.Message);
            }
            
        }
        public static async Task OperacionCancelableAsync(CancellationToken token, IProgress<int> progreso)
        {
            for (int i = 1; i <= 10; i++)
            {
                await Task.Delay(1000);
                token.ThrowIfCancellationRequested();
                progreso.Report(i * 10);
            }
        }
    }
}
