using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Views.Pages;

namespace Views
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cargarDatos();
        }

        private void cargarDatos()
        {
            var fotoUsuario = UsuarioCache.Foto;
            MemoryStream stream = new MemoryStream(fotoUsuario);
            BitmapImage imageSource = new BitmapImage();
            imageSource.BeginInit();
            imageSource.StreamSource = stream;
            imageSource.EndInit();
            ImageBrush imageUsuario = new ImageBrush();
            imageUsuario.ImageSource = imageSource;

            this.FotoUsuario.Fill = imageUsuario;
            this.txtUsuarioActual.Text = UsuarioCache.LoginName;
            this.txtRolActual.Text = UsuarioCache.Rol;
        }

        private void btnUsuarios_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Usuarios().obtenerInstancia());
        }

<<<<<<< HEAD
        private void btnPersonas_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Personas().obtenerInstancia());
        }

        private void btnAulas_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Locaciones().obtenerInstancia());
=======
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
>>>>>>> 8ad2148e9b5bd8024d9c17c0ef498b380a79fd6a
        }
    }
}
