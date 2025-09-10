using DTOs;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WindowsForms
{
    //Revisar si no seria mejor usar metodos estaticos        
   
    public class ClienteApiClient
    {
        private static HttpClient client = new HttpClient();
        static ClienteApiClient() 
        {
            client.BaseAddress = new Uri("http://localhost:5183/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public static async Task<Cliente> GetAsync(int id)
        {
            Cliente cliente = null;
            HttpResponseMessage response = await client.GetAsync("clientes/"+id);
            if (response.IsSuccessStatusCode)
            {
                cliente = await response.Content.ReadAsAsync<Cliente>();
            }
            return cliente;
        }

        public static async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            IEnumerable<Cliente> clientes = null;
            HttpResponseMessage response = await client.GetAsync("clientes");
            if (response.IsSuccessStatusCode)
            {
                clientes = await response.Content.ReadAsAsync<IEnumerable<Cliente>>();
            }
            return clientes;
        }

        public async static Task AddAsync(Cliente cliente)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("clientes", cliente);
            response.EnsureSuccessStatusCode();
        }

        public static async Task DeleteAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync("clientes/" + id);
            response.EnsureSuccessStatusCode();
        }

        public static async Task UpdateAsync(Cliente cliente)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync("clientes", cliente);
            response.EnsureSuccessStatusCode();
        }
    }
}
