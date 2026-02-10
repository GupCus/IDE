using DTOs;
using Microsoft.JSInterop;
using API.Clients;
using System.IdentityModel.Tokens.Jwt;

namespace API.Auth.Blazor.WebAssembly
{
    public class BlazorWasmAuthService : IAuthService
    {
        private readonly IJSRuntime _jsRuntime;
        private const string TOKEN_KEY = "auth_token";
        private const string USERNAME_KEY = "auth_username";
        private const string EXPIRATION_KEY = "auth_expiration";

        public event Action<bool>? AuthenticationStateChanged;

        public BlazorWasmAuthService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            // Simplificar y usar exactamente el mismo código que funciona manualmente
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TOKEN_KEY);
            var expiration = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", EXPIRATION_KEY);
            
            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(expiration))
            {
                if (DateTime.TryParse(expiration, null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime exp))
                {
                    var isValid = DateTime.UtcNow < exp;
                    return isValid;
                }
            }
            
            return false;
        }

        public async Task<string?> GetTokenAsync()
        {
            try
            {
                return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TOKEN_KEY);
            }
            catch
            {
                return null;
            }
        }

        public async Task<string?> GetUsernameAsync()
        {
            try
            {
                var isAuth = await IsAuthenticatedAsync();
                if (!isAuth) return null;
                
                return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", USERNAME_KEY);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"[BlazorWasmAuth] LoginAsync iniciado para usuario: {username}");
                
                var request = new LoginRequest
                {
                    Username = username,
                    Password = password
                };

                var authClient = new AuthApiClient();
                var response = await authClient.LoginAsync(request);

                if (response != null)
                {
                    System.Diagnostics.Debug.WriteLine($"[BlazorWasmAuth] Response recibida, guardando en localStorage...");
                    System.Diagnostics.Debug.WriteLine($"[BlazorWasmAuth] Token: {response.Token?.Substring(0, Math.Min(50, response.Token?.Length ?? 0))}...");
                    
                    try
                    {
                        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TOKEN_KEY, response.Token);
                        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", USERNAME_KEY, response.Username);
                        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", EXPIRATION_KEY, response.ExpiresAt.ToString("O"));
                        
                        AuthenticationStateChanged?.Invoke(true);
                        return true;
                    }
                    catch (Exception jsEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"[BlazorWasmAuth] ERROR guardando en localStorage: {jsEx.Message}");
                        throw;
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"[BlazorWasmAuth] Response fue null - credenciales inválidas");
                }

                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[BlazorWasmAuth] Error en LoginAsync: {ex.Message}");
                throw;
            }
        }

        public async Task LogoutAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TOKEN_KEY);
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", USERNAME_KEY);
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", EXPIRATION_KEY);
            
            AuthenticationStateChanged?.Invoke(false);
        }

        public async Task CheckTokenExpirationAsync()
        {
            if (!await IsAuthenticatedAsync())
            {
                await LogoutAsync();
            }
        }

        public async Task<bool> HasPermissionAsync(string permission)
        {
            try
            {
                var token = await GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    return false;

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(token);
                
                var permissionClaims = jsonToken.Claims
                    .Where(c => c.Type == "permission")
                    .Select(c => c.Value);

                return permissionClaims.Contains(permission);
            }
            catch
            {
                return false;
            }
        }
    }
}