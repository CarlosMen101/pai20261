using Microsoft.Data.SqlClient;
using System;
using System.Configuration;
using System.Windows;

namespace EjemploSQLCommandInsertar
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            string cn = ConfigurationManager.ConnectionStrings["EjemploSQLCommandInsertar.Properties.Settings.Northwind"].ConnectionString;

            try
            {
                using (SqlConnection conex = new SqlConnection(cn))
                {
                    SqlCommand cmd = conex.CreateCommand();
                    cmd.CommandText = "INSERT INTO Customers(CustomerID, CompanyName) VALUES(@Id, @Nombre);";
                    cmd.Parameters.Add("@Id", System.Data.SqlDbType.NChar, 5).Value = txtId.Text;
                    cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.NVarChar, 40).Value = txtNombre.Text;

                    conex.Open();
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Cliente registrado exitosamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agregar cliente: {ex.Message}");
            }
        }
    }
}