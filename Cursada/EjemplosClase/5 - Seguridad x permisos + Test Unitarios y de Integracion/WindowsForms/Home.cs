using System;
using System.Windows.Forms;
using API.Clients;

namespace WindowsForms
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            ConfigureMenuPermissions();
        }

        private async void ConfigureMenuPermissions()
        {
            var authService = AuthServiceProvider.Instance;
            
            // Verificar permiso para Clientes
            bool canViewClientes = await authService.HasPermissionAsync("clientes.leer");
            clientesToolStripMenuItem.Visible = canViewClientes;
            
            // Verificar permiso para Pedidos
            bool canViewPedidos = await authService.HasPermissionAsync("pedidos.leer");
            pedidosToolStripMenuItem.Visible = canViewPedidos;
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClienteLista clientesForm = new ClienteLista();
            clientesForm.ShowDialog();
        }

        private void pedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PedidoLista pedidosForm = new PedidoLista();
            pedidosForm.ShowDialog();
        }
    }
}