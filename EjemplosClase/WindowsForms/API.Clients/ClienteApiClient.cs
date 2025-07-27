using DTOs;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace API.Clients
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


        public static async Task<ClienteDTO> GetAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("clientes/" + id);
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<ClienteDTO>();
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al obtener cliente con Id {id}. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al obtener cliente con Id {id}: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al obtener cliente con Id {id}: {ex.Message}", ex);
            }
        }

        public static async Task<IEnumerable<ClienteDTO>> GetAllAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("clientes");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<IEnumerable<ClienteDTO>>();
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al obtener lista de clientes. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al obtener lista de clientes: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al obtener lista de clientes: {ex.Message}", ex);
            }
        }

        public async static Task AddAsync(ClienteDTO cliente)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("clientes", cliente);
                
                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al crear cliente. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al crear cliente: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al crear cliente: {ex.Message}", ex);
            }
        }

        public static async Task DeleteAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("clientes/" + id);
                
                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al eliminar cliente con Id {id}. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al eliminar cliente con Id {id}: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al eliminar cliente con Id {id}: {ex.Message}", ex);
            }
        }

        public static async Task UpdateAsync(ClienteDTO cliente)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("clientes", cliente);
                
                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al actualizar cliente con Id {cliente.Id}. Status: {response.StatusCode}, Detalle: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión al actualizar cliente con Id {cliente.Id}: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception($"Timeout al actualizar cliente con Id {cliente.Id}: {ex.Message}", ex);
            }
        }
    }
}
