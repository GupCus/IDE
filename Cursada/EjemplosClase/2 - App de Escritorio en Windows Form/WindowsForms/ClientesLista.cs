using DTOs;
using API.Clients;

namespace WindowsForms
{
    public partial class ClientesLista : Form
    {
        public ClientesLista()
        {            
            InitializeComponent();
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            this.GetAllAndLoad();
        }

        private void agregarButton_Click(object sender, EventArgs e)
        {
            ClienteDetalle clienteDetalle = new ClienteDetalle();

            ClienteDTO clienteNuevo = new ClienteDTO();

            clienteDetalle.Mode = FormMode.Add;
            clienteDetalle.Cliente = clienteNuevo;

            clienteDetalle.ShowDialog();

            this.GetAllAndLoad();
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

                clienteDetalle.ShowDialog();

                this.GetAllAndLoad();
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
                int id = this.SelectedItem().Id;
                
                var result = MessageBox.Show($"¿Está seguro que desea eliminar el cliente con Id {id}?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    await ClienteApiClient.DeleteAsync(id);
                    this.GetAllAndLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void GetAllAndLoad()
        {
            try
            {
                this.clientesDataGridView.DataSource = null;
                this.clientesDataGridView.DataSource = await ClienteApiClient.GetAllAsync();

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


    }
}
