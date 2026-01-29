using Clases;
using System.Collections.ObjectModel;

namespace WinFormsSnippet
{
    public partial class FormPrincipal : Form
    {
        private Button? botonActual;
        List<Lenguaje> Lenguajes { get; set; }

        // Colores para estado activo/inactivo
        private readonly Color _colorNormal = ColorTranslator.FromHtml("#1E1E1E");
        private readonly Color _colorDeshabilitado = ColorTranslator.FromHtml("#007ACC");
        private readonly Color _textoNormal = ColorTranslator.FromHtml("#E6E6E6");
        private readonly Color _textoDeshabilitado = ColorTranslator.FromHtml("#FFFFFF");

        public FormPrincipal()
        {
            InitializeComponent();
            Lenguajes = [new Lenguaje { Nombre = "C#" }, new Lenguaje { Nombre = "Python" }];
            CargarSnippets();
        }

        public void DesactivarBoton()
        {
            if (botonActual != null)
            {
                botonActual.BackColor = _colorNormal;
                botonActual.ForeColor = _textoNormal;
                botonActual.Cursor = Cursors.Hand;
            }
        }

        public void ActivarBoton(Button sender)
        {
            DesactivarBoton();
            botonActual = sender;
            sender.BackColor = _colorDeshabilitado;
            sender.ForeColor = _textoDeshabilitado;
            sender.Cursor = Cursors.Default;
        }

        private void botonej_Click(object sender, EventArgs e)
        {
            if (sender != botonActual) // Evita reactivar el mismo botón
            {
                ActivarBoton((Button)sender);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Lenguajes.Add(new Lenguaje { Nombre = "JavaScript" });
            CargarSnippets();
        }

        private void CargarSnippets()
        {
            panellenguajes.Controls.Clear();
            foreach (var lenguaje in Lenguajes)
            {
                var boton = new Button
                {
                    Text = lenguaje.Nombre,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Dock = DockStyle.Top,
                    Height = 40,
                    BackColor = _colorNormal,
                    ForeColor = _textoNormal,
                    FlatStyle = FlatStyle.Flat,
                    FlatAppearance = { BorderSize = 0 }
                };
                boton.Click += botonej_Click;
                panellenguajes.Controls.Add(boton);

            }
        }
    }
}
