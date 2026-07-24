using System.Windows;
using System.Windows.Controls;

namespace Votos
{
    public partial class MainWindow : Window
    {
        // Arrays con los nombres de los partidos y zonas para la evaluación
        private readonly string[] partidos = { "Buhito", "Aguila", "Torito", "Lorito" };
        private readonly string[] zonas = { "A", "B", "C", "D" };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Asigna el evento Click al botón
            btnCalcular.Click += BtnCalcular_Click;
        }

        private void BtnCalcular_Click(object sender, RoutedEventArgs e)
        {
            // Matriz de 4x4 para almacenar los controles TextBox
            TextBox[,] txtMatriz = new TextBox[4, 4]
            {
                { txt00, txt01, txt02, txt03 },
                { txt10, txt11, txt12, txt13 },
                { txt20, txt21, txt22, txt23 },
                { txt30, txt31, txt32, txt33 }
            };

            // Matriz numérica para guardar los votos procesados
            int[,] votos = new int[4, 4];

            // 1. Lectura y validación de la matriz de datos
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (int.TryParse(txtMatriz[i, j].Text.Trim(), out int valor) && valor >= 0)
                    {
                        votos[i, j] = valor;
                    }
                    else
                    {
                        // Si la celda está vacía o no es un entero válido, asumimos 0
                        votos[i, j] = 0;
                    }
                }
            }

            // Arreglos para almacenar los totales por Partido (filas) y Zona (columnas)
            int[] totalPartidos = new int[4];
            int[] totalZonas = new int[4];
            int totalGeneral = 0;

            // 2. Cálculo de sumas por Partido, por Zona y Total General
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    totalPartidos[i] += votos[i, j];
                    totalZonas[j] += votos[i, j];
                    totalGeneral += votos[i, j];
                }
            }

            // 3. Asignación de sumas a la interfaz
            tbTotalP0.Text = totalPartidos[0].ToString();
            tbTotalP1.Text = totalPartidos[1].ToString();
            tbTotalP2.Text = totalPartidos[2].ToString();
            tbTotalP3.Text = totalPartidos[3].ToString();

            tbTotalZ0.Text = totalZonas[0].ToString();
            tbTotalZ1.Text = totalZonas[1].ToString();
            tbTotalZ2.Text = totalZonas[2].ToString();
            tbTotalZ3.Text = totalZonas[3].ToString();

            tbTotalVotantes.Text = totalGeneral.ToString();

            // 4. Determinación del Candidato Ganador
            int maxVotosPartido = totalPartidos[0];
            int idxPartidoGanador = 0;

            for (int i = 1; i < 4; i++)
            {
                if (totalPartidos[i] > maxVotosPartido)
                {
                    maxVotosPartido = totalPartidos[i];
                    idxPartidoGanador = i;
                }
            }

            lblCandidatoGanador.Text = partidos[idxPartidoGanador];

            // 5. Determinación de la Zona con más votantes
            int maxVotosZona = totalZonas[0];
            int idxZonaMax = 0;

            for (int j = 1; j < 4; j++)
            {
                if (totalZonas[j] > maxVotosZona)
                {
                    maxVotosZona = totalZonas[j];
                    idxZonaMax = j;
                }
            }

            lblZonaMax.Text = zonas[idxZonaMax];
        }
    }
}