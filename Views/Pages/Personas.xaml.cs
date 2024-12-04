using Entities;
using Microsoft.Win32;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Lógica de interacción para Personas.xaml
    /// </summary>
    public partial class Personas : Page
    {
        PersonaDAO personaDAO = new PersonaDAO();
        LocacionDAO locacionDAO = new LocacionDAO();
        private string imageSource = null;
        byte[] FotoPersona;
        MemoryStream stream;
        MemoryStream memoryStream; //Variable Global
        private static Personas _instancia = null;

        public Personas()
        {
            InitializeComponent();
            ListarPersonas();
            ListarLocaciones();
        }

        public Personas obtenerInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new Personas();
            }
            return _instancia;
        }

        private void ListarPersonas()
        {
            var DtPersonas = personaDAO.ListarPersonas();
            TraerPersonas(DtPersonas);
        }

        private void ListarLocaciones()
        {
            var DtLocaciones = locacionDAO.ListarLocaciones();
            TraerLocaciones(DtLocaciones);
        }

        private void TraerPersonas(DataTable dtPersonas)
        {
            List<Persona> personaList = dtPersonas.AsEnumerable()
            .Select(dr => new Persona
            {
                IdPersona = Convert.ToInt32(dr["IdPersona"]),
                Nombre = dr["Nombre"].ToString(),
                Apellido = dr["Apellido"].ToString(),
                Cuil = dr["Cuit"].ToString(),
                Domicilio = dr["Domicilio"].ToString(),
                Telefono = dr["Telefono"].ToString(),
                Tipo = dr["Tipo"].ToString(),
                IdLocacion = Convert.ToInt32(dr["Locacion"]),
                Foto = (byte[])dr["Foto"]
            }).ToList();
            this.dgPersonas.ItemsSource = personaList;
        }

        private void TraerLocaciones(DataTable dtLocaciones)
        {
            List<Locacion> locacionList = dtLocaciones.AsEnumerable().Select(dr => new Locacion
            {
                IdLocacion = Convert.ToInt32(dr["IdLocacion"]),
                Descripcion = dr["Descripcion"].ToString()
            }).ToList();

            // Crear una colección de ComboBoxItem a partir de la lista de Locacion
            var locacionComboBoxItems = locacionList.Select(l => new ComboBoxItem
            {
                Content = l.Descripcion,
                Tag = l.IdLocacion
            }).ToList();

            // Asignar la colección de ComboBoxItem al ItemsSource del ComboBox
            this.cmbLocacion.ItemsSource = locacionComboBoxItems;
        }

        private void btnExaminarFoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de Imagen (.jpg) | *.jpg | PNG (.png) | *.png | All files(*.*) | *.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;

            bool? revisarOk = openFileDialog.ShowDialog();
            if (revisarOk == true)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(openFileDialog.FileName));
                imageSource = openFileDialog.FileName.ToString();
                ImageBrush fotoUsuario = new ImageBrush();
                fotoUsuario.ImageSource = bitmapImage;
                this.imgPerfil.Fill = fotoUsuario;
            }
        }

        private void LimpiarCampos()
        {
            this.txtNombre.Text = "";
            this.txtApellido.Clear();
            this.txtCuil.Clear();
            this.txtDomicilio.Clear();
            this.txtTelefono.Clear();
            this.txtIdPersona.Clear();
            this.cmbTipo.Text = "";
            this.cmbLocacion.Text = "";

            ImageBrush fotoPersona = new ImageBrush();
            fotoPersona.ImageSource = new BitmapImage(new Uri(@"usuario.png", UriKind.RelativeOrAbsolute));
            this.imgPerfil.Fill = fotoPersona;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (this.imgPerfil != null & this.txtNombre.Text != "")
            {
                MemoryStream ms = new MemoryStream(File.ReadAllBytes(this.imageSource));
                byte[] pic = ms.ToArray();

                Persona persona = new Persona();
                persona.Nombre = this.txtNombre.Text;
                persona.Apellido = this.txtApellido.Text;
                persona.Cuil = this.txtCuil.Text;
                persona.Domicilio = this.txtDomicilio.Text;
                persona.Telefono = this.txtTelefono.Text;
                persona.Tipo = this.cmbTipo.Text;
                persona.IdLocacion = Convert.ToInt32(((ComboBoxItem)this.cmbLocacion.SelectedItem).Tag);
                persona.Foto = pic;

                try
                {
                    if (imageSource != null)
                    {
                        personaDAO.AgregarPersona(persona);
                        Mensaje mensaje = new Mensaje();
                        mensaje.MensajePersonalizado("Agregar", "Persona " + persona.Nombre + " " + persona.Apellido + " agregada con Exito");
                        mensaje.ShowDialog();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar Persona " + ex.Message);
                }
                ListarPersonas();
                LimpiarCampos();
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.txtIdPersona.Text != "")
                {
                    if (imageSource == null)
                    {
                        stream = this.memoryStream;
                    }
                    else
                    {
                        stream = new MemoryStream(File.ReadAllBytes(this.imageSource));
                    }
                    byte[] pic = stream.ToArray();
                    Persona persona = new Persona();
                    persona.Nombre = this.txtNombre.Text;
                    persona.Apellido = this.txtApellido.Text;
                    persona.Cuil = this.txtCuil.Text;
                    persona.Domicilio = this.txtDomicilio.Text;
                    persona.Telefono = this.txtTelefono.Text;
                    persona.Tipo = this.cmbTipo.Text;
                    persona.IdLocacion = Convert.ToInt32(this.cmbLocacion.Text);
                    persona.Foto = pic;
                    persona.IdPersona = Convert.ToInt32(this.txtIdPersona.Text);
                    try
                    {
                        personaDAO.EditarPersona(persona);
                        Mensaje mensaje = new Mensaje();
                        mensaje.MensajePersonalizado("Editar", "Persona " + persona.Nombre + " " + persona.Apellido + " Editado con Exito");
                        mensaje.ShowDialog();
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "No se pudo Actualizar los datos de la Persona");
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
            ListarPersonas();
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.txtIdPersona.Text != "")
                {
                    Persona persona = new Persona();

                    personaDAO.EliminarPersona(persona.IdPersona);
                    Mensaje mensaje = new Mensaje();
                    mensaje.MensajePersonalizado("Eliminar", "Persona " + persona.Nombre + " " + persona.Apellido + " eliminado con Exito");
                    mensaje.ShowDialog();
                  
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar a la Persona");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Error al eliminar");
            }
            ListarPersonas();
        }

        private void dgPersonas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!this.dgPersonas.Items.IsEmpty)
            {
                Persona persona = (Persona)this.dgPersonas.CurrentItem;

                this.txtNombre.Text = persona.Nombre;
                this.txtApellido.Text = persona.Apellido;
                this.txtCuil.Text = persona.Cuil;
                this.txtDomicilio.Text = persona.Domicilio;
                this.txtTelefono.Text = persona.Telefono;
                this.cmbTipo.Text = persona.Tipo;
                // Busca la locación correspondiente en el ComboBox y la selecciona
                var locacionItem = this.cmbLocacion.Items.Cast<ComboBoxItem>()
                                                .FirstOrDefault(item => (int)item.Tag == persona.IdLocacion);
                if (locacionItem != null)
                {
                    this.cmbLocacion.SelectedItem = locacionItem;
                }

                this.txtIdPersona.Text = persona.IdPersona.ToString();
                FotoPersona = persona.Foto;
                if (FotoPersona != null)
                {
                    MemoryStream ms = new MemoryStream(FotoPersona);
                    memoryStream = ms;
                    BitmapImage img = new BitmapImage();
                    img.BeginInit();
                    img.StreamSource = ms;
                    img.EndInit();

                    ImageBrush miFoto = new ImageBrush();
                    miFoto.ImageSource = img;
                    this.imgPerfil.Fill = miFoto;
                }
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }
    }
}
