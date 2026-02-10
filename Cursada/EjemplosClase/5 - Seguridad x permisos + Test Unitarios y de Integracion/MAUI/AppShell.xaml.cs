using API.Clients;

namespace MAUI;

public partial class AppShell : Shell
{
	private readonly IAuthService _authService;

	public AppShell()
	{
		InitializeComponent();
		
		// Get AuthService instance
		_authService = AuthServiceProvider.Instance ?? throw new InvalidOperationException("AuthService not configured");
		
		// Subscribe to authentication changes
		_authService.AuthenticationStateChanged += OnAuthenticationStateChanged;
		
		// Register routes for navigation
		Routing.RegisterRoute("login", typeof(LoginPage));
		
		// Check initial permissions
		_ = UpdateMenuVisibilityAsync();
	}

	private void OnAuthenticationStateChanged(bool isAuthenticated)
	{
		Dispatcher.Dispatch(async () =>
		{
			await UpdateMenuVisibilityAsync();
		});
	}

	private async Task UpdateMenuVisibilityAsync()
	{
		try
		{
			var isAuthenticated = await _authService.IsAuthenticatedAsync();
			if (isAuthenticated)
			{
				var canReadClientes = await _authService.HasPermissionAsync("clientes.leer");
				ClientesShellContent.IsVisible = canReadClientes;
			}
			else
			{
				ClientesShellContent.IsVisible = false;
			}
		}
		catch (Exception ex)
		{
			// Log error and hide menu item by default
			System.Diagnostics.Debug.WriteLine($"Error updating menu visibility: {ex.Message}");
			ClientesShellContent.IsVisible = false;
		}
	}
}
