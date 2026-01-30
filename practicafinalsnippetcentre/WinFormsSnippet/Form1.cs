using Clases;
using System.Collections.ObjectModel;

namespace WinFormsSnippet
{
    public partial class FormPrincipal : Form
    {
        private Button? botonActual;
        private EventHandler? _handlerNvoSnippet;
        List<Lenguaje> Lenguajes { get; set; }

        // Colores para estado
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

                var lenguajeCapturado = lenguaje;
                boton.Click += (sender, e) => botonLenguaje_Click(sender, e, lenguajeCapturado);

                panellenguajes.Controls.Add(boton);

            }
        }

        public void SetearLenguaje(Lenguaje lenguaje)
        {
            labelLenguaje.Text = lenguaje.Nombre;

            // Desuscribir el handler anterior si existe
            if (_handlerNvoSnippet != null)
            {
                buttonNvoSnippet.Click -= _handlerNvoSnippet;
            }

            // Crear y guardar el nuevo handler
            _handlerNvoSnippet = (sender, e) => buttonNvoSnippet_Click(sender, e, lenguaje);
            buttonNvoSnippet.Click += _handlerNvoSnippet;

            MostrarSnippets(lenguaje);

        }

        public void MostrarSnippets(Lenguaje lenguaje)
        {
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
                    labelTitulo.DoubleClick += label_DoubleClick;

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
                labelSinSeleccion.Dispose();
                DesactivarBoton();
                botonActual = (Button)sender;
                botonActual.BackColor = _colorDeshabilitado;
                botonActual.ForeColor = _textoDeshabilitado;
                botonActual.Cursor = Cursors.Default;
                SetearLenguaje(l);
                buttonNvoSnippet.Visible = true;
            }
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

        private void buttonNvaCat_Click(object sender, EventArgs e)
        {
            Lenguajes.Add(new Lenguaje { Nombre = "JavaScript" });
            CargarSnippets();
        }

        private void buttonNvoSnippet_Click(object sender, EventArgs e, Lenguaje l)
        {
            l.Snippets.Add(new Snippet
            {
                Titulo = "Nuevo Snippet",
                Codigo = "// Escribe tu código aquí",
            });
            MostrarSnippets(l);
        }

        private void label_DoubleClick(object sender, EventArgs e)
        {
            if (sender is Label label)
            {
                // Obtener el índice del label en el FlowLayoutPanel
                int indice = flowSnippets.Controls.GetChildIndex(label);

                var textBox = new TextBox
                {
                    Text = label.Text,
                    Font = label.Font,
                    Size = label.Size,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = label.Margin,
                    ForeColor = label.ForeColor,
                    BackColor = this.BackColor,
                };

                // Ocultar el label
                label.Visible = false;

                // Insertar el TextBox en el mismo índice
                flowSnippets.Controls.Add(textBox);
                flowSnippets.Controls.SetChildIndex(textBox, indice);
                flowSnippets.SetFlowBreak(textBox, true);
                textBox.Focus();
                textBox.SelectAll();

                // Al perder foco, guardar y restaurar
                textBox.LostFocus += (s, ev) =>
                {
                    label.Text = textBox.Text;
                    label.Visible = true;
                    textBox.Dispose();
                };

                textBox.KeyDown += (s, ev) =>
                {
                    if (ev.KeyCode == Keys.Enter)
                    {
                        label.Text = textBox.Text;
                        label.Visible = true;
                        textBox.Dispose();
                        ev.SuppressKeyPress = true;
                    }
                    else if (ev.KeyCode == Keys.Escape)
                    {
                        label.Visible = true;
                        textBox.Dispose();
                    }
                };
            }
        }
    }
}
