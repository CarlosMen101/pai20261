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

namespace ComboBox_ListBox
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

        private void cbxDivisas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)cbxDivisas.SelectedItem;
            if (item.Content.ToString() == "Dolar")
            { 
                Double valor = Convert.ToDouble(tbxSoles.Text);

                tbxResultado.Text = (valor / 3.75).ToString();
            } else if (item.Content.ToString() == "Euro") {

                Double valor = Convert.ToDouble(tbxSoles.Text);
                tbxResultado.Text = (valor / 4).ToString();
            }
            else if (item.Content.ToString() == "peso chileno")
            {

                Double valor = Convert.ToDouble(tbxSoles.Text);
                tbxResultado.Text = (valor * 250).ToString();
            }
        }
    }
}