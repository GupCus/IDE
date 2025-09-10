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

            Mode = FormMode.Add;
        }

        private async void aceptarButton_Click(object sender, EventArgs e)
        {
            if (this.ValidateCliente())
            {
                try
                {
                    this.Cliente.Nombre = nombreTextBox.Text;
                    this.Cliente.Apellido = apellidoTextBox.Text;
                    this.Cliente.Email = emailTextBox.Text;
                    this.Cliente.FechaAlta = DateTime.Parse(fechaAltaTextBox.Text);

                    //El Detalle se esta llevando la responsabilidad de llamar al servicio
                    //pero tal vez deberia ser solo una vista y que esta responsabilidad quede
                    //en la Lista o tal vez en un Presenter o Controler

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
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

            if (this.nombreTextBox.Text == string.Empty)
            {
                isValid = false;
                errorProvider.SetError(nombreTextBox, "El Nombre es Requerido");
            }

            if (this.apellidoTextBox.Text == string.Empty)
            {
                isValid = false;
                errorProvider.SetError(apellidoTextBox, "El Apellido es Requerido");
            }

            if (this.emailTextBox.Text == string.Empty)
            {
                isValid = false;
                errorProvider.SetError(emailTextBox, "El Email es Requerido");
            }
            else if (!EsEmailValido(this.emailTextBox.Text))
            {
                isValid = false;
                errorProvider.SetError(emailTextBox, "El formato del Email no es válido");
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
