using DTOs;
using MAUI.ViewModels;

namespace MAUI.Views;

public partial class ClienteDetallePage : ContentPage
{
    public ClienteDetallePage()
    {
        InitializeComponent();
        BindingContext = new ClienteDetalleViewModel();
    }

    public ClienteDetallePage(ClienteDTO cliente)
    {
        InitializeComponent();
        BindingContext = new ClienteDetalleViewModel(cliente);
    }
}