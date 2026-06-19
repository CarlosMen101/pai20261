using System.Windows;
using System.Windows.Controls;

namespace Estructuras_de_repeticion
{
    public partial class VentanaComboBox : Window
    {
        public VentanaComboBox()
        {
            InitializeComponent();
        }

        private void btnMarcar_Click(object sender, RoutedEventArgs e)
        {
            if (cmbFrutas.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una fruta.");
                return;
            }

            ComboBoxItem seleccionado =
                (ComboBoxItem)cmbFrutas.SelectedItem;

            string valorSeleccionado =
                seleccionado.Content.ToString();

            MessageBox.Show(
                $"Fruta seleccionada: {valorSeleccionado}"
            );
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxFruta.Text))
            {
                MessageBox.Show("Ingrese una fruta.");
                return;
            }

            ComboBoxItem nuevoItem = new ComboBoxItem();
            nuevoItem.Content = tbxFruta.Text;

            cmbFrutas.Items.Add(nuevoItem);

            tbxFruta.Clear();

            MessageBox.Show("Fruta agregada correctamente.");
        }
    }
}