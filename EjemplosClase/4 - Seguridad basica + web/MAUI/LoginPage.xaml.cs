using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using API.Clients;

namespace MAUI;

public partial class LoginPage : ContentPage, INotifyPropertyChanged
{
    private string _username = "";
    private string _password = "";
    private string _errorMessage = "";
    private bool _hasError = false;

    public string Username
    {
        get => _username;
        set { _username = value; OnPropertyChanged(); }
    }

    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(); }
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set { _errorMessage = value; OnPropertyChanged(); }
    }

    public bool HasError
    {
        get => _hasError;
        set { _hasError = value; OnPropertyChanged(); }
    }

    public ICommand LoginCommand { get; private set; }

    public LoginPage()
    {
        InitializeComponent();
        LoginCommand = new Command(async () => await LoginAsync());
        BindingContext = this;
    }

    private async Task LoginAsync()
    {
        try
        {
            HasError = false;
            
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Usuario y contraseña son requeridos";
                HasError = true;
                return;
            }

            var authService = AuthServiceProvider.Instance;
            var success = await authService.LoginAsync(Username, Password);

            if (success)
            {
                // Navegar a la página principal
                await Shell.Current.GoToAsync("//main");
            }
            else
            {
                ErrorMessage = "Usuario o contraseña incorrectos";
                HasError = true;
            }
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage = $"Error de conexión: {ex.Message}";
            HasError = true;
        }
        catch (TaskCanceledException ex)
        {
            ErrorMessage = $"Timeout de conexión: {ex.Message}";
            HasError = true;
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error inesperado: {ex.Message}";
            HasError = true;
        }
    }

    public new event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}