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

namespace Imagenes2
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // No need to call InitializeComponent again here
            /*BitmapImage bitmapImage= new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(@"D:\Programacion aplicada 1\Imagenes\plaza.jpg");
            bitmapImage.EndInit();
            imagen1.Source = bitmapImage;*/

            // Ensure we load the embedded resource via pack URI (project BuildAction=Resource)
            imagen2.Source = new BitmapImage(new Uri("pack://application:,,,/Imagenes2;component/plaza.jpg"));
        }
    }
}