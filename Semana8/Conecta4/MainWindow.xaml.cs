using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Conecta4App
{
    public partial class MainWindow : Window
    {
        // Variables de control
        private JuegoConecta4 _juego;
        private Ellipse[,] _fichasVisuales;
        private Border[] _columnasHover;
        private bool _bloquearInteraccion = false;

        public MainWindow()
        {
            InitializeComponent();
            _juego = new JuegoConecta4();
            _fichasVisuales = new Ellipse[Tablero.TOTAL_FILAS, Tablero.TOTAL_COLUMNAS];
            _columnasHover = new Border[Tablero.TOTAL_COLUMNAS];

            ConstruirEstructuraTablero();

            // Ejecutar la ventana modal al cargar la aplicación
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MostrarModalSeleccionModo();
        }

        // Muestra la ventana modal inicial para definir el modo
        private void MostrarModalSeleccionModo()
        {
            ModoJuegoWindow modal = new ModoJuegoWindow { Owner = this };
            if (modal.ShowDialog() == true)
            {
                IniciarNuevaPartida(modal.ContraPC, modal.IniciaHumanoPrimero);
            }
        }

        // Construye las celdas circulares y los detectores de columnas
        private void ConstruirEstructuraTablero()
        {
            gridTableroVisual.Children.Clear();
            gridColumnasInteractivas.Children.Clear();

            // 1. Matriz de círculos
            for (int fila = 0; fila < Tablero.TOTAL_FILAS; fila++)
            {
                for (int col = 0; col < Tablero.TOTAL_COLUMNAS; col++)
                {
                    Ellipse ficha = new Ellipse
                    {
                        Fill = new SolidColorBrush(Color.FromRgb(236, 240, 241)),
                        Margin = new Thickness(8)
                    };

                    Grid.SetRow(ficha, fila);
                    Grid.SetColumn(ficha, col);

                    _fichasVisuales[fila, col] = ficha;
                    gridTableroVisual.Children.Add(ficha);
                }
            }

            // 2. Columnas para eventos Hover y Clic
            for (int col = 0; col < Tablero.TOTAL_COLUMNAS; col++)
            {
                Border columnaBorder = new Border
                {
                    Background = Brushes.Transparent,
                    Tag = col,
                    Cursor = Cursors.Hand
                };

                // Eventos de selección de columna
                columnaBorder.MouseEnter += Columna_MouseEnter;
                columnaBorder.MouseLeave += Columna_MouseLeave;
                columnaBorder.MouseLeftButtonDown += Columna_MouseLeftButtonDown;

                _columnasHover[col] = columnaBorder;
                gridColumnasInteractivas.Children.Add(columnaBorder);
            }
        }

        private void IniciarNuevaPartida(bool contraPC, bool iniciaHumano)
        {
            _juego.IniciarJuego(contraPC, iniciaHumano);
            LimpiarTableroVisual();
            _bloquearInteraccion = false;
            ActualizarTurnoGUI();

            // Si le toca a la Computadora en el primer turno
            if (_juego.JugadorActual.Tipo == TipoJugador.Computadora)
            {
                ProcesarTurnoComputadora();
            }
        }

        private void LimpiarTableroVisual()
        {
            for (int f = 0; f < Tablero.TOTAL_FILAS; f++)
            {
                for (int c = 0; c < Tablero.TOTAL_COLUMNAS; c++)
                {
                    _fichasVisuales[f, c].Fill = new SolidColorBrush(Color.FromRgb(236, 240, 241));
                }
            }
        }

        // --- EFECTOS VISUALES DE HOVER DE COLUMNA ---
        private void Columna_MouseEnter(object sender, MouseEventArgs e)
        {
            if (_bloquearInteraccion || _juego.JuegoTerminado) return;
            Border col = (Border)sender;
            col.Background = new SolidColorBrush(Color.FromArgb(40, 255, 255, 255)); // Resaltado blanco traslúcido
        }

        private void Columna_MouseLeave(object sender, MouseEventArgs e)
        {
            Border col = (Border)sender;
            col.Background = Brushes.Transparent;
        }

        // --- EVENTO CLIC EN COLUMNA ---
        private async void Columna_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_bloquearInteraccion || _juego.JuegoTerminado) return;

            Border col = (Border)sender;
            int columnaIdx = (int)col.Tag;

            bool valido = await EjecutarMovimientoConAnimacion(columnaIdx);

            if (valido && !_juego.JuegoTerminado && _juego.JugadorActual.Tipo == TipoJugador.Computadora)
            {
                await ProcesarTurnoComputadora();
            }
        }

        // Soltar la ficha con animación de caída por gravedad
        private async Task<bool> EjecutarMovimientoConAnimacion(int columna)
        {
            int filaCaida = _juego.TableroJuego.ColocarFicha(columna, _juego.JugadorActual.ValorMatriz);

            if (filaCaida == -1) return false; // Columna llena

            _bloquearInteraccion = true;

            // Ficha visual a animar
            Ellipse ficha = _fichasVisuales[filaCaida, columna];
            ficha.Fill = _juego.JugadorActual.ObtenerBrushColor();

            // Animación de Caída por Gravedad (DoubleAnimation)
            double distanciaDistanciaY = (filaCaida + 1) * 100; // Cálculo aproximado de la distancia
            TranslateTransform trans = new TranslateTransform();
            ficha.RenderTransform = trans;

            DoubleAnimation animacionCaida = new DoubleAnimation
            {
                From = -distanciaDistanciaY,
                To = 0,
                Duration = TimeSpan.FromMilliseconds(500),
                EasingFunction = new BounceEase
                {
                    Bounces = 2,
                    Bounciness = 3,
                    EasingMode = EasingMode.EaseOut
                }
            };

            trans.BeginAnimation(TranslateTransform.YProperty, animacionCaida);
            await Task.Delay(520); // Esperar a que la animación termine

            // Evaluar Estado de Victoria o Empate
            if (_juego.TableroJuego.VerificarVictoria(_juego.JugadorActual.ValorMatriz))
            {
                _juego.FinalizarJuego();
                MostrarResultadoFinal($"🏆 ¡Felicidades, {_juego.JugadorActual.Nombre} ha ganado!");
                return true;
            }

            if (_juego.TableroJuego.EstaLleno())
            {
                _juego.FinalizarJuego();
                MostrarResultadoFinal("🤝 ¡Empate! El tablero está lleno.");
                return true;
            }

            _juego.CambiarTurno();
            ActualizarTurnoGUI();
            _bloquearInteraccion = false;
            return true;
        }

        private async Task ProcesarTurnoComputadora()
        {
            _bloquearInteraccion = true;
            lblTurnoActual.Text = "Turno de: Computadora (Pensando...)";

            await Task.Delay(800);

            int columnaPC = _juego.ObtenerColumnaAleatoriaPC();
            if (columnaPC != -1)
            {
                await EjecutarMovimientoConAnimacion(columnaPC);
            }
        }

        private void ActualizarTurnoGUI()
        {
            lblTurnoActual.Text = $"Turno de: {_juego.JugadorActual.Nombre}";
            indicadorColorTurno.Fill = _juego.JugadorActual.ObtenerBrushColor();
        }

        // Muestra la ventana modal final con las opciones de reiniciar o salir
        private void MostrarResultadoFinal(string mensaje)
        {
            ResultadoWindow resWindow = new ResultadoWindow(mensaje) { Owner = this };
            if (resWindow.ShowDialog() == true)
            {
                // Jugar otra vez
                MostrarModalSeleccionModo();
            }
            else
            {
                // Cerrar aplicación
                this.Close();
            }
        }

        private void btnCambiarModo_Click(object sender, RoutedEventArgs e)
        {
            MostrarModalSeleccionModo();
        }
    }
}