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

namespace AdivinaEdad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int edadMinima;
        private int edadMaxima;
        private int edadAdPropuesta;
        private int contadorIntentos;

        private Random random = new Random();   
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnPrimerIntento_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(tbxLimiteInf.Text, out edadMinima))
            {
                MessageBox.Show("Ingrese una edad minima valida ", "Validacion!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!int.TryParse(tbxLimiteSup.Text, out edadMaxima))
            {
                MessageBox.Show("Ingrese una edad maxima valida ", "Validacion!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (edadMinima >= edadMaxima) 
            {
                MessageBox.Show("Ingrese un rando de edades validas, edad maxima mayor que edad minima");
                return;
            }

            edadAdPropuesta =random.Next(edadMinima, edadMaxima+1);

            contadorIntentos++;

            tbxResultado.Text =edadAdPropuesta.ToString();
        }

        private void btnIncorrecto_Click(object sender, RoutedEventArgs e)
        {
            if (contadorIntentos == 0) { 
                MessageBox.Show("Debe realizar el primer intento antes de continuar", "Validacion!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            edadAdPropuesta=random.Next(edadMinima, edadMaxima + 1);
            contadorIntentos++;
            tbxResultado.Text = edadAdPropuesta.ToString();

        }

        private void BtnCorrecto_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Felicidades, adivinaste la edad en {contadorIntentos} intentos", "Resultado", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}