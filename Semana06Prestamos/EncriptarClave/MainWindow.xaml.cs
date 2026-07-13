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

namespace EncriptarClave
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

        private void btnEncriptar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbxClave.Text))
            {
                MessageBox.Show("Por favor, ingrese una clave para encriptar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string claveEncriptada = EncriptarClave(tbxClave.Text);

            tbxResultado.Text = claveEncriptada;
        }
        private string EncriptarClave(string texto)
        {
            StringBuilder resultado = new StringBuilder();

            foreach (char caracter in texto)
            {
                // Encriptar letras mayúsculas
                if (char.IsUpper(caracter))
                {
                    char c = (char)(((caracter + 3 - 'A') % 26) + 'A');
                    resultado.Append(c);
                }
                // Encriptar letras minúsculas
                else if (char.IsLower(caracter))
                {
                    char c = (char)(((caracter + 3 - 'a') % 26) + 'a');
                    resultado.Append(c);
                }
                // Si es un número, espacio o signo, se queda igual
                else
                {
                    resultado.Append(caracter);
                }
            }

            return resultado.ToString();
        }

    }

}