namespace Ejercicio7
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // No es posible hacer este laboratorio en NET 8 debido a que cambio la manera de hacer databinding con EF
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}