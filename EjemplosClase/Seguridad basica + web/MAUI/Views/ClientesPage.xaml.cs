using MAUI.ViewModels;

namespace MAUI.Views;

public partial class ClientesPage : ContentPage
{
    public ClientesPage()
    {
        InitializeComponent();
        BindingContext = new ClientesViewModel();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ClientesViewModel viewModel)
        {
            await viewModel.LoadClientesAsync();
        }
    }
}