using System.Net.Http.Headers;

namespace Barroso.Negocio
{
    public static class Conexion
    {
        public static HttpClient Cliente { get; }

        static Conexion()
        {
            Cliente = new HttpClient();
            Cliente.BaseAddress = new Uri("https://localhost:7261/api/");
            Cliente.DefaultRequestHeaders.Accept.Clear();
            Cliente.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
        }
    }
}
