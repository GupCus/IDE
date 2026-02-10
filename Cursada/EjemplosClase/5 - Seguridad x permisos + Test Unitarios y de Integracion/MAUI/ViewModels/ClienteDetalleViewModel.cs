using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Input;
using DTOs;
using API.Clients;
using Microsoft.Maui.Controls;

namespace MAUI.ViewModels
{
    public class ClienteDetalleViewModel : INotifyPropertyChanged
    {
        private ClienteDTO _cliente;
        private bool _isLoading;
        private string _successMessage = string.Empty;
        private List<PaisDTO> _paises = new();
        private PaisDTO _selectedPais;

        public ClienteDTO Cliente
        {
            get => _cliente;
            set
            {
                _cliente = value;
                OnPropertyChanged();
            }
        }

        public bool IsEditMode { get; }
        public string PageTitle => IsEditMode ? "Editar Cliente" : "Agregar Cliente";

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public string SuccessMessage
        {
            get => _successMessage;
            set
            {
                _successMessage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasSuccessMessage));
            }
        }

        public bool HasSuccessMessage => !string.IsNullOrEmpty(SuccessMessage);

        public List<PaisDTO> Paises
        {
            get => _paises;
            set
            {
                _paises = value;
                OnPropertyChanged();
            }
        }

        public PaisDTO SelectedPais
        {
            get => _selectedPais;
            set
            {
                _selectedPais = value;
                if (value != null)
                {
                    Cliente.PaisId = value.Id;
                }
                OnPropertyChanged();
            }
        }

        public Dictionary<string, string> ErrorMessages { get; set; } = new();
        public Dictionary<string, bool> HasError { get; set; } = new();

        public ICommand AceptarCommand { get; private set; }
        public ICommand CancelarCommand { get; private set; }

        public ClienteDetalleViewModel()
        {
            Cliente = new ClienteDTO();
            IsEditMode = false;
            InitializeCommands();
            InitializeValidation();
            _ = InitializeAsync();
        }

        public ClienteDetalleViewModel(ClienteDTO cliente)
        {
            Cliente = cliente ?? new ClienteDTO();
            IsEditMode = cliente?.Id > 0;
            InitializeCommands();
            InitializeValidation();
            _ = InitializeAsync();
        }

        private void InitializeCommands()
        {
            AceptarCommand = new Command(async () => await GuardarCliente());
            CancelarCommand = new Command(async () => await CancelarOperacion());
        }

        private void InitializeValidation()
        {
            ErrorMessages["Nombre"] = string.Empty;
            ErrorMessages["Apellido"] = string.Empty;
            ErrorMessages["Email"] = string.Empty;
            ErrorMessages["Pais"] = string.Empty;
            
            HasError["Nombre"] = false;
            HasError["Apellido"] = false;
            HasError["Email"] = false;
            HasError["Pais"] = false;
        }

        private async Task InitializeAsync()
        {
            await LoadPaises();
        }

        private async Task GuardarCliente()
        {
            try
            {
                if (!ValidateCliente())
                    return;

                IsLoading = true;
                SuccessMessage = string.Empty;

                if (IsEditMode)
                {
                    await ClienteApiClient.UpdateAsync(Cliente);
                }
                else
                {
                    await ClienteApiClient.AddAsync(Cliente);
                }
                
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al guardar cliente: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task CancelarOperacion()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private bool ValidateCliente()
        {
            bool isValid = true;
            
            // Limpiar errores anteriores
            InitializeValidation();

            // Validar Nombre
            if (string.IsNullOrWhiteSpace(Cliente.Nombre))
            {
                ErrorMessages["Nombre"] = "El Nombre es requerido";
                HasError["Nombre"] = true;
                isValid = false;
            }

            // Validar Apellido
            if (string.IsNullOrWhiteSpace(Cliente.Apellido))
            {
                ErrorMessages["Apellido"] = "El Apellido es requerido";
                HasError["Apellido"] = true;
                isValid = false;
            }

            // Validar Email
            if (string.IsNullOrWhiteSpace(Cliente.Email))
            {
                ErrorMessages["Email"] = "El Email es requerido";
                HasError["Email"] = true;
                isValid = false;
            }
            else if (!EsEmailValido(Cliente.Email))
            {
                ErrorMessages["Email"] = "El formato del Email no es válido";
                HasError["Email"] = true;
                isValid = false;
            }

            // Validar País
            if (Cliente.PaisId <= 0)
            {
                ErrorMessages["Pais"] = "Debe seleccionar un país";
                HasError["Pais"] = true;
                isValid = false;
            }

            // Notificar cambios en las propiedades de validación
            OnPropertyChanged(nameof(ErrorMessages));
            OnPropertyChanged(nameof(HasError));

            return isValid;
        }

        private async Task LoadPaises()
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(() => IsLoading = true);
                
                var paises = await PaisApiClient.GetAllAsync();
                
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Paises = paises?.ToList() ?? new List<PaisDTO>();
                    
                    // Si estamos en modo edición, seleccionar el país del cliente
                    if (IsEditMode)
                    {
                        SelectedPais = Paises.FirstOrDefault(p => p.Id == Cliente.PaisId);
                    }
                });
            }
            catch (Exception ex)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar países: {ex.Message}", "OK");
                });
            }
            finally
            {
                MainThread.BeginInvokeOnMainThread(() => IsLoading = false);
            }
        }

        private static bool EsEmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}