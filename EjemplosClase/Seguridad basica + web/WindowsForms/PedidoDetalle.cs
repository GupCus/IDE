using DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using API.Clients;

namespace WindowsForms
{
    public partial class PedidoDetalle : Form
    {
        private PedidoDTO pedido;
        private FormMode mode;
        private List<ItemPedidoDTO> itemsLocales;

        public PedidoDTO Pedido
        {
            get { return pedido; }
            set
            {
                pedido = value;
                this.SetPedido();
            }
        }

        public FormMode Mode
        {
            get
            {
                return mode;
            }
            set
            {
                SetFormMode(value);
            }
        }

        public PedidoDetalle()
        {
            InitializeComponent();
        }

        public PedidoDetalle(FormMode mode, PedidoDTO pedido) : this()
        {
            Init(mode, pedido);
        }

        private async void Init(FormMode mode, PedidoDTO pedido)
        {
            ConfigurarColumnas();
            itemsLocales = new List<ItemPedidoDTO>();
            await LoadClientes();
            this.Mode = mode;
            this.Pedido = pedido;
        }

        private void ConfigurarColumnas()
        {
            this.itemsDataGridView.AutoGenerateColumns = false;

            this.itemsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductoNombre",
                HeaderText = "Producto",
                DataPropertyName = "ProductoNombre",
                Width = 300
            });

            this.itemsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Cantidad",
                HeaderText = "Cantidad",
                DataPropertyName = "Cantidad",
                Width = 120
            });

            this.itemsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PrecioUnitario",
                HeaderText = "Precio Unitario",
                DataPropertyName = "PrecioUnitario",
                Width = 250,
                DefaultCellStyle = { Format = "C2" }
            });

            this.itemsDataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                HeaderText = "Total",
                DataPropertyName = "Total",
                Width = 150,
                DefaultCellStyle = { Format = "C2" }
            });
        }

        private async Task LoadClientes()
        {
            var clientes = await ClienteApiClient.GetAllAsync();
            var clientesConNombre = clientes.Select(c => new
            {
                Id = c.Id,
                NombreCompleto = $"{c.Nombre} {c.Apellido}"
            }).ToList();

            clienteComboBox.DataSource = clientesConNombre;
            clienteComboBox.DisplayMember = "NombreCompleto";
            clienteComboBox.ValueMember = "Id";
            clienteComboBox.SelectedIndex = -1;
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (this.ValidatePedido())
            {
                this.Pedido.ClienteId = (int)clienteComboBox.SelectedValue;
                this.Pedido.FechaPedido = DateTime.ParseExact(fechaPedidoTextBox.Text, "dd/MM/yyyy", null);
                this.Pedido.Items = itemsLocales.ToList();

                if (this.Mode == FormMode.Update)
                {
                    await PedidoApiClient.UpdateAsync(this.Pedido);
                }
                else
                {
                    await PedidoApiClient.AddAsync(this.Pedido);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void SetPedido()
        {
            this.idTextBox.Text = this.Pedido.Id.ToString();
            
            // En modo Add, siempre inicializar con fecha actual
            if (this.Mode == FormMode.Add)
            {
                this.Pedido.FechaPedido = DateTime.Now;
            }
            
            this.fechaPedidoTextBox.Text = this.Pedido.FechaPedido.ToString("dd/MM/yyyy");
            this.clienteComboBox.SelectedValue = this.Pedido.ClienteId;

            // Clonar items para modelo local
            itemsLocales = this.Pedido.Items.Select(item => new ItemPedidoDTO
            {
                PedidoId = item.PedidoId,
                ProductoId = item.ProductoId,
                ProductoNombre = item.ProductoNombre,
                Cantidad = item.Cantidad,
                PrecioUnitario = item.PrecioUnitario
            }).ToList();

            RefreshItemsGrid();
        }

        private void SetFormMode(FormMode value)
        {
            mode = value;

            if (Mode == FormMode.Add)
            {
                idLabel.Visible = false;
                idTextBox.Visible = false;
                fechaPedidoLabel.Visible = true;
                fechaPedidoTextBox.Visible = true;
            }

            if (Mode == FormMode.Update)
            {
                idLabel.Visible = true;
                idTextBox.Visible = true;
                fechaPedidoLabel.Visible = true;
                fechaPedidoTextBox.Visible = true;
            }
        }

        private bool ValidatePedido()
        {
            bool isValid = true;

            errorProvider.SetError(clienteComboBox, string.Empty);
            errorProvider.SetError(itemsDataGridView, string.Empty);

            if (this.clienteComboBox.SelectedValue == null)
            {
                isValid = false;
                errorProvider.SetError(clienteComboBox, "Debe seleccionar un cliente");
            }

            if (itemsLocales.Count == 0)
            {
                isValid = false;
                errorProvider.SetError(itemsDataGridView, "Debe agregar al menos un producto al pedido");
            }

            return isValid;
        }

        private void agregarItemButton_Click(object sender, EventArgs e)
        {
            ItemPedidoDTO nuevoItem = new ItemPedidoDTO();
            ItemPedidoDetalle itemDetalle = new ItemPedidoDetalle(FormMode.Add, nuevoItem);

            if (itemDetalle.ShowDialog() == DialogResult.OK)
            {
                itemsLocales.Add(itemDetalle.Item);
                RefreshItemsGrid();
            }
        }

        private void modificarItemButton_Click(object sender, EventArgs e)
        {
            if (itemsDataGridView.SelectedRows.Count > 0)
            {
                ItemPedidoDTO selectedItem = (ItemPedidoDTO)itemsDataGridView.SelectedRows[0].DataBoundItem;

                // Clonar item para edición
                ItemPedidoDTO itemCopia = new ItemPedidoDTO
                {
                    PedidoId = selectedItem.PedidoId,
                    ProductoId = selectedItem.ProductoId,
                    ProductoNombre = selectedItem.ProductoNombre,
                    Cantidad = selectedItem.Cantidad,
                    PrecioUnitario = selectedItem.PrecioUnitario
                };

                ItemPedidoDetalle itemDetalle = new ItemPedidoDetalle(FormMode.Update, itemCopia);

                if (itemDetalle.ShowDialog() == DialogResult.OK)
                {
                    // Actualizar item en la lista
                    int index = itemsLocales.FindIndex(i => i.ProductoId == selectedItem.ProductoId);
                    if (index >= 0)
                    {
                        itemsLocales[index] = itemDetalle.Item;
                        RefreshItemsGrid();
                    }
                }
            }
        }

        private void eliminarItemButton_Click(object sender, EventArgs e)
        {
            if (itemsDataGridView.SelectedRows.Count > 0)
            {
                ItemPedidoDTO selectedItem = (ItemPedidoDTO)itemsDataGridView.SelectedRows[0].DataBoundItem;

                var result = MessageBox.Show($"¿Está seguro que desea eliminar el producto {selectedItem.ProductoNombre}?",
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    itemsLocales.RemoveAll(i => i.ProductoId == selectedItem.ProductoId);
                    RefreshItemsGrid();
                }
            }
        }

        private void RefreshItemsGrid()
        {
            itemsDataGridView.DataSource = null;
            itemsDataGridView.DataSource = itemsLocales;

            // Habilitar/deshabilitar botones
            bool hasItems = itemsLocales.Count > 0;
            modificarButton.Enabled = hasItems;
            eliminarButton.Enabled = hasItems;

            if (hasItems)
            {
                itemsDataGridView.Rows[0].Selected = true;
            }

            // Actualizar totales en footer
            UpdateTotales();
        }

        private void UpdateTotales()
        {
            int totalProductos = itemsLocales.Sum(i => i.Cantidad);
            decimal totalPrecio = itemsLocales.Sum(i => i.Total);

            totalProductosLabel.Text = $"Total Productos: {totalProductos}";
            totalPrecioLabel.Text = $"Total Precio: {totalPrecio:C2}";
        }
    }
}