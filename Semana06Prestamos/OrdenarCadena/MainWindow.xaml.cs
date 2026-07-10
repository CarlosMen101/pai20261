using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OrdenarCadena
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] listaNombres;

        public MainWindow()
        {
            InitializeComponent();

            // Asignamos los eventos a los botones desde el código
            btnListar.Click += BtnListar_Click;
            btnPasar.Click += BtnPasar_Click;
        }

        // 1. Evento para el botón <<Listar>>
        private void BtnListar_Click(object sender, RoutedEventArgs e)
        {
            string nombres = txtCadena.Text;
            if (string.IsNullOrEmpty(nombres))
            {
                MessageBox.Show("Ingrese la cadena de nombres",
                    "Validación", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Separar por espacios
            listaNombres = nombres.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            lstTodos.Items.Clear();
            foreach (string nombre in listaNombres)
            {
                lstTodos.Items.Add(nombre);
            }

            // Mostrar el total en el cuadro de texto inferior izquierdo
            txtConteoTodos.Text = lstTodos.Items.Count.ToString();
        }

        // 2. Evento para el botón <<Pasar>> (Filtrar)
        private void BtnPasar_Click(object sender, RoutedEventArgs e)
        {
            // Validar que primero se haya listado el array de nombres
            if (listaNombres == null || listaNombres.Length == 0)
            {
                MessageBox.Show("Primero debes listar los nombres.", "Validación", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string filtro = txtLetra.Text;
            if (string.IsNullOrWhiteSpace(filtro))
            {
                MessageBox.Show("Ingrese la letra a filtrar", "Validación!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            lstFiltrados.Items.Clear();
            foreach (string nombre in listaNombres)
            {
                if (nombre.StartsWith(filtro, StringComparison.OrdinalIgnoreCase))
                {
                    lstFiltrados.Items.Add(nombre);
                }
            }

            // Mostrar el total filtrado en el cuadro de texto inferior derecho
            txtConteoFiltrados.Text = lstFiltrados.Items.Count.ToString();
        }

        // 3. Evento por si cambias el texto en la caja principal (opcional, evita errores si se borra en el XAML)
        private void txtCadena_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Puedes dejarlo vacío o limpiar las listas si el usuario modifica el texto original
        }
    }
}