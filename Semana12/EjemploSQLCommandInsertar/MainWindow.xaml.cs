using Microsoft.Data.SqlClient;
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
using System.Configuration;

namespace EjemploSQLCommandinsertar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            // Obtiene la cadena de conexión desde el archivo de configuración (App.config)
            string cn = ConfigurationManager.ConnectionStrings["EjemploSQLCommandInsertar.Properties.Settings.cn"].ConnectionString;

            using (SqlConnection conex = new SqlConnection(cn))
            {
                try
                {
                    string query = "INSERT INTO CUSTOMERS(CustomerID, CompanyName) VALUES(@Id, @Nombre)";
                    SqlCommand cmd = new SqlCommand(query, conex);
                    //cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@Id", SqlDbType.NChar, 5).Value = txtId.Text;
                    cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 40).Value = txtNombre.Text;

                    conex.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Cliente agregado");
                    }
                    else
                    {
                        MessageBox.Show("No se pudo registrar");
                    }
                }
                catch (SqlException ex)
                {
                    // Captura errores específicos de SQL Server (ej. ID duplicado)
                    MessageBox.Show($"Error al insertar codigo: {ex.Number}, Descripcion: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Captura cualquier otro error general
                    MessageBox.Show($"Error inesperado, Descripcion: {ex.Message}");
                }
            }
        }
    }
}