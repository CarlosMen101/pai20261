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

namespace Ejercicio_Propuesto
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

        private void btnCalcular_Click(object sender, RoutedEventArgs e)
        {
            double monto = 0 ;
            if (double.TryParse(tbxIngresos.Text, out monto) != true) {

                MessageBox.Show("Ingrese un numero");
            }
            ;
            double fonaviMonto = ((monto / 100) * 8);
            double importeMonto = ((monto / 100) * 5);
            double AFPMonto = ((monto / 100) * 12);
            
            if (cbFonavi.IsChecked == true) {

                monto = monto - fonaviMonto;
                lbFonavi.Content = ("Fonavi: "+ fonaviMonto );

                lbTotal.Content = ("Total a pagar: "+monto); 
            }
            if (cbImporte1.IsChecked == true)
            {
                monto = monto - importeMonto;
                lbImporte.Content = ("Importe: " + importeMonto);
                lbTotal.Content = ("Total a pagar: " + monto);
            }
            if (cbAFP.IsChecked == true)
            {
                monto = monto - AFPMonto;
                lbAFP.Content = ("AFP: " + AFPMonto);
                lbTotal.Content = ("Total a pagar: " + monto);
            }
            if (cbAFP.IsChecked != true && cbImporte1.IsChecked != true && cbFonavi.IsChecked != true) {

                lbTotal.Content = ("Total a pagar: " + monto);
            }
        }
    }
}