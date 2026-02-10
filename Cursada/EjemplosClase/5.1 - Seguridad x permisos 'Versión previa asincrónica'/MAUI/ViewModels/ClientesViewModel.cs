using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using DTOs;
using API.Clients;
using MAUI.Views;

namespace MAUI.ViewModels
{
    public class ClientesViewModel : INotifyPropertyChanged
    {
        private bool _isLoading;
        
        public ObservableCollection<ClienteDTO> Clientes { get; set; } = new();

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public ICommand AgregarCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand EliminarCommand { get; }
        public ICommand RefreshCommand { get; }

        public ClientesViewModel()
        {
            AgregarCommand = new Command(async () => await AgregarCliente());
            EditarCommand = new Command<ClienteDTO>(async (cliente) => await EditarCliente(cliente));
            EliminarCommand = new Command<ClienteDTO>(async (cliente) => await EliminarCliente(cliente));
            RefreshCommand = new Command(async () => await LoadClientesAsync());
        }

        public async Task LoadClientesAsync()
        {
            try
            {
                IsLoading = true;
                var clientes = await ClienteApiClient.GetAllAsync();
                
                Clientes.Clear();
                if (clientes != null)
                {
                    foreach (var cliente in clientes)
                    {
                        Clientes.Add(cliente);
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar clientes: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task AgregarCliente()
        {
            try
            {
                var detallePage = new ClienteDetallePage();
                await Application.Current.MainPage.Navigation.PushAsync(detallePage);
                
                // Refresh cuando vuelve de la página de detalle
                await LoadClientesAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al abrir formulario: {ex.Message}", "OK");
            }
        }

        private async Task EditarCliente(ClienteDTO cliente)
        {
            try
            {
                var detallePage = new ClienteDetallePage(cliente);
                await Application.Current.MainPage.Navigation.PushAsync(detallePage);
                
                // Refresh cuando vuelve de la página de detalle
                await LoadClientesAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al abrir formulario: {ex.Message}", "OK");
            }
        }

        private async Task EliminarCliente(ClienteDTO cliente)
        {
            try
            {
                bool confirm = await Application.Current.MainPage.DisplayAlert(
                    "Confirmar eliminación", 
                    $"¿Está seguro que desea eliminar el cliente {cliente.Nombre} {cliente.Apellido} ({cliente.Email})?", 
                    "Sí", "No");

                if (confirm)
                {
                    await ClienteApiClient.DeleteAsync(cliente.Id);
                    await LoadClientesAsync();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al eliminar cliente: {ex.Message}", "OK");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}