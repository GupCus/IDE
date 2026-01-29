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
            buttonNvoSnippet.Visible = false;
            //Carga lenguaje y snippets de ejemplo
            Lenguajes = [new Lenguaje { Nombre = "C#" }, new Lenguaje { Nombre = "Python" }];
            Lenguajes[0].Snippets.Add(new Snippet
            {
                Titulo = "Hola Mundo C#",
                Codigo = "Console.WriteLine(\"Hola, Mundo!\");",
            });
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

                // Captura la variable lenguaje en el closure
                var lenguajeCapturado = lenguaje;
                boton.Click += (sender, e) => botonLenguaje_Click(sender, e, lenguajeCapturado);

                panellenguajes.Controls.Add(boton);

            }
        }
        public void ActivarBoton(Button sender, Lenguaje l)
        {
            DesactivarBoton();
            botonActual = sender;
            sender.BackColor = _colorDeshabilitado;
            sender.ForeColor = _textoDeshabilitado;
            sender.Cursor = Cursors.Default;
            MostrarSnippets(l);
            buttonNvoSnippet.Visible = true;
            labelSinSeleccion.Dispose();
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

        public void MostrarSnippets(Lenguaje lenguaje)
        {
            labelLenguaje.Text = lenguaje.Nombre;
            flowSnippets.Controls.Clear();
            if (lenguaje.Snippets.Count == 0)
            {
                var labelVacio = new Label
                {
                    Anchor = AnchorStyles.None,
                    AutoSize = true,
                    Font = new Font("Segoe UI", 25F, FontStyle.Italic),
                    ForeColor = Color.DimGray,
                    Margin = new Padding(130, 120, 120, 120),
                    Size = new Size(332, 108),
                    Text = "No hay Snippets aún",
                };
                flowSnippets.Controls.Add(labelVacio);
                return;

            }
            else
            {
                foreach (var snippet in lenguaje.Snippets)
                {
                    var labelTitulo = new Label();
                    flowSnippets.Controls.Add(labelTitulo);
                    flowSnippets.SetFlowBreak(labelTitulo, true);
                    labelTitulo.Anchor = AnchorStyles.None;
                    labelTitulo.AutoSize = true;
                    labelTitulo.Font = new Font("Segoe UI", 15F);
                    labelTitulo.Margin = new Padding(18, 8, 3, 4);
                    labelTitulo.Size = new Size(65, 28);
                    labelTitulo.Text = snippet.Titulo;

                    var textBoxSnippet = new RichTextBox
                    {
                        Multiline = true,
                        Width = 400,
                        Height = 100,
                        Margin = new Padding(3, 4, 3, 12),
                        Font = new Font("Consolas", 10),
                        ForeColor = Color.Black,
                    };

                    flowSnippets.Controls.Add(textBoxSnippet);
                    flowSnippets.SetFlowBreak(textBoxSnippet, true);
                    textBoxSnippet.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                    textBoxSnippet.BackColor = SystemColors.WindowFrame;
                    textBoxSnippet.Margin = new Padding(18, 2, 18, 2);
                    textBoxSnippet.Name = "textBoxSnippet";
                    textBoxSnippet.Size = new Size(600, 91);
                    textBoxSnippet.Text = snippet.Codigo;

                }
            }
        }

        private void botonLenguaje_Click(object sender, EventArgs e, Lenguaje l)
        {
            if (sender != botonActual)
            {
                ActivarBoton((Button)sender, l);
            }
        }

        private void buttonNvaCat_Click(object sender, EventArgs e)
        {
            Lenguajes.Add(new Lenguaje { Nombre = "JavaScript" });
            CargarSnippets();
        }
    }
}
