using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace TragamonedasV2
{
    public partial class MainWindow : Window
    {
        // 1. Temporizador para el movimiento rápido de los rodillos
        private DispatcherTimer timerGiro;
        private int tiempoGiroTranscurrido = 0; // Milisegundos transcurridos en el giro actual

        // 2. Temporizador para la cuenta regresiva del juego (8 segundos)
        private DispatcherTimer timerJuego;
        private int tiempoRestante = 8; // Segundos de juego

        // Generador de números aleatorios para el resultado final
        private Random random = new Random();

        // Lista de imágenes precargadas
        private List<BitmapImage> imagenes = new List<BitmapImage>();

        // Índices actuales de cada rodillo para que avancen de forma "ordenada" (secuencial)
        private int indiceRodillo1 = 0;
        private int indiceRodillo2 = 1;
        private int indiceRodillo3 = 2;

        // Variables de juego
        private int puntajeActual = 0;
        private bool juegoActivo = false;

        public MainWindow()
        {
            InitializeComponent();
            CargarImagenes();
            ConfigurarTemporizadores();

            // Mostrar tiempo inicial en la interfaz
            txtTemporizador.Text = "00:08";
        }

        private void CargarImagenes()
        {
            try
            {
                for (int i = 1; i <= 6; i++)
                {
                    Uri uri = new Uri($"pack://application:,,,/Imagenes/{i}.png", UriKind.Absolute);
                    imagenes.Add(new BitmapImage(uri));
                }

                if (imagenes.Count > 0)
                {
                    imgTraga1.Source = imagenes[0];
                    imgTraga2.Source = imagenes[1];
                    imgTraga3.Source = imagenes[2];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar imágenes: {ex.Message}");
            }
        }

        private void ConfigurarTemporizadores()
        {
            // Temporizador de giro (mueve las imágenes rápido)
            timerGiro = new DispatcherTimer();
            timerGiro.Interval = TimeSpan.FromMilliseconds(60); // Ajusta este valor si quieres que vayan más rápido/lento
            timerGiro.Tick += TimerGiro_Tick;

            // Temporizador general del juego (cuenta regresiva de 1 en 1 segundo)
            timerJuego = new DispatcherTimer();
            timerJuego.Interval = TimeSpan.FromSeconds(1);
            timerJuego.Tick += TimerJuego_Tick;
        }

        /// <summary>
        /// Hace avanzar los rodillos de forma ordenada (secuencial: 1, 2, 3, 4, 5, 6, 1...)
        /// </summary>
        private void TimerGiro_Tick(object sender, EventArgs e)
        {
            // Hacemos que cada rodillo avance secuencialmente al siguiente índice
            indiceRodillo1 = (indiceRodillo1 + 1) % 6;
            indiceRodillo2 = (indiceRodillo2 + 1) % 6;
            indiceRodillo3 = (indiceRodillo3 + 1) % 6;

            // Mostramos la imagen correspondiente a su posición actual
            imgTraga1.Source = imagenes[indiceRodillo1];
            imgTraga2.Source = imagenes[indiceRodillo2];
            imgTraga3.Source = imagenes[indiceRodillo3];

            tiempoGiroTranscurrido += 60;

            // Al cumplirse el segundo de giro, forzamos un resultado aleatorio final
            if (tiempoGiroTranscurrido >= 1000)
            {
                timerGiro.Stop();

                // Detenemos los rodillos en una posición aleatoria definitiva
                indiceRodillo1 = random.Next(0, 6);
                indiceRodillo2 = random.Next(0, 6);
                indiceRodillo3 = random.Next(0, 6);

                imgTraga1.Source = imagenes[indiceRodillo1];
                imgTraga2.Source = imagenes[indiceRodillo2];
                imgTraga3.Source = imagenes[indiceRodillo3];

                CalcularPuntaje();

                // Solo reactivamos el botón si el juego sigue en marcha (le queda tiempo)
                if (juegoActivo && tiempoRestante > 0)
                {
                    btnIniciar.IsEnabled = true;
                }
            }
        }

        /// <summary>
        /// Controla la cuenta regresiva de los 8 segundos de juego.
        /// </summary>
        private void TimerJuego_Tick(object sender, EventArgs e)
        {
            tiempoRestante--;
            txtTemporizador.Text = $"00:{tiempoRestante:D2}";

            if (tiempoRestante <= 0)
            {
                FinalizarJuego();
            }
        }

        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            // Si es la primera jugada de la partida, iniciamos el cronómetro de 8 segundos
            if (!juegoActivo)
            {
                juegoActivo = true;
                tiempoRestante = 8;
                puntajeActual = 0;
                txtPuntaje.Text = "0000";
                txtTemporizador.Text = "00:08";
                timerJuego.Start();
            }

            // Bloqueamos el botón durante el giro de 1 segundo
            btnIniciar.IsEnabled = false;
            tiempoGiroTranscurrido = 0;

            // Arrancamos el efecto visual de giro ordenado
            timerGiro.Start();
        }

        private void CalcularPuntaje()
        {
            // Evaluamos según los índices finales donde se detuvieron
            if (indiceRodillo1 == indiceRodillo2 && indiceRodillo2 == indiceRodillo3)
            {
                puntajeActual += 30;
            }
            else if (indiceRodillo1 == indiceRodillo2 || indiceRodillo1 == indiceRodillo3 || indiceRodillo2 == indiceRodillo3)
            {
                puntajeActual += 10;
            }

            txtPuntaje.Text = puntajeActual.ToString("D4");
        }

        /// <summary>
        /// Evalúa si ganaste o perdiste al agotarse el tiempo.
        /// </summary>
        private void FinalizarJuego()
        {
            timerJuego.Stop();
            timerGiro.Stop(); // Por si acaso se quedó girando
            juegoActivo = false;
            btnIniciar.IsEnabled = true; // Permitir iniciar una nueva partida entera

            if (puntajeActual >= 80)
            {
                MessageBox.Show($"¡Felicidades, GANASTE!\nAlcanzaste un puntaje de: {puntajeActual} puntos.", "¡Victoria!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"¡PERDISTE!\nTu puntaje fue de {puntajeActual} puntos. Se necesitan al menos 80 puntos para ganar.", "Fin del Juego", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}