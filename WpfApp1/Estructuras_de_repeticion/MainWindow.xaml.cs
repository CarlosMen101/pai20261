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

namespace Estructuras_de_repeticion
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

        private void btnCombo_Click(object sender, RoutedEventArgs e)
        {
            Estructuras_de_repeticion.ComboBox ventana = new Estructuras_de_repeticion.ComboBox();
            ventana.Show();
        }

        private void btnListBox_Click(object sender, RoutedEventArgs e)
        {
            Estructuras_de_repeticion.ListBox ventana2 = new Estructuras_de_repeticion.ListBox();
            ventana2.Show();
        }

        private void ListView_Click(object sender, RoutedEventArgs e)
        {
            Estructuras_de_repeticion.ListView ventana3 = new Estructuras_de_repeticion.ListView();
            ventana3.Show();
        }
    }
}