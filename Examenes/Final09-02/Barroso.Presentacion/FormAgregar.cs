using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Barroso.Negocio;
using Barroso.DTOs;

namespace Barroso.Presentacion
{
    public partial class FormAgregar : Form
    {
        public FormAgregar()
        {
            InitializeComponent();
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            await LiquidacionDTONegocio.Post(new LiquidacionCreateDTO
            {
                Fecha = DateOnly.FromDateTime(dtpFecha.Value),
                Empleado = txtEmpleado.Text,
                Monto = (decimal)float.Parse(txtMonto.Text)

            });
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
