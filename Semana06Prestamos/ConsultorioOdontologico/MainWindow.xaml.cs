using System.Globalization;
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

namespace ConsultorioOdontologico
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbx_Cliente.Text))
            {
                MessageBox.Show("Por favor, ingrese el nombre del cliente.", "Cliente invalido", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string cliente = tbx_Cliente.Text;
            if (cbx_Tratamiento.SelectedIndex==-1) {

                MessageBox.Show("Por favor, ingrese un tratamiento", "Valor invalido", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string tratamiento = ((ComboBoxItem)cbx_Tratamiento.SelectedItem).Content.ToString();
            if (cbx_Diente.SelectedIndex == -1)
            {

                MessageBox.Show("Por favor, ingrese un tipo de diente", "Valor invalido", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string piezaDental = ((ComboBoxItem)cbx_Diente.SelectedItem).Content.ToString();

            if (dp_Fecha.SelectedDate == null)
            {
                MessageBox.Show("Por favor, seleccione una fecha en el calendario.", "Fecha requerida", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DateTime fechaIngreso = dp_Fecha.SelectedDate.Value;
            DateTime fechaNueva = fechaIngreso.AddDays(15);
            string fechaFormateada = fechaNueva.ToString("dd/MM/yyyy");

            tbx_Resultado.Text = $"Cita agendada para {cliente}: {tratamiento} en {piezaDental} el día {fechaFormateada}.";
        }

        private void btn_Nuevo_Click(object sender, RoutedEventArgs e)
        {
            tbx_Cliente.Clear();
            tbx_Resultado.Clear();
            cbx_Tratamiento.SelectedIndex = -1;
            cbx_Diente.SelectedIndex = -1;
            dp_Fecha.SelectedDate = null;
        }
    }
}