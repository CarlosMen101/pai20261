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

namespace radiobuttonycheckbox
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

        private void btnApliCheck_Click(object sender, RoutedEventArgs e)
        {
            if (cbxCambiarTipo.IsChecked == true)
            {
                tbxCheckbox.FontFamily = new FontFamily("Arial");
            }
            else { 
                tbxCheckbox.FontFamily = new FontFamily("Segoe UI");
            }
            if (cbxColorLetra.IsChecked == true)
            {
                tbxCheckbox.Foreground = Brushes.Red;
            }
            else { 
                tbxCheckbox.Foreground = Brushes.Black;
            }
            if (cbxColorfondo.IsChecked == true)
            {
                tbxCheckbox.Background = Brushes.Yellow;
            }
            else { 
                tbxCheckbox.Background = Brushes.Transparent;
            }
    }

        private void btnApliRadio_Click(object sender, RoutedEventArgs e)
        {
            if (rbCambiarTipo.IsChecked == true)
            {

                tbxRadio.FontFamily = new FontFamily("Arial");
            }
            else
            {
                tbxRadio.FontFamily = new FontFamily("Segoe UI");
            }
            if (rbColorLetra.IsChecked == true)
            {
                tbxRadio.Foreground = Brushes.Red;
            }
            else
            {
                tbxRadio.Foreground = Brushes.Black;
            }
            if (rbColorfondo.IsChecked == true)
            {
                tbxRadio.Background = Brushes.Yellow;
            }
            else {
                tbxRadio.Background = Brushes.Transparent;
            }

        }
    }
}