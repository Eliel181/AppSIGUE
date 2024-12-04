using Entities;
using Models;
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
    /// Lógica de interacción para Locaciones.xaml
    /// </summary>
    public partial class Locaciones : Page
    {

        LocacionDAO locacionDAO = new LocacionDAO();
        private static Locaciones _instancia = null;

        public Locaciones()
        {
            InitializeComponent();
            ListarLocaciones();
        }

        public Locaciones obtenerInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new Locaciones();
            }
            return _instancia;
        }

        private void ListarLocaciones()
        {
            var DtLocaciones = locacionDAO.ListarLocaciones();
            TraerLocaciones(DtLocaciones);
        }

        private void TraerLocaciones(DataTable dtLocaciones)
        {
            List<Locacion> locacionList = dtLocaciones.AsEnumerable()
            .Select(dr => new Locacion
            {
                IdLocacion = Convert.ToInt32(dr["IdLocacion"]),
                Descripcion = dr["Descripcion"].ToString()
            }).ToList();
            this.dgLocaciones.ItemsSource = locacionList;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtDescripcion.Text != "")
            {
                Locacion locacion = new Locacion();
                locacion.Descripcion = this.txtDescripcion.Text;
        
                try
                {
                    if (txtIdLocacion != null)
                    {
                        locacionDAO.AgregarLocacion(locacion);
                        Mensaje mensaje = new Mensaje();
                        mensaje.MensajePersonalizado("Agregar", "Locacion " + locacion.Descripcion + " agregada con Exito");
                        mensaje.ShowDialog();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar Locacion " + ex.Message);
                }
                ListarLocaciones();
                LimpiarCampos();
            }
        }

        private void LimpiarCampos()
        {
            this.txtDescripcion.Text = "";
            this.txtIdLocacion.Clear();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.txtIdLocacion.Text != "")
                {
               
                    Locacion locacion = new Locacion();
                    locacion.Descripcion = this.txtDescripcion.Text;
                    
                    locacion.IdLocacion = Convert.ToInt32(this.txtIdLocacion.Text);
                    try
                    {
                        locacionDAO.EditarLocacion(locacion);
                        Mensaje mensaje = new Mensaje();
                        mensaje.MensajePersonalizado("Editar", "Locacion " + locacion.Descripcion + " Editada con Exito");
                        mensaje.ShowDialog();
                        //MessageBox.Show("Usuario actualizado con Exito!!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "No se pudo Actualizar los datos de la Locacion");
                    }
                }
                else
                {
                    MessageBox.Show("No seleccionaste ningun elemento");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Error al Editar");
            }
            ListarLocaciones();
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.txtIdLocacion.Text != "")
                {
                    Locacion locacion = new Locacion();

                    locacionDAO.EliminarLocacion(locacion.IdLocacion);
                    Mensaje mensaje = new Mensaje();
                    mensaje.MensajePersonalizado("Eliminar", "Locacion " + locacion.Descripcion + " eliminada con Exito");
                    mensaje.ShowDialog();
                    //MessageBox.Show("Usuario eliminado con Exito!!");
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar la Locacion");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Error al eliminar");
            }
            ListarLocaciones();
        }

        private void dgLocaciones_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!this.dgLocaciones.Items.IsEmpty)
            {
                Locacion locacion = (Locacion)this.dgLocaciones.CurrentItem;

                this.txtDescripcion.Text = locacion.Descripcion;

                this.txtIdLocacion.Text = locacion.IdLocacion.ToString();
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }
    }
}
