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
    public partial class FormMain : Form
    {
        public BindingList<Usuario> Lista = new()
        {
            new Usuario("1", "Ana", "Pérez", "aperez", "ana.perez@email.com", true),
            new Usuario("2", "Luis", "García", "lgarcia", "luis.garcia@email.com", false),
            new Usuario("3", "Marta", "López", "mlopez", "marta.lopez@email.com", true)
        };
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            dgvUsuarios.AutoGenerateColumns = false;
        }
        private void FormMain_Shown(object sender, EventArgs e)
        {
            FormLogin appLogin = new();
            if (appLogin.ShowDialog() != DialogResult.OK)
            {
                this.Dispose();
            }
            else
            {
                Listar();
            }
        }

        public void Listar()
        {
            dgvUsuarios.DataSource = Lista;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            dgvUsuarios.SelectAll();
            dgvUsuarios.ClearSelection();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            FormAgregar formusuario = new();
            if((formusuario.ShowDialog() == DialogResult.OK) && formusuario.UsuarioEditado != null)
            {
                Lista.Add(formusuario.UsuarioEditado);
            }
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if(dgvUsuarios.CurrentRow.DataBoundItem != null)
            {
                Usuario usueditado = (Usuario)dgvUsuarios.CurrentRow.DataBoundItem;
                FormAgregar formusuario = new(usueditado);
                if (formusuario.ShowDialog() == DialogResult.OK)
                {
                    usueditado.Nombre = formusuario.UsuarioEditado.Nombre;
                    usueditado.Apellido = formusuario.UsuarioEditado.Apellido;
                    usueditado.UsuarioN = formusuario.UsuarioEditado.UsuarioN;
                    usueditado.Email = formusuario.UsuarioEditado.Email;
                    usueditado.Habilitado = formusuario.UsuarioEditado.Habilitado;
                }
            }
            
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if(dgvUsuarios.CurrentRow.DataBoundItem != null)
            {
                Lista.Remove(dgvUsuarios.CurrentRow.DataBoundItem as Usuario);
            }

        }
    }
}
