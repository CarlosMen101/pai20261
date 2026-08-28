using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using NorthwindApp.Data;
using NorthwindApp.Models;

namespace NorthwindApp
{
    public partial class MainWindow : Window
    {
        private readonly PedidoDAL _pedidoDAL;

        public MainWindow()
        {
            InitializeComponent();
            _pedidoDAL = new PedidoDAL();
            // Ya NO se llama a CargarDatos() aquí para que la tabla inicie en blanco.
        }

        /// <summary>
        /// Método de evento para el botón "Mostrar Todo".
        /// Muestra el MessageBox de conexión exitosa antes de cargar los datos.
        /// </summary>
        private void BtnMostrarTodo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtFiltro.Clear();
                var pedidos = _pedidoDAL.ObtenerPedidos();

                // Notificación solicitada mediante MessageBox
                MessageBox.Show("Conexión exitosa", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

                ActualizarInterfaz(pedidos);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al conectar con la base de datos: {ex.Message}",
                                "Error de Conexión", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filtro = txtFiltro.Text.Trim();

                if (string.IsNullOrEmpty(filtro))
                {
                    MessageBox.Show("Por favor, ingrese un nombre de cliente para buscar.",
                                    "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var resultados = _pedidoDAL.BuscarPedidosPorCliente(filtro);
                ActualizarInterfaz(resultados);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al buscar: {ex.Message}",
                                "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Recibe la lista de pedidos, puebla el DataGrid y calcula el Total y Promedio del Flete.
        /// </summary>
        private void ActualizarInterfaz(List<Pedido> listaPedidos)
        {
            dgPedidos.ItemsSource = listaPedidos;

            int totalPedidos = listaPedidos.Count;
            decimal totalFlete = listaPedidos.Sum(p => p.Freight);
            decimal promedioFlete = totalPedidos > 0 ? listaPedidos.Average(p => p.Freight) : 0;

            sbiCantidad.Content = $"Pedidos listados: {totalPedidos}";
            sbiTotalFlete.Content = $"Monto Total Flete: {totalFlete:C2}";
            sbiPromedioFlete.Content = $"Promedio Flete: {promedioFlete:C2}";
        }
    }
}