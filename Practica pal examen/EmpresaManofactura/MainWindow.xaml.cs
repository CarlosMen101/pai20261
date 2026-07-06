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

namespace EmpresaManofactura
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
            Double sueldoBase = 1025.00;

            Double horasExtra = Double.Parse(tbxHorasExt.Text);
            Double tarifaHoraExtra = Double.Parse(tbxTarifa.Text);
            Int16 inacistencias = Int16.Parse(tbxInacistencias.Text);


            ///calculos
          
            //descuentos
            Double Descuento_ =(sueldoBase/30)* inacistencias;
            //calculo de horas extra
            Double PagoHorasExtra = horasExtra * tarifaHoraExtra;
            //sueldo neto
            Double sueldoNeto1 = sueldoBase + PagoHorasExtra - Descuento_;
            
            //Bonificaciones
            Double sueldoNetoFinal = 0;
            if (sueldoNeto1 >= 2000)
            {
                sueldoNetoFinal = sueldoNeto1 + (sueldoNeto1 * 0.10);
            }
            else { 
                sueldoNetoFinal = sueldoNeto1+(sueldoNeto1*0.05);
            }
            
            

            //Mostrando en pantalla
            //sueldo base
            tbxSueldoBase.Text = sueldoBase.ToString();
            //monto de horas extra
            tbxMontoHoras.Text = PagoHorasExtra.ToString();
            //bonificaciones
            tbxBonificacion.Text = (sueldoNetoFinal - sueldoNeto1).ToString();
            //descuentos
            tbxDescuento.Text = Descuento_.ToString();
            //sueldo neto
            tbxResultado.Text = sueldoNetoFinal.ToString();
        }
    }
}