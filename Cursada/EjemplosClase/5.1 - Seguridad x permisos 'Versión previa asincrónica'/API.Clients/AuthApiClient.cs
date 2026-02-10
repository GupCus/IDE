using DTOs;
using System.Text;
using System.Text.Json;

namespace API.Clients
{
    public class AuthApiClient : BaseApiClient
    {
        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            using var httpClient = await CreateHttpClientAsync();

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<LoginResponse>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            // Si no es successful, devolver null (credenciales incorrectas)
            return null;
        }
    }
}