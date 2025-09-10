using DTOs;
using API.Clients;

namespace API.Auth.Blazor.Server
{
    public class BlazorServerAuthService : IAuthService
    {
        // Almacenamiento global para el usuario actual (singleton per app)
        private static SessionData? _currentSession;

        public event Action<bool>? AuthenticationStateChanged;

        public BlazorServerAuthService()
        {
        }

        private class SessionData
        {
            public string? Token { get; set; }
            public string? Username { get; set; }
            public DateTime Expiration { get; set; }
        }

        public Task<bool> IsAuthenticatedAsync()
        {
            try
            {
                if (_currentSession != null)
                {
                    return Task.FromResult(!string.IsNullOrEmpty(_currentSession.Token) && DateTime.UtcNow < _currentSession.Expiration);
                }
                return Task.FromResult(false);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        public Task<string?> GetTokenAsync()
        {
            try
            {
                return Task.FromResult(_currentSession?.Token);
            }
            catch
            {
                return Task.FromResult<string?>(null);
            }
        }

        public Task<string?> GetUsernameAsync()
        {
            try
            {
                return Task.FromResult(_currentSession?.Username);
            }
            catch
            {
                return Task.FromResult<string?>(null);
            }
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
                _currentSession = new SessionData
                {
                    Token = response.Token,
                    Username = response.Username,
                    Expiration = response.ExpiresAt
                };

                AuthenticationStateChanged?.Invoke(true);
                return true;
            }

            return false;
        }

        public Task LogoutAsync()
        {
            _currentSession = null;
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