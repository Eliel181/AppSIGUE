using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Views
{
    /// <summary>
    /// Lógica de interacción para Mensaje.xaml
    /// </summary>
    public partial class Mensaje : Window
    {
        public string TituloMsj { get; set; }
        public string DetalleMsj { get; set; }

        public Mensaje()
        {
            InitializeComponent();
        }

        public void MensajePersonalizado(string titulo, string msj)
        {
            this.txtMensajeTitulo.Text = titulo;
            this.txtMensajeDetalle.Text = msj;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MoverVentana(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
