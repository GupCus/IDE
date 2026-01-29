using Clases;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace proyectoAeroSnippet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Snippet> Snippets { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Snippets = [new Snippet{Titulo = "Prueba", Lenguaje = "C#", Codigo = " List<Snippet> Snippets { get; set; }" }];
            ListaSnippets.ItemsSource = Snippets;
        }

        private void ListaSnippets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ListaSnippets.SelectedItem != null){
                Snippet seleccion = (Snippet)ListaSnippets.SelectedItem;
                VisorCodigo.Text = seleccion.Codigo;
            }
        }

        private void BtnAgregarSnippet_Click(object sender, RoutedEventArgs e)
        {
            Snippets.Add(new Snippet { Titulo = "Nuevo Snippet", Lenguaje = "C#", Codigo = "// Escribe tu código aquí" });
        }
    }
}