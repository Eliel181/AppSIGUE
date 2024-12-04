using Controllers;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Views.Pages
{
    /// <summary>
    /// Lógica de interacción para EspecificacionesTecnicas.xaml
    /// </summary>
    public partial class EspecificacionesTecnicas : Page
    {
        private EspTecnicaController espTecnicaController = new EspTecnicaController();
        private Mensaje mensaje = new Mensaje();

        public EspecificacionesTecnicas()
        {
            InitializeComponent();
            listarEspTecnicas();
        }

        private void listarEspTecnicas()
        {
            var DtEspTecnicas = espTecnicaController.ListarEspTecnicaes();
            traerDtEspTecnicas(DtEspTecnicas);
        }

        private void traerDtEspTecnicas(DataTable DtEspTecnicas)
        {
            List<EspTecnica> espTecnicas = new List<EspTecnica>();
            espTecnicas = (from DataRow dr in DtEspTecnicas.Rows
                           select new EspTecnica()
                        {
                            IdEspTecnica = Convert.ToInt32(dr["IdEspTecnica"]),
                            Programa = dr["Programa"].ToString(),
                            Titulo = dr["Titulo"].ToString(),
                            Descripcion = dr["Descripcion"].ToString()
                        }).ToList();

            this.dgEspTecnicas.ItemsSource = espTecnicas;
        }

        private void limpiarCampos()
        {
            this.txtId.Text = "";
            this.txtTitulo.Text = "";
            this.txtDescripcion.Text = "";
            this.cmbPrograma.SelectedIndex = -1;
            
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.txtTitulo.Text != "" &&
                    this.txtDescripcion.Text != "" &&
                    this.cmbPrograma.Text != "")
                {

                    EspTecnica espTecnica = new EspTecnica();
                    espTecnica.Programa = this.cmbPrograma.Text;
                    espTecnica.Titulo = this.txtTitulo.Text;
                    espTecnica.Descripcion = this.txtDescripcion.Text;

                    espTecnicaController.AgregarEspTecnica(espTecnica);
                    mensaje.MensajePersonalizado("¡Correcto!", " Guardada Correctamente");
                    mensaje.ShowDialog();

                }
                else
                {
                    mensaje.MensajePersonalizado("Aviso", "Debes completar todos los campos");
                    mensaje.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("ERROR:" + ex.Message, " ERROR");

            }
            listarEspTecnicas();
            limpiarCampos();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dgEspTecnicas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
