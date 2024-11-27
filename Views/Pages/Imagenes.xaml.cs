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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Views.Pages
{
    /// <summary>
    /// Lógica de interacción para Imagenes.xaml
    /// </summary>
    public partial class Imagenes : Page
    {
        private ImagenController imagenController = new ImagenController();
        private string imageSource = null;
        private byte[] image;
        MemoryStream memoryStream;
        MemoryStream stream;
        private Mensaje mensaje = new Mensaje();

        public Imagenes()
        {
            InitializeComponent();
            listarImagenes();
        }

        private void listarImagenes()
        {
            var DtImagenes = imagenController.ListarImagenes();
            traerImagenes(DtImagenes);
        }

        private void traerImagenes(DataTable DtImagenes)
        {
            List<Imagen> imagenes = new List<Imagen>();
            imagenes = (from DataRow dr in DtImagenes.Rows
                        select new Imagen()
                        {
                            IdImagen = Convert.ToInt32(dr["IdImagen"]),
                            Titulo = dr["Titulo"].ToString(),
                            Foto = dr["Foto"] != DBNull.Value ? (byte[])dr["Foto"] : new byte[0]
                        }).ToList();

            this.dgImagenes.ItemsSource = imagenes;
        }

        private void limpiarCampos()
        {
            this.txtId.Text = "";
            this.txtTitulo.Text = "";
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(@"equipo.jpg", UriKind.Relative));
            this.eFoto.Fill = brush;
        }

        private void btnExaminarFoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos de Imagen (.jpg)|*.jpg|PNG(*.png)|*.png|All Files(*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.Multiselect = false;//solo permito seleccionar una imagen

            bool checkOk = ofd.ShowDialog() == DialogResult.OK;
            if (checkOk)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(ofd.FileName));
                imageSource = ofd.FileName.ToString();
                ImageBrush fotoUsuario = new ImageBrush();
                fotoUsuario.ImageSource = bitmapImage;
                this.eFoto.Fill = fotoUsuario;
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.eFoto != null &&
                    this.txtTitulo.Text != "")
                {
                    MemoryStream ms = new MemoryStream(File.ReadAllBytes(imageSource));
                    byte[] dataFoto = ms.ToArray();

                    Imagen imagen = new Imagen();
                    imagen.Titulo = this.txtTitulo.Text;
                    imagen.Foto = dataFoto;

                    imagenController.AgregarImagen(imagen);
                    mensaje.MensajePersonalizado("¡Correcto!", "Imagen Guardada Correctamente");
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
            listarImagenes();
            limpiarCampos();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.txtId.Text != "" &&
                    this.eFoto != null &&
                    this.txtTitulo.Text != "")
                {
                    if (imageSource == null)
                    {
                        stream = memoryStream;
                    }
                    else
                    {
                        stream = new MemoryStream(File.ReadAllBytes(imageSource));
                    }

                    byte[] dataFoto = stream.ToArray();

                    Imagen imagen = new Imagen();
                    imagen.IdImagen = Convert.ToInt32(this.txtId.Text);
                    imagen.Titulo = this.txtTitulo.Text;
                    imagen.Foto = dataFoto;

                    imagenController.EditarImagen(imagen);
                    mensaje.MensajePersonalizado("¡Correcto!", "Imagen Editada Correctamente");
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
            listarImagenes();
            limpiarCampos();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.txtId.Text != "")
                {
                    int idImagenEliminar = Convert.ToInt32(this.txtId.Text);
                    imagenController.EliminarImagen(idImagenEliminar);
                    mensaje.MensajePersonalizado("¡Correcto!", "Img Eliminada Correctamente");
                    mensaje.ShowDialog();
                }
                else
                {
                    mensaje.MensajePersonalizado("Aviso", "Debes seleccionar una Img");
                    mensaje.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("ERROR:" + ex.Message, " ERROR");
            }
            listarImagenes();
            limpiarCampos();
        }

        private void dgImagenes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!dgImagenes.Items.IsEmpty)
            {

                Imagen imagen = (Imagen)this.dgImagenes.CurrentItem;
                this.txtId.Text = imagen.IdImagen.ToString();
                this.txtTitulo.Text = imagen.Titulo;

                image = imagen.Foto;
                if (image != null && image.Length != 0)
                {
                    MemoryStream ms = new MemoryStream(image);
                    memoryStream = ms;
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = ms;
                    bitmap.EndInit();

                    ImageBrush imageBrush = new ImageBrush();
                    imageBrush.ImageSource = bitmap;

                    this.eFoto.Fill = imageBrush;
                }
            }
        }
    }
}
