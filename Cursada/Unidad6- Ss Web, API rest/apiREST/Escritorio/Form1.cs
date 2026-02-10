using Dominio;
using System.Net.Http;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Escritorio
{
    public partial class Form1 : Form
    {
        private bool confirmarEliminar = false;
        //Inicializacion httpclient
        private readonly HttpClient httpClient = new()
        {
            BaseAddress = new Uri("http://localhost:5024")
        };
        //Constructor
        public Form1()
        {
            InitializeComponent();
        }

        //Evento Load
        private void Form1_Load(object sender, EventArgs e)
        {
            this.GetAlumnos();
        }

        //Metodo que se encarga de manipular el objeto alumno antes de ser enviado
        private Alumno LimpiarAlumno()
        {

            Alumno al = new()
            {
                Apellido = string.IsNullOrEmpty(txtApellido.Text) ? "Doe" : txtApellido.Text,
                Nombre = string.IsNullOrEmpty(txtNombre.Text) ? "Juan" : txtNombre.Text,
                Legajo = (!int.TryParse(txtLegajo.Text, out int leg)) ? (new Random()).Next(99999) : leg,
                Direccion = string.IsNullOrEmpty(txtDireccion.Text) ? "Black Mesa, Nuevo Mexico" : txtDireccion.Text,
            };

            return al;
        }
        //Limpia las casillas al hacer click
        private void Txt_Click(object sender, EventArgs e)
        {
            ((TextBox)sender).Text = "";
        }
        //Al Seleccionar un alumno (fila), se rescatan sus datos a la UI
        private void dgvAlumnos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAlumnos.CurrentRow != null && dgvAlumnos.CurrentRow.DataBoundItem is Alumno alumno)
            {

                txtID.Text = alumno.Id.ToString();
                txtNombre.Text = alumno.Nombre;
                txtApellido.Text = alumno.Apellido;
                txtLegajo.Text = alumno.Legajo.ToString();
                txtDireccion.Text = alumno.Direccion;

                Modificar.Enabled = true;
                Eliminar.Enabled = true;

                //Reset eliminar
                if (confirmarEliminar)
                {
                    Eliminar.Text = "ELIMINAR ALUMNO";
                    confirmarEliminar = false;
                }

            }
        }

        //GET ALL Alumnos || Actualización de la tabla principal
        private async void GetAlumnos()
        {
            IEnumerable<Alumno>? alumnos =
            await httpClient.GetFromJsonAsync<IEnumerable<Alumno>>("alumnos");
            this.dgvAlumnos.DataSource = alumnos;
        }

        //POST Alumno
        private async void Cargar_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            Alumno al = this.LimpiarAlumno();
            await httpClient.PostAsJsonAsync("alumnos", al);
            this.GetAlumnos();
        }
        //PUT Alumno
        private async void Modificar_Click(object sender, EventArgs e)
        {
            Alumno al = this.LimpiarAlumno();
            await httpClient.PutAsJsonAsync($"alumnos/{((Alumno)dgvAlumnos.CurrentRow.DataBoundItem).Id}", al);
            this.GetAlumnos();
        }

        //DELETE alumno
        private async void Eliminar_Click(object sender, EventArgs e)
        {
            if (!confirmarEliminar)
            {
                Eliminar.Text = "¿ESTÁ SEGURO?";
                confirmarEliminar = true;
            }
            //Hacer click de vuelta para ejecutar esto
            else
            {
                await httpClient.DeleteAsync($"alumnos/{((Alumno)dgvAlumnos.CurrentRow.DataBoundItem).Id}");
                this.GetAlumnos();
                Eliminar.Text = "ELIMINAR ALUMNO";
                confirmarEliminar = false;
            }
        }
    }
}
