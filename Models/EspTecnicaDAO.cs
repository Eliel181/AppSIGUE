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
    public class EspTecnicaDAO : ConnectionSQL
    {
        public DataTable listarEspTecnicas()
        {
            DataTable listEspTecnicas = new DataTable();
            using (var connection = GetConnection())
            {
                SqlDataReader leerfilas;
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "sp_ListarEspTecnicas";
                    command.CommandType = CommandType.StoredProcedure;
                    leerfilas = command.ExecuteReader();
                    listEspTecnicas.Load(leerfilas);
                    leerfilas.Close();
                    return listEspTecnicas;
                }
            }
        }

        public void agregarEspTecnica(EspTecnica esp)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@programa", esp.Programa);
                    command.Parameters.AddWithValue("@titulo", esp.Titulo);
                    command.Parameters.AddWithValue("@descripcion", esp.Descripcion);
                    command.CommandText = "sp_AgregarEspTecnica";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }

        public void editarEspTecnica(EspTecnica esp)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@idEspTecnica", esp.IdEspTecnica);
                    command.Parameters.AddWithValue("@programa", esp.Programa);
                    command.Parameters.AddWithValue("@titulo", esp.Titulo);
                    command.Parameters.AddWithValue("@descripcion", esp.Descripcion);
                    command.CommandText = "sp_EditarEspTecnica";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }
            }
        }

        public void eliminarEspTecnica(int IdEspTecnica)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@idEspTecnica", IdEspTecnica);
                    command.CommandText = "sp_EliminarEspTecnica";
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
