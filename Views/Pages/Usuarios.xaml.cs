using Entities;
using Microsoft.Win32;
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
    /// Lógica de interacción para Usuarios.xaml
    /// </summary>
    public partial class Usuarios : Page
    {
        UsuarioDAO usuarioDao = new UsuarioDAO();
        private string imageSource = null;
        byte[] FotoEstudiante;
        MemoryStream stream;
        MemoryStream memoryStream; //Variable Global
        private static Usuarios _instancia = null;

        public Usuarios()
        {
            InitializeComponent();
            ListarUsuarios();
        }
        public Usuarios obtenerInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new Usuarios();
            }
            return _instancia;
        }

        private void ListarUsuarios()
        {
            var DtUsuarios = usuarioDao.ListarUsuarios();
            TraerUsuarios(DtUsuarios);
        }

        private void TraerUsuarios(DataTable dtUsuarios)
        {
            List<Usuario> usuarioList = dtUsuarios.AsEnumerable()
            .Select(dr => new Usuario
            {
                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                Nombre = dr["Nombre"].ToString(),
                Apellido = dr["Apellido"].ToString(),
                LoginName = dr["LoginName"].ToString(),
                Password = dr["Password"].ToString(),
                Rol = dr["Rol"].ToString(),
                Estado = Convert.ToBoolean(dr["Estado"]),
                Foto = (byte[])dr["Foto"]
            }).ToList();
            this.dgUsuarios.ItemsSource = usuarioList;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (this.imgPerfil != null & this.txtNombre.Text != "")
            {
                MemoryStream ms = new MemoryStream(File.ReadAllBytes(this.imageSource));
                byte[] pic = ms.ToArray();

                Usuario usuario = new Usuario();
                usuario.Nombre = this.txtNombre.Text;
                usuario.Apellido = this.txtApellido.Text;
                usuario.LoginName = this.txtLoginName.Text;
                usuario.Password = this.txtPassword.Text;
                usuario.Estado = this.chkEstado.IsChecked.Value;
                usuario.Rol = this.cmbRol.Text;
                usuario.Foto = pic;

                try
                {
                    if (imageSource != null)
                    {
                        usuarioDao.AgregarUsuario(usuario);
                        Mensaje mensaje = new Mensaje();
                        mensaje.MensajePersonalizado("Agregar", "Usuario " + usuario.Apellido + " agregado con Exito");
                        mensaje.ShowDialog();
                       
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar Usuario " + ex.Message);
                }
                ListarUsuarios();
                LimpiarCampos();
            }
        }

        private void LimpiarCampos()
        {
            this.txtNombre.Text = "";
            this.txtApellido.Clear();
            this.txtLoginName.Clear();
            this.txtPassword.Clear();
            this.txtIdUsuario.Clear();
            this.cmbRol.Text = "";

            ImageBrush fotoEstudiante = new ImageBrush();
            fotoEstudiante.ImageSource = new BitmapImage(new Uri(@"usuario.png", UriKind.RelativeOrAbsolute));
            this.imgPerfil.Fill = fotoEstudiante;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.txtIdUsuario.Text != "")
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
                    Usuario usuario = new Usuario();
                    usuario.Nombre = this.txtNombre.Text;
                    usuario.Apellido = this.txtApellido.Text;
                    usuario.Rol = this.cmbRol.Text;
                    usuario.LoginName = this.txtLoginName.Text;
                    usuario.Password = this.txtPassword.Text;
                    usuario.Estado = this.chkEstado.IsChecked.Value;
                    usuario.Foto = pic;
                    usuario.IdUsuario = Convert.ToInt32(this.txtIdUsuario.Text);
                    try
                    {
                        usuarioDao.EditarUsuario(usuario);
                        Mensaje mensaje = new Mensaje();
                        mensaje.MensajePersonalizado("Editar", "Usuario " + usuario.Apellido + " Editado con Exito");
                        mensaje.ShowDialog();
                        //MessageBox.Show("Usuario actualizado con Exito!!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "No se pudo Actualizar los datos del Usuario");
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
            ListarUsuarios();
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.txtIdUsuario.Text != "")
                {
                    Usuario usuario = new Usuario();

                    usuarioDao.EliminarUsuario(usuario.IdUsuario);
                    Mensaje mensaje = new Mensaje();
                    mensaje.MensajePersonalizado("Eliminar", "Usuario " + usuario.Apellido + " eliminado con Exito");
                    mensaje.ShowDialog();
                    //MessageBox.Show("Usuario eliminado con Exito!!");
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el Usuario");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Error al eliminar");
            }
            ListarUsuarios();
        }


        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        private void dgUsuarios_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!this.dgUsuarios.Items.IsEmpty)
            {
                Usuario usuario = (Usuario)this.dgUsuarios.CurrentItem;
               
                this.txtNombre.Text = usuario.Nombre;
                this.txtApellido.Text = usuario.Apellido;
                this.txtLoginName.Text = usuario.LoginName;
                this.txtPassword.Text = usuario.Password;
                this.chkEstado.IsChecked = usuario.Estado;
                this.cmbRol.Text = usuario.Rol;

                this.txtIdUsuario.Text = usuario.IdUsuario.ToString();
                FotoEstudiante = usuario.Foto;
                if (FotoEstudiante != null)
                {
                    MemoryStream ms = new MemoryStream(FotoEstudiante);
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
    }
}
