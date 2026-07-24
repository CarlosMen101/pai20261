using System;
using System.Windows;
using System.Windows.Controls;

namespace Temperaturas
{
    public partial class MainWindow : Window
    {
        // =================================================================
        // VARIABLES GLOBALES
        // =================================================================
        private TextBox[] txtMeses;

        private string[] nombresMeses = { "Enero", "Febrero", "Marzo", "Abril",
                                          "Mayo", "Junio", "Julio", "Agosto",
                                          "Setiembre", "Octubre", "Noviembre", "Diciembre" };

        public MainWindow()
        {
            InitializeComponent();

            // Suscribimos los eventos desde C#
            this.Loaded += Window_Loaded;
            btnMostrarCalculos.Click += btnMostrarCalculos_Click;
        }

        // =================================================================
        // EVENTO: AL CARGAR LA VENTANA
        // =================================================================
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 1. Agrupamos los TextBox en un arreglo para recorrerlos
            txtMeses = new TextBox[] {
                txtEnero, txtFebrero, txtMarzo, txtAbril,
                txtMayo, txtJunio, txtJulio, txtAgosto,
                txtSetiembre, txtOctubre, txtNoviembre, txtDiciembre
            };

            // 2. Generamos temperaturas aleatorias entre 0 y 100
            Random random = new Random();
            for (int i = 0; i < txtMeses.Length; i++)
            {
                txtMeses[i].Text = (random.NextDouble() * 100).ToString("N2");
            }
        }

        // =================================================================
        // EVENTO: BOTÓN MOSTRAR CÁLCULOS
        // =================================================================
        private void btnMostrarCalculos_Click(object sender, RoutedEventArgs e)
        {
            double[] temperaturas = new double[12];
            double sumaTemperaturas = 0;

            // --- PRIMER RECORRIDO: Validar y sumar ---
            for (int i = 0; i < temperaturas.Length; i++)
            {
                if (double.TryParse(txtMeses[i].Text, out double temperatura))
                {
                    temperaturas[i] = temperatura;
                    sumaTemperaturas += temperatura;
                }
                else
                {
                    MessageBox.Show("Ingrese una temperatura válida en " + nombresMeses[i], "Error de entrada", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtMeses[i].Focus();
                    return;
                }
            }

            // --- CÁLCULO DEL PROMEDIO ---
            double promedio = sumaTemperaturas / 12;
            txtPromedio.Text = promedio.ToString("N2");

            // --- SEGUNDO RECORRIDO: Listar meses sobre el promedio ---
            int mayores = 0;
            lstMesesMayores.Items.Clear();

            for (int i = 0; i < temperaturas.Length; i++)
            {
                if (temperaturas[i] > promedio)
                {
                    mayores++;
                    lstMesesMayores.Items.Add(nombresMeses[i]);
                }
            }

            txtMayores.Text = mayores.ToString();
        }
    }
}