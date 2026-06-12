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

namespace WpfApp1
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

        private void btnAplicar_Click(object sender, RoutedEventArgs e)
        {
            lbCheckBox.FontFamily = new FontFamily("Segoe UI");
            lbCheckBox.Foreground = Brushes.Black;
            lbCheckBox.Background = Brushes.Transparent;

            if (cbTipoLetra.IsChecked == true) {
                lbCheckBox.FontFamily = new FontFamily("Calibri");
            }
            if (cbColorTexto.IsChecked == true) {

                lbCheckBox.Foreground = Brushes.Red;
            }
            if (cbColorFondo.IsChecked == true) { 
            
                lbCheckBox.Background = Brushes.YellowGreen;
            }
        }
    }
}