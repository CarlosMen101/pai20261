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

        private void btnApliRadio_Click(object sender, RoutedEventArgs e)
        {
            lbCheckB_Radio.FontFamily = new FontFamily("Segoe UI");
            lbCheckB_Radio.Foreground = Brushes.Black;
            lbCheckB_Radio.Background = Brushes.Transparent;

            if (rbTipoLetra.IsChecked == true)
            {
                lbCheckB_Radio.FontFamily = new FontFamily("Calibri");
            }
            else if (rbColorTexto.IsChecked == true)
            {

                lbCheckB_Radio.Foreground = Brushes.Brown;
            }
            else    if (rbColorFondo.IsChecked == true)
            {

                lbCheckB_Radio.Background = Brushes.Aqua;
            }
        }
    }
}