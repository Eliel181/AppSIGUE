using Controllers;
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
using System.Windows.Shapes;

namespace Views
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void txtUsuario_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (txtUsuario.Text == "USUARIO")
            {
                txtUsuario.Text = "";
            }
        }

        private void txtUsuario_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (txtUsuario.Text == "")
            {
                txtUsuario.Text = "USUARIO";
            }
        }

        private void psbPassword_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (psbPassword.Password == "CONTRASEÑA")
            {
                psbPassword.Password = "";
            }
        }

        private void psbPassword_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (psbPassword.Password == "")
            {
                psbPassword.Password = "CONTRASEÑA";
            }
        }

        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtUsuario.Text != "USUARIO")
                {
                    if (psbPassword.Password != "CONTRASEÑA")
                    {
                        UsuarioController usuarioController = new UsuarioController();
                        var validarlogin = usuarioController.login(txtUsuario.Text, psbPassword.Password);
                        if (validarlogin)
                        {
                            MainWindow fUsuarios = new MainWindow();
                            Mensaje msj = new Mensaje();
                            msj.MensajePersonalizado("Bienvenido", UsuarioCache.Nombre + " " + UsuarioCache.Apellido);
                            MostrarFoto();
                            msj.ShowDialog();
                            fUsuarios.ShowDialog();
                            fUsuarios.Closed += LogOut;
                            this.Hide();
                        }
                        else
                        {
                            Mensaje msj = new Mensaje();
                            msj.MensajePersonalizado("LOGIN", "Error el usuario o contraseña no coinciden");
                            msj.ShowDialog();
                        }
                    }
                    else
                    {
                        Mensaje msj = new Mensaje();
                        msj.MensajePersonalizado("LOGIN", "Error ingrese la coontraseña");
                        msj.ShowDialog();
                    }
                }
                else
                {
                    Mensaje msj = new Mensaje();
                    msj.MensajePersonalizado("LOGIN", "Error ingrese el usuario");
                    msj.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Mensaje msj = new Mensaje();
                msj.MensajePersonalizado("LOGIN", "Error" + ex.Message);
                msj.ShowDialog();
            }
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MostrarFoto()
        {
            var fotoUsuario = UsuarioCache.Foto;
            MemoryStream stream = new MemoryStream(fotoUsuario);
            BitmapImage imageSource = new BitmapImage();
            imageSource.BeginInit();
            imageSource.StreamSource = stream;
            imageSource.EndInit();
            ImageBrush imageUsuario = new ImageBrush();
            imageUsuario.ImageSource = imageSource;
            this.FotoUsuario.Opacity = 0.9;
            this.FotoUsuario.Fill = imageUsuario;
        }

        private void LogOut(object sender, EventArgs e)
        {
            try
            {
                txtUsuario.Text = "USUARIO";
                psbPassword.Password = "CONTRASEÑA";
                ImageBrush image = new ImageBrush();
                image.ImageSource = new BitmapImage(new Uri(@"usuario.png", UriKind.Relative));
                FotoUsuario.Fill = image;

                this.Show();
            }
            catch (Exception ex)
            {
                Mensaje msj = new Mensaje();
                msj.MensajePersonalizado("LOGOUT", "Error" + ex.Message);
                msj.ShowDialog();
            }
        }
    }
}
