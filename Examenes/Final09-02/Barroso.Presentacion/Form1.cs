using Barroso.Negocio;

namespace Barroso.Presentacion
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }
        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            using var formAgregar = new FormAgregar();
            formAgregar.StartPosition = FormStartPosition.CenterParent;

            if (formAgregar.ShowDialog(this) == DialogResult.OK)
            {
                await CargarLiquidacionesAsync();
            }
        }

        private async void FormPrincipal_Load(object? sender, EventArgs e)
        {
            await CargarLiquidacionesAsync();
        }

        private async Task CargarLiquidacionesAsync()
        {
            var liquidaciones = await LiquidacionDTONegocio.GetAll();
            dgvLiquidaciones.DataSource = liquidaciones.ToList();
        }

        private async void cbEstado_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var estado = cbEstado.SelectedItem.ToString()!;
            var liquidaciones = await LiquidacionDTONegocio.GetPorEstado(estado);
            dgvLiquidaciones.DataSource = liquidaciones.ToList();
        }
    }
}
