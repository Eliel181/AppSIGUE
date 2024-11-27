using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ImagenDAO : ConnectionSQL
    {
        public DataTable listarImagenes()
        {
            DataTable listImagens = new DataTable();
            using (var connection = GetConnection())
            {
                SqlDataReader leerfilas;
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "sp_ListarImagenes";
                    command.CommandType = CommandType.StoredProcedure;
                    leerfilas = command.ExecuteReader();
                    listImagens.Load(leerfilas);
                    leerfilas.Close();
                    return listImagens;
                }
            }
        }

        public void agregarImagen(Imagen img)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@titulo", img.Titulo);
                    command.Parameters.AddWithValue("@foto", img.Foto);
                    command.CommandText = "sp_AgregarImagen";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }


        public byte[] obtenerFoto(int idImagen)
        {
            byte[] fotoDB = null;
            using (var connection = GetConnection())
            {
                SqlDataReader leerfilas;
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "select Foto from Imagenes where IdImagen = @IdImagen";
                    command.Parameters.AddWithValue("idImagen", idImagen);
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

        public void editarImagen(Imagen img)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@idImagen", img.IdImagen);
                    command.Parameters.AddWithValue("@titulo", img.Titulo);
                    command.Parameters.AddWithValue("@foto", img.Foto);
                    command.CommandText = "sp_EditarImagen";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }

        public void eliminarImagen(int IdImagen)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@idImagen", IdImagen);
                    command.CommandText = "sp_EliminarImagen";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
