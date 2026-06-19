using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Registro_de_clientes
{
    public partial class MainWindow : Window
    {
        // Colección que se conecta al ListView utilizando tu clase Cliente
        private ObservableCollection<Cliente> listaClientes;

        public MainWindow()
        {
            InitializeComponent();
            listaClientes = new ObservableCollection<Cliente>();
            lvRegistro.ItemsSource = listaClientes;
        }

        // Botón Grabar
        private void btnGrabar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDNI.Text) ||
                cbEstadoCivil.SelectedItem == null)
            {
                MessageBox.Show("Por favor, complete todos los campos requeridos.", "Campos vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string estadoCivil = ((ComboBoxItem)cbEstadoCivil.SelectedItem).Content.ToString();

            // Instanciamos tu clase Cliente asignando las propiedades correspondientes
            Cliente nuevoCliente = new Cliente
            {
                Apellidos = txtApellido.Text,
                Nombre = txtNombre.Text, // <--- Singular de acuerdo a tu clase
                DNI = txtDNI.Text,
                Direccion = txtDireccion.Text,
                EstadoCivil = estadoCivil
            };

            listaClientes.Add(nuevoCliente);
            LimpiarCampos();
        }

        // Botón Nuevo
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        // Botón Estadística
        private void btnEstadistica_Click(object sender, RoutedEventArgs e)
        {
            int solteros = 0;
            int casados = 0;

            foreach (var cliente in listaClientes)
            {
                if (cliente.EstadoCivil.Contains("Soltero"))
                    solteros++;
                else if (cliente.EstadoCivil.Contains("Casado"))
                    casados++;
            }

            txtContadorSolteros.Text = solteros.ToString();
            txtContadorCasados.Text = casados.ToString();
        }

        // Botón Salir
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LimpiarCampos()
        {
            txtApellido.Clear();
            txtNombre.Clear();
            txtDNI.Clear();
            txtDireccion.Clear();
            cbEstadoCivil.SelectedIndex = -1;
            txtApellido.Focus();
        }
    }
}