using DTOs;
using API.Clients;

namespace WindowsForms
{
    public partial class ClientesLista : Form
    {
        public ClientesLista()
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
                Name = "FechaAlta",
                HeaderText = "Fecha Alta",
                DataPropertyName = "FechaAlta",
                Width = 250,
                DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm:ss" }
            });
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            this.GetByCriteriaAndLoad();
        }

        private void agregarButton_Click(object sender, EventArgs e)
        {
            ClienteDetalle clienteDetalle = new ClienteDetalle();

            ClienteDTO clienteNuevo = new ClienteDTO();

            clienteDetalle.Mode = FormMode.Add;
            clienteDetalle.Cliente = clienteNuevo;

            if (clienteDetalle.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Cliente agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.GetByCriteriaAndLoad();
        }

        private async void modificarButton_Click(object sender, EventArgs e)
        {
            try
            {
                ClienteDetalle clienteDetalle = new ClienteDetalle();
               
                int id = this.SelectedItem().Id;

                ClienteDTO cliente = await ClienteApiClient.GetAsync(id);

                clienteDetalle.Mode = FormMode.Update;
                clienteDetalle.Cliente = cliente;

                if (clienteDetalle.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Cliente actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.GetByCriteriaAndLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar cliente para modificar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void eliminarButton_Click(object sender, EventArgs e)
        {
            try
            {
                ClienteDTO cliente = this.SelectedItem();
                
                var result = MessageBox.Show($"¿Está seguro que desea eliminar el cliente {cliente.Nombre} {cliente.Apellido} ({cliente.Email})?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    await ClienteApiClient.DeleteAsync(cliente.Id);
                    MessageBox.Show("Cliente eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.GetByCriteriaAndLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void GetByCriteriaAndLoad(string texto = "")
        {
            try
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

                if (this.clientesDataGridView.Rows.Count > 0)
                {
                    this.clientesDataGridView.Rows[0].Selected = true;
                    this.eliminarButton.Enabled = true;
                    this.modificarButton.Enabled = true;
                }
                else
                {
                    this.eliminarButton.Enabled = false;
                    this.modificarButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar la lista de clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.eliminarButton.Enabled = false;
                this.modificarButton.Enabled = false;
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
