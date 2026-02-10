using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class FormAgregar : Form
    {
        private Usuario? _usuario;
        public Usuario? UsuarioEditado;
        public FormAgregar()
        {
            InitializeComponent();
        }
        public FormAgregar(Usuario? usuario)
        {
            InitializeComponent();
            _usuario = usuario;
            this.Text = "Editar Usuario";
            btnAgregar.Text = "Editar";
            labelForm.Text = "Editar Usuario";
        }

        private void FormAgregar_Load(object sender, EventArgs e)
        {
            if (_usuario != null)
            {
                nombre.Text = _usuario.Nombre;
                apellido.Text = _usuario.Apellido;
                usuario.Text = _usuario.UsuarioN;
                email.Text = _usuario.Email;
                habilitado.Checked = _usuario.Habilitado;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Crea o actualiza el usuario con los datos de los controles
            UsuarioEditado = new Usuario(
                    _usuario?.Id,
                    nombre.Text,
                    apellido.Text,
                    usuario.Text,
                    email.Text,
                    habilitado.Checked
            );
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
