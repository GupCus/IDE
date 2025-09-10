using DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using API.Clients;

namespace WindowsForms
{
    public partial class ItemPedidoDetalle : Form
    {
        private ItemPedidoDTO item;
        private FormMode mode;

        public ItemPedidoDTO Item
        {
            get { return item; }
            set
            {
                item = value;
                this.SetItem();
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

        public ItemPedidoDetalle()
        {
            InitializeComponent();
        }

        public ItemPedidoDetalle(FormMode mode, ItemPedidoDTO item) : this()
        {
            Init(mode, item);
        }

        private async void Init(FormMode mode, ItemPedidoDTO item)
        {
            await LoadProductos();
            this.Mode = mode;
            this.Item = item;
        }

        private async Task LoadProductos()
        {
            var productos = await ProductoApiClient.GetAllAsync();
            productoComboBox.DataSource = productos.ToList();
            productoComboBox.DisplayMember = "Nombre";
            productoComboBox.ValueMember = "Id";
            productoComboBox.SelectedIndex = -1;
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (this.ValidateItem())
            {
                this.Item.ProductoId = (int)productoComboBox.SelectedValue;
                this.Item.ProductoNombre = productoComboBox.Text;
                this.Item.Cantidad = int.Parse(cantidadTextBox.Text);
                this.Item.PrecioUnitario = decimal.Parse(precioUnitarioTextBox.Text);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void SetItem()
        {
            this.productoComboBox.SelectedValue = this.Item.ProductoId;
            this.cantidadTextBox.Text = this.Item.Cantidad.ToString();
            this.precioUnitarioTextBox.Text = this.Item.PrecioUnitario.ToString("F2");
        }

        private void SetFormMode(FormMode value)
        {
            mode = value;

            if (Mode == FormMode.Update)
            {
                // En modo Update, no permitir cambiar el Producto
                productoComboBox.Enabled = false;
            }
            else
            {
                productoComboBox.Enabled = true;
            }
        }

        private bool ValidateItem()
        {
            bool isValid = true;

            errorProvider.SetError(productoComboBox, string.Empty);
            errorProvider.SetError(cantidadTextBox, string.Empty);
            errorProvider.SetError(precioUnitarioTextBox, string.Empty);

            if (this.productoComboBox.SelectedValue == null)
            {
                isValid = false;
                errorProvider.SetError(productoComboBox, "Debe seleccionar un producto");
            }

            if (string.IsNullOrWhiteSpace(this.cantidadTextBox.Text))
            {
                isValid = false;
                errorProvider.SetError(cantidadTextBox, "La cantidad es requerida");
            }
            else if (!int.TryParse(this.cantidadTextBox.Text, out int cantidad) || cantidad <= 0)
            {
                isValid = false;
                errorProvider.SetError(cantidadTextBox, "La cantidad debe ser un número mayor a 0");
            }

            if (string.IsNullOrWhiteSpace(this.precioUnitarioTextBox.Text))
            {
                isValid = false;
                errorProvider.SetError(precioUnitarioTextBox, "El precio unitario es requerido");
            }
            else if (!decimal.TryParse(this.precioUnitarioTextBox.Text, out decimal precio) || precio < 0)
            {
                isValid = false;
                errorProvider.SetError(precioUnitarioTextBox, "El precio unitario debe ser un número mayor o igual a 0");
            }

            return isValid;
        }

        private async void productoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Auto-completar precio del Producto seleccionado
            if (productoComboBox.SelectedValue != null && productoComboBox.SelectedValue is int productoId)
            {
                var producto = await ProductoApiClient.GetAsync(productoId);
                if (producto != null)
                {
                    precioUnitarioTextBox.Text = producto.Precio.ToString("F2");
                }
            }
        }
    }
}