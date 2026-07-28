using System.Windows;

namespace Conecta4App
{
    public partial class ResultadoWindow : Window
    {
        public ResultadoWindow(string mensaje)
        {
            InitializeComponent();
            lblMensajeResultado.Text = mensaje;
        }

        private void btnJugarOtraVez_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true; // Indicará que se debe reiniciar el juego
            this.Close();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; // Indicará salir o cerrar la ventana
            this.Close();
        }
    }
}