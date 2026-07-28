using System;
using System.Threading.Tasks;
using System.Windows;

namespace Conecta4App
{
    public partial class ModoJuegoWindow : Window
    {
        // Documentación de variables públicas de resultado
        public bool ContraPC { get; private set; }
        public bool IniciaHumanoPrimero { get; private set; }

        private Random _random = new Random();

        public ModoJuegoWindow()
        {
            InitializeComponent();
        }

        private async void btnHumanoVsHumano_Click(object sender, RoutedEventArgs e)
        {
            ContraPC = false;
            await SimularSorteoInicio();
        }

        private async void btnHumanoVsPC_Click(object sender, RoutedEventArgs e)
        {
            ContraPC = true;
            await SimularSorteoInicio();
        }

        // Simula la selección aleatoria de quién inicia la partida
        private async Task SimularSorteoInicio()
        {
            panelBotonesModo.Visibility = Visibility.Collapsed;
            panelSorteo.Visibility = Visibility.Visible;

            // Ruleta visual de selección
            string[] opciones = { "Jugador 1 (Rojo)", ContraPC ? "Computadora (Azul)" : "Jugador 2 (Azul)" };

            for (int i = 0; i < 10; i++)
            {
                lblResultadoSorteo.Text = opciones[i % 2];
                await Task.Delay(120);
            }

            // Elección final aleatoria
            int ganadorSorteo = _random.Next(0, 2);
            IniciaHumanoPrimero = (ganadorSorteo == 0);
            lblResultadoSorteo.Text = $"¡Empieza: {(IniciaHumanoPrimero ? opciones[0] : opciones[1])}!";

            await Task.Delay(1200);

            this.DialogResult = true;
            this.Close();
        }
    }
}