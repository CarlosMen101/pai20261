using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Estructuras_de_repeticion
{
    /// <summary>
    /// Lógica de interacción para ListBox.xaml
    /// </summary>
    public partial class ListBox : Window
    {
        public ListBox()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnMostrar_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem itemSeleccionado = (ListBoxItem)lbxFrutas.SelectedItem;

        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem nuevoItem =new ListBoxItem();
            
            nuevoItem.Content = tbxAgregarFru.Text.ToUpper();

            lbxFrutas.Items.Add(nuevoItem);

            tbxAgregarFru.Text = "";
        }
    }
}
