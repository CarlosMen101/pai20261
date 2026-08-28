using System.Data;
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
using Microsoft.Data.SqlClient;

namespace SQLServerEjemplo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString = "Server=localhost;Database=Northwind;Integrated Security =true ; TrustServerCertificate = True;Encrypt = True";
        public MainWindow()
        {
            
            InitializeComponent();
        }

        private void btnConectar_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Server=localhost;Database=Northwind;Integrated Security =true ; TrustServerCertificate = True;Encrypt = True";

            using (SqlConnection con = new SqlConnection(connectionString)) {
                try {
                    con.Open();
                    MessageBox.Show($"Conexion exitosa :{con.Database}");
                } catch (SqlException ex) {
                    MessageBox.Show($"Error de SQL: {ex.Message}");
                }
                    
            }
        }

        private void btnCargar_Click(object sender, RoutedEventArgs e)
        {
            string query = "SELECT CategoryID, CategoryName FROM Categories";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    // 1. ABRIR LA CONEXIÓN PRIMERO
                    con.Open();

                    SqlCommand cmd = new SqlCommand(query, con);

                    // 2. Ejecutar el Reader
                    using (SqlDataReader dataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        cbxCategorias.Items.Clear();

                        while (dataReader.Read())
                        {
                            cbxCategorias.Items.Add(new
                            {
                                Id = dataReader.GetInt32(0),
                                Nombre = dataReader.GetString(1)
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error de SQL: {ex.Message}");
                }
            }
        }

        private void btnSeleccionado_Click(object sender, RoutedEventArgs e)
        {
            if (cbxCategorias.SelectedItem != null) {
                dynamic categoriaseleccionada = cbxCategorias.SelectedItem;
                int id = categoriaseleccionada.Id;
                String nombre = categoriaseleccionada.Nombre;

                MessageBox.Show($"seleccionado:ID={id},Nombre={nombre}");
            }
        }

        private void btnMostrarProductos_Click(object sender, RoutedEventArgs e)
        {
            string query = "SELECT ProductID, ProductName, UnitPrice, UnitsInStock " +"FROM Products WHERE Discontinued = 0";
            using (SqlConnection con = new SqlConnection(connectionString)) 
            {
                SqlDataAdapter sqlData = new SqlDataAdapter(query, con);
                DataSet ds= new DataSet();

                sqlData.Fill(ds, "Producto");
                dgMostrarProductos.ItemsSource = ds.Tables["Producto"].DefaultView;
            }
        }
    }
}