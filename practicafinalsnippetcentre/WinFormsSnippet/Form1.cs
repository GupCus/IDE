using Clases;
using Context;
using System.Collections.ObjectModel;

namespace WinFormsSnippet
{
    public partial class FormPrincipal : Form
    {

        /* Definiciones */
        private Button? botonActual;
        List<Lenguaje> Lenguajes;
        //Handlers para eventos que hay que ir cambiando
        private EventHandler? _handlerNvoSnippet;
        private EventHandler? _handlerEditLabel;
        private EventHandler? _handlerBorrarLenguaje;

        // Colores para mostrar los botones seleccionados (o sea los desactivados)
        private readonly Color _colorNormal = ColorTranslator.FromHtml("#1E1E1E");
        private readonly Color _colorDeshabilitado = ColorTranslator.FromHtml("#007ACC");
        private readonly Color _textoNormal = ColorTranslator.FromHtml("#E6E6E6");
        private readonly Color _textoDeshabilitado = ColorTranslator.FromHtml("#FFFFFF");

        /* Metodos principales */
        public FormPrincipal()
        {
            InitializeComponent();
            buttonNvoSnippet.Visible = false;
            
        }
        private async void FormPrincipal_Load(object sender, EventArgs e)
        {
            await CargarSnippets();
        }
        private async Task CargarSnippets()
        {
            panellenguajes.Controls.Clear();
            Lenguajes = await LenguajeRepository.EncontrarLenguajesAsync();
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

        // Desuscribir el handler anterior si existe
        // Crear y guardar el nuevo handler
        public async void SetearLenguaje(Lenguaje lenguaje)
        {
            labelLenguaje.Text = lenguaje.Nombre;

            if (_handlerEditLabel != null)
            {
                labelLenguaje.DoubleClick -= _handlerEditLabel;
            }
            _handlerEditLabel = (sender, e) => labelLenguaje_DoubleClick(sender, e, lenguaje);
            labelLenguaje.DoubleClick += _handlerEditLabel;

            if (_handlerNvoSnippet != null)
            {
                buttonNvoSnippet.Click -= _handlerNvoSnippet;
            }

            _handlerNvoSnippet = (sender, e) => buttonNvoSnippet_Click(sender, e, lenguaje);
            buttonNvoSnippet.Click += _handlerNvoSnippet;

            if (_handlerBorrarLenguaje != null)
            {
                btnEliminarLenguaje.Click -= _handlerBorrarLenguaje;
            }

            _handlerBorrarLenguaje = (sender, e) => btnEliminarLenguaje_Click(sender, e, lenguaje);
            btnEliminarLenguaje.Click += _handlerBorrarLenguaje;

            await MostrarSnippets(lenguaje);

        }

        public async Task MostrarSnippets(Lenguaje lenguaje)
        {
            flowSnippets.Controls.Clear();
            lenguaje = await LenguajeRepository.EncontrarLenguajeAsync(lenguaje.Id);
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
                    labelTitulo.Anchor = AnchorStyles.None;
                    labelTitulo.AutoSize = true;
                    labelTitulo.Font = new Font("Segoe UI", 15F);
                    labelTitulo.Margin = new Padding(18, 8, 3, 4);
                    labelTitulo.Size = new Size(65, 28);
                    labelTitulo.Text = snippet.Titulo;
                    labelTitulo.DoubleClick += (s, e) => label_DoubleClick(s, e, lenguaje, snippet);

                    var botoneliminar = new Button
                    {
                        Text = "Eliminar",
                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                        BackColor = Color.DarkRed,
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        Height = 25,
                        Width = 80,
                        Margin = new Padding(18, 8, 3, 4),
                        Anchor = AnchorStyles.Right,
                        Dock = DockStyle.Right,
                    };
                    botoneliminar.Click += (sender, e) => btnEliminarSnippet_Click(sender, e, lenguaje, snippet);
                    flowSnippets.Controls.Add(botoneliminar);
                    var textBoxSnippet = new RichTextBox
                    {
                        Multiline = true,
                        Width = 750,
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
                    textBoxSnippet.Text = snippet.Codigo;
                    //En caso de modificación
                    textBoxSnippet.TextChanged += (sender, e) =>
                    {
                        snippet.Codigo = textBoxSnippet.Text;
                    };

                }
            }
        }

        public void DesactivarBoton()
        {
            if (botonActual != null)
            {
                botonActual.BackColor = _colorNormal;
                botonActual.ForeColor = _textoNormal;
                botonActual.Cursor = Cursors.Hand;
                flowSnippets.Controls.Clear();
                buttonNvoSnippet.Visible = false;
                btnEliminarLenguaje.Visible = false;
                labelLenguaje.Visible = false;
                Label labelSinSeleccion = new();
                labelSinSeleccion.Anchor = AnchorStyles.None;
                labelSinSeleccion.AutoSize = true;
                labelSinSeleccion.Font = new Font("Segoe UI", 30F, FontStyle.Italic);
                labelSinSeleccion.ForeColor = Color.DimGray;
                labelSinSeleccion.Location = new Point(183, 160);
                labelSinSeleccion.Margin = new Padding(183, 160, 137, 160);
                labelSinSeleccion.Size = new Size(418, 134);
                labelSinSeleccion.TabIndex = 0;
                labelSinSeleccion.Text = "No hay lenguajes seleccionados...";
                flowSnippets.Controls.Add(labelSinSeleccion);
            }
        }

        /* Eventos */
        private void botonLenguaje_Click(object sender, EventArgs e, Lenguaje l)
        {
            if (sender != botonActual)
            {
                DesactivarBoton();
                botonActual = (Button)sender;
                botonActual.BackColor = _colorDeshabilitado;
                botonActual.ForeColor = _textoDeshabilitado;
                botonActual.Cursor = Cursors.Default;
                SetearLenguaje(l);
                labelLenguaje.Visible = true;
                buttonNvoSnippet.Visible = true;
                btnEliminarLenguaje.Visible = true;
            }
        }

        private async void buttonNvaCat_Click(object sender, EventArgs e)
        {
            await LenguajeRepository.CrearLenguajeAsync(new Lenguaje
            {
                Nombre = "Nuevo Lenguaje",
            });
            await CargarSnippets();
        }

        private async void buttonNvoSnippet_Click(object sender, EventArgs e, Lenguaje l)
        {
            await LenguajeRepository.CrearSnippetAsync(l.Id, new Snippet
            {
                Titulo = "Nuevo Snippet",
                Codigo = "// Escribe tu código aquí",
            });
            await MostrarSnippets(l);
        }

        private async void label_DoubleClick(object sender, EventArgs e, Lenguaje l, Snippet snip)
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
                textBox.Focus();
                textBox.SelectAll();

                // Al perder foco, guardar y restaurar
                textBox.LostFocus += async (s, ev) =>
                {
                    label.Text = textBox.Text;
                    textBox.Dispose();
                    snip.Titulo = label.Text;
                    await LenguajeRepository.ActualizarSnippetAsync(snip);
                    label.Visible = true;

                };

                textBox.KeyDown += async (s, ev) =>
                {
                    if (ev.KeyCode == Keys.Enter)
                    {
                        label.Text = textBox.Text;
                        textBox.Dispose();
                        snip.Titulo = label.Text;
                        await LenguajeRepository.ActualizarSnippetAsync(snip);
                        label.Visible = true;
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

        private async void labelLenguaje_DoubleClick(object sender, EventArgs e, Lenguaje l)
        {
            if (sender is Label label)
            {

                var textBox = new TextBox
                {
                    Text = label.Text,
                    Font = label.Font,
                    Size = label.Size,
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = label.Margin,
                    ForeColor = label.ForeColor,
                    BackColor = this.BackColor,
                    Dock = label.Dock,
                    TextAlign = HorizontalAlignment.Center,
                    Anchor = label.Anchor
                };

                // Ocultar el label
                label.Visible = false;

                // Insertar el TextBox en el mismo índice
                label.Parent?.Controls.Add(textBox);
                textBox.Focus();
                textBox.SelectAll();

                // Al perder foco, guardar y restaurar
                textBox.LostFocus += async (s, ev) =>
                {

                    label.Text = textBox.Text;
                    l.Nombre = textBox.Text;
                    label.Visible = true;
                    textBox.Dispose();
                    await LenguajeRepository.ActualizarLenguajeAsync(l);
                    await CargarSnippets();
                };

                textBox.KeyDown += async (s, ev) =>
                {
                    if (ev.KeyCode == Keys.Enter)
                    {
                        label.Text = textBox.Text;
                        l.Nombre = textBox.Text;
                        label.Visible = true;
                        textBox.Dispose();
                        ev.SuppressKeyPress = true;
                        await LenguajeRepository.ActualizarLenguajeAsync(l);
                        
                    }
                    else if (ev.KeyCode == Keys.Escape)
                    {
                        label.Visible = true;
                        textBox.Dispose();
                    }
                };


            }
        }

        private async void btnEliminarLenguaje_Click(object sender, EventArgs e, Lenguaje l)
        {
            if (MessageBox.Show("Desea eliminar el lenguaje?", "Confirmar", MessageBoxButtons.OKCancel) == DialogResult.OK) { await LenguajeRepository.EliminarLenguajeAsync(l.Id); }
            await CargarSnippets();
            DesactivarBoton();
        }

        private async void btnEliminarSnippet_Click(object sender, EventArgs e, Lenguaje l, Snippet s)
        {
            if (MessageBox.Show("Desea eliminar el snippet?", "Confirmar", MessageBoxButtons.OKCancel) == DialogResult.OK) { await LenguajeRepository.EliminarSnippetAsync(l.Id,s.Id); }
            await MostrarSnippets(l);
        }

    }
}
