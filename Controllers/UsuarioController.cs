using Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class UsuarioController
    {
        UsuarioDAO usuarioDao = new UsuarioDAO();

        public DataTable listarUsuarios()
        {
            return usuarioDao.ListarUsuarios();
        }

        public void agregarUsuario(Usuario usu)
        {
            usuarioDao.AgregarUsuario(usu);
        }

        public void editarUsuario(Usuario usu)
        {
            usuarioDao.EditarUsuario(usu);
        }

        public void eliminarUsuario(int idUsuario)
        {
            usuarioDao.EliminarUsuario(idUsuario);
        }

        public byte[] obtenerFoto(int idUsuario)
        {
            return usuarioDao.ObtenerFoto(idUsuario);
        }

        public bool login(string loginName, string password)
        {
            return usuarioDao.Login(loginName, password);
        }
    }
}
