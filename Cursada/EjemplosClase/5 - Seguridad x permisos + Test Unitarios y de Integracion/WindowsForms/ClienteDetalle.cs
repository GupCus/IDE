using DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using API.Clients;

namespace WindowsForms
{
    public enum FormMode
    {
        Add,
        Update
    }

    public partial class ClienteDetalle : Form
    {
        private ClienteDTO cliente;
        private FormMode mode;

        public ClienteDTO Cliente
        {
            get { return cliente; }
            set
            {
                cliente = value;
                this.SetCliente();
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

        public ClienteDetalle()
        {
            InitializeComponent();
        }

        public ClienteDetalle(FormMode mode, ClienteDTO cliente) : this()
        {
            Init(mode, cliente);
        }

        private async void Init(FormMode mode, ClienteDTO cliente)
        {
            await LoadPaises();
            this.Mode = mode;
            this.Cliente = cliente;
        }

        private async Task LoadPaises()
        {
            var paises = await PaisApiClient.GetAllAsync();
            paisComboBox.DataSource = paises.ToList();
            paisComboBox.DisplayMember = "Nombre";
            paisComboBox.ValueMember = "Id";
            paisComboBox.SelectedIndex = -1;
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (this.ValidateCliente())
            {
                this.Cliente.Nombre = nombreTextBox.Text;
                this.Cliente.Apellido = apellidoTextBox.Text;
                this.Cliente.Email = emailTextBox.Text;
                this.Cliente.PaisId = (int)paisComboBox.SelectedValue;
                this.Cliente.FechaAlta = DateTime.Parse(fechaAltaTextBox.Text);

                if (this.Mode == FormMode.Update)
                {
                    await ClienteApiClient.UpdateAsync(this.Cliente);
                }
                else
                {
                    await ClienteApiClient.AddAsync(this.Cliente);
                }

                this.Close();
            }
        }

        private void cancelarButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetCliente()
        {
            this.idTextBox.Text = this.Cliente.Id.ToString();
            this.nombreTextBox.Text = this.Cliente.Nombre;
            this.apellidoTextBox.Text = this.Cliente.Apellido;
            this.fechaAltaTextBox.Text = this.Cliente.FechaAlta.ToString();
            this.emailTextBox.Text = this.Cliente.Email;
            this.paisComboBox.SelectedValue = this.Cliente.PaisId;
        }

        private void SetFormMode(FormMode value)
        {
            mode = value;

            if (Mode == FormMode.Add)
            {
                idLabel.Visible = false;
                idTextBox.Visible = false;
                fechaAltaLabel.Visible = false;
                fechaAltaTextBox.Visible = false;
            }

            if (Mode == FormMode.Update)
            {
                idLabel.Visible = true;
                idTextBox.Visible = true;
                fechaAltaLabel.Visible = true;
                fechaAltaTextBox.Visible = true;
            }
        }

        private bool ValidateCliente()
        {
            bool isValid = true;

            errorProvider.SetError(nombreTextBox, string.Empty);
            errorProvider.SetError(apellidoTextBox, string.Empty);
            errorProvider.SetError(emailTextBox, string.Empty);
            errorProvider.SetError(paisComboBox, string.Empty);

            if (this.nombreTextBox.Text == string.Empty)
            {
                isValid = false;
                errorProvider.SetError(nombreTextBox, "El Nombre es requerido");
            }

            if (this.apellidoTextBox.Text == string.Empty)
            {
                isValid = false;
                errorProvider.SetError(apellidoTextBox, "El Apellido es requerido");
            }

            if (this.emailTextBox.Text == string.Empty)
            {
                isValid = false;
                errorProvider.SetError(emailTextBox, "El Email es requerido");
            }
            else if (!EsEmailValido(this.emailTextBox.Text))
            {
                isValid = false;
                errorProvider.SetError(emailTextBox, "El formato del Email no es válido");
            }

            if (this.paisComboBox.SelectedValue == null)
            {
                isValid = false;
                errorProvider.SetError(paisComboBox, "Debe seleccionar un País");
            }

            return isValid;
        }

        private static bool EsEmailValido(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}
