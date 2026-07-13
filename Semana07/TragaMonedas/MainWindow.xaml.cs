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
using System.Windows.Threading;

namespace TragaMonedas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timerReloj;
        private DispatcherTimer timerJuego;
        private Random random = new Random();
        private int contadorTicks ;

        private const int TIEMPO_TOTAL_TICKS = 60; // 10 segundos 
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timerJuego.Start();
            contadorTicks = 0;
            lblResultado.Visibility = Visibility.Hidden;
            btnInicio.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timerReloj = new DispatcherTimer();
            timerReloj.Interval = TimeSpan.FromMilliseconds(1);
            timerReloj.Tick += TimerReloj_Tick;
            timerReloj.Start();

            timerJuego = new DispatcherTimer();
            timerJuego.Interval = TimeSpan.FromMilliseconds(1);
            timerJuego.Tick += timerJuego_Tick;
        }
        private void timerJuego_Tick(object? sender, EventArgs e)
        { 
            int n1 =random.Next(10,30);
            int n2 = random.Next(10, 30);
            int n3 = random.Next(10, 30);

            tbxJugada1.Text = n1.ToString();
            tbxJugada2.Text = n2.ToString();
            tbxJugada3.Text = n3.ToString();

            contadorTicks++;

            if (contadorTicks>= TIEMPO_TOTAL_TICKS) 
            {
                timerJuego.Stop();
                Validar_Jugada(n1, n2, n3);
            }

            
        }
        
        private void TimerReloj_Tick(object? sender, EventArgs e)
        {
            lblReloj.Content = DateTime.Now.ToString("HH:mm:ss");
        }
        private void Validar_Jugada(int n1, int n2, int n3)
        {
            if (n1 == n2 && n2 == n3)
            {
                MessageBox.Show("Ganaste el premio mayor");
            }
            else if (n1 == n2 && n2 == n3)
            {
                lblResultado.Content = ("Ganaste un premio menor");
                lblResultado.Background = Brushes.Green;
            }
            else
            {
                lblResultado.Content = ("Perdiste");
                lblResultado.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9AE7FC"));
            }
        
            lblResultado.Visibility = Visibility.Visible;
            btnInicio.IsEnabled = true;
        }
    }
}