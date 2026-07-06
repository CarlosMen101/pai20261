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

namespace Practica_pal_examen
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

        
        private void btnSuma_Click(object sender, RoutedEventArgs e)
        {
            string numero13 = tbxNumero1.Text;
            Int32 num1 = Int32.Parse(numero13);
            string numero23 = tbxNumero2.Text;
            Int32 num2 = Int32.Parse(numero23);

            int suma = num1+ num2;

            tbxResultado.Text = suma.ToString();
        }

        private void btnResta_Click(object sender, RoutedEventArgs e)
        {
            string numero13 = tbxNumero1.Text;
            Int32 num1 = Int32.Parse(numero13);
            string numero23 = tbxNumero2.Text;
            Int32 num2 = Int32.Parse(numero23);

            int resta = num1 - num2;

            tbxResultado.Text = resta.ToString();
        }

        private void btnMultiplicacion_Click(object sender, RoutedEventArgs e)
        {
            
            Int32 num1 = Int32.Parse(tbxNumero1.Text);
            
            Int32 num2 = Int32.Parse(tbxNumero2.Text);

            int multiplicacion = num1 * num2;  

            tbxResultado.Text = multiplicacion.ToString();
        }

        private void btnDivision_Click(object sender, RoutedEventArgs e)
        {
            Double num1 = Double.Parse(tbxNumero1.Text);

            Double num2 = Double.Parse(tbxNumero2.Text);

            if (num2 == 0) {
                MessageBox.Show("No se puede dividir por cero");
            }
            else{
                Double division = num1 / num2;

                tbxResultado.Text = division.ToString();
            }

            
        }
    }
}