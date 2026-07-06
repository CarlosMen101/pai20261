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

namespace _4Notas
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
            Double nota1 = Convert.ToDouble(tbxNota1.Text);
            Double nota2 = Convert.ToDouble(tbxNota2.Text);
            Double nota3 = Convert.ToDouble(tbxNota3.Text);
            Double nota4 = Convert.ToDouble(tbxNota4.Text);

            Double [] notas = { nota1, nota2, nota3, nota4 };

            Double notaMenor = 0;

            for (int i = 0; i < notas.Length-1; i++)
            {
                if (notas[i] > notas[1 + i])
                {
                    notaMenor = notas[1 + i];
                }
                else { 
                    notaMenor=notas[i];
                }
            }
            Double promedio= (nota1 + nota2 + nota3 + nota4 - notaMenor) / 3;

            if (promedio>=10.5)
            {
                lblResultado.Content = "Aprobado";
                lblResultado.Foreground = Brushes.Green;
            }
            else { 
                lblResultado.Content = "Reprobado";
                lblResultado.Foreground = Brushes.Red;
            }
            tbxResultado.Text = promedio.ToString();



        }

    }
}