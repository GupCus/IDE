using DTOs;
using API.Clients;

namespace WindowsForms
{
    public partial class ClienteLista : Form
    {
        public ClienteLista()
        {            
            InitializeComponent();
            ConfigurarColumnas();
        }

        private void ConfigurarColumnas()
        {
            this.clientesDataGridView.AutoGenerateColumns = false;
            
            this.clientesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                HeaderText = "Id",
                DataPropertyName = "Id",
                Width = 80
            });
            
            this.clientesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nombre",
                HeaderText = "Nombre",
                DataPropertyName = "Nombre",
                Width = 200
            });
            
            this.clientesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Apellido",
                HeaderText = "Apellido",
                DataPropertyName = "Apellido",
                Width = 200
            });
            
            this.clientesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                HeaderText = "Email",
                DataPropertyName = "Email",
                Width = 250
            });

            this.clientesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PaisNombre",
                HeaderText = "País",
                DataPropertyName = "PaisNombre",
                Width = 150
            });
            
            this.clientesDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FechaAlta",
                HeaderText = "Fecha Alta",
                DataPropertyName = "FechaAlta",
                Width = 250,
                DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm:ss" }
            });
        }

        private async void Clientes_Load(object sender, EventArgs e)
        {
            await ConfigureButtonPermissions();
            this.GetByCriteriaAndLoad();
        }

        private async Task ConfigureButtonPermissions()
        {
            var authService = AuthServiceProvider.Instance;
            
            // Verificar permisos para cada botón
            bool canAdd = await authService.HasPermissionAsync("clientes.agregar");
            bool canUpdate = await authService.HasPermissionAsync("clientes.actualizar");
            bool canDelete = await authService.HasPermissionAsync("clientes.eliminar");
            
            // Configurar visibilidad de botones según permisos
            agregarButton.Visible = canAdd;
            actualizarButton.Visible = canUpdate;
            eliminarButton.Visible = canDelete;
            
            // Guardar permisos en Tag para uso posterior
            agregarButton.Tag = canAdd;
            actualizarButton.Tag = canUpdate;
            eliminarButton.Tag = canDelete;
        }

        private void agregarButton_Click(object sender, EventArgs e)
        {
            ClienteDTO clienteNuevo = new ClienteDTO();
            ClienteDetalle clienteDetalle = new ClienteDetalle(FormMode.Add, clienteNuevo);

            clienteDetalle.ShowDialog();

            this.GetByCriteriaAndLoad();
        }

        private async void actualizarButton_Click(object sender, EventArgs e)
        {
            int id = this.SelectedItem().Id;

            ClienteDTO cliente = await ClienteApiClient.GetAsync(id);

            ClienteDetalle clienteDetalle = new ClienteDetalle(FormMode.Update, cliente);

            clienteDetalle.ShowDialog();

            this.GetByCriteriaAndLoad();
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            ClienteDTO cliente = this.SelectedItem();
            
            var result = MessageBox.Show($"¿Está seguro que desea eliminar el cliente {cliente.Nombre} {cliente.Apellido} ({cliente.Email})?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                await ClienteApiClient.DeleteAsync(cliente.Id);
                this.GetByCriteriaAndLoad();
            }
        }

        private async void GetByCriteriaAndLoad(string texto = "")
        {
            this.clientesDataGridView.DataSource = null;
            
            IEnumerable<ClienteDTO> clientes;
            if (string.IsNullOrWhiteSpace(texto))
            {
                clientes = await ClienteApiClient.GetAllAsync();
            }
            else
            {
                clientes = await ClienteApiClient.GetByCriteriaAsync(texto);
            }
            
            this.clientesDataGridView.DataSource = clientes;

            // Solo manejar Enabled/Disabled si los botones son visibles (tienen permisos)
            bool canUpdate = actualizarButton.Tag is bool updatePermission && updatePermission;
            bool canDelete = eliminarButton.Tag is bool deletePermission && deletePermission;

            if (this.clientesDataGridView.Rows.Count > 0)
            {
                this.clientesDataGridView.Rows[0].Selected = true;
                
                // Solo habilitar si tiene permisos Y hay elementos
                if (canDelete) this.eliminarButton.Enabled = true;
                if (canUpdate) this.actualizarButton.Enabled = true;
            }
            else
            {
                // Solo deshabilitar si son visibles
                if (canDelete) this.eliminarButton.Enabled = false;
                if (canUpdate) this.actualizarButton.Enabled = false;
            }
        }

        private ClienteDTO SelectedItem()
        {
            ClienteDTO cliente;

            cliente = (ClienteDTO)clientesDataGridView.SelectedRows[0].DataBoundItem;

            return cliente;
        }

        private void buscarButton_Click(object sender, EventArgs e)
        {
            string texto = this.buscarTextBox.Text.Trim();
            this.GetByCriteriaAndLoad(texto);
        }

    }
}
