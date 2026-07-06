using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ExamenUnidad1
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Boleto> ListaBoletos { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ListaBoletos = new ObservableCollection<Boleto>();
            lvRegistro.ItemsSource = ListaBoletos;

            // Fecha automática al iniciar
            tbxFechaActual.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxApellidos.Text) || string.IsNullOrWhiteSpace(tbxNombres.Text))
            {
                MessageBox.Show("Por favor, ingrese los apellidos y nombres completos del cliente.", "Atención", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string cliente = $"{tbxApellidos.Text.Trim()}, {tbxNombres.Text.Trim()}";
            string formato = rb2D.IsChecked == true ? "2D" : "3D";
            string categoria = ((ComboBoxItem)cbCategoria.SelectedItem).Content.ToString();

            double precio = CalcularPrecio(formato, categoria);

            Boleto nuevoBoleto = new Boleto
            {
                Fecha = tbxFechaActual.Text,
                NombreCompleto = cliente,
                Formato = formato,
                Categoria = categoria,
                Precio = precio
            };

            ListaBoletos.Add(nuevoBoleto);
            ActualizarEstadisticas();

            // Limpieza
            tbxApellidos.Clear();
            tbxNombres.Clear();
            tbxApellidos.Focus();
        }

        private double CalcularPrecio(string formato, string categoria)
        {
            if (formato == "2D")
            {
                switch (categoria)
                {
                    case "General": return 20.0;
                    case "Estudiante": return 15.0;
                    case "Adulto Mayor": return 12.0;
                    default: return 0.0;
                }
            }
            else
            {
                switch (categoria)
                {
                    case "General": return 30.0;
                    case "Estudiante": return 25.0;
                    case "Adulto Mayor": return 20.0;
                    default: return 0.0;
                }
            }
        }

        private void ActualizarEstadisticas()
        {
            int generales = ListaBoletos.Count(b => b.Categoria == "General");
            int estudiantes = ListaBoletos.Count(b => b.Categoria == "Estudiante");
            int adultos = ListaBoletos.Count(b => b.Categoria == "Adulto Mayor");

            double total2D = ListaBoletos.Where(b => b.Formato == "2D").Sum(b => b.Precio);
            double total3D = ListaBoletos.Where(b => b.Formato == "3D").Sum(b => b.Precio);

            lblGeneral.Text = generales.ToString();
            lblEstudiantes.Text = estudiantes.ToString();
            lblAdultoMayor.Text = adultos.ToString();
            lblAcumulado2D.Text = $"S/{total2D:F2}";
            lblAcumulado3D.Text = $"S/{total3D:F2}";
        }
    }

    public class Boleto
    {
        public string Fecha { get; set; }
        public string NombreCompleto { get; set; }
        public string Formato { get; set; }
        public string Categoria { get; set; }
        public double Precio { get; set; }
    }
}