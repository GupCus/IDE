using DTOs;
using API.Clients;

namespace API.Auth.MAUI
{
    public class MAUIAuthService : IAuthService
    {
        // Almacenamiento temporal en memoria hasta resolver workloads MAUI
        private static string? _token;
        private static string? _username;
        private static DateTime? _expiration;

        public event Action<bool>? AuthenticationStateChanged;

        public Task<bool> IsAuthenticatedAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(_token) || !_expiration.HasValue)
                    return Task.FromResult(false);

                return Task.FromResult(DateTime.UtcNow < _expiration.Value);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        public Task<string?> GetTokenAsync()
        {
            return Task.FromResult(_token);
        }

        public async Task<string?> GetUsernameAsync()
        {
            var isAuth = await IsAuthenticatedAsync();
            return isAuth ? _username : null;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var request = new LoginRequest
            {
                Username = username,
                Password = password
            };

            var authClient = new AuthApiClient();
            var response = await authClient.LoginAsync(request);

            if (response != null)
            {
                _token = response.Token;
                _username = response.Username;
                _expiration = response.ExpiresAt;

                AuthenticationStateChanged?.Invoke(true);
                return true;
            }

            return false;
        }

        public Task LogoutAsync()
        {
            _token = null;
            _username = null;
            _expiration = null;
            
            AuthenticationStateChanged?.Invoke(false);
            return Task.CompletedTask;
        }

        public async Task CheckTokenExpirationAsync()
        {
            if (!await IsAuthenticatedAsync())
            {
                await LogoutAsync();
            }
        }
    }
}