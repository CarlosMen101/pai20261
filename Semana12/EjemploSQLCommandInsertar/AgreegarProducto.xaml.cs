using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EjemploSQLCommandInsertar.Models;

namespace EjemploSQLCommandInsertar
{
    /// <summary>
    /// Lógica de interacción para AgreegarProducto.xaml
    /// </summary>
    public partial class AgreegarProducto : Window
    {
        public AgreegarProducto()
        {
            InitializeComponent();
            CargarProductos();
        }

        private async void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            btnRegistrar.IsEnabled = false;
            string cadena = ConfigurationManager.ConnectionStrings["EjemploSQLCommandInsertar.Properties.Settings.cn"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    await conn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SP_AgregarProducto";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.NVarChar, 40).Value = txtNombre.Text;
                        cmd.Parameters.Add("@Precio", System.Data.SqlDbType.Money).Value = txtPrecio.Text;
                        cmd.Parameters.Add("@NombreCategoria", System.Data.SqlDbType.NVarChar, 15).Value = txtCategoria.Text;

                        cmd.CommandTimeout = 60;
                        await cmd.ExecuteNonQueryAsync();

                        MessageBox.Show($"El producto ha sido agregado a la categoría {txtCategoria.Text}");
                        Limpiar();
                        CargarProductos();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Error SQL {ex.Number}, {ex.Message}");
            }
            finally
            {
                btnRegistrar.IsEnabled = true;
            }
        }

        private void Limpiar()
        {
            txtNombre.Clear();
            txtPrecio.Clear();
            txtCategoria.Clear();
            txtNombre.Focus();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void CargarProductos()
        {
            string cadena = ConfigurationManager.ConnectionStrings["EjemploSQLCommandInsertar.Properties.Settings.cn"].ConnectionString;
            List<ProductoVista> listaProductos = new List<ProductoVista>();

            try
            {
                using (SqlConnection conn = new SqlConnection(cadena))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_ListarProductosConCategoria", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listaProductos.Add(new ProductoVista
                                {
                                    Id = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    Precio = reader.GetDecimal(2),
                                    Categoria = reader.GetString(3)
                                });
                            }
                        }
                    }
                }

                dgProductos.ItemsSource = listaProductos;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los productos: {ex.Message}");
            }
        }
    }
}