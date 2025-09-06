namespace ExcepcionesAsincronicas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProbarManejoExcepcionesAsync();
            Console.ReadKey();
        }

        public static async Task OperacionConErrorAsync()
        {
            await Task.Delay(2000);
            throw new InvalidOperationException("Error simulado en operación asincrónica");
        }
        public static async Task ProbarManejoExcepcionesAsync()
        {
            try
            {
                await OperacionConErrorAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine("Se capturó una excepción: ");
                Console.WriteLine(e.Message);
            }
        }
    }
}
