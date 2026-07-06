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

namespace Semana06Prestamos
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

        private void btn_Calcular_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbx_Cliente.Text)) {
                MessageBox.Show("Por favor, ingrese el nombre del cliente.", "Cliente invalido", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string cliente = tbx_Cliente.Text;

            if (!double.TryParse(tbx_MontoPago.Text, out double montoPagar)) {
                MessageBox.Show("Por favor, ingrese el monto.", "Monto invalido", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            DateTime fechaVencimiento = dp_FechaVencimiento.SelectedDate.Value;
            DateTime fechaPago = dp_FechaPago.SelectedDate.Value;

            int diasDemora = 0;

            if (fechaPago > fechaVencimiento) {
                TimeSpan diferencia = fechaPago.Subtract(fechaVencimiento);
                diasDemora = (int)diferencia.TotalDays;
            }
            double demoraPorcentaje = diasDemora * 0.5;

            double demoraSoles = montoPagar * demoraPorcentaje / 100;

            double totalPagar = montoPagar + demoraSoles;

            tbx_DiasDemora.Text = diasDemora.ToString();
            tbx_DemoraPorcentual.Text = demoraPorcentaje.ToString("F2");
            tbx_SolesDemora.Text = demoraSoles.ToString("F2");
            tbx_MontoaPagar.Text = totalPagar.ToString("F2");

        }

        private void btn_Nuevo_Click(object sender, RoutedEventArgs e)
        {
            tbx_Cliente.Clear();
            tbx_DemoraPorcentual.Clear();
            tbx_DiasDemora.Clear();
            tbx_MontoaPagar.Clear();
            tbx_MontoPago.Clear();
            tbx_SolesDemora.Clear();
            dp_FechaPago.SelectedDate = null;
            dp_FechaVencimiento.SelectedDate = null; 

        }

        private void btn_Finalizar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}