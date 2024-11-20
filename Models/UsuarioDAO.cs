using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Models
{
    public class UsuarioDAO : ConnectionSQL
    {
        //metodo de validacion del login
        public bool Login(string loginName, string password)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@loginName", loginName);
                    command.Parameters.AddWithValue("@password", password);
                    command.CommandText = "SELECT * FROM USUARIOS WHERE LoginName = @loginName AND Password = @password";
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UsuarioCache.IdUsuario = reader.GetInt32(0);
                            UsuarioCache.Nombre = reader.GetString(1);
                            UsuarioCache.Apellido = reader.GetString(2);
                            UsuarioCache.LoginName = reader.GetString(3);
                            UsuarioCache.Password = reader.GetString(4);
                            UsuarioCache.Rol = reader.GetString(5);
                            UsuarioCache.Estado = reader.GetBoolean(6);
                            byte[] fotoDB = (byte[])reader[7];
                            UsuarioCache.Foto = fotoDB;
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        //metodo para listar a los usuarios
        public DataTable ListarUsuarios()
        {
            DataTable listUsuarios = new DataTable();
            using (var connection = GetConnection())
            {
                SqlDataReader leerfilas;
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "sp_ListarUsuarios";
                    command.CommandType = CommandType.StoredProcedure;
                    leerfilas = command.ExecuteReader();
                    listUsuarios.Load(leerfilas);
                    leerfilas.Close();
                    return listUsuarios;
                }
            }
        }

        //metodo para agregar usuario
        public void AgregarUsuario(Usuario usu)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@nombre", usu.Nombre);
                    command.Parameters.AddWithValue("@apellido", usu.Apellido);
                    command.Parameters.AddWithValue("@loginName", usu.LoginName);
                    command.Parameters.AddWithValue("@password", usu.Password);
                    command.Parameters.AddWithValue("@rol", usu.Rol);
                    command.Parameters.AddWithValue("@estado", usu.Estado);
                    command.Parameters.AddWithValue("@foto", usu.Foto);
                    command.CommandText = "sp_AgregarUsuario";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }

        //metodo para obtener la foto de una BD
        public byte[] ObtenerFoto(int idUsuario)
        {
            byte[] fotoDB = null;
            using (var connection = GetConnection())
            {
                SqlDataReader leerfilas;
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select Foto from USUARIOS where IdUsuario = @IdUsuario";
                    command.Parameters.AddWithValue("idUsuario", idUsuario);
                    command.CommandType = CommandType.StoredProcedure;
                    leerfilas = command.ExecuteReader();
                    if (leerfilas.Read())
                    {
                        fotoDB = (byte[])leerfilas.GetValue(0);
                    }
                    leerfilas.Close();
                    return fotoDB;
                }
            }
        }

        //metodo para editar usuarios
        public void EditarUsuario(Usuario usu)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@idUsuario", usu.IdUsuario);
                    command.Parameters.AddWithValue("@nombre", usu.Nombre);
                    command.Parameters.AddWithValue("@apellido", usu.Apellido);
                    command.Parameters.AddWithValue("@loginName", usu.LoginName);
                    command.Parameters.AddWithValue("@password", usu.Password);
                    command.Parameters.AddWithValue("@rol", usu.Rol);
                    command.Parameters.AddWithValue("@estado", usu.Estado);
                    command.Parameters.AddWithValue("@foto", usu.Foto);
                    command.CommandText = "sp_EditarUsuario";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }

        //metodo para eliminar usuarios
        public void EliminarUsuario(int IdUsuario)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@idUsuario", IdUsuario);
                    command.CommandText = "sp_EliminarUsuario";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
