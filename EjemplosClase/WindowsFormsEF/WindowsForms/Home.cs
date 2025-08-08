using System;
using System.Windows.Forms;

namespace WindowsForms
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClientesLista clientesForm = new ClientesLista();
            clientesForm.ShowDialog();
        }
    }
}