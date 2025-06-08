namespace ProgresoAsincronico
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            Progress<int> progreso = new Progress<int>((porcentaje)=>
            Console.WriteLine("Progreso " + porcentaje+"%"));
            await OperacionLargaConProgresoAsync(progreso);

        }
        public static async Task OperacionLargaConProgresoAsync(IProgress<int> progreso)
        {
            for (int i = 1; i <= 10; i++)
            {
                await Task.Delay(500);
                progreso.Report(i * 10);
            }
        }
    }
}
